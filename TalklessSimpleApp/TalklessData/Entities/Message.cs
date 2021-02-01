using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TalklessData.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int MessageGroupId { get; set; }
        public string SenderUser { get; set; }
        public string ReceiverUser { get; set; }
        public DateTime CreateTime { get; set; }
        public string MessageText { get; set; }
        public bool SeenByReceiver { get; set; }

        internal static void SaveMessage(Message message)
        {
            using (var talklessContext = new TalklessContext())
            {
                MessageGroup messageGroup = MessageGroup.GetMessageGroup(message.ReceiverUser, message.SenderUser);
                if (messageGroup != null)
                {
                    message.MessageGroupId = messageGroup.Id;
                }
                else
                {
                    MessageGroup.SaveMessageGroup(message.ReceiverUser, message.SenderUser);
                    messageGroup = MessageGroup.GetMessageGroup(message.ReceiverUser, message.SenderUser);
                    message.MessageGroupId = messageGroup.Id;
                }

                talklessContext.Messages.Add(message);
                talklessContext.SaveChanges();
            }
        }

        internal static List<Message> GetMessages(string userId)
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

        internal static void SetMessageAsReceived(int id)
        {
            using (var talklessContext = new TalklessContext())
            {
                var message = talklessContext.Messages.Where(messageElement => messageElement.Id == id).FirstOrDefault();
                message.SeenByReceiver = true;
                talklessContext.SaveChanges();
            }
        }
    }
}
