using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PFCWebPanel.Context;

namespace PFCWebPanel.Classes
{
    public class PersianFiberRoleProvider:RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (PFCSqlEntities db =new PFCSqlEntities())
            {
                    string[] result = new string[1];
            int userid = Convert.ToInt32(username);
             result[0] = db.TblUsers.Where(ex => ex.Id == userid).Select(x => x.TblRoles.Name).FirstOrDefault();
            return result;
            }
        
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}