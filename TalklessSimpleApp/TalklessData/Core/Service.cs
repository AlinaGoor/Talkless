using System;
using System.Collections.Generic;
using System.Linq;
using TalklessData.Entities;

namespace TalklessData.Core
{
    public static class Service
    {
        public static UserAccount GetUserAccount(string userId)
        {
            UserAccount userAccount;
            using (var talklessContext = new TalklessContext())
            {
                userAccount = talklessContext.UserAccounts.Where(userElement => userElement.UserId.Equals(userId))
                                                          .FirstOrDefault();

            }
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
            using (var talklessContext = new TalklessContext())
            {
                talklessContext.UserAccounts.Add(newUserAccount);
                talklessContext.SaveChanges();
            }
        }

        public static List<UserAccount> GetUserAccounts()
        {
            using (var talklessContext = new TalklessContext())
            {
                var userAccount = talklessContext.UserAccounts
                                                 .ToList();
                return userAccount;
            }
        }

        public static void ModifyUserAccount(string userId, bool visibility)
        {
            using (var talklessContext = new TalklessContext())
            {
                var userAccount = talklessContext.UserAccounts.Where(userElement => userElement.UserId == userId)
                                                              .FirstOrDefault();
                if (userAccount != null)
                {
                    userAccount.IsVisibleProfile = visibility;
                    talklessContext.SaveChanges();
                }
            }
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
            using (var talklessContext = new TalklessContext())
            {
                MessageGroup messageGroup = talklessContext.MessageGroups.Where(mGroupElement => (mGroupElement.Participant1.Equals(receiverUser)
                                                                                                    || mGroupElement.Participant1.Equals(senderUser))
                                                                                              && (mGroupElement.Participant2.Equals(receiverUser)
                                                                                                    || mGroupElement.Participant2.Equals(senderUser)))
                                                                          .FirstOrDefault();

                if (messageGroup != null)
                {
                    message.MessageGroupId = messageGroup.Id;
                }
                else
                {
                    var messageGroupForSave = new MessageGroup()
                    {
                        Participant1 = senderUser,
                        Participant2 = receiverUser
                    };
                    talklessContext.MessageGroups.Add(messageGroupForSave);
                    talklessContext.SaveChanges();
                    message.MessageGroupId = messageGroupForSave.Id;
                }

                talklessContext.Messages.Add(message);
                talklessContext.SaveChanges();
            }
        }

        public static List<Message> GetMessages(string userId)
        {
            List<Message> messages;
            using (var talklessContext = new TalklessContext())
            {
                messages = talklessContext.Messages.Where(messageElement => messageElement.ReceiverUser.Equals(userId)
                                                                         || messageElement.SenderUser.Equals(userId))
                                                   .OrderByDescending(messageElement => messageElement.CreateTime)
                                                   .ThenByDescending(messageElement => messageElement.Id)
                                                   .ToList();
            }
            return messages;
        }

        public static List<int> GetMessageGroupIds(string userId)
        {
            using (var talklessContext = new TalklessContext())
            {
                List<int> messageGroupIds = talklessContext.MessageGroups.Where(mGroupElement => mGroupElement.Participant1.Equals(userId)
                                                                                              || mGroupElement.Participant2.Equals(userId))
                                                                         .Select(messageGroupElement => messageGroupElement.Id)
                                                                         .Distinct()
                                                                         .ToList();
                return messageGroupIds;
            }
        }

        public static void SetMessageAsReceived(int id)
        {
            using (var talklessContext = new TalklessContext())
            {
                var message = talklessContext.Messages.Where(messageElement => messageElement.Id == id).FirstOrDefault();
                message.SeenByReceiver = true;
                talklessContext.SaveChanges();
            }
        }

        #endregion
    }
}
