using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        List<LLButton> buttonsToWrite;

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.DefaultExt = ".txt";
            openDlg.Filter = "Tab separated variable|*.txt";
            openDlg.Title = "List of values";
            buttonsToWrite = new List<LLButton>();
            int buttonCount = 0;
            if (openDlg.ShowDialog() == true)
            {
                using (MemoryStream memStream = new MemoryStream(File.ReadAllBytes(openDlg.FileName)))
                using (StreamReader sr = new StreamReader(memStream))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            buttonsToWrite.Add(lineToButton(sr.ReadLine(), buttonCount++));
                        }
                        catch (Exception ex)
                        {
                            string msg = Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
                            msg += Environment.NewLine + "Exception caught, continuing";
                            txtOutput.AppendText(msg);
                        }
                    }
                }
                txtOutput.AppendText("Done parsing input");
            }
        }

        const int btnsPerRow = 3;
        private  LLButton lineToButton(string line,int lineN)
        {
            try
            {
                string[] parts = line.Split('\t');
               // string[] rangeParts = parts[1].Split('-');
                uint min = uint.Parse(parts[0]);
                uint max = uint.Parse(parts[1]);
                bool rowStart = lineN % btnsPerRow == 0;
                return new LLButton(parts[2].Trim(), min, max, rowStart,chkShorten.IsChecked.Value );
            }
            catch (Exception ex)
            {
                string msg = Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
                msg += Environment.NewLine + "Exception caught in line to button, continuing";
                txtOutput.AppendText(msg);
                return null;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer buttonSerializer = new XmlSerializer(typeof(List<LLButton>));

            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.Filter = "xml|*.xml";
            dlgSave.Title = "target";
            if (dlgSave.ShowDialog() == true)
            {
                TextWriter writer = new StreamWriter(dlgSave.FileName);
                try
                {
                    //foreach(LLButton btn in buttonsToWrite)
                    buttonSerializer.Serialize(writer, buttonsToWrite);
                }
                finally
                {
                    writer.Close();
                }
                txtOutput.AppendText("Done writing");
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
