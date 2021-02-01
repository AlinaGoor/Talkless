using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;
using TalklessData.Entities;
using TalklessWeb.Models;

namespace TalklessWeb.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        readonly MapperConfiguration config = new AutomapperConfig.AutoMapperConfig().Configure();

        // GET: Message/Create
        public ActionResult Create(string id)
        {
            var messageCreateView = new MessageCreateViewModel();
            messageCreateView.ReceiverView = GetProfile(id);
            var userId = User.Identity.GetUserId();
            List<TalklessData.Entities.Message> messages = TalklessData.Core.Service.GetMessages(userId);
            messages = messages.Where(messagElement => messagElement.CreateTime.Date == DateTime.Today).ToList();
            messageCreateView.sendedMessages = messages.Where(messageElement => messageElement.SenderUser.Equals(userId)).Count();

            return View(messageCreateView);
        }


        // POST: Message/Create
        [HttpPost]
        public ActionResult Create(MessageCreateViewModel newMessageView, string id)
        {
            try
            {
                newMessageView.SenderUser = User.Identity.GetUserId();
                SaveMessage(newMessageView);
                return RedirectToAction("MessageList", "Home");
            }
            catch
            {
                return View();
            }
        }

        private ProfileViewModel GetProfile(string userId)
        {
            UserAccount userAccounts = TalklessData.Core.Service.GetUserAccount(userId);
            IMapper imapper = config.CreateMapper();
            ProfileViewModel userAccountViewModel = imapper.Map<UserAccount, ProfileViewModel>(userAccounts);

            return userAccountViewModel;
        }

        private void SaveMessage(MessageCreateViewModel newMessage)
        {
            TalklessData.Core.Service.CreateMessage(newMessage.SenderUser, newMessage.ReceiverUser, newMessage.MessageText);
        }

    }
}
