using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TalklessData.Entities
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVisibleProfile { get; set; }

        public static void SaveUserAccount(UserAccount userAccount)
        {
            using (var talklessContext = new TalklessContext())
            {
                talklessContext.UserAccounts.Add(userAccount);
                talklessContext.SaveChanges();
            }
        }

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

        internal static void ModifyUserAccountVisibility(string userId, bool visibility)
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

        internal static List<UserAccount> GetUserAccounts()
        {
            using (var talklessContext = new TalklessContext())
            {
                var userAccount = talklessContext.UserAccounts
                                                 .ToList();
                return userAccount;
            }
        }

        public static void DeleteUserAccount(string userId)
        {
            using (var talklessContext = new TalklessContext())
            {
                var userAccount = talklessContext.UserAccounts.Where(userElement => userElement.UserId == userId)
                                                       .FirstOrDefault();
                talklessContext.UserAccounts.Remove(userAccount);
                talklessContext.SaveChanges();
            }
        }
    }

}
