using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

//using System.Buffers.Binary;

using Microsoft.WindowsAPICodePack.Dialogs;

public enum Stage : UInt32
{
    FountainOfDreams = 002,
    PokemonStadium = 003,
    PrincessPeachsCastle = 004,
    KongoJungle = 005,
    Brinstar = 006,
    Corneria = 007,
    YoshisStory = 008,
    Onett = 009,
    MuteCity = 010,
    RainbowCruise = 011,
    JungleJapes = 012,
    GreatBay = 013,
    HyruleTemple = 014,
    BrinstarDepths = 015,
    YoshisIsland = 016,
    GreenGreens = 017,
    Fourside = 018,
    MushroomKingdomI = 019,
    MushroomKingdomII = 020,
    Venom = 022,
    PokeFloats = 023,
    BigBlue = 024,
    IcicleMountain = 025,
    FlatZone = 027,
    DreamLandN64 = 028,
    YoshisIslandN64 = 029,
    KongoJungleN64 = 030,
    Battlefield = 031,
    FinalDestination = 032,
}

public enum Character : byte
{
    Mario = 00,
    Fox = 01,
    CaptainFalcon = 02,
    DonkeyKong = 03,
    Kirby = 04,
    Bowser = 05,
    Link = 06,
    Sheik = 07,
    Ness = 08,
    Peach = 09,
    Popo = 10,
    Nana = 11,
    Pikachu = 12,
    Samus = 13,
    Yoshi = 14,
    Jigglypuff = 15,
    Mewtwo = 16,
    Luigi = 17,
    Marth = 18,
    Zelda = 19,
    YoungLink = 20,
    DrMario = 21,
    Falco = 22,
    Pichu = 23,
    MrGameAndWatch = 24,
    Ganondorf = 25,
    Roy = 26,
}




public enum CharacterColour : UInt32
{
    Red = 0,
    Blue = 1,
    // Add others...
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct OptionalCharacterColour
{
    public Character character;
    public byte color;
}

//[StructLayout(LayoutKind.Sequential)]
//public struct Time
//{
//    public UInt64 time;
//    //public int hours;
//    //public int minutes;
//    //public int seconds;
//    // Update to match actual Rust Time struct
//}

[StructLayout(LayoutKind.Sequential, Size = 31)]
public struct Name
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
    public byte[] bytes;
}

[StructLayout(LayoutKind.Sequential, Size = 10)]
public struct ConnectCode
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public byte[] bytes;
}

//[StructLayout(LayoutKind.Sequential, Pack = 1)]
[StructLayout(LayoutKind.Sequential)]
struct GameInfo
{
    public Stage stage;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte [] ports_used;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public OptionalCharacterColour[] starting_character_colours;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public Name [] names;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public ConnectCode[] connect_codes;

    public UInt64 start_time;

    public UInt32 timer;
    public Int32 duration;

    public byte version_major;
    public byte version_minor;
    public byte version_patch;
}

namespace SlippiGUI
{
    public partial class Form1 : Form
    {
        public string[] ShortNames => new string[]
        {
            "Mario",
            "Fox",
            "Falcon",
            "DK",
            "Kirby",
            "Bowser",
            "Link",
            "Sheik",
            "Ness",
            "Peach",
            "Popo",
            "Nana",
            "Pikachu",
            "Samus",
            "Yoshi",
            "Puff",
            "Mew2",
            "Luigi",
            "Marth",
            "Zelda",
            "YLink",
            "Doc",
            "Falco",
            "Pichu",
            "GaW",
            "Ganon",
            "Roy",
            "XX"
        };

        private string[] slpPaths;
        private string exportFolderPath;
        private string importFolderPath;
        private List<SlippiEntry> allEntries;
        private BindingList<SlippiEntry> filteredEntries;
        ToolTip exportPathToolTip;
        ToolTip importPathToolTip;
        private string originalGameName;
        private CancellationTokenSource populateCts;

