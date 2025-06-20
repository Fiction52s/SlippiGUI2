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

[StructLayout(LayoutKind.Sequential)]
public struct Time
{
    public UInt32 one;
    public UInt32 two;
    //public int hours;
    //public int minutes;
    //public int seconds;
    // Update to match actual Rust Time struct
}

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

[StructLayout(LayoutKind.Sequential, Pack = 1)]
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

    public Time start_time;

    public uint timer;
    public int duration;

    public byte version_major;
    public byte version_minor;
    public byte version_patch;
}

namespace SlippiGUI
{
    public partial class Form1 : Form
    {
        private string[] slpPaths;
        private string exportFolderPath;
        private BindingList<SlippiEntry> entries;
        ToolTip exportPathToolTip;
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("Player 1");
            comboBox1.Items.Add("Player 2");
            //comboBox1.SelectedIndex = 0;
            //comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

            comboBox1.SelectedIndex = 0;  // Set default without triggering event

            label3.Text = "no export path set";

            textBox1.Text = 360.ToString();

            entries = new BindingList<SlippiEntry>();

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Clear();

            exportPathToolTip = new ToolTip();
            exportPathToolTip.SetToolTip(label3, "no export path set");
            //    public string path { get; set; }
            //public string gameName { get; set; }
            //public Character p1 { get; set; }
            //public Character p2 { get; set; }
            //public Stage stage { get; set; }
            //public string dateAndTime { get; set}
            //public string matchLength { get; set}

            // Define column for 'name'

            AddGameInfoColumn("File Path", "filePath", 200, true);
            AddGameInfoColumn("Save State Name", "gameName", 200, false);
            AddGameInfoColumn("Player 1", "p1", 100, true);
            AddGameInfoColumn("Player 2", "p2", 100, true);
            AddGameInfoColumn("Stage", "stage", 100, true);
            AddGameInfoColumn("P1 Save State Count", "numClipsP1", 50, true);
            AddGameInfoColumn("P2 Save State Count", "numClipsP2", 50, true);

            totalClipsLabel.Text = "Total P1 Save States: " + 0 + "        Total P2 Save States: " + 0;//"Total save states: " + 0;

            label3.AutoSize = false;
            label3.AutoEllipsis = true;
            label3.Width = 200;

            if (File.Exists("config.txt"))
            {
                exportFolderPath = File.ReadAllText("config.txt");
                label3.Text = exportFolderPath;
                exportPathToolTip.SetToolTip(label3, exportFolderPath);
            }

            //AddGameInfoColumn("Date and Time", "dateAndTime", 100, true);
            //AddGameInfoColumn("Match Length", "matchLength", 100, true);

            // public UInt32 clipFrame { get; set; }
            //public UInt32 clipLength { get; set; }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //CheckForAllClips();

            int clipLength = 360;
            int foundLength = 0;

            if (int.TryParse(textBox1.Text.Trim(), out foundLength))
            {
                clipLength = foundLength;
                // It's a valid integer — use it
                //Console.WriteLine($"User entered: {parsedValue}");
            }
            else
            {
                // Invalid input — optionally do nothing or handle it
                // Console.WriteLine("Invalid number, ignoring input.");
                textBox1.Text = clipLength.ToString();
            }

            if( exportFolderPath == null )
            {
                MessageBox.Show("export path not set");
                return;
            }

            int totalExportedClips = 0;
            foreach( var item in entries )
            {
                if( comboBox1.SelectedIndex == 0 )
                {
                    totalExportedClips += item.numClipsP1;
                }
                else
                {
                    totalExportedClips += item.numClipsP2;
                }
                CreateSavestates(exportFolderPath, item.filePath, item.gameName, clipLength);
            }

            MessageBox.Show("Created " + totalExportedClips + " save states in " + exportFolderPath);
            // Remove invalid file name characters

            //later make sure the gameName is safe before validating

            // Or use Regex:
            //string safeName = string.Join("_", textBox2.Text.Split(Path.GetInvalidFileNameChars()));
            //safeName = Regex.Replace(safeName, @"[<>:""/\\|?*]", "_");

            //// Optional: Fallback if name is empty after cleaning
            //if (string.IsNullOrWhiteSpace(safeName))
            //{
            //    MessageBox.Show("game name is not valid");
            //    return;
            //}
    }

        private void CreateSavestates(string exportFolder, string currSlp, string gameName, int clipLength)
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

