using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TalklessData.Entities;
using TalklessWeb.Models;

namespace TalklessWeb.Controllers
{
    public class HomeController : Controller
    {
        readonly MapperConfiguration config = new AutomapperConfig.AutoMapperConfig().Configure();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult UserList()
        {
            List<UserAccount> userAccounts = TalklessData.Core.Service.GetUserAccounts();
            IMapper imapper = config.CreateMapper();
            List<ProfileViewModel> userAccountViewModels = imapper.Map<List<UserAccount>, List<ProfileViewModel>>(userAccounts);

            return View(userAccountViewModels);
        }

        [Authorize]
        public ActionResult MessageList()
        {
            ListedMessageViewModel messageViewModels = GetMessages(User.Identity.GetUserId());
            return View(messageViewModels);
        }
        [Authorize]
        public ActionResult SetReceived(int id)
        {
            TalklessData.Core.Service.SetMessageAsReceived(id);
            return RedirectToAction("MessageList", "Home");
        }
    
        private ListedMessageViewModel GetMessages(string userId)
        {

            List<Message> messages = TalklessData.Core.Service.GetMessages(userId);
            IMapper imapper = config.CreateMapper();
            List<MessageViewModel> messageViewModels = imapper.Map<List<Message>, List<MessageViewModel>>(messages);

            foreach (var messageView in messageViewModels)
            {
                messageView.Receiver = GetProfileViewModel(messageView.ReceiverUser);
                messageView.Sender = GetProfileViewModel(messageView.SenderUser);
            }

            List<int> messageGroupIds = TalklessData.Core.Service.GetMessageGroupIds(userId);
            ListedMessageViewModel listedMessageViewModels = new ListedMessageViewModel()
            {
                MessageViews = messageViewModels,
                MessageGroupIds = messageGroupIds
            };

            return listedMessageViewModels;
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