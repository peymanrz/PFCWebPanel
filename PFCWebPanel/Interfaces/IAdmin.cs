using System;
using System.Collections.Generic;
using System.Text;
using PFCWebPanel.Context;
using PFCWebPanel.Interfaces;
 
namespace PFCWebPanel.Interfaces
{
  public interface IAdmin
    {
        List<TblUsers> ShowUsers();

        bool RemoveUser(int id);

        bool UpdateUser(TblUsers tblUsers);

        TblUsers ShowUserById(int id);

        //////////////////////////

     }
}
