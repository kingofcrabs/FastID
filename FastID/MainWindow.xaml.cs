using FastID.controls;
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
        
        Thread thread;

        ObservableCollection<TestResult> testResults = new ObservableCollection<TestResult>();
        ObservableCollection<PlateModel> plateModels = new ObservableCollection<PlateModel>();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.SizeChanged += MainWindow_SizeChanged;
            Tester.Instance.onLABUpdate += tester_onLABUpdate;
            Tester.Instance.onProgressChanged += tester_onProgressChanged;
            Tester.Instance.onMoveGarbageUpdate += Instance_onMoveGarbageUpdate;
            IOController.Instance.onChannelError += Instance_onChannelError;
            this.Closing += MainWindow_Closing;
        }

        void Instance_onMoveGarbageUpdate(int plateID, int ledID)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                simulationViewer.SetCurrentInfo(plateID, ledID);
            }));
        }

        void Instance_onChannelError(int channelNum)
        {
            this.Dispatcher.Invoke(()=>{
                SetInfo(string.Format("轴{0}报警！", channelNum));
                Tester.Instance.Abort();
            });
            
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
            if (sMessage.Contains("Abort") || Tester.Instance.Finished || sMessage.Contains("Error"))
                this.Dispatcher.BeginInvoke((Action)(() => OnFinishAbortOrError(sMessage.Contains("Error")))
                    );
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
            try
            {
                MotorController.Instance.Init();
                IOController.Instance.Init();
            }
            catch(Exception ex)
            {
                SetInfo(ex.Message, false);
                IOController.Instance.RedLightOn();
            }
            
            IOController.Instance.YellowLightOn();
            string defaultFile = Helper.GetConfigFolder() + "default.xml";
            lstviewResult.ItemsSource = testResults;
            if(File.Exists(defaultFile))
            {
                GlobalVars.Instance.Recipe = SerializeHelper.Load<Recipe>(defaultFile) as Recipe;
                txtCurrentConfig.Text = "default";
                InitialUI();
            }
            
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

        private void OnFinishAbortOrError(bool error)
        {
            toolbarContainer.IsEnabled = true;
            int cnt = Tester.Instance.StatisticInvalidLEDs();
            AddLog(string.Format("不合格的LED数量：{0}", cnt));
            if(error)
            {
                IOController.Instance.RedLightOn();
            }
            else
            {
                string sFile = Helper.GetOutputFolder() + DateTime.Now.ToString("yyMMddHHmmss") + ".csv";
                ResultWriter.Save(sFile);
                IOController.Instance.YellowLightOn();
            }
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            IOController.Instance.GreenLightOn();
            InitialListBox();
            testResults.Clear();
            toolbarContainer.IsEnabled = false;
            simulationViewer.Moving2Garbage = false;
            thread = new Thread(Tester.Instance.Go);
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
            Tester.Instance.Abort();
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

        private void btnPickError_Click(object sender, RoutedEventArgs e)
        {
            if(Tester.Instance.TestResult.Count == 0)
            {
                SetInfo("没有错误样品！");
                return;
            }
            simulationViewer.Moving2Garbage = true;
            thread = new Thread(Tester.Instance.MoveInvalidSamples2Garbage);
            thread.Start();
        }

      
    }
}