            //Program.create_savestates(exportFolderPtr, slpPathPtr, gameNamePtr, comboBox1.SelectedIndex, clipLength);
            Program.create_filtered_savestates(exportFolderPtr, slpPathPtr, gameNamePtr, comboBox1.SelectedIndex, clipLength);

            //MessageBox.Show("Created " + listBox1.Items.Count + " save states in " + exportFolder + " with game name " + gameName + " with clip length " + clipLength);
        }

        private void SetAllInfo()
        {
            if (entries == null)
                return;

            entries.Clear();
            for( int i = 0; i < slpPaths.Length; ++i )
            {
                AddEntry(slpPaths[i]);
            }

            int totalClipsP1 = 0;
            int totalClipsP2 = 0;
            foreach (var item in entries)
            {
                totalClipsP1 += item.numClipsP1;
                totalClipsP2 += item.numClipsP2;
            }

            totalClipsLabel.Text = "Total P1 Save States: " + totalClipsP1 + "        Total P2 Save States: " + totalClipsP2;

            dataGridView1.DataSource = entries;

            //entries[0].name = "FF";
           // entries[0].p1 = Character.Falco;
            //entries.First().name = "F";
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
           

            //dataGridView1.Columns["name"].ReadOnly = false;
           // dataGridView1.Columns["p1"].ReadOnly = true;
           // dataGridView1.Columns["p2"].ReadOnly = true;
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

                SlippiEntry se = new SlippiEntry();
                se.filePath = path;
                se.gameName = Path.GetFileNameWithoutExtension(path);
                se.p1 = test.starting_character_colours[0].character;
                se.p2 = test.starting_character_colours[1].character;
                se.stage = test.stage;
                se.clipFramesP1 = CheckForClips(path, 0);
                se.clipFramesP2 = CheckForClips(path, 1);
                se.numClipsP1 = se.clipFramesP1.Length;
                se.numClipsP2 = se.clipFramesP2.Length;
               
                entries.Add(se);
            }
            finally
            {
                Marshal.FreeHGlobal(namePtr);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAllInfo();
            //CheckForAllClips();
        }

        private int[] CheckForClips( string path, int pIndex )
        {
            byte[] utf8 = Encoding.UTF8.GetBytes(path + "\0");
            IntPtr namePtr = Marshal.AllocHGlobal(utf8.Length);
            Marshal.Copy(utf8, 0, namePtr, utf8.Length);

            UIntPtr len;
            IntPtr arrayPtr = Program.check_clips(namePtr, out len, pIndex);
            int[] result = new int[(int)len];

            if (arrayPtr != IntPtr.Zero)
            {
                Marshal.Copy(arrayPtr, result, 0, (int)len);
                Program.free_clip_array(arrayPtr, len);
            }

            Marshal.FreeHGlobal(namePtr);

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Select a folder"
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string selectedPath = dialog.FileName;
                exportFolderPath = selectedPath;
                label3.Text = selectedPath;
                exportPathToolTip.SetToolTip(label3, exportFolderPath);
                File.WriteAllText("config.txt", exportFolderPath);
                //textBoxFolder.Text = selectedPath;
            }
            else
            {

            }

            //folderBrowserDialog1.Description = "Select a folder to export to";
            ////folderBrowserDialog1.UseDescriptionForTitle = true; // Optional, for newer Windows

            //if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    string selectedPath = folderBrowserDialog1.SelectedPath;
            //    //MessageBox.Show("Export folder: " + selectedPath);
            //    textBox2.Text = selectedPath;

            //    exportFolderPath = selectedPath;

            //    File.WriteAllText("config.txt", exportFolderPath);

            //    // Use selectedPath for your export logic
            //}
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                if( slpPaths == null )
                {
                    slpPaths = files;
                }
                else
                {
                    slpPaths = slpPaths.Union(files).ToArray();
                }
                

                SetAllInfo();

               // CheckForAllClips();

                //button1.Show();

                //listBox1.Show();
            }
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

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

        private void clearButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            entries.Clear();
            slpPaths = null;
        }
    }
}

public class SlippiEntry
{
    public string filePath { get; set; }
    public string gameName { get; set; }
    public Character p1 { get; set; }
    public Character p2 { get; set; }
    public Stage stage { get; set; }
    public int numClipsP1 { get; set; }
    public int numClipsP2 { get; set; }
    public string dateAndTime { get; set; }
    public string matchLength { get; set; }
    public int[] clipFramesP1 { get; set; }
    public int[] clipFramesP2 { get; set; }
}
