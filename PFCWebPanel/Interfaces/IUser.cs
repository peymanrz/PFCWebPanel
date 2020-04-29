using System;
using System.Collections.Generic;
using System.Text;
using PFCWebPanel.Context;


namespace PFCWebPanel.Interfaces
{
   public interface IUser
    {
        TblUsers LoginUser(string mobileNumber, string password);

        bool IsMobileNumberExist(string mobileNumber);
        int AddUser(TblUsers TblUsers);

        TblUsers ActiveUser(string activeCode);

        TblUsers ForgetPassword(string mobileNumber);

        bool ResetPassword(string activeCode, string password);

        int GetUserId(string mobileNumber);

        bool ProfileUser(int id, string mobileNumber);

        bool ChangePassword(string mobileNumber, string currentPassword, string password);

        bool CheckUserRole(string roleName, string mobileNumber);

        string GetRoleName(int id);
            
    }
}
