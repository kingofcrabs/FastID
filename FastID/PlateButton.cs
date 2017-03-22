using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FastID
{
    class PlateButton : Button
    {
        public int ID = 1;
        public PlateButton(int ID)
        {
            this.ID = ID;
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            this.Background = null;
        }


    }
}
