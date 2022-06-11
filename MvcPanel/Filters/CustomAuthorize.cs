using DatabaseDomain.Entities;
using DatabaseDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreServices;
using Microsoft.AspNetCore.Mvc;

namespace MvcPanel.Filters
{
    public class CustomAuthorize : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private static List<RoleDomain> _databaseRoles;
        private static List<UserDomain> _databaseUsers;
        private static List<PermisionDomain> _databasePermisions;
        private static List<UserRoleDomain> _databaseUserRoles;
        private static List<RolePermisionDomain> _databaseRolePermision;
        private readonly string _permision;
        private readonly string _roles;

        public CustomAuthorize(string permision, string roles)
        {
            _permision = permision;
            _roles = roles;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var _unitOfWork = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

            if (context == null)
                return;

            #region Initial

            if (_databaseRoles == null)
            {
                var r = await _unitOfWork._role.GetAll();
                _databaseRoles = r.ToList();
            }

            if (_databaseUsers == null)
            {
                var users = await _unitOfWork._user.GetAll();
                _databaseUsers = users.ToList();
            }

            if (_databasePermisions == null)
            {
                var p = await _unitOfWork._permision.GetAll();
                _databasePermisions = p.ToList();
            }

            if (_databaseUserRoles == null)
            {
                var userRoles = await _unitOfWork._userRole.GetAll();
                _databaseUserRoles = userRoles.ToList();
            }

            if (_databaseRolePermision == null)
            {
                var rp = await _unitOfWork._rolePermision.GetAll();
                _databaseRolePermision = rp.ToList();
            }

            #endregion



            //check user
            if (context.HttpContext.User == null)
                return;

            if (context.HttpContext.User.Identity.Name == null)
                return;

            var user = _databaseUsers.FirstOrDefault(r => r.Username == context.HttpContext.User.Identity.Name);
            if (user == null)
                return;

            if (user.IsAdmin.ToBoolean())
                return;

            //get userRoles
            var userRoleIds = _databaseUserRoles.Where(r => r.UserId == user.Id).Select(r => r.RoleId).ToList();

            if (userRoleIds == null)
                return;

            //get roles of user
            var roles = _databaseRoles.Where(r => userRoleIds.Contains(r.Id)).ToList();
            if (roles == null)
                return;

            //get rolePermisions
            var rolePermisions = _databaseRolePermision.Where(r => roles.Select(x => x.Id).Contains(r.RoleId)).Select(r => r.PermisionId).ToList();
            if (rolePermisions == null)
                return;

            //get Permisions
            var permisions = _databasePermisions.Where(r => rolePermisions.Contains(r.Id)).ToList();
            if (permisions == null)
                return;

            if (!permisions.Any(r => r.Value == _permision))
                context.Result = new ForbidResult();

            if (_roles.Trim(',') != string.Empty)
            {
                var arrRoles = _roles.Split(",");

                foreach (var item in arrRoles)
                {
                    if (!roles.Any(r => r.Title == item))
                        context.Result = new ForbidResult();
                }
            }

            return;
        }
    }
}
