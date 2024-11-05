using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace aplikacija
{
   
    public partial class Form1 : Form
    {
        string putanja;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             // Dobijanje teksta iz TextBox kontrole
            string tekstZaUpis = textBox1.Text;

            // Provera da li je unet tekst pre nego što se pokuša upis
            if (!string.IsNullOrEmpty(tekstZaUpis))
            {
                // Postavljanje putanje do datoteke
                string putanjaDoDatoteke = "C:/Users/LENOVO/moguce_rijeci.txt";

                // Upisivanje teksta u datoteku
                try
                {
                    File.WriteAllText(putanjaDoDatoteke, tekstZaUpis);
                    MessageBox.Show("Tekst je uspešno upisan u datoteku!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom upisa u datoteku: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Morate uneti tekst pre nego što ga upišete u datoteku.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // putanja = pictureBox1.ImageLocation;
            
               putanja = "C:/Users/LENOVO/Desktop/Paper_sheet.jpg";
            // Putanja do Python interpretera
            string pythonPath = "C:/Users/LENOVO/Desktop/MySQL Workbench 5.2.30 CE/python.exe";
            label2.Text = putanja;
            // Putanja do Python skripte
            string pythonScriptPath = "C:/Users/LENOVO/open.py";

            // Argument koji ćete proslediti Python skripti
            string argument = putanja;

            // Kreiranje procesa za pokretanje Python skripte
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = "\"{pythonScriptPath}\" \"{argument}\"",  // Postavljanje argumenata
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();

                // Čekanje da proces završi
                process.WaitForExit();

                // Čitanje izlaza iz Python skripte
                string output = process.StandardOutput.ReadToEnd();
                label1.Text = "Izlaz iz Python skripte: " + output;
               // Console.WriteLine("Izlaz iz Python skripte: {output}");
            }



        }
    }
}
