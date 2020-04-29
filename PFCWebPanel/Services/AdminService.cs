using System;
using System.Collections.Generic;
using System.Text;
using PFCWebPanel.Interfaces;

using System.Linq;
using PFCWebPanel.Context;

namespace PFCWebPanel.Services
{
    public class AdminService : IAdmin
    {
        private PFCSqlEntities _context;

        public AdminService(PFCSqlEntities context)
        {
            _context = context;
        }



        public bool RemoveUser(int id)
        {
            var TblUsers = _context.TblUsers.Find(id);
            if (TblUsers != null)
            {
                _context.TblUsers.Remove(TblUsers);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }



        public TblUsers ShowUserById(int id)
        {
            return _context.TblUsers.Find(id);
        }

        public List<TblUsers> ShowUsers()
        {
            return _context.TblUsers.OrderBy(m => m.Mobile).ToList();
        }


        public bool UpdateUser(TblUsers tblUsers)
        {
            TblUsers tblUser = _context.TblUsers.Find(tblUsers.Id);
            if (tblUser != null)
            {
                tblUser.Mobile = tblUsers.Mobile;
                tblUser.Name = tblUsers.Name;
                tblUser.Password = tblUsers.Password;
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
