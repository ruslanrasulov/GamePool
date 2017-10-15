using GamePool.BLL.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GamePool.PL.MVC.App_Start
{
    public class CommonRoleProvider : RoleProvider
    {
        private IUserRoleLogic userRoleLogic;

        public CommonRoleProvider()
        {
            this.userRoleLogic = DependencyResolver.Current.GetService<IUserRoleLogic>();
        }

        #region NotImplemented
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        } 
        #endregion

        public override string[] GetRolesForUser(string username)
        {
            return this.userRoleLogic
                .GetByUserLogin(username)
                .Select(x => x.Name)
                .ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return this.userRoleLogic.IsUserInRole(username, roleName);
        }

    }
}