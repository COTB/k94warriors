using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K94Warriors.Logger
{
    public class EmailMessage
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
    }
}