using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FastID
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        TestViewer testViewer = null;
        SimulationViewer simulationViewer = null;
        Tester tester = new Tester();
        Thread thread;

        ObservableCollection<TestResult> testResults = new ObservableCollection<TestResult>();
        ObservableCollection<PlateModel> plateModels = new ObservableCollection<PlateModel>();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.SizeChanged += MainWindow_SizeChanged;
            tester.onLABUpdate += tester_onLABUpdate;
            tester.onProgressChanged += tester_onProgressChanged;
            this.Closing += MainWindow_Closing;
        }

        

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (thread != null)
                thread.Abort();
        }

        void tester_onLABUpdate(int plateID, int xLEDIndex, int yLEDIndex, LAB val, bool isFirst)
        {
            GlobalVars.Instance.plateID_LABInfos[plateID].Add(new PositionInt(xLEDIndex, yLEDIndex), val);
            
            this.Dispatcher.BeginInvoke(new Action(()=>UpdateUI(plateID,xLEDIndex,yLEDIndex,val ,isFirst)));  
        }

        private void UpdateUI(int plateID, int xLED, int yLED, LAB lab,bool isLast)
        {
            testViewer.Position_LAB = GlobalVars.Instance.plateID_LABInfos[plateID];
            int plateIndex = plateID - 1;
            int ledID = GlobalVars.Instance.PlateInfo.GetLEDID(xLED, yLED);
            testResults.Add(new TestResult(plateID, ledID, lab));
            simulationViewer.SetCurrentInfo(plateID, ledID);
            if (isLast)
            {
                plateModels[plateIndex].IsFinished = true;
                plateModels[plateIndex].IsChecking = false;
            }
            if (xLED + yLED == 0) //first
            {
                plateModels[plateIndex].IsChecking = true;
            }
        }
   
        void tester_onProgressChanged(string sMessage)
        {
            AddLog(sMessage);
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (testViewer == null)
                return;
            Size testViewerSize = new Size(viewerContainer.ActualWidth, viewerContainer.ActualHeight);
            testViewer.Resize(testViewerSize);

            Size simuationSize = new Size(simuationContainer.ActualWidth, simuationContainer.ActualHeight);
            simulationViewer.Resize(simuationSize);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string defaultFile = Helper.GetConfigFolder() + "default.xml";
            lstviewResult.ItemsSource = testResults;
            if(File.Exists(defaultFile))
            {
                GlobalVars.Instance.Recipe = SerializeHelper.Load<Recipe>(defaultFile) as Recipe;
                txtCurrentConfig.Text = "default";
                InitialUI();
            }
            //try
            //{
            //    MotorController.Instance.Init();
            //}
            //catch(Exception ex)
            //{
            //    SetInfo(ex.Message);
            //}
            
        }

        private void SetInfo(string s, bool ok = false)
        {
            Brush brush = ok ? Brushes.Black : Brushes.Red;
            txtInfo.Foreground = brush;
            txtInfo.Text = s;
        }


        void AddLog(string s)
        {
            txtLog.Dispatcher.BeginInvoke((Action)(() => txtLog.Text += s + "\r\n"));
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            thread = new Thread(tester.Go);
            thread.Start();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectionWindow selectionWindow = new SelectionWindow();
            selectionWindow.ShowDialog();
            txtCurrentConfig.Text = SelectionWindow.curConfigName;
            
            string sFile = Helper.GetConfigFolder() + SelectionWindow.curConfigName + ".xml";
            if(File.Exists(sFile))
            {
                GlobalVars.Instance.Recipe = SerializeHelper.Load<Recipe>(sFile) as Recipe;
            }
            InitialUI();
        }

        private void InitialUI()
        {
            InitialListBox();
            simulationViewer = new SimulationViewer(viewerContainer.ActualWidth, viewerContainer.ActualHeight);

            testViewer = new TestViewer(viewerContainer.ActualWidth, viewerContainer.ActualHeight);
            viewerContainer.Children.Clear();
            simuationContainer.Children.Clear();
           
            viewerContainer.Children.Add(testViewer);
            simuationContainer.Children.Add(simulationViewer);
        }

        private void InitialListBox()
        {
            int plateCnt = GlobalVars.Instance.PlateCnt;
            GlobalVars.Instance.plateID_LABInfos.Clear();
            
            lstPlates.ItemsSource = plateModels;
            plateModels.Clear();
            for (int i = 0; i < plateCnt; i++)
            {
                plateModels.Add(new PlateModel((i + 1).ToString()));
                GlobalVars.Instance.plateID_LABInfos.Add(i + 1, new Dictionary<PositionInt, LAB>());
            }
        }



        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            LayoutDef layoutDef = new LayoutDef();
            layoutDef.ShowDialog();
        }

        private void lstPlates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstPlates.SelectedIndex == -1)
                return;
            int selIndex = lstPlates.SelectedIndex;
            testViewer.Position_LAB =  GlobalVars.Instance.plateID_LABInfos[selIndex + 1];

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string sFile = Helper.GetOutputFolder() + DateTime.Now.ToString("yyMMddHHmmss") + ".csv";
            ResultWriter.Save(sFile);
            this.Close();
        }

        private void btnAbort_Click(object sender, RoutedEventArgs e)
        {
            tester.Abort();
        }

        private void CommandHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void CommandHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
