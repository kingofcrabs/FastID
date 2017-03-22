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

namespace FastID
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AddButtons(5);
        }

        private void AddButtons(int cnt)
        {
            for(int i = 0; i< cnt; i++)
            {
                PlateButton plateButton = new PlateButton(i+1);
                plateButton.Height = 200;
                plateButton.Click += plateButton_Click;
                plateContainer.Children.Add(plateButton);
            }
            
        }

        void plateButton_Click(object sender, RoutedEventArgs e)
        {
            PlateButton btn = (PlateButton)sender;
            
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            LayoutDef layoutDef = new LayoutDef();
            layoutDef.ShowDialog();
        }

       
    }
}
