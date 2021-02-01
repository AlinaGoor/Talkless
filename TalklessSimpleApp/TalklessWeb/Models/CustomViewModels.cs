using System;
using System.Collections.Generic;

namespace TalklessWeb.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phonenumber { get; set; }
        public bool IsVisibleProfile { get; set; }
    }

    public class MessageViewModel
    {
        public int Id { get; set; }
        public int MessageGroupId { get; set; }
        public string SenderUser { get; set; }
        public ProfileViewModel Sender { get; set; }
        public string ReceiverUser { get; set; }
        public ProfileViewModel Receiver { get; set; }
        public DateTime CreateTime { get; set; }
        public string MessageText { get; set; }
        public bool SeenByReceiver { get; set; }

    }

    public class ListedMessageViewModel
    {
        public List<MessageViewModel> MessageViews { get; set; }
        public List<int> MessageGroupIds { get; set; }
    }

    public class MessageCreateViewModel
    {
        public ProfileViewModel ReceiverView { get; set; }
        public int sendedMessages { get; set; }
        public string SenderUser { get; set; }
        public string ReceiverUser { get; set; }
        public string MessageText { get; set; }
    }

}