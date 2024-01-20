using System;
using MasonVeteransMemorial.Common;
using MasonVeteransMemorial.Data;
using MasonVeteransMemorial.Models;

namespace MasonVeteransMemorial.ViewModels
{
    public interface IAboutViewModelDelegate
    {
        void OnLoadSuccess();
        void OnLoadFailure(string title, string message);
    }

    public class AboutViewModel : BaseViewModel
    {
        public IAboutViewModelDelegate Delegate { get; set; }

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        public AppCommand LoadCommand => new AppCommand(Load);

        protected void Load()
        {
            Delegate?.OnLoadSuccess();
        }
    }
}
