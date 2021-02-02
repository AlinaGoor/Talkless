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

    }
}
