using System.Collections.Generic;

namespace K94Warriors.ViewModels.EmailNotifications
{
    public class EmailViewModel
    {
        public EmailViewModel()
        {
            To = new List<string>();
        }

        public string From { get; set; }

        public List<string> To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}