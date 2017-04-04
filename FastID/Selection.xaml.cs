using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace FastID
{
    /// <summary>
    /// Interaction logic for Selection.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {
        public static string curConfigName = "";
        public SelectionWindow()
        {
            InitializeComponent();
            this.Loaded += Selection_Loaded;
            
        }

        void Selection_Loaded(object sender, RoutedEventArgs e)
        {
            string sFolder = Helper.GetConfigFolder();
            DirectoryInfo dirInfo = new DirectoryInfo(sFolder);
            var files = dirInfo.EnumerateFiles("*.xml").ToList();
            List<string> configNames = new List<string>();
            foreach (var fileInfo in files)
            {
                configNames.Add(fileInfo.Name.Replace(".xml",""));
            }
            lstConfig.ItemsSource = configNames;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (lstConfig.SelectedIndex == -1)
                return;
            
            curConfigName = lstConfig.SelectedItem.ToString();
            this.Close();
        }
    }
}
