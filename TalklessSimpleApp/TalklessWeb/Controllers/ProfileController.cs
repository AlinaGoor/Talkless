using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using TalklessData.Entities;
using TalklessWeb.Models;

namespace TalklessWeb.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        readonly MapperConfiguration config = new AutomapperConfig.AutoMapperConfig().Configure();

        // GET: Profile
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            ProfileViewModel profileViewModel = GetProfileViewModel(userId);

            return View(profileViewModel);
        }

        // GET: Profile/Details/5
        public ActionResult Details(string id)
        {
            ProfileViewModel profileViewModel = GetProfileViewModel(id);
            return View(profileViewModel);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            ProfileViewModel profileViewModel = GetProfileViewModel(userId);
            return View(profileViewModel);
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(ProfileViewModel editViewModel)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                TalklessData.Core.Service.ModifyUserAccount(userId, editViewModel.IsVisibleProfile);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ProfileViewModel GetProfileViewModel(string userId)
        {
            UserAccount userAccount = TalklessData.Core.Service.GetUserAccount(userId);
            IMapper imapper = config.CreateMapper();
            ProfileViewModel profileViewModel = imapper.Map<UserAccount, ProfileViewModel>(userAccount);
            return profileViewModel;
        }
    }
}