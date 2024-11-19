using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization; //added 12/18/11 to use CultureInfo.Invariant
using System.Windows; //added 01/07/12 for MessageBoxResult enumeration
using System.Threading; //added 01/07/12 for Thread.CurrentThread.CultureInfo change

namespace CondorWPFileGen
{
    //From a Dec 15 post on the Condor forum in the Scenery section
    //Here is the format of <scenery_name>.apt file which contains what you need:

    //Each airport are saved on 72 bytes, where file size/72 = nb of airports in file
    //1 byte: airport name length
    //35 bytes: airport name
    //float : latitude (in °)
    //float : longitude (in °)
    //float : altitude (in meters)
    //int : orientation (in °)
    //int : runway length
    //int : runway width
    //int : surface (1=asphalt)
    //float : radio frequency
    //boolean (1 byte) : flag runway (1=primary dir reversed)
    //boolean (1 byte) : flag runway (1=tow primary dir left side)
    //boolean (1 byte) : flag runway (1=tow secondary dir left side)
    //1 empty byte

    //Notes:
    //  05/11/20 Landscape Editor 2.01 altered the encoding of the runway direction long_int
    //      to encode a 2-decimal float value.  The encoding multiplies the floating point value
    //      by 100 and then adds 100,000.  This results in an integer > 365, which is the way
    //      the new format is detected.  To decode the floating point runway heading, subtract
    //      100,000 and then divide by 100
    //  05/12/20 Need to round result to nearest degree, as some soaring apps exclude float values

    public partial class frm_Main : Form
    {
        public List<Waypoint> WPList { get; set; } //holds all waypoint data

        public frm_Main()
        {
            InitializeComponent();
            tb_Output.Clear();
            WPList = new List<Waypoint>();
            UpdateControls();
        }

        private void btn_BrowseApt_Click(object sender, EventArgs e)
        {
            //Notes:
            //  01/07/12 bugfix - need to clear WPList before populating from .apt file

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "APT files (*.apt)|*.apt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.ShowReadOnly = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "apt";
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Load Scenery APT file";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    WPList.Clear(); //01/07/12 bugfix
                    FileInfo f_info = new FileInfo(openFileDialog1.FileName);
                    int s1 = (int)f_info.Length;
                    int numapts = s1 /72;
                    tb_APTFilename.Text = openFileDialog1.FileName.ToString();
                    BinaryReader binReader =
                        new BinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open), Encoding.Default);
                    try
                    {
                        for (int aptidx = 0; aptidx < numapts; aptidx++)
                        {
                            byte aptnamlen = binReader.ReadByte();
                            char[] aptnamechars = binReader.ReadChars(35);

                            char[] charsToTrim = {'\0'};
                            string aptname = new string(aptnamechars);
                            string lenaptname = aptname.Substring(0, aptnamlen);
                            //aptname = aptname.Trim(charsToTrim);
                            float aptlatdeg = binReader.ReadSingle();
                            float aptlondeg = binReader.ReadSingle();
                            float aptelevmeters = binReader.ReadSingle();
                            int aptrunwydeg = binReader.ReadInt32();
                            int aptlenmeters = binReader.ReadInt32();
                            int aptwidthmeters = binReader.ReadInt32();
                            int aptsfctype = binReader.ReadInt16();
                            float aptfreqmhz = binReader.ReadSingle();
                            byte[] aptmisc = binReader.ReadBytes(6); //remainder discarded

                            //11/18/24 bugfix: wasn't trimming apt name to aptnamlen properly
                            //Waypoint wp = new Waypoint(aptname, aptlatdeg, aptlondeg, aptelevmeters,
                            //    aptrunwydeg, aptlenmeters, aptwidthmeters, aptfreqmhz.ToString()); //put freq in comments
                            Waypoint wp = new Waypoint(lenaptname, aptlatdeg, aptlondeg, aptelevmeters,
                                aptrunwydeg, aptlenmeters, aptwidthmeters, aptfreqmhz.ToString()); //put freq in comments
                            WPList.Add(wp);
                        }
                    }

