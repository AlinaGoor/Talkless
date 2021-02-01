using System.Data.Entity;

namespace TalklessData.Entities
{
    class TalklessContext : DbContext
    {
        public TalklessContext() : base("Talkless")
        {

        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<MessageGroup> MessageGroups { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
