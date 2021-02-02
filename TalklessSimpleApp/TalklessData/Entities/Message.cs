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

    }
}
