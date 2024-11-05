using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Data.OleDb;


namespace Object_recognizer_UI
{
    public partial class Form1 : Form
    {

        OleDbCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            Class1.counter += 1;
            Class1.merge_img_path();
            if (File.Exists(Class1.full_img_path) == true)
            {


                pictureBox1.ImageLocation = Class1.full_img_path;
                label1.Text = "";
                Form_settings form2 = new Form_settings();

                await Task.Delay(150);

                string pythonPath = "python";
                string scriptPath = "recognizer.py";

                string arguments = Class1.full_img_path + " rijeci.txt " + Class1.api_key;

                button1.BackColor = Color.FromArgb(123, 69, 161);

                string txt = StartPythonScript(pythonPath, scriptPath, arguments);

                button1.BackColor = Color.FromArgb(2, 30, 54);



                label1.Text = txt;

                string index = form2.CompareTextFromForm1(txt);
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write("#" + index + "/");
                }
                label2.Text = index;

            }

            else {

                MessageBox.Show("No more images");
                Class1.counter = 0;

            }
                  


        }





        



        static string StartPythonScript(string pythonPath, string scriptPath, string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = $"\"{scriptPath}\" {arguments}",  // Enclose script path in quotes
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true

            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();
                process.WaitForExit();

                // Read the output and error messages
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();


                
                Console.WriteLine("Output: " + output);
                Console.WriteLine("Error: " + error);
                return output;

            }
        }

        private void settings_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_settings form = new Form_settings();
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                this.Show();
            }
            else if (form.ShowDialog() == DialogResult.OK){
                this.Show();
            }
            else
            {
                this.Hide();
            }
            // Show the new form
        

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string putanjaDoDatoteke = "rijeci.txt";
            string tekstIzDatoteke =string.Empty;
            // Provera da li datoteka postoji
            if (File.Exists(putanjaDoDatoteke))
            {
                try
                {
                    // Čitanje teksta iz datoteke
                     tekstIzDatoteke = File.ReadAllText(putanjaDoDatoteke);

                    // Prikazivanje pročitanog teksta
                    
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Greška prilikom čitanja datoteke: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Datoteka ne postoji.");
            }

            if (tekstIzDatoteke == string.Empty)
            {
                button1.Enabled = false;
                btnOpenPort.Enabled = false;
            }
            else {
                button1.Enabled = true;
                btnOpenPort.Enabled = true;
            }

            
            try
            {
                string[] ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    cbPort.Items.Add(port);
                }
                // Selecting second port in list. First is usualy virtual COM1 port on PC
                //cbPort.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {

            try
            {

                serialPort1.PortName = cbPort.SelectedItem.ToString();

              
                if (serialPort1.IsOpen)
                {

                    serialPort1.Close();

                }
                else
                {

                    serialPort1.Open();
                    btnClosePort.Enabled = true;
                    btnOpenPort.Enabled = false;
                  
                    cbPort.Enabled = true;
             

                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            Form_settings form2 = new Form_settings();

            serialPort1.Write("@" + form2.counting().ToString() + "/");

        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            try
            {

                serialPort1.Close();
                btnClosePort.Enabled = false;
                btnOpenPort.Enabled = true;
                cbPort.Enabled = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Resize(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox4.Location = new System.Drawing.Point(10, this.ClientSize.Height - 45);
        }
    }

}
