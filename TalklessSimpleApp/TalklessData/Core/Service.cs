using System;
using System.Collections.Generic;
using TalklessData.Entities;

namespace TalklessData.Core
{
    public static class Service
    {
        public static UserAccount GetUserAccount(string userId)
        {
            UserAccount userAccount = UserAccount.GetUserAccount(userId);
            return userAccount;
        }

        public static void CreateUserAccount(string userId, string userName, string firstName, string lastName, string number)
        {
            var newUserAccount = new UserAccount()
            {
                UserId = userId,
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = number
            };
            UserAccount.SaveUserAccount(newUserAccount);
        }

        public static List<UserAccount> GetUserAccounts()
        {
            List<UserAccount> userAccounts = UserAccount.GetUserAccounts();
            return userAccounts;
        }

        public static void ModifyUserAccount(string userId, bool visibility)
        {
            UserAccount.ModifyUserAccountVisibility(userId, visibility);
        }


        #region Message
        public static void CreateMessage(string senderUser, string receiverUser, string messageText)
        {
            var message = new Message()
            {
                SenderUser = senderUser,
                ReceiverUser = receiverUser,
                CreateTime = DateTime.Now,
                MessageText = messageText,
                SeenByReceiver = false
            };

            Message.SaveMessage(message);
        }

        public static List<Message> GetMessages(string userId)
        {
            List<Message> message = Message.GetMessages(userId);
            return message;
        }

        public static List<int> GetMessageGroupIds(string userId)
        {
            List<int> messageGroupIds = MessageGroup.GetMessageGroupIds(userId);
            return messageGroupIds;
        }

        public static void SetMessageAsReceived(int id)
        {
            Message.SetMessageAsReceived(id);
        }

        #endregion
    }
}
