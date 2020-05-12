using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CondorWPFileGen
{
    public class Waypoint
    {
        public float LatDecDeg { get; set; }
        public float LonDecDeg { get; set; }
        public float ElevMeters { get; set; }
        public String WPName { get; set; }
        public String Comments { get; set; }
        public int OrientDeg { get; set; }
        public int RwyWidthMeters { get; set; }
        public int RwyLengthMeters { get; set; }
        public float FreqMHz { get; set; }

        public Waypoint( string name, float latdeg, float londeg,
                        float elevm, int orientdeg, int widthm, int lenm, 
            float freqmhz, string comstr)
        {
            LatDecDeg = latdeg;
            LonDecDeg = londeg;
            ElevMeters = elevm;
            WPName = name;
            OrientDeg = orientdeg;
            RwyLengthMeters = lenm;
            RwyWidthMeters = widthm;
            Comments = comstr;
        }

        public Waypoint( string name, float latdeg, float londeg,
                        float elevm, int orientdeg, int widthm, int lenm)
            :this(name, latdeg, londeg, elevm, orientdeg, widthm, lenm, 0, "")
        {
        }

        public Waypoint( string name, float latdeg, float londeg,
                        float elevm)
            :this(name, latdeg, londeg, elevm, 0, 0, 0, 0, "")
        {
        }

        public Waypoint( string name, float latdeg, float londeg)
            :this(name, latdeg, londeg, 0, 0, 0, 0, 0, "")
        {
        }

        public Waypoint( string name, float latdeg, float londeg, string comment)
            :this(name, latdeg, londeg, 0, 0, 0, 0, 0, comment)
        {
        }

        //cup format
        public Waypoint( string name, float latdeg, float londeg,
                        float elevm, int orientdeg, int lenm, float freqmhz, string comment)
            : this(name, latdeg, londeg, elevm, orientdeg, 0, lenm, freqmhz, comment)
        {
        }
    }
}