                    catch (EndOfStreamException err)
                    {
                        MessageBox.Show("Unexpected End of File" + err.ToString());
                    }
                    finally
                    {
                        binReader.Close();
                    }

                    //display the results in the output window in CUP form, as shown below
                    //name,code,country,lat,lon,elev,style,rwdir,rwlen,freq,desc
                    //"01 CCSC",,,3928.633N,08405.650W,286.5m,2,0,0.0m,"23.3   09/27","23.3   09/27"
                    //"02 Beachy",,,4005.817N,08321.367W,295.7m,1,,,,
                    //"03 Bellefntn",,,4022.333N,08349.133W,333.1m,2,0,0.0m,"7I7","7I7"
                    //"04 Chillicth",,,3920.500N,08257.317W,187.5m,1,,,,"City"
                    //write "*********** [Condor Cup filename] *********" to textbox
                    tb_Output.AppendText("*******************************************************************************" + Environment.NewLine);
                    tb_Output.AppendText("========= " + tb_APTFilename.Text + Environment.NewLine);
                    tb_Output.AppendText("*******************************************************************************" + Environment.NewLine);

                    foreach (Waypoint wpi in WPList)
	                {
                        string wpstr = GenCUPString(wpi);
                        tb_Output.AppendText(wpstr + Environment.NewLine);
	                }
                }
            }

            UpdateControls();
        }

        private void btn_SaveApt_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "CUP files (*.cup)|*.cup|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter outfile =
                            new StreamWriter(saveFileDialog1.FileName))
                {
                    tb_Output.AppendText("*********************************************************************************" + Environment.NewLine);
                    tb_Output.AppendText("============== AIRPORT CUP FILE  =================" + Environment.NewLine);
                    tb_Output.AppendText("*********************************************************************************" + Environment.NewLine);

                    //required header line
                    string hdrstr = "name,code,country,lat,lon,elev,style,rwdir,rwlen,freq,desc";
                    outfile.WriteLine(hdrstr);
                    tb_Output.AppendText(hdrstr + Environment.NewLine);

                    //WriteOutAirportData(outfile);
                    WriteOutAirportData(outfile, true);//12/18/11 rev to display results in console
                    outfile.Close();
                }
            }
        }

        private string GenCUPString(Waypoint wpi)
        {
            //Purpose:
            //Generate CUP formatted string from waypoint data, as shown below
            //name,code,country,lat,lon,elev,style,rwdir,rwlen,freq,desc
            //"01 CCSC",,,3928.633N,08405.650W,286.5m,2,0,0.0m,"23.3   09/27","23.3   09/27"
            //"02 Beachy",,,4005.817N,08321.367W,295.7m,1,,,,
            //"03 Bellefntn",,,4022.333N,08349.133W,333.1m,2,0,0.0m,"7I7","7I7"
            //"04 Chillicth",,,3920.500N,08257.317W,187.5m,1,,,,"City"

            //Notes:
            //  have to convert coords from ddd.ddddd to dddmm.mmm format
            //  note that latitude deg is only 2 digits, while londeg must be 3
            //  force waypoint style to '5' (hard surface airport)
            //  12/25/11 bugfix - have to ensure that minutes are positive
            //  12/25/11 bugfix - apparently must also include 'NSEW' indicators AND ensure exactly 3 digits after decimal
            //  12/25/11 bugfix - MUST be exactly ddmm.mmmN/S for latitude, and dddmm.mmmE/W for latitude
            //  11/18/12 bugfix - must do N/S, E/W compare on original LatDecDeg or LonDecDeg before truncating
            //  05/11/20 Landscape Editor 2.01 encodes a 2-decimal floating point number into runway dir value
            //      by multiplying val by 100 & then adding 100,000. Result is always > 360.  See
            //      https://www.condorsoaring.com/forums/viewtopic.php?f=38&t=19890&p=170411&hilit=ChristChurch#p170411
            //      for details.
            //  05/12/20 Need to round result to nearest degree, as some soaring apps exclude float values

            int latdeg = (int)wpi.LatDecDeg; //truncates
            float latmin = 60 * (Math.Abs(wpi.LatDecDeg) - Math.Abs(latdeg));
            string NSstr = (wpi.LatDecDeg > 0) ? "N" : "S"; //11/18/12 must do compare before truncation
            string latstr = Math.Abs(latdeg).ToString("00", CultureInfo.InvariantCulture) + latmin.ToString("00.000", CultureInfo.InvariantCulture) + NSstr;

            int londeg = (int)wpi.LonDecDeg; //truncates
            float lonmin = 60 * (Math.Abs(wpi.LonDecDeg) - Math.Abs(londeg));
            string EWstr = (wpi.LonDecDeg > 0) ? "E" : "W"; //11/18/12 must do compare before truncation
            string lonstr = Math.Abs(londeg).ToString("000", CultureInfo.InvariantCulture) + lonmin.ToString("00.000", CultureInfo.InvariantCulture) + EWstr;

            //05/11/20 have to check wpi.OrientDeg for possible new LE2.01 format
            string rwydirstr = String.Empty;
            if (wpi.OrientDeg > 360)
            {
                //is new LE2.01 format - convert to 2-digit value
                float rnwyhdg = (float)(wpi.OrientDeg - 100000) / 100;
                //rwydirstr = string.Format(CultureInfo.InvariantCulture, "{0}", (float)(wpi.OrientDeg - 100000) / 100);
                rwydirstr = string.Format(CultureInfo.InvariantCulture, "{0}", Math.Round(rnwyhdg));
            }
            else
            {
                rwydirstr = string.Format(CultureInfo.InvariantCulture, "{0}", wpi.OrientDeg);
            }

            //note that latitude deg is only 2 digits, while londeg must be 3
            //12/18/11 bugfix - use 'CultureInfo.InvariantCulture' to force dot fraction sep
            //05/11/20 use rwydirstr vs wpi.OrientDeg
            //string wpstr = string.Format(CultureInfo.InvariantCulture, "\"{0}\",,,{1},{2},{3}m,5,{4},{5},{6},",
            //    wpi.WPName, latstr, lonstr, wpi.ElevMeters, wpi.OrientDeg,
            //    wpi.RwyLengthMeters, wpi.FreqMHz);
            string wpstr = string.Format(CultureInfo.InvariantCulture, "\"{0}\",,,{1},{2},{3}m,5,{4},{5},{6},",
                wpi.WPName, latstr, lonstr, wpi.ElevMeters, rwydirstr,
                wpi.RwyLengthMeters, wpi.FreqMHz);

            return wpstr;
        }

        private void UpdateControls()
        {
            btn_SaveApt.Enabled = (tb_APTFilename.Text.Length > 0);
            btn_BrowseCondorCup.Enabled = (tb_APTFilename.Text.Length > 0);
            btn_GenCombOutput.Enabled = btn_SaveApt.Enabled && (tb_CondorCupFilename.Text.Length > 0);
        }

        private void btn_GenCombOutput_Click(object sender, EventArgs e)
        {
            //Notes:
            //  01/07/12 - rev to warn if user tries to combine .cup from one folder with .apt from another
//DEBUG!!
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE", false);
//DEBUG!!

            //01/07/12 warn if .CUP and .APT files come from different folders
            string aptfoldername = Path.GetDirectoryName(tb_APTFilename.Text);
            string cupfoldername = Path.GetDirectoryName(tb_CondorCupFilename.Text);
            if (cupfoldername != aptfoldername)
            {
                DialogResult dlgresult = MessageBox.Show(".CUP and .APT folder names are different - Are you sure you want to do this?",
                    "Attention!", MessageBoxButtons.YesNoCancel);

                if (dlgresult != DialogResult.Yes)
                {
                    return;
                }
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "CUP files (*.cup)|*.cup|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter outfile =
                            new StreamWriter(saveFileDialog1.FileName))
                {
                    //separator for log console
                    tb_Output.AppendText("*******************************************************************************" + Environment.NewLine);
                    tb_Output.AppendText("============== " + saveFileDialog1.FileName + Environment.NewLine);
                    tb_Output.AppendText("*******************************************************************************" + Environment.NewLine);

                    //required header line
                    string hdrstr = "name,code,country,lat,lon,elev,style,rwdir,rwlen,freq,desc";
                    outfile.WriteLine(hdrstr);

                    //write out airport data in CUP format
                    tb_Output.AppendText("*******************************************************************************" + Environment.NewLine);
                    tb_Output.AppendText("============== AIRPORT DATA  =================" + Environment.NewLine);
                    tb_Output.AppendText("*******************************************************************************" + Environment.NewLine);

                    //required header line
                    WriteOutAirportData(outfile, true); //rev 12/18/11 to display to console

                    //copy Condor waypoints from Condor CUP file to output file
                    if(File.Exists(tb_CondorCupFilename.Text))
                    {
                        tb_Output.AppendText("*********************************************************************" + Environment.NewLine);
                        tb_Output.AppendText("============== CONDOR CUP DATA  =================" + Environment.NewLine);
                        tb_Output.AppendText("*********************************************************************" + Environment.NewLine);

                        //10/31/12 bugfix - use Encoding.Default to properly handled accented chars
                        //using (StreamReader infile =
                        //        new StreamReader(tb_CondorCupFilename.Text))
                        using (StreamReader infile =
                                new StreamReader(tb_CondorCupFilename.Text, Encoding.Default))
                        //using (StreamReader infile =
                        //        new StreamReader(tb_CondorCupFilename.Text, System.Text.Encoding.GetEncoding("iso-8859-8")))
                        {
                            infile.ReadLine(); //read and discard header line
                            while (!infile.EndOfStream)
                            {
                                string wpstr = infile.ReadLine();
                                outfile.WriteLine(wpstr);
                                tb_Output.AppendText(wpstr + Environment.NewLine);
                            }
                            infile.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Condor CUP file doesn't exist");
                    }

                    outfile.Close();
                }
            }
        }

        //private void WriteOutAirportData(StreamWriter outfile)
        private void WriteOutAirportData(StreamWriter outfile, Boolean bWriteToConsole)
        {
            //write out airport lines
            foreach (Waypoint wpi in WPList)
            {
                string wpstr = GenCUPString(wpi);
                outfile.WriteLine(wpstr);

                if (bWriteToConsole)
                {
                    tb_Output.AppendText(wpstr + Environment.NewLine);
                }
            }
        }

        private void btn_BrowseCondorCup_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CUP files (*.cup)|*.cup|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.ShowReadOnly = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "apt";
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Load Scenery CUP file";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                //10/31/12 bugfix - use Encoding.Default to properly handle accented chars
                //using (StreamReader infile = new StreamReader(filename))
                using (StreamReader infile = new StreamReader(filename, Encoding.Default))
                //using (StreamReader infile = new StreamReader(filename, System.Text.Encoding.GetEncoding("iso-8859-8")))
                {
                    //write "*********** [Condor Cup filename] *********" to textbox
                    tb_Output.AppendText("*******************************************************************************" + Environment.NewLine);
                    tb_Output.AppendText("========= " + filename + Environment.NewLine);
                    tb_Output.AppendText("*******************************************************************************" + Environment.NewLine);
                    string hdrstr = infile.ReadLine();
                    tb_Output.AppendText(hdrstr + Environment.NewLine);

                    //write each line to output textbox
                    while (!infile.EndOfStream)
                    {
                        string wpstr = infile.ReadLine();
                        tb_Output.AppendText(wpstr + Environment.NewLine);
                    }
                    infile.Close();
                }

                tb_CondorCupFilename.Text = filename;
                UpdateControls();
            }
        }

        //01/07/12 added for convenience
        private void button1_Click(object sender, EventArgs e)
        {
            tb_Output.Clear();
        }
    }
}
