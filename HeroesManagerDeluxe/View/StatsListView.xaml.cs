using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeroesManagerDeluxe.View
{
    /// <summary>
    /// Interaction logic for StatsListView.xaml
    /// </summary>
    public partial class StatsListView : UserControl
    {
        public StatsListView()
        {
            InitializeComponent();
            Messenger.Default.Register<DialogService>(this, OpenFilePickerDialog);
        }

        private void OpenFilePickerDialog(DialogService _service)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "Storm Replay|*.StormReplay";
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _service.PickedFiles = dialog.FileNames.ToList();
                if (_service.FilePicked != null)
                {
                    _service.FilePicked.Invoke();
                }
            }
        }
    }
}