        public Form1()
        {
            InitializeComponent();

            this.ActiveControl = dataGridView1;

            exportLabel.Text = "no export path set";
            importLabel.Text = "no replay path set";

            clipLengthTextBox.TextChanged -= clipLengthTextBox_TextChanged;

            clipLengthTextBox.Text = 360.ToString();

            clipLengthTextBox.TextChanged += clipLengthTextBox_TextChanged;

            allEntries = new List<SlippiEntry>();
            filteredEntries = new BindingList<SlippiEntry>();

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Clear();

            exportPathToolTip = new ToolTip();
            exportPathToolTip.SetToolTip(exportLabel, "no export path set");

            importPathToolTip = new ToolTip();
            importPathToolTip.SetToolTip(exportLabel, "no replay path set");

            numDisplayedFilesLabel.Text = "Displayed files: 0";
            //exportMenuStrip.Items.Add(deleteItem);

            saveStateButtonComboBox.Items.Add("D-PAD DOWN");
            saveStateButtonComboBox.Items.Add("D-PAD RIGHT");
            saveStateButtonComboBox.Items.Add("D-PAD LEFT");
            saveStateButtonComboBox.Items.Add("START");
            saveStateButtonComboBox.Items.Add("X");
            saveStateButtonComboBox.Items.Add("Y");
            saveStateButtonComboBox.Items.Add("L");
            saveStateButtonComboBox.Items.Add("R");

            saveStateButtonComboBox.SelectedIndexChanged -= saveStateButtonComboBox_SelectedIndexChanged;

            saveStateButtonComboBox.SelectedIndex = 0;

            saveStateButtonComboBox.SelectedIndexChanged += saveStateButtonComboBox_SelectedIndexChanged;


            namingStyleBox.Items.Add("Name + State Number");
            namingStyleBox.Items.Add("Name + Frame Number");
            namingStyleBox.Items.Add("Game Info + State Number");
            namingStyleBox.Items.Add("Game Info + Frame Number");

            namingStyleBox.SelectedIndexChanged -= frameComboBox_SelectedIndexChanged;

            namingStyleBox.SelectedIndex = 2;

            namingStyleBox.SelectedIndexChanged += frameComboBox_SelectedIndexChanged;
            //dataGridView1.ContextMenuStrip = exportMenuStrip;
            //    public string path { get; set; }
            //public string gameName { get; set; }
            //public Character p1 { get; set; }
            //public Character p2 { get; set; }
            //public Stage stage { get; set; }
            //public string dateAndTime { get; set}
            //public string matchLength { get; set}

            // Define column for 'name'

            AddGameInfoColumn("File Name", "gameName", 220, false);
            AddGameInfoColumn("Date", "dateAndTime", 225, true);
            AddGameInfoColumn("Duration", "duration", 100, true);
            //AddGameInfoColumn("Player Info", "vsString", 300, true);
            AddGameInfoColumn("Player 1", "p1String", 125, true);
            AddGameInfoColumn("Player 2", "p2String", 125, true);

            AddGameInfoColumn("P1 Code", "p1ConnectCode", 125, true);
            AddGameInfoColumn("P2 Code", "p2ConnectCode", 125, true);
            // AddGameInfoColumn("Player 1 Connect Code", "p1ConnectCode", 150, true);
            // AddGameInfoColumn("Player 2 Connect Code", "p2ConnectCode", 150, true);
            AddGameInfoColumn("Stage", "stageString", 60, true);
            AddGameInfoColumn("P1 Save State Count", "numClipsP1", 75, true);
            AddGameInfoColumn("P2 Save State Count", "numClipsP2", 75, true);

            //totalClipsLabel.Text = "Total P1 Save States: " + 0 + "        Total P2 Save States: " + 0;//"Total save states: " + 0;

            importLabel.AutoSize = false;
            importLabel.AutoEllipsis = true;
            importLabel.Width = 300;

            exportLabel.AutoSize = false;
            exportLabel.AutoEllipsis = true;
            exportLabel.Width = 300;

            ReadSettings();

            if( importFolderPath == null )
            {
                openImportButton.Hide();
            }

            if( exportFolderPath == null )
            {
                openExportButton.Hide();
            }
            
            //AddGameInfoColumn("Date and Time", "dateAndTime", 100, true);
            //AddGameInfoColumn("Match Length", "matchLength", 100, true);

            // public UInt32 clipFrame { get; set; }
            //public UInt32 clipLength { get; set; }
        }

        private void ExportFromPlayer( int pIndex )
        {
            if( exportFolderPath == null )
            {
                MessageBox.Show("export path was empty");
                return;
            }

            try
            {
                string exporTest = Path.GetFullPath(exportFolderPath);
            }
            catch( Exception )
            {
                MessageBox.Show("export path was invalid");
            }


            int clipLength = 360;
            int foundLength = 0;

            if (int.TryParse(clipLengthTextBox.Text.Trim(), out foundLength))
            {
                clipLength = foundLength;
                // It's a valid integer — use it
                //Console.WriteLine($"User entered: {parsedValue}");
            }
            else
            {
                MessageBox.Show("clip length was not valid. Setting to default");
                // Invalid input — optionally do nothing or handle it
                // Console.WriteLine("Invalid number, ignoring input.");

                clipLengthTextBox.TextChanged -= clipLengthTextBox_TextChanged;
                clipLengthTextBox.Text = clipLength.ToString();

                clipLengthTextBox.TextChanged += clipLengthTextBox_TextChanged;
            }

            if (exportFolderPath == null)
            {
                MessageBox.Show("export path not set");
                return;
            }

            int totalExportedClips = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                SlippiEntry entry = row.DataBoundItem as SlippiEntry;
                if (entry != null)
                {
                    if (pIndex == 0)
                    {
                        totalExportedClips += entry.numClipsP1;
                    }
                    else
                    {
                        totalExportedClips += entry.numClipsP2;
                    }

                    CreateSaveStates(exportFolderPath, entry.filePath, entry.gameName.Substring( 0, 20 ), clipLength, pIndex);
                }
            }

            MessageBox.Show("Created " + totalExportedClips + " save states in " + exportFolderPath);
        }

        private void saveStatesButton_Click_1(object sender, EventArgs e)
        {
            ////CheckForAllClips();

            //int clipLength = 360;
            //int foundLength = 0;

            //if (int.TryParse(clipLengthTextBox.Text.Trim(), out foundLength))
            //{
            //    clipLength = foundLength;
            //    // It's a valid integer — use it
            //    //Console.WriteLine($"User entered: {parsedValue}");
            //}
            //else
            //{
            //    // Invalid input — optionally do nothing or handle it
            //    // Console.WriteLine("Invalid number, ignoring input.");
            //    clipLengthTextBox.Text = clipLength.ToString();
            //}

            //if( exportFolderPath == null )
            //{
            //    MessageBox.Show("export path not set");
            //    return;
            //}

            //int totalExportedClips = 0;
            //foreach( var item in filteredEntries )
            //{
            //    if( comboBox1.SelectedIndex == 0 )
            //    {
            //        totalExportedClips += item.numClipsP1;
            //    }
            //    else
            //    {
            //        totalExportedClips += item.numClipsP2;
            //    }
            //    CreateSaveStates(exportFolderPath, item.filePath, item.gameName, clipLength);
            //}

            //MessageBox.Show("Created " + totalExportedClips + " save states in " + exportFolderPath);
    }

