using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastID
{
    class ResultWriter
    {
        public static void Save(string sFile)
        {
            var plateID_LabInfos = GlobalVars.Instance.plateID_LABInfos;
            var delta = GlobalVars.Instance.Recipe.labDelta.delta;

            List<string> strs = new List<string>();
            strs.Add(GetHeader());
            foreach(var pair in plateID_LabInfos)
            {
                int plateID = pair.Key;
                Dictionary<PositionInt, LAB> eachPos_Val = pair.Value;
                foreach(var subPair in eachPos_Val)
                {
                    PositionInt position = subPair.Key;
                    LAB lab = subPair.Value;
                    string line = GetLine(plateID, position, lab);
                    strs.Add(line);
                }
            }
            File.WriteAllLines(sFile, strs);
        }

        private static string GetHeader()
        {
            return "PlateID,XLED,YLED,L,A,B,RealDelta,AcceptDelta,IsOk";
        }
        private static string GetLine(int plateID, PositionInt position, LAB lab)
        {
            bool bok = false;
            double delta = Helper.GetDelta(lab,ref bok);
            double acceptDelta = GlobalVars.Instance.Recipe.labDelta.delta;
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", plateID, position.x+1, position.y+1, lab.l, lab.a, lab.b, delta, acceptDelta,bok.ToString());
        }
    }
}
