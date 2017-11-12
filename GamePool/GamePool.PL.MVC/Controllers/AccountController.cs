using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using GamePool.BLL.LogicContracts;
using GamePool.PL.MVC.Models.Account;
using UserEntity = GamePool.Common.Entities.User;

namespace GamePool.PL.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserLogic userLogic;

        public AccountController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return this.View(new UserAggregatedVM
            {
                LoginVM = new UserLoginVM(),
                RegisterVM = new UserRegisterVM()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserLoginVM userLoginVM)
        {
            if (userLoginVM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (this.ModelState.IsValid)
            {
                UserEntity user = Mapper.Map<UserLoginVM, UserEntity>(userLoginVM);

                if (this.userLogic.IsExists(user))
                {
                    FormsAuthentication.SetAuthCookie(user.Name, createPersistentCookie: userLoginVM.RememberMe);

                    return this.RedirectToAction("Index", "Product");
                }
                else
                {
                    userLoginVM.IsExist = false;
                }
            }

            return this.View(new UserAggregatedVM
            {
                LoginVM = userLoginVM
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserRegisterVM userRegisterVM)
        {
            if (userRegisterVM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (this.ModelState.IsValid)
            {
                UserEntity user = Mapper.Map<UserRegisterVM, UserEntity>(userRegisterVM);

                if (this.userLogic.Add(user))
                {
                    FormsAuthentication.SetAuthCookie(user.Name, createPersistentCookie: true);

                    return this.RedirectToAction("Index", "Product");
                }
            }

            return this.View("SignIn", new UserAggregatedVM
            {
                RegisterVM = userRegisterVM
            });
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return this.RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult IsUsernameNotExist(string username)
        {
            bool isExist = this.userLogic.IsLoginExists(username);

            return this.Json(!isExist, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult SignInForm(UserLoginVM userLoginVM)
        {
            this.ModelState.Clear();

            if (userLoginVM.IsExist.HasValue && !userLoginVM.IsExist.Value)
            {
                this.ModelState.AddModelError(string.Empty, "Incorrect login or password");
            }

            return this.View("_SignInPartial", userLoginVM);
        }

        [ChildActionOnly]
        public ActionResult SignUpForm(UserRegisterVM userRegisterVM)
        {
            this.ModelState.Clear();

            return this.View("_SignUpPartial", userRegisterVM);
        }
    }
}