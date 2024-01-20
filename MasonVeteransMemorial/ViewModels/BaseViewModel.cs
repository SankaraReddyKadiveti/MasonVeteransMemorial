﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MasonVeteransMemorial.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        string title = string.Empty;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        string subtitle = string.Empty;

        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        /// <value>The subtitle.</value>
        public string Subtitle
        {
            get => subtitle;
            set => SetProperty(ref subtitle, value);
        }

        string icon = string.Empty;

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        bool isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (SetProperty(ref isBusy, value))
                    IsNotBusy = !isBusy;

            }
        }

        bool isNotBusy = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// </summary>
        /// <value><c>true</c> if this instance is not busy; otherwise, <c>false</c>.</value>
        public bool IsNotBusy
        {
            get => isNotBusy;
            set
            {
                if (SetProperty(ref isNotBusy, value))
                    IsBusy = !isNotBusy;

            }
        }

        string isBusyMessage = string.Empty;
        public string IsBusyMessage
        {
            get => isBusyMessage;
            set
            {
                SetProperty(ref isBusyMessage, value);
            }
        }


        bool canLoadMore = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can load more.
        /// </summary>
        /// <value><c>true</c> if this instance can load more; otherwise, <c>false</c>.</value>
        public bool CanLoadMore
        {
            get => canLoadMore;
            set => SetProperty(ref canLoadMore, value);
        }


        string header = string.Empty;

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public string Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        string footer = string.Empty;

        /// <summary>
        /// Gets or sets the footer.
        /// </summary>
        /// <value>The footer.</value>
        public string Footer
        {
            get => footer;
            set => SetProperty(ref footer, value);
        }

        double checkoutProgress;
        public double LoadingProgress
        {
            get { return checkoutProgress; }
            set { SetProperty(ref checkoutProgress, value); }
        }


        public BaseViewModel()
        {

        }
    }
}
