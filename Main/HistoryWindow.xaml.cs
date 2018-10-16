using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Main
{
    public partial class HistoryWindow : UserControl
    {
        static public ObservableCollection<string> result = new ObservableCollection<string>();

        public HistoryWindow()
        {
            InitializeComponent();
            myLabels.ItemsSource = result;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (sender == Back)
            {
                MainWindow.History = true;
            }
        }
        static public void AddingNewItem(string str)
        {
            result.Insert(0, str);
        }
    }
}
