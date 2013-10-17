using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public EmailBuilder WithBody(IDictionary<string, IList> lists)
        {
            throw new NotImplementedException();
            return this;
        }

        public EmailViewModel ToViewModel()
        {
            return _viewModel;
        }
    }
}