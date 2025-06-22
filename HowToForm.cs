using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SlippiGUI
{
    public partial class HowToForm : Form
    {
        public HowToForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://docs.google.com/document/d/1x-JuWv5rK7K9ah-slT_b2_sdMfrM11q_gaO_dzhZjys/edit?usp=sharing";

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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://github.com/AlexanderHarrison/TrainingMode-CommunityEdition";

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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://docs.google.com/document/d/1A0Chln1Y0SZyPvfK46EjG-iDI7BzjjsGlG5mbmN2nu4/edit?usp=sharing";//"https://github.com/AlexanderHarrison/TrainingMode-CommunityEdition";

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
    }
}