        private void CreateSaveStates(string exportFolder, string currSlp, string gameName, int clipLength, int pIndex)
        {
            byte[] exportFolder_utf8 = Encoding.UTF8.GetBytes(exportFolder + "\0");
            IntPtr exportFolderPtr = Marshal.AllocHGlobal(exportFolder_utf8.Length);
            Marshal.Copy(exportFolder_utf8, 0, exportFolderPtr, exportFolder_utf8.Length);

            byte[] slpPath_utf8 = Encoding.UTF8.GetBytes(currSlp + "\0");
            IntPtr slpPathPtr = Marshal.AllocHGlobal(slpPath_utf8.Length);
            Marshal.Copy(slpPath_utf8, 0, slpPathPtr, slpPath_utf8.Length);

            byte[] gameName_utf8 = Encoding.UTF8.GetBytes(gameName + "\0");
            IntPtr gameNamePtr = Marshal.AllocHGlobal(gameName_utf8.Length);
            Marshal.Copy(gameName_utf8, 0, gameNamePtr, gameName_utf8.Length);

            Program.create_savestates(exportFolderPtr, slpPathPtr, gameNamePtr, pIndex, clipLength, namingStyleBox.SelectedIndex,
                saveStateButtonComboBox.SelectedIndex);

            //MessageBox.Show("Created " + listBox1.Items.Count + " save states in " + exportFolder + " with game name " + gameName + " with clip length " + clipLength);
        }

        private void SetInfo( int index )
        {
            AddEntry(slpPaths[index]);
        }

