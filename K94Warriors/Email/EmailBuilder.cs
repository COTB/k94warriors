using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;
using K94Warriors.ViewModels.EmailNotifications;

namespace K94Warriors.Email
{
    public class EmailBuilder
    {
        private readonly EmailViewModel _viewModel = new EmailViewModel();

        public EmailBuilder From(string from)
        {
            _viewModel.From = from;
            return this;
        }

        public EmailBuilder To(string to)
        {
            _viewModel.To = new List<string> { to };
            return this;
        }

        public EmailBuilder To(IList<string> to)
        {
            _viewModel.To = new List<string>();
            _viewModel.To.AddRange(to);
            return this;
        }

        public EmailBuilder AndTo(string to)
        {
            _viewModel.To = _viewModel.To ?? new List<string>();
            _viewModel.To.Add(to);
            return this;
        }

        public EmailBuilder AndTo(IList<string> to)
        {
            _viewModel.To = _viewModel.To ?? new List<string>();
            _viewModel.To.AddRange(to);
            return this;
        }

        public EmailBuilder WithSubject(string subject)
        {
            _viewModel.Subject = subject;
            return this;
        }

        public EmailBuilder WithBody(string body)
        {
            _viewModel.Body = body;
            return this;
        }

        public EmailBuilder WithBody(IEnumerable<KeyValuePair<string, IList>> lists)
        {
            var writer = new StringWriter();
            var html = new HtmlTextWriter(writer);

            html.RenderBeginTag(HtmlTextWriterTag.H1);
            html.WriteEncodedText(_viewModel.Subject);
            html.RenderEndTag();
            html.WriteBreak();
            html.WriteBreak();
            foreach (var list in lists)
            {
                html.RenderBeginTag(HtmlTextWriterTag.H3);
                html.WriteEncodedText(list.Key);
                html.RenderEndTag();
                html.RenderBeginTag(HtmlTextWriterTag.Ul);
                foreach (var item in list.Value)
                {
                    html.RenderBeginTag(HtmlTextWriterTag.Li);
                    html.WriteEncodedText(item.ToString());
                    html.RenderEndTag();
                }
                html.RenderEndTag();
                html.WriteBreak();
                html.WriteBreak();
            }

            _viewModel.Body = writer.ToString();

            return this;
        }

        public EmailViewModel ToViewModel()
        {
            return _viewModel;
        }
    }
}