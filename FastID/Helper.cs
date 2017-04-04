using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FastID
{
    class Helper
    {
        static public string GetExeParentFolder()
        {
            string s = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            int index = s.LastIndexOf("\\");
            return s.Substring(0, index) + "\\";
        }
        static public string GetConfigFolder()
        {
            string s = GetExeParentFolder();
            string folder = s + "Config\\";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }


        static public string GetOutputFolder()
        {
            string s = GetExeParentFolder();
            string folder = s + "Output\\";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }

        static public string GetExeFolder()
        {
            string s = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return s + "\\";
        }

        internal static double GetDelta(LAB lab,ref bool isOk)
        {
            LABDelta labDelta = GlobalVars.Instance.Recipe.labDelta;
            double l = lab.l - labDelta.l;
            double a = lab.a - labDelta.a;
            double b = lab.b - labDelta.b;
            
            double delta = Math.Sqrt(l * l + a * a + b * b);
            isOk = delta < labDelta.delta;
            return delta;
        }
    }


    public class SerializeHelper
    {
        static public void Save<T>(T setting, string sFile) where T : class
        {
            int pos = sFile.LastIndexOf("\\");
            string sDir = sFile.Substring(0, pos);

            if (!Directory.Exists(sDir))
                Directory.CreateDirectory(sDir);

            if (File.Exists(sFile))
                File.Delete(sFile);

            XmlSerializer xs = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(sFile, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            xs.Serialize(stream, setting);
            stream.Close();
        }
        static public Object Load<T>(string sFile) where T : class
        {
            Object obj = new object();
            if (!File.Exists(sFile))
                throw new FileNotFoundException(string.Format("位于：{0}的配置文件不存在", sFile));
            Stream stream = new FileStream(sFile, FileMode.Open, FileAccess.Read, FileShare.Read);
           
            XmlSerializer xs = new XmlSerializer(typeof(T));
            obj = xs.Deserialize(stream) as T;
            stream.Close();
            return obj;
        }

        static public void SaveSettings(Recipe recipe, string sFile)
        {
            int pos = sFile.LastIndexOf("\\");
            string sDir = sFile.Substring(0, pos);

            if (!Directory.Exists(sDir))
                Directory.CreateDirectory(sDir);

            if (File.Exists(sFile))
                File.Delete(sFile);

            XmlSerializer xs = new XmlSerializer(typeof(Recipe));
            Stream stream = new FileStream(sFile, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            xs.Serialize(stream, recipe);
            stream.Close();
        }

        static public void LoadSettings(ref Recipe recipe, string sFile)
        {
            if (!File.Exists(sFile))
                throw new FileNotFoundException(string.Format("位于：{0}的配置文件不存在", sFile));
            Stream stream = new FileStream(sFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            XmlSerializer xs = new XmlSerializer(typeof(Recipe));
            recipe = xs.Deserialize(stream) as Recipe;
            stream.Close();
        }
    }
}