        private void AddEntry(string path)
        {
            IntPtr namePtr = Marshal.StringToHGlobalAnsi(path);

            try
            {
                IntPtr gameInfoPtr = Program.read_info(namePtr);

                if (gameInfoPtr == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to read info (null pointer returned)");
                    return;
                }

                GameInfo test = Marshal.PtrToStructure<GameInfo>(gameInfoPtr);

                int lowPortIndex = -1;
                int highPortIndex = -1;
                for( int i = 0; i < 4; ++i )
                {
                    if( test.ports_used[i] > 0 )
                    {
                        if( lowPortIndex == -1 )
                        {
                            lowPortIndex = i;
                        }
                        else
                        {
                            highPortIndex = i;
                            break;
                        }
                    }
                }

                //should always be two ports though
                if( lowPortIndex == -1 || highPortIndex == -1 )
                {
                    return;
                }

                SlippiEntry se = new SlippiEntry();
                se.filePath = path;
                se.gameName = Path.GetFileNameWithoutExtension(path);
                se.p1 = test.starting_character_colours[lowPortIndex].character;
                se.p2 = test.starting_character_colours[highPortIndex].character;
                se.stage = test.stage;

                //only support competitive stages
                if (se.stage != Stage.FountainOfDreams && se.stage != Stage.PokemonStadium
                   && se.stage != Stage.YoshisStory && se.stage != Stage.DreamLandN64
                   && se.stage != Stage.Battlefield && se.stage != Stage.FinalDestination)
                {
                    return;
                }

                string shortStage = "";
                switch( se.stage )
                {
                    case Stage.FinalDestination:
                        shortStage = "FD";
                        break;
                    case Stage.FountainOfDreams:
                        shortStage = "FoD";
                        break;
                    case Stage.PokemonStadium:
                        shortStage = "PS";
                        break;
                    case Stage.YoshisStory:
                        shortStage = "YS";
                        break;
                    case Stage.DreamLandN64:
                        shortStage = "DL";
                        break;
                    case Stage.Battlefield:
                        shortStage = "BF";
                        break;
                    default:
                        shortStage = "XX";
                        break;
                }

                se.stageString = shortStage;

                se.clipFramesP1 = CheckForClips(path, 0);
                se.clipFramesP2 = CheckForClips(path, 1);

                se.numClipsP1 = se.clipFramesP1.Length;
                se.numClipsP2 = se.clipFramesP2.Length;

                if( se.numClipsP1 == 0 && se.numClipsP2 == 0 )
                {
                    return;
                }

                //se.p1ConnectCode = test.connect_codes[0].bytes.ToArray().ToString();
                //se.p2ConnectCode = test.connect_codes[1].bytes.ToString();

                string[] codes = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    codes[i] = System.Text.Encoding.ASCII
                        .GetString(test.connect_codes[i].bytes)
                        .TrimEnd('\0'); // Remove null terminator if present
                }

                int gameSeconds = (test.duration + 123) / 60;//(test.duration + 123) / 60;
                int minutes = gameSeconds / 60;
                int seconds = gameSeconds % 60;
                se.duration = $"{minutes}m {seconds}s";//String.Format("{}m {}s", minutes, seconds); //gameLen.ToString();//test.version_major + " " + test.version_minor + " " + test.version_patch;//SwapEndian(test.timer).ToString();
                                                       //DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(se.dateAndTime);
                                                       //DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


                se.p1ConnectCode = Encoding.GetEncoding("shift_jis").GetString(test.connect_codes[lowPortIndex].bytes);
                se.p2ConnectCode = Encoding.GetEncoding("shift_jis").GetString(test.connect_codes[highPortIndex].bytes);

                if( se.p1ConnectCode[0] == '\0' || se.p2ConnectCode[0] == '\0')
                {
                    se.p1ConnectCode = null;
                    se.p2ConnectCode = null;
                }

                se.p1String = ShortNames[(int)se.p1];
                se.p2String = ShortNames[(int)se.p2];

                if( se.p1String == se.p2String )
                {
                    string p1Color = GetCharacterColorStr(se.p1, test.starting_character_colours[lowPortIndex].color);
                    string p2Color = GetCharacterColorStr(se.p2, test.starting_character_colours[highPortIndex].color);

                    if( p1Color != "" )
                    {
                        se.p1String = se.p1String + "(" + p1Color + ")";
                    }

                    if (p2Color != "")
                    {
                        se.p2String = se.p2String + "(" + p2Color + ")";
                    }
                }


                se.stage = Stage.FinalDestination;
                // Add seconds to epoch
                //DateTime dateTime = epoch.AddTicks((long)test.start_time);//.ToLocalTime();
                TimeFields tm = new TimeFields(test.start_time);
                se.dateAndTime = tm.ToString();//tm.ToString();//test.timer.ToString();//dateTime.ToString();//dateTime.ToString();

                

                //se.dateAndTime = se.p1ConnectCode + ", " + se.p2ConnectCode;
                //se.dateAndTime = "test";//$"Date: {dateTime:yyyy-MM-dd HH:mm:ss}";

                Program.free_info(gameInfoPtr);

                if( se.dateAndTime == "Invalid Date" || se.dateAndTime == "INVALID" )
                {
                    //file metadata is corrupted
                    return;
                }


                if (populateCts.IsCancellationRequested)
                    return;

               

                //this.Invoke(() => entries.Add(se));
                this.Invoke((MethodInvoker)(() => allEntries.Add(se)));
                this.Invoke((MethodInvoker)(() => filteredEntries.Add(se)));
                this.Invoke(new Action(() =>
                {
                    int totalClipsOverall = 0;
                    foreach (SlippiEntry entry in filteredEntries)
                    {
                        if (entry != null)
                        {
                            totalClipsOverall += entry.numClipsP1;
                            totalClipsOverall += entry.numClipsP2;
                        }
                    }


                    numDisplayedFilesLabel.Text = "Displayed files: " + filteredEntries.Count.ToString() 
                    + "     Total save states found: " + totalClipsOverall;
                }));
            }
            catch( Exception e )
            {
                MessageBox.Show("here: " + e);
                //some slippi files are corrupted or badly written (raw tag has 0 size)
                //might be fixed in a future slp-parser update
            }
            finally
            {
                Marshal.FreeHGlobal(namePtr);
            }
        }

        private int[] CheckForClips( string path, int pIndex )
        {
            byte[] utf8 = Encoding.UTF8.GetBytes(path + "\0");
            IntPtr namePtr = Marshal.AllocHGlobal(utf8.Length);
            Marshal.Copy(utf8, 0, namePtr, utf8.Length);

            try
            {
                int selectedButtonComboIndex = -1;

                if (saveStateButtonComboBox.InvokeRequired)
                {
                    saveStateButtonComboBox.Invoke(new MethodInvoker(() =>
                    {
                        selectedButtonComboIndex = saveStateButtonComboBox.SelectedIndex;
                    }));
                }
                else
                {
                    selectedButtonComboIndex = saveStateButtonComboBox.SelectedIndex;
                }


                UIntPtr len;
                IntPtr arrayPtr = Program.check_clips(namePtr, out len, pIndex, selectedButtonComboIndex);//saveStateButtonComboBox.SelectedIndex);
                int[] result = new int[(int)len];

                if (arrayPtr != IntPtr.Zero)
                {
                    Marshal.Copy(arrayPtr, result, 0, (int)len);
                    Program.free_clip_array(arrayPtr, len);
                }

                return result;
            }
            catch( Exception e )
            {
                MessageBox.Show("here: " + e);
            }
            finally
            {
                Marshal.FreeHGlobal(namePtr);
            }

            return null;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new CommonOpenFileDialog
                {
                    IsFolderPicker = true,
                    Title = "Select a save state export folder"
                };

                if (exportFolderPath != null)
                {
                    dialog.InitialDirectory = exportFolderPath;
                }

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string selectedPath = dialog.FileName;
                    exportFolderPath = selectedPath;

                    openExportButton.Show();

                    exportLabel.Text = "Export Folder: " + exportFolderPath;
                    exportPathToolTip.SetToolTip(exportLabel, exportFolderPath);
                    WriteSettings();
                    //textBoxFolder.Text = selectedPath;
                }
                else
                {

                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Folder not found: {ex.FileName}");
            }
        }

        //private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        //{
        //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

        //    if (files.Length > 0)
        //    {
        //        if( slpPaths == null )c
        //        {
        //            slpPaths = files;
        //        }
        //        else
        //        {
        //            slpPaths = slpPaths.Union(files).ToArray();
        //        }
                

        //        SetAllInfo();
        //    }
        //}

        //private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //        e.Effect = DragDropEffects.Copy;
        //    else
        //        e.Effect = DragDropEffects.None;
        //}

        private void AddGameInfoColumn(string headerStr, string propertyName, int width, bool readOnly)
        {
            var col = new DataGridViewTextBoxColumn();
            col.HeaderText = headerStr;
            col.DataPropertyName = propertyName; // Must match property in your class
            col.Width = width;
            col.ReadOnly = readOnly;
            if( propertyName == "gameName")
            {
                col.MaxInputLength = 20;
            }
            dataGridView1.Columns.Add(col);
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Select a replay folder"
            };

            try
            {
                if (importFolderPath != null)
                {
                    dialog.InitialDirectory = importFolderPath;
                }

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string selectedPath = dialog.FileName;
                    importFolderPath = selectedPath;
                    importLabel.Text = "Replay Folder: " + selectedPath;

                    openImportButton.Show();

                    importPathToolTip.SetToolTip(importLabel, importFolderPath);

                    WriteSettings();

                    allEntries.Clear();
                    filteredEntries.Clear();

                    UpdateGrid();
                    //PopulateGridFromInputFolder();
                    //textBoxFolder.Text = selectedPath;
                }
                else
                {

                }
            }
            catch( FileNotFoundException ex )
            {
                MessageBox.Show($"Folder not found: {ex.FileName}");
            }
        }

        private void PopulateGridFromInputFolder(CancellationToken token)
        {
            try
            {
                //files = Directory.GetFiles(importFolderPath, "*.slp"); // Get all .txt files
                var files = Directory.GetFiles(importFolderPath, "*.slp")
                    .OrderByDescending(f => File.GetCreationTime(f))
                    .ToList();

               

                if (files.Count > 0)
                {
                    slpPaths = files.ToArray();

                    allEntries.Clear();
                    //filteredEntries.Clear();

                    this.Invoke((MethodInvoker)(() => dataGridView1.DataSource = filteredEntries));
                    

                    for( int i = 0; i < slpPaths.Length; ++i )
                    {
                        if (token.IsCancellationRequested)
                            return;

                        SetInfo(i);

                        
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Error: Folder not found.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
                //Console.WriteLine();
            }
        }

        private void WriteSettings()
        {
            int clipLength = 360;
            int foundLength = 0;
            if (int.TryParse(clipLengthTextBox.Text.Trim(), out foundLength))
            {
                clipLength = foundLength;
                // It's a valid integer — use it
                //Console.WriteLine($"User entered: {parsedValue}");
            }
            else
            {
                clipLengthTextBox.TextChanged -= clipLengthTextBox_TextChanged;

                clipLengthTextBox.Text = clipLength.ToString();

                clipLengthTextBox.TextChanged += clipLengthTextBox_TextChanged;
            }

            string imp = "NONE";
            if( importFolderPath != null )
            {
                imp = importFolderPath;
            }

            string outp = "NONE";
            if( exportFolderPath != null )
            {
                outp = exportFolderPath;
            }


            string text = outp + "\n" + imp + "\n" + saveStateButtonComboBox.SelectedIndex.ToString() + "\n" +  namingStyleBox.SelectedIndex.ToString() + "\n" + clipLength.ToString();
            File.WriteAllText("config.txt", text);
        }

        private void ReadSettings()
        {
            if (File.Exists("config.txt"))
            {
                var configLines = File.ReadAllLines("config.txt"); // loads all lines

                if (configLines.Length < 5)
                {
                   // Console.WriteLine("Error: The config.txt file must contain at least two lines.");
                    // You can also throw an exception or handle this differently
                    return;
                }

                if( configLines[0] != "NONE")
                {
                    exportFolderPath = configLines[0];
                }
                

                if( configLines[1] != "NONE")
                {
                    importFolderPath = configLines[1];
                }


                int buttonInputType = 0;
                if (int.TryParse(configLines[2], out buttonInputType))
                {
                }
                else
                {
                    buttonInputType = 0;
                }

                saveStateButtonComboBox.SelectedIndexChanged -= saveStateButtonComboBox_SelectedIndexChanged;

                saveStateButtonComboBox.SelectedIndex = buttonInputType;

                saveStateButtonComboBox.SelectedIndexChanged += saveStateButtonComboBox_SelectedIndexChanged;


                int namingStyleIndex = 0;
                if (int.TryParse(configLines[3], out namingStyleIndex))
                {
                }
                else
                {
                    namingStyleIndex = 0;
                }

                namingStyleBox.SelectedIndexChanged -= frameComboBox_SelectedIndexChanged;

                namingStyleBox.SelectedIndex = namingStyleIndex;

                namingStyleBox.SelectedIndexChanged += frameComboBox_SelectedIndexChanged;

                int clipLength = 360;
                if (int.TryParse(configLines[4], out clipLength))
                {
                }
                else
                {
                    clipLength = 360;
                }

                clipLengthTextBox.TextChanged -= clipLengthTextBox_TextChanged;
                clipLengthTextBox.Text = clipLength.ToString();
                clipLengthTextBox.TextChanged += clipLengthTextBox_TextChanged;


                

                exportLabel.Text = "Export Folder: " + exportFolderPath;
                exportPathToolTip.SetToolTip(exportLabel, exportFolderPath);

                importLabel.Text = "Replay Folder: " + importFolderPath;
                importPathToolTip.SetToolTip(importLabel, importFolderPath);
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (importFolderPath != null)
            {
                //Refresh();
                //Application.DoEvents();

                

                dataGridView1.DataSource = filteredEntries;


                await Task.Yield();              // Yield back to UI thread to finish painting
                await Task.Delay(250);

                

                UpdateGrid();

                dataGridView1.Focus();
                //await Task.Run(() => PopulateGridFromInputFolder());
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var row = dataGridView1.Rows[e.RowIndex];
            var entry = row.DataBoundItem as SlippiEntry;

            if (entry == null)
                return;

            // Only rename if FileName column was edited
            if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "gameName")
            {
                string oldPath = entry.filePath;
                string newFileName = entry.gameName + ".slp"; // updated name from the edited cell
                string newPath = Path.Combine(Path.GetDirectoryName(oldPath), newFileName);

                try
                {
                    if (File.Exists(oldPath) && !File.Exists(newPath))
                    {
                        File.Move(oldPath, newPath);
                        entry.filePath = newPath; // update path after rename
                    }
                    else
                    {
                        MessageBox.Show("Rename failed: File already exists or original file missing.");
                        entry.gameName = originalGameName;
                        // Optionally revert the value here
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error renaming file: {ex.Message}");
                }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "gameName")
            {
                var entry = dataGridView1.Rows[e.RowIndex].DataBoundItem as SlippiEntry;
                if (entry != null)
                    originalGameName = entry.gameName;
            }
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            string filter = searchTextBox.Text.Trim().ToLower();

            filteredEntries.Clear();

            foreach (var entry in allEntries)
            {
                if (entry.gameName.ToLower().Contains(filter))
                {
                    filteredEntries.Add(entry);
                }
            }
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            if (importFolderPath == null)
                return;

            if (populateCts != null)
            {
                populateCts?.Cancel();
            }

            await Task.Yield();              // Yield back to UI thread to finish painting
            await Task.Delay(200);

            UpdateGrid();



            // UpdateGrid();
        }

        private async void UpdateGrid()
        {
            if (populateCts != null)
            {
                populateCts?.Cancel();
            }

            populateCts = new CancellationTokenSource();

            try
            {
                allEntries.Clear();
                filteredEntries.Clear();
                await Task.Run(() => PopulateGridFromInputFolder(populateCts.Token));
            }
            catch (OperationCanceledException)
            {
                // Optional: handle cancellation (e.g., log or ignore)
            }
            //await Task.Run(() => PopulateGridFromInputFolder(populateCts.Token));
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0 && hit.RowIndex < dataGridView1.Rows.Count)
                {
                    if (!dataGridView1.Rows[hit.RowIndex].Selected)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[hit.RowIndex].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[hit.RowIndex].Cells[0];
                    }
                }

                ContextMenuStrip menu = new ContextMenuStrip();

                int totalClipsP1 = 0;
                int totalClipsP2 = 0;

                Dictionary<string, int> connectCodeCounts = new Dictionary<string, int>();

               
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    SlippiEntry entry = row.DataBoundItem as SlippiEntry;
                    if (entry != null)
                    {
                        if (entry.numClipsP1 > 0 && entry.p1ConnectCode != null)
                        {
                            if (connectCodeCounts.ContainsKey(entry.p1ConnectCode))
                            {
                                if (entry.numClipsP1 > 0)
                                {
                                    connectCodeCounts[entry.p1ConnectCode] += 1;
                                }
                            }
                            else
                            {
                                if (entry.numClipsP1 > 0)
                                {
                                    connectCodeCounts[entry.p1ConnectCode] = 1;
                                }
                            }
                        }

                        if (entry.numClipsP2 > 0 && entry.p2ConnectCode != null)
                        {
                            if (connectCodeCounts.ContainsKey(entry.p2ConnectCode))
                            {
                                if (entry.numClipsP2 > 0)
                                {
                                    connectCodeCounts[entry.p2ConnectCode] += 1;
                                }
                            }
                            else
                            {
                                if (entry.numClipsP2 > 0)
                                {
                                    connectCodeCounts[entry.p2ConnectCode] = entry.numClipsP2;
                                }
                            }
                        }


                        totalClipsP1 += entry.numClipsP1;
                        totalClipsP2 += entry.numClipsP2;
                    }
                }
                

                var sortedConnectCodeClipNums = connectCodeCounts.OrderByDescending(pair => pair.Value).Take(3).ToList();

                if( dataGridView1.SelectedRows.Count > 1 )
                {
                    foreach (var pair in sortedConnectCodeClipNums)
                    {
                        ToolStripMenuItem exportCC = new ToolStripMenuItem("Export " + pair.Value + " States from " + pair.Key);
                        exportCC.Click += (s, ev) => ExportFromConnectCode(pair.Key);
                        menu.Items.Add(exportCC);
                    }
                }

                if ( totalClipsP1 > 0)
                {
                    ToolStripMenuItem exportP1 = new ToolStripMenuItem("Export " + totalClipsP1 + " States from Player 1");
                    exportP1.Click += (s, ev) => ExportP1();
                    menu.Items.Add(exportP1);
                }
                
                if( totalClipsP2 > 0 )
                {
                    ToolStripMenuItem exportP2 = new ToolStripMenuItem("Export " + totalClipsP2 + " States from Player 2");
                    exportP2.Click += (s, ev) => ExportP2();
                    menu.Items.Add(exportP2);
                }

                if(dataGridView1.SelectedRows.Count == 1 )
                {
                    

                    string path = null;
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        SlippiEntry entry = row.DataBoundItem as SlippiEntry;
                        if (entry != null)
                            path = entry.filePath;

                    }

                    if( path != null )
                    {
                        ToolStripMenuItem openReplay = new ToolStripMenuItem("Open Slippi Replay");
                        openReplay.Click += (s, ev) => OpenReplay(path);
                        menu.Items.Add(openReplay);

                        ToolStripMenuItem goFile = new ToolStripMenuItem("Open in Explorer");
                        goFile.Click += (s, ev) => GoToFile(path);
                        menu.Items.Add(goFile);
                    }
                }
                

                // Show the menu
                menu.Show(dataGridView1, new Point(e.X, e.Y));

                
            }
        }

        private void ExportP1()
        {
            ExportFromPlayer(0);
        }

        private void ExportP2()
        {
            ExportFromPlayer(1);
        }

        private void ExportFromConnectCode( string connectCode )
        {
            if (exportFolderPath == null)
            {
                MessageBox.Show("export path was empty");
                return;
            }

            try
            {
                string exporTest = Path.GetFullPath(exportFolderPath);
            }
            catch (Exception)
            {
                MessageBox.Show("export path was invalid");
            }


            int clipLength = 360;
            int foundLength = 0;

            if (int.TryParse(clipLengthTextBox.Text.Trim(), out foundLength))
            {
                clipLength = foundLength;
                // It's a valid integer — use it
                //Console.WriteLine($"User entered: {parsedValue}");
            }
            else
            {
                MessageBox.Show("clip length was not valid. Setting to default");
                // Invalid input — optionally do nothing or handle it
                // Console.WriteLine("Invalid number, ignoring input.");

                clipLengthTextBox.TextChanged -= clipLengthTextBox_TextChanged;
                clipLengthTextBox.Text = clipLength.ToString();

                clipLengthTextBox.TextChanged += clipLengthTextBox_TextChanged;
            }

            if (exportFolderPath == null)
            {
                MessageBox.Show("export path not set");
                return;
            }

            int totalExportedClips = 0;
            int currPIndex = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                SlippiEntry entry = row.DataBoundItem as SlippiEntry;
                if (entry != null )
                {
                    if( entry.p1ConnectCode == connectCode )
                    {
                        totalExportedClips += entry.numClipsP1;
                        currPIndex = 0;
                    }
                    else if( entry.p2ConnectCode == connectCode )
                    {
                        totalExportedClips += entry.numClipsP2;
                        currPIndex = 1;
                    }

                    CreateSaveStates(exportFolderPath, entry.filePath, entry.gameName, clipLength, currPIndex);
                }
            }

            MessageBox.Show("Created " + totalExportedClips + " save states in " + exportFolderPath);
        }

        private void GoToFile(string filePath)
        {
            Process.Start("explorer.exe", $"/select,\"{filePath}\"");
        }

        private void OpenReplay( string filePath )
        {
            Process.Start("explorer.exe", filePath);
        }

        private void exportMenuStrip_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               // int rowIndex = dataGridView1.SelectedRows[0].Index;
                // Remove from underlying data source if bound
                // Example: entries.RemoveAt(rowIndex);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int totalClipsP1 = 0;
            int totalClipsP2 = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                SlippiEntry entry = row.DataBoundItem as SlippiEntry;
                if (entry != null)
                {
                    totalClipsP1 += entry.numClipsP1;
                    totalClipsP2 += entry.numClipsP2;
                }
            }

           // totalClipsLabel.Text = "Selected P1 Save States: " + totalClipsP1 + "           Selected P2 Save States: " + totalClipsP2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HowToForm popup = new HowToForm();
            popup.ShowDialog();
        }

        private void frameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WriteSettings();
        }

        private void clipLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            if (clipLengthTextBox.Text == "")
                return;

            int foundLength = 0;
            if (int.TryParse(clipLengthTextBox.Text.Trim(), out foundLength))
            {
            }
            else
            {
                return;
            }

            WriteSettings();
            //WriteSettings();
        }

        private void clipLengthTextBox_Leave(object sender, EventArgs e)
        {
            //WriteSettings();
        }

        private string GetCharacterColorStr(Character charIndex, int colorIndex)
        {
            switch (charIndex)
            {
                case Character.Mario:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Ylw";
                        case 2:
                            return "Blk";
                        case 3:
                            return "Blue";
                        case 4:
                            return "Grn";
                    }
                    break;
                case Character.Fox:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                    }
                    break;
                case Character.CaptainFalcon:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Blk";
                        case 2:
                            return "Red";
                        case 3:
                            return "Wht";
                        case 4:
                            return "Grn";
                        case 5:
                            return "Blue";
                    }
                    // Handle CaptainFalcon
                    break;
                case Character.DonkeyKong:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Blk";
                        case 2:
                            return "Red";
                        case 3:
                            return "Blue";
                        case 4:
                            return "Grn";
                    }
                    break;
                case Character.Kirby:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Ylw";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Red";
                        case 4:
                            return "Grn";
                        case 5:
                            return "Wht";
                    }
                    break;
                case Character.Bowser:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Blk";
                    }
                    break;
                case Character.Link:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Blk";
                        case 4:
                            return "Wht";
                    }
                    break;
                case Character.Sheik:
                case Character.Zelda:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                        case 4:
                            return "Wht";
                    }
                    break;
                case Character.Ness:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Ylw";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                    }
                    break;
                case Character.Peach:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Ylw";
                        case 2:
                            return "Wht";
                        case 3:
                            return "Blue";
                        case 4:
                            return "Grn";
                    }
                    break;
                case Character.Popo:
                case Character.Nana:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Grn";
                        case 2:
                            return "Orng";
                        case 3:
                            return "Red";
                    }
                    break;
                case Character.Pikachu:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                    }
                    break;
                case Character.Samus:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Pink";
                        case 2:
                            return "Blk";
                        case 3:
                            return "Grn";
                        case 4:
                            return "Blue";
                    }
                    break;
                case Character.Yoshi:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Ylw";
                        case 4:
                            return "Pink";
                        case 5:
                            return "Aqua";
                    }
                    break;
                case Character.Jigglypuff:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                        case 4:
                            return "Ylw";
                    }
                    break;
                case Character.Mewtwo:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                    }
                    break;
                case Character.Luigi:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Wht";
                        case 2:
                            return "Aqua";
                        case 3:
                            return "Pink";
                    }
                    break;
                case Character.Marth:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Grn";
                        case 3:
                            return "Blk";
                        case 4:
                            return "Wht";
                    }
                    break;
                case Character.YoungLink:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Wht";
                        case 4:
                            return "Blk";
                    }
                    break;
                case Character.DrMario:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                        case 4:
                            return "Blk";
                    }
                    break;
                case Character.Falco:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                    }
                    break;
                case Character.Pichu:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                    }
                    break;
                case Character.MrGameAndWatch:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Grey";
                        case 2:
                            return "Red";
                        case 3:
                            return "Wht";
                        case 4:
                            return "Grn";
                        case 5:
                            return "Blue";
                    }
                    break;
                case Character.Ganondorf:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                        case 4:
                            return "Purp";
                    }
                    break;
                case Character.Roy:
                    switch (colorIndex)
                    {
                        case 0:
                            return "";
                        case 1:
                            return "Red";
                        case 2:
                            return "Blue";
                        case 3:
                            return "Grn";
                        case 4:
                            return "Ylw";
                    }
                    break;
                default:
                    return "";
            }

            return "";
        }

        private void openImportButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", importFolderPath);
        }

        private void openExportButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", exportFolderPath);
        }

        private async void saveStateButtonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WriteSettings();

            if (importFolderPath == null)
                return;

            if (populateCts != null)
            {
                populateCts?.Cancel();
            }

            await Task.Yield();              // Yield back to UI thread to finish painting
            await Task.Delay(200);

            UpdateGrid();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);


            for( int i = 0; i < files.Length; ++i )
            {
                if (File.Exists(files[i]))
                {
                   if( Path.GetExtension(files[i]).Equals(".slp", StringComparison.OrdinalIgnoreCase))
                   {
                        AddEntry(files[i]);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "https://discord.com/channels/1287883534182256661/1293634600920289430/1386156526649217065";

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Required to open URL in default browser
                });
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error opening URL: {ex.Message}");
                MessageBox.Show("URL was invalid");
            }

            
        }

        private void aitchPatreonLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://www.patreon.com/c/rwing_aitch/about";

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Required to open URL in default browser
                });
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error opening URL: {ex.Message}");
                MessageBox.Show("URL was invalid");
            }
        }
    }


}

