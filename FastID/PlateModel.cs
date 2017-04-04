using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FastID
{
    class PlateModel : INotifyPropertyChanged
    {
        public string name;
        public PlateModel(string s)
        {
            name = s;
        }

        public string Name 
        {
            get
            {
                return name;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string s)
        {
           if (PropertyChanged != null)
           {
               PropertyChanged(this, new PropertyChangedEventArgs(s));
           }
        }

     
       private bool isFinished;
       public bool IsFinished
       {
           get { return isFinished; }
           set
           {
               isFinished = value;
               OnPropertyChanged("IsFinished");
           }
        }



       private bool isChecking;

       public bool IsChecking
       {
           get { return isChecking; }
           set
           {
               isChecking = value;
               OnPropertyChanged("IsChecking");
           }
       }

      


    }
}
