﻿using Meridian.ViewModel.VK;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Meridian.View.VK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SocietyMusicView : Page
    {
        public SocietyMusicViewModel ViewModel => DataContext as SocietyMusicViewModel;

        public SocietyMusicView()
        {
            this.InitializeComponent();
        }
    }
}