public class SlippiEntry
{
    public string filePath { get; set; }
    public string gameName { get; set; }
    public Character p1 { get; set; }
    public Character p2 { get; set; }
    public string p1String { get; set; }
    public string p2String { get; set; }
    public Stage stage { get; set; }
    public string stageString { get; set; }
    public int numClipsP1 { get; set; }
    public int numClipsP2 { get; set; }
    public string dateAndTime { get; set; }
    public string duration { get; set; }
    public int[] clipFramesP1 { get; set; }
    public int[] clipFramesP2 { get; set; }
    public string p1ConnectCode { get; set; }
    public string p2ConnectCode { get; set; }
}

struct TimeFields
{
    public UInt16 year;
    public byte month;
    public byte day;
    public byte hour;
    public byte minute;
    public byte second;

    public override string ToString()
    {
        try
        {
            if (year < 1 || year > 9999 || month < 1 || month > 12 || day < 1 || day > 31 || hour < 0 || hour > 23)
            {
                return "INVALID";
            }

            var utc = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
            var local = utc.ToLocalTime();
            //return local.ToString("MMMM d, yyyy  -  h:mm tt");
            //return local.ToString("MM / dd / yy - h:mm tt");//  -  h:mm tt");
            return local.ToString("yyyy-MM-dd - h:mm tt");//  -  h:mm tt");
           
        }
        catch (ArgumentOutOfRangeException)
        {
            return "Invalid Date";
        }

        //var utc = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
        //var local = utc.ToLocalTime();
        //return local.ToString("MMMM d, yyyy  -  h:mm tt");
    }
    //new DateTime(year, month, day, hour, minute, second)
     //   .ToString("MMMM d, yyyy  -  h:mm tt");
    public TimeFields( UInt64 t )
    {
        year = (ushort)(t >> 48);
        month = (byte)((t >> 40) & 0xFF);
        day = (byte)((t >> 32) & 0xFF);
        hour = (byte)((t >> 24) & 0xFF);
        minute = (byte)((t >> 16) & 0xFF);
        second = (byte)((t >> 8) & 0xFF);
    }
}
