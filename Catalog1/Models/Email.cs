using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog1.Models
{
    public class Email
    {
        public Email()
        {
            TimeStamp = DateTime.Now;
            IsProcessed = false;
        }
        public int ID { get; set; }

        [DisplayName("Email Name")]
        public string Name { get; set; }
        [ReadOnly(true)]
        public DateTime TimeStamp { get; set; }

        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsProcessed { get; set; }
    }
}
