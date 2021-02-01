using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TalklessData.Entities
{
    public class MessageGroup
    {
        [Key]
        public int Id { get; set; }
        public string Participant1 { get; set; }
        public string Participant2 { get; set; }

        internal static void SaveMessageGroup(string user1, string user2)
        {
            using (var talklessContext = new TalklessContext())
            {
                var messageGroup = new MessageGroup()
                {
                    Participant1 = user1,
                    Participant2 = user2
                };
                talklessContext.MessageGroups.Add(messageGroup);
                talklessContext.SaveChanges();
            }
        }

        internal static MessageGroup GetMessageGroup(string user1, string user2)
        {
            using (var talklessContext = new TalklessContext())
            {
                MessageGroup messageGroup = talklessContext.MessageGroups.Where(mGroupElement => (mGroupElement.Participant1.Equals(user1)
                                                                                                               || mGroupElement.Participant1.Equals(user2))
                                                                                              && (mGroupElement.Participant2.Equals(user1)
                                                                                                               || mGroupElement.Participant2.Equals(user2)))
                                                                         .FirstOrDefault();
                return messageGroup;
            }
        }

        internal static List<int> GetMessageGroupIds(string userId)
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
    }


}
