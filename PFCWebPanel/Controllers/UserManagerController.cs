using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PFCWebPanel.Classes;
using PFCWebPanel.Context;
using PFCWebPanel.Interfaces;
using PFCWebPanel.ViewModels;


namespace PFCWebPanel.Controllers
{
    public class UserManagerController : Controller
    {
        private IAdmin _iAdmin;
        private IUser _iUser;

        public UserManagerController(IAdmin iAdmin, IUser iUser)
        {
            _iAdmin = iAdmin;
            _iUser = iUser;
        }
        [Authorize]
        public ActionResult Index()
        {
            List<TblUsers> users = _iAdmin.ShowUsers();
            return View(users);

        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            TblUsers users = _iAdmin.ShowUserById(id);
            EditUserViewModel model = new EditUserViewModel
            {
                Id = users.Id,
                Mobile = users.Mobile,
                Name = users.Name,
                RoleId = users.RoleId.Value,
                Password = ""
            };
            return base.View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, EditUserViewModel tblUsers)
        {
            if (base.ModelState.IsValid)
            {
                string password = tblUsers.Password;
                tblUsers.Password = HashGenerators.EncodingPassWithMd5(password);
                TblUsers users = new TblUsers
                {
                    Id = tblUsers.Id,
                    Mobile = tblUsers.Mobile,
                    Name = tblUsers.Name,
                    Password = tblUsers.Password
                };
                if (_iAdmin.UpdateUser(users))
                {
                    return base.RedirectToAction("Index");
                }
                tblUsers.Password = password;
            }
            return View(tblUsers);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool isdelete = _iAdmin.RemoveUser(id);
                if (isdelete)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


        [Authorize]
        public ActionResult CreateUser()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateUser(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (!_iUser.IsMobileNumberExist(register.Mobile))
            {
                TblUsers users1 = new TblUsers();
                users1.IsActive = true;
                users1.Mobile = register.Mobile;
                users1.Name = register.Name;
                users1.Code = CodeGenerators.ActiveCode();
                users1.Password = HashGenerators.EncodingPassWithMd5(register.Password);
                users1.RoleId = 1;
                TblUsers user = users1;
                this._iUser.AddUser(user);
                return base.RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Mobile", "شما قبلا ثبت نام کرده اید");
                return View(register);
            }
        }









        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [AllowAnonymous, HttpPost]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                TblUsers users = _iUser.LoginUser(login.Mobile, login.Password);
                if (users != null)
                {
                    FormsAuthentication.SetAuthCookie(users.Id.ToString(), login.RememberMe);
                    return ((returnUrl == null) ? ((ActionResult)base.RedirectToAction("Index", "Home")) : ((ActionResult)this.Redirect(returnUrl)));
                }
                base.ModelState.AddModelError("Password", "مشخصات کاربری صحیح نمی باشد");
            }
            return base.View(login);
        }

    }
}