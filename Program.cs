using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace SlippiGUI
{
   

    static class Program
    {
        [DllImport("slippi_analyzer.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void test_replay(IntPtr name, Int32 pChar1, Int32 pChar2 );

        //[DllImport("slippi_analyzer.dll")]
        [DllImport("slippi_analyzer.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr read_info(IntPtr namePtr);

        [DllImport("slippi_analyzer.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void free_info(IntPtr ptr);

        [DllImport("slippi_analyzer.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr check_clips(IntPtr namePtr, out UIntPtr length, Int32 hmnPlayer, Int32 saveButtonType );

        [DllImport("slippi_analyzer.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void free_clip_array(IntPtr ptr, UIntPtr length);

        [DllImport("slippi_analyzer.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void create_savestates(IntPtr exportPathPtr, IntPtr fileNamePtr, IntPtr gameNamePtr, Int32 hmnPlayer,
            Int32 clipLength, Int32 namingStyle, Int32 saveButtonType );

        [DllImport("slippi_analyzer.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void create_filtered_savestates(IntPtr exportPathPtr, IntPtr fileNamePtr, IntPtr gameNamePtr, Int32 hmnPlayer,
            Int32 clipLength);

        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
