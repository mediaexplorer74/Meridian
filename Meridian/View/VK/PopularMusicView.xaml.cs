using Meridian.ViewModel.VK;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Meridian.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PopularMusicView : Page
    {
        public PopularMusicViewModel ViewModel => DataContext as PopularMusicViewModel;

        public PopularMusicView()
        {
            this.InitializeComponent();
        }
    }
}
