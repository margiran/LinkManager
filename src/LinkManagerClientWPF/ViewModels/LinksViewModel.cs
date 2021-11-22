using LinkManagerClientWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.ViewModels
{
    public class LinksViewModel:BaseViewModel
    {
        private readonly LinkModel _linkModel;
        public Guid Id => _linkModel.Id;

        public string Title => _linkModel.Title;

        public string Argument => _linkModel.Argument;

        public string FileName => _linkModel.FileName;  

        public bool InternetNeeded => _linkModel.InternetNeeded;

        public string ShortDescription => _linkModel.ShortDescription;  

        public string Description => _linkModel.Description;

        public string UserName => _linkModel.UserName;

        public string DefaultPassword => _linkModel.DefaultPassword;

        public int Order => _linkModel.Order;

        public int LinkVisitCount => _linkModel.LinkVisitCount;

        public LinksViewModel(LinkModel  linkModel)
        {
                _linkModel = linkModel;
        }

    }
}
