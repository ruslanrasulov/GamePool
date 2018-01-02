using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.PL.MVC.Models.Account;

namespace GamePool.PL.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserLogic _userLogic;

        public AccountController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View(new UserAggregatedVm
            {
                LoginVm = new UserLoginVm(),
                RegisterVm = new UserRegisterVm()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserLoginVm userLoginVm)
        {
            if (userLoginVm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                UserEntity user = Mapper.Map<UserLoginVm, UserEntity>(userLoginVm);

                if (_userLogic.IsExists(user))
                {
                    FormsAuthentication.SetAuthCookie(user.Name, userLoginVm.RememberMe);

                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    userLoginVm.IsExist = false;
                }
            }

            return View(new UserAggregatedVm
            {
                LoginVm = userLoginVm
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserRegisterVm userRegisterVm)
        {
            if (userRegisterVm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                UserEntity user = Mapper.Map<UserRegisterVm, UserEntity>(userRegisterVm);

                if (_userLogic.Add(user))
                {
                    FormsAuthentication.SetAuthCookie(user.Name, true);

                    return RedirectToAction("Index", "Product");
                }
            }

            return View("SignIn", new UserAggregatedVm
            {
                RegisterVm = userRegisterVm
            });
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult IsUsernameNotExist(string username)
        {
            bool isExist = _userLogic.IsLoginExists(username);

            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult SignInForm(UserLoginVm userLoginVm)
        {
            ModelState.Clear();

            if (userLoginVm.IsExist.HasValue && !userLoginVm.IsExist.Value)
            {
                ModelState.AddModelError(string.Empty, "Incorrect login or password");
            }

            return View("_SignInPartial", userLoginVm);
        }

        [ChildActionOnly]
        public ActionResult SignUpForm(UserRegisterVm userRegisterVm)
        {
            ModelState.Clear();

            return View("_SignUpPartial", userRegisterVm);
        }
    }
}