using Microsoft.Win32;
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
    /// Interaction logic for LayoutDef.xaml
    /// </summary>
    public partial class LayoutDef : Window
    {
        
        public LayoutDef()
        {
            InitializeComponent();
            this.Loaded += LayoutDef_Loaded;
        }

        void LayoutDef_Loaded(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe();
            string sFile = Helper.GetConfigFolder() + "default.xml";
            if(File.Exists(sFile))
            {
                SerializeHelper.LoadSettings(ref recipe, sFile);
                InitUI(recipe, sFile);
            }
        }

        private void SaveSettings()
        {
            double a = GetDouble(txtA.Text,"A");
            double l = GetDouble(txtL.Text, "L");
            double b = GetDouble(txtB.Text, "B");
            double delta = GetDouble(txtDelta.Text, "Delta");
            LABDelta labDelta = new LABDelta(l, a, b, delta);

            double refX = GetDouble(txtRefX.Text, "RefX");
            double refY = GetDouble(txtRefY.Text, "RefY");
            Point refPoint = new Point(refX,refY);

            //layout info 
            int plateXCnt = GetInt(txtXPlateCnt.Text, "X方向板子数量");
            int plateYCnt = GetInt(txtYPlateCnt.Text, "Y方向板子数量");
            double plateTopLeftX = GetDouble(txtTopLeftPlateX.Text, "板子X起始位");
            double plateTopLeftY = GetDouble(txtTopLeftPlateY.Text, "板子Y起始位");
            double plateBottomRightX = GetDouble(txtBottomRightPlateX.Text, "板子X结束位");
            double plateBottomRightY = GetDouble(txtBottomRightPlateY.Text, "板子Y结束位");
            Point firstPlate = new Point(plateTopLeftX, plateTopLeftY);
            Point lastPlate = new Point(plateBottomRightX, plateBottomRightY);

            //plateInfo
            int ledXCnt = GetInt(txtXLEDCnt.Text, "X方向LED数量");
            int ledYCnt = GetInt(txtYLEDCnt.Text, "Y方向LED数量");
            double ledTopLeftX = GetDouble(txtTopLeftLEDX.Text, "LED X起始位");
            double ledTopLeftY = GetDouble(txtTopLeftLEDY.Text, "LED Y起始位");
            double ledBottomRightX = GetDouble(txtBottomRightLEDX.Text, "LED X结束位");
            double ledBottomRightY = GetDouble(txtBottomRightLEDY.Text, "LED Y结束位");
            Point firstLED = new Point(ledTopLeftX, ledTopLeftY);
            Point lastLED = new Point(ledBottomRightX, ledBottomRightY);
            PlateInfo plateInfo = new PlateInfo(firstLED, lastLED, ledXCnt, ledYCnt);
            LayoutInfo layoutInfo = new LayoutInfo(plateInfo, firstPlate, lastPlate,plateXCnt,plateYCnt);
            Recipe recipe = new Recipe(layoutInfo, refPoint, labDelta);
            string sFile = Helper.GetConfigFolder() + string.Format("{0}.xml",txtRecipeName.Text );
            SerializeHelper.Save(recipe, sFile);
        }

        private int GetInt(string s, string desc)
        {
            int val = 0;
            if (!int.TryParse(s, out val))
                throw new Exception(string.Format("{0}的值不是int类型！", desc));
            return val;
        }

        private double GetDouble(string s,string desc)
        {
            double val = 0;
            if (!double.TryParse(s, out val))
                throw new Exception(string.Format("{0}的值不是double类型！",desc));
            return val;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                string sFile = "";
                if (openFileDialog.ShowDialog() == true)
                    sFile = openFileDialog.FileName;
                if (sFile == "")
                    return;

                Recipe recipe = new Recipe();
                SerializeHelper.LoadSettings(ref recipe, sFile);
                InitUI(recipe, sFile);
                SetInfo("成功打开配置！");
            }
            catch(Exception ex)
            {
                SetInfo(ex.Message, true);
            }
        }

        private void SetInfo(string s, bool isError = false)
        {
            txtInfo.Text = s;
            txtInfo.Foreground = isError ? Brushes.Red : Brushes.Black;
        }

        private void InitUI(Recipe recipe,string sFile)
        {
            txtA.Text = recipe.labDelta.a.ToString();
            txtL.Text = recipe.labDelta.l.ToString();
            txtB.Text = recipe.labDelta.b.ToString();
            txtDelta.Text = recipe.labDelta.delta.ToString();
            txtRefX.Text = recipe.refPoint.X.ToString();
            txtRefY.Text = recipe.refPoint.Y.ToString();

            txtXPlateCnt.Text = recipe.layoutInfo.xPlateCount.ToString();
            txtYPlateCnt.Text = recipe.layoutInfo.yPlateCount.ToString();
            txtTopLeftPlateX.Text = recipe.layoutInfo.topLeft.X.ToString();
            txtTopLeftPlateY.Text = recipe.layoutInfo.topLeft.Y.ToString();
            txtBottomRightPlateX.Text = recipe.layoutInfo.bottomRight.X.ToString();
            txtBottomRightPlateY.Text = recipe.layoutInfo.bottomRight.Y.ToString();

       
            //led info
            txtXLEDCnt.Text = recipe.layoutInfo.plateInfo.xLEDCount.ToString();
            txtYLEDCnt.Text = recipe.layoutInfo.plateInfo.yLEDCount.ToString();
            txtTopLeftLEDX.Text = recipe.layoutInfo.plateInfo.firstLEDPos.X.ToString();
            txtTopLeftLEDY.Text = recipe.layoutInfo.plateInfo.firstLEDPos.Y.ToString();
            txtBottomRightLEDX.Text = recipe.layoutInfo.plateInfo.lastLEDPos.X.ToString();
            txtBottomRightLEDY.Text = recipe.layoutInfo.plateInfo.lastLEDPos.Y.ToString();

            int pos = sFile.LastIndexOf("\\") + 1;
            sFile = sFile.Substring(pos).Replace(".xml","");
            txtRecipeName.Text = sFile;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveSettings();
            }
            catch (Exception ex)
            {
                txtInfo.Text = ex.Message;
                txtInfo.Foreground = Brushes.Red;
                return;
            }
            txtInfo.Text = "保存成功！";
        }
    }
}
