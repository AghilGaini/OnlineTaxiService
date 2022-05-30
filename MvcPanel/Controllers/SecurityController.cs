using DatabaseDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DatabaseDomain.Models;
using DatabaseDomain.DTOs.Security.Role;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;
using DatabaseDomain.DTOs.Security.Permision;

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

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var res = await _unitOfWork._role.GetRolesAsync();

            res.Actions.Add(new ActionItem() { Controller = "Security", Action = "EditRole", Title = "ویرایش" });
            res.Actions.Add(new ActionItem() { Controller = "Security", Action = "Permisions", Title = "دسترسی ها" });
            res.Actions.Add(new ActionItem() { Controller = "Security", Action = "DeleteRole", Title = "حذف" });

            return View(res);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

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

            foreach (var item in allPermisions)
            {
                var permisionid = permisionsId.FirstOrDefault(r => r == item.Id);
                if (permisionid != 0)
                {
                    item.IsSelected = true;
                }
            }

            ViewBag.RoleTitle = role.Title;

            return View(allPermisions);
        }

        public IActionResult UpdateRolePermisions(List<PermisionDTO>)
        {

        }

        #endregion
    }
}
