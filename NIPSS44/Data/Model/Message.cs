using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NIPSS44.Data.Model;

namespace NIPSS44.Data.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public string Mail { get; set; }
        public string Title { get; set; }
        public int Retries { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
