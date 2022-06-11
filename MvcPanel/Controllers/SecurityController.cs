using DatabaseDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DatabaseDomain.Models;
using DatabaseDomain.DTOs.Security.Role;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;
using DatabaseDomain.DTOs.Security.Permision;
using DatabaseDomain.DTOs.Security.RolePermision;
using CoreServices;
using DatabaseDomain.DTOs.Account.UserRoles;
using MvcPanel.Filters;

namespace MvcPanel.Controllers
{

    public class SecurityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Roles

        [CustomAuthorize(PermisionManager.Permision.Security_Roles_HttpGet, roles: "")]
        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var res = await _unitOfWork._role.GetRolesAsync();

            res.Actions.Add(new ActionItem() { Controller = "Security", Action = "EditRole", Title = "ویرایش" });
            res.Actions.Add(new ActionItem() { Controller = "Security", Action = "Permisions", Title = "دسترسی ها" });
            res.Actions.Add(new ActionItem() { Controller = "Security", Action = "DeleteRole", Title = "حذف" });

            return View(res);
        }

        [CustomAuthorize(PermisionManager.Permision.Security_CreateRole_HttpGet, roles: "")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [CustomAuthorize(PermisionManager.Permision.Security_CreateRole_HttpPost, roles: "")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(NewRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                if (await _unitOfWork._role.IsDuplicateByName(0, model.Title))
                {
                    ModelState.AddModelError("", "نام نقش تکراری میباشد");
                    return View(model);
                }
                await _unitOfWork._role.AddRoleAcyncDTO(model);
                _unitOfWork.Commit();

                return RedirectToAction("Roles");

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(long Id)
        {
            var oldRole = await _unitOfWork._role.GetById(Id);
            if (oldRole == null)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View();
            }
            var roleDTO = new UpdateRoleDTO()
            {
                Id = Id,
                Title = oldRole.Title,
            };

            return View(roleDTO);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(UpdateRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                var oldRole = await _unitOfWork._role.GetById(model.Id);
                if (oldRole == null)
                {
                    ModelState.AddModelError("", "نقشی پیدا نشد");
                    return View(model);
                }

                if (await _unitOfWork._role.UpdateRoleDTO(model))
                {
                    _unitOfWork.Commit();
                    return RedirectToAction("Roles");
                }
                else
                {
                    ModelState.AddModelError("", "خطا در عملیات");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(long Id)
        {
            var oldRole = await _unitOfWork._role.GetById(Id);
            if (oldRole == null)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return RedirectToAction("Roles");
            }

            if (await _unitOfWork._role.RemoveById(oldRole.Id))
            {
                _unitOfWork.Commit();
            }
            else
            {
                ModelState.AddModelError("", "عملیات با خطا مواجه شد");
            }

            return RedirectToAction("Roles");
        }

        #endregion

        #region Permisions

        [HttpGet]
        public async Task<IActionResult> Permisions(long Id)
        {
            var allPermisions = await _unitOfWork._permision.GetAllPermisionsDTO();

            if (Id == 0)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View(allPermisions);
            }

            var role = await _unitOfWork._role.GetById(Id);
            if (role == null)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View(allPermisions);
            }

            var permisionsId = await _unitOfWork._rolePermision.GetPermisionsIdByRoleId(role.Id);

            foreach (var item in allPermisions.Permisions)
            {
                if (permisionsId.Any(r => r == item.Id))
                {
                    item.IsSelected = true;
                }
            }

            ViewBag.RoleTitle = role.Title;

            allPermisions.RoleId = role.Id;

            return View(allPermisions);
        }

        [HttpPost]
        public async Task<IActionResult> Permisions(PermisionDTO model)
        {
            if (ModelState.IsValid)
            {
                var role = await _unitOfWork._role.GetById(model.RoleId);
                if (role == null)
                {
                    ModelState.AddModelError("", "نقشی پیدا نشد");
                    return View(model);
                }

                if (await _unitOfWork._rolePermision.DeleteByRoleId(model.RoleId))
                {
                    var newRolePermisions = new List<RolePermisionDTO>();

                    foreach (var item in model.Permisions)
                    {
                        if (item.IsSelected)
                        {
                            newRolePermisions.Add(new RolePermisionDTO()
                            {
                                PermisionId = item.Id,
                                RoleId = model.RoleId
                            });
                        }
                    }

                    if (await _unitOfWork._rolePermision.AddRolePermisionsDTO(newRolePermisions))
                    {
                        _unitOfWork.Commit();

                        return RedirectToAction("Roles");
                    }
                }
            }

            return View(model);
        }

        #endregion

        #region Users

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _unitOfWork._user.GetAllUsersDTO();

            foreach (var item in users.Users)
            {
                switch (item.UserType)
                {
                    case (int)Enums.UserType.Passenger:
                        item.UserTypeTitle = "مسافر";
                        break;
                    case (int)Enums.UserType.Driver:
                        item.UserTypeTitle = "راننده";
                        break;
                    case (int)Enums.UserType.Admin:
                        item.UserTypeTitle = "ادمین";
                        break;
                }

            }

            users.Actions.Add(new ActionItem() { Controller = "Security", Action = "userRoles", Title = "مدیریت نقش" });

            return View(users);
        }

        #endregion

        #region UserRoles

        public async Task<IActionResult> UserRoles(long id)
        {
            var allRoles = await _unitOfWork._role.GetRolesDTO();

            if (id == 0)
            {
                ModelState.AddModelError("", "کاربری پیدا نشد");
                return View(allRoles);
            }

            var user = await _unitOfWork._user.GetById(id);

            if (user == null)
            {
                ModelState.AddModelError("", "کاربری پیدا نشد");
                return View(allRoles);
            }

            var userRoles = await _unitOfWork._userRole.GetRolesByUserId(id);

            foreach (var item in allRoles.Roles)
            {
                if (userRoles.Any(r => r == item.RoleId))
                {
                    item.IsSelected = true;
                }
            }

            allRoles.UserId = id;

            return View(allRoles);
        }

        [HttpPost]
        public async Task<IActionResult> UserRoles(UserRolesDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserId == 0)
                {
                    ModelState.AddModelError("", "کاربری پیدا نشد");
                    return View(model);
                }

                var user = await _unitOfWork._user.GetById(model.UserId);
                if (user == null)
                {
                    ModelState.AddModelError("", "کاربری پیدا نشد");
                    return View(model);
                }

                if (await _unitOfWork._userRole.DeleteByUserId(model.UserId))
                {
                    var newUserRoles = new UserRolesDTO();
                    newUserRoles.UserId = model.UserId;

                    foreach (var item in model.Roles)
                    {
                        if (item.IsSelected)
                        {
                            newUserRoles.Roles.Add(new UserRolesInfoDTO() { RoleId = item.RoleId, RoleTitle = item.RoleTitle });
                        }
                    }

                    if (await _unitOfWork._userRole.AddRangeUserRoleDTO(newUserRoles))
                    {
                        _unitOfWork.Commit();
                        return RedirectToAction("Users");
                    }
                }

            }
            return View(model);
        }

        #endregion
    }
}
