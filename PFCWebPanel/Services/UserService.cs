using System;
using System.Collections.Generic;
using System.Text;
using PFCWebPanel.Interfaces;
 using PFCWebPanel.Classes;
using PFCWebPanel.Context;
using System.Linq;

namespace PFCWebPanel.Services
{
    public class UserService : IUser
    {
        PFCSqlEntities _context;

        public UserService(PFCSqlEntities context)
        {
            _context = context;
        }

        public TblUsers ActiveUser(string activeCode)
        {
            var TblUsers = _context.TblUsers.FirstOrDefault(u => u.IsActive == false && u.Code == activeCode);
            if (TblUsers!= null)
            {
                TblUsers.Code = CodeGenerators.ActiveCode();
                TblUsers.IsActive = true;
                _context.SaveChanges();
               
            }
            return TblUsers;
            
        }

        public int AddUser(TblUsers TblUsers)
        {
            _context.TblUsers.Add(TblUsers);
            _context.SaveChanges();

            return TblUsers.Id;
        }

        public bool ChangePassword(string mobileNumber, string currentPassword, string password)
        {
            string HashCurrentPass = HashGenerators.EncodingPassWithMd5(currentPassword);

            var TblUsers = _context.TblUsers.FirstOrDefault(u => u.Mobile == mobileNumber && u.Password == HashCurrentPass);

            if (TblUsers!= null)
            {
                string HashNewPass = HashGenerators.EncodingPassWithMd5(password);
                TblUsers.Password = HashNewPass;

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckUserRole(string roleName, string mobileNumber)
        {
            return _context.TblUsers.Any(u => u.Mobile == mobileNumber && u.TblRoles.Name == roleName);
        }

        public TblUsers ForgetPassword(string mobileNumber)
        {
            return _context.TblUsers.FirstOrDefault(u => u.Mobile == mobileNumber);
        }

        public string GetRoleName(int id)
        {
            var role = _context.TblRoles.Find(id);
            return role.Name;
        }

        public int GetUserId(string mobileNumber)
        {
            TblUsers TblUsers = _context.TblUsers.FirstOrDefault(u => u.Mobile == mobileNumber);
            if (TblUsers!= null)
            {
                return TblUsers.Id;
            }
            else
            {
                return 0;
            }
        }

        public bool IsMobileNumberExist(string mobileNumber)
        {
            return _context.TblUsers.Any(u => u.Mobile == mobileNumber);
        }

        public TblUsers LoginUser(string mobileNumber, string password)
        {
            string HashPassword = HashGenerators.EncodingPassWithMd5(password);
            return _context.TblUsers.FirstOrDefault(u => u.Mobile == mobileNumber && u.Password == HashPassword);
        }

        public bool ProfileUser(int id, string mobileNumber)
        {
            var TblUsers = _context.TblUsers.FirstOrDefault(u => u.IsActive == true && u.Id == id);

            if (TblUsers != null)
            {
                if (!_context.TblUsers.Any(u=>u.Mobile==mobileNumber))
                {
                    TblUsers.Mobile = mobileNumber;
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ResetPassword(string activeCode, string password)
        {
            var TblUsers = _context.TblUsers.FirstOrDefault(u => u.Code == activeCode && u.IsActive == true);

            if (TblUsers != null)
            {
                string hashpassword = HashGenerators.EncodingPassWithMd5(password);
                TblUsers.Password = hashpassword;
                TblUsers.Code = CodeGenerators.ActiveCode();
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
