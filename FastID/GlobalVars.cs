using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastID
{
    class GlobalVars
    {
        static private GlobalVars instance;

        public Recipe Recipe { get; set; }



        public Dictionary<int, Dictionary<PositionInt, LAB>> plateID_LABInfos { get; set; }

        public int PlateCnt 
        {
            get
            {
                var layoutInfo = Recipe.layoutInfo;
                int plateCnt = layoutInfo.xPlateCount * layoutInfo.yPlateCount;
                return plateCnt;
            }
        
        }


        static public GlobalVars Instance
        {
            get
            { 
                if(instance == null)
                {
                    instance = new GlobalVars();
                }
                return instance;
            }
        }

        private GlobalVars()
        {
            plateID_LABInfos = new Dictionary<int, Dictionary<PositionInt, LAB>>();
        }



        public PlateInfo PlateInfo
        {
            get
            {
                return Recipe.layoutInfo.plateInfo;
            }

        }

    }
}
