using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace Object_recognizer_UI
{
    public partial class Form_settings : Form
    {
        public Form_settings()
        {
            InitializeComponent();
        }

        int n = 0;

        private void Form_settings_Load(object sender, EventArgs e)  // Citanje predodredjenih ponudjenih rijeci
        {
                                                                     // Čitanje teksta iz datoteke
            string filePath = "rijeci.txt";
            string fileContent = File.ReadAllText(filePath);

                                                                     // Podjela teksta na rijeci (odvojene zarezom) i dodavanje u ListBox
            string[] words = fileContent.Split(',');
            foreach (string word in words)
            {
                listBox1.Items.Add(word.Trim());                     // Koristi Trim() za uklanjanje eventualnih praznih prostora oko reči
            }
            n = 0;

        }

   
        public int counting()                                       //  Odredjivanje broja artikala u listi
        {
            string filePath = "rijeci.txt";
            string fileContent = File.ReadAllText(filePath);
            string[] words = fileContent.Split(',');
            foreach (string word in words)
            {
                listBox1.Items.Add(word.Trim()); 
            }
            return listBox1.Items.Count;
        }
        public string CompareTextFromForm1(string textFromForm1)    //  Poredjenje generisanog odgovora sa ponudjenim odgovorima
        {
                                                                    // Čitanje teksta iz datoteke
            if (n == 0)
            {
                string filePath = "rijeci.txt";
                string fileContent = File.ReadAllText(filePath);

                string[] words = fileContent.Split(',');
                foreach (string word in words)
                {
                    listBox1.Items.Add(word.Trim());                // Koristi Trim() za uklanjanje eventualnih praznih prostora oko rijeci
                }
                                                                    // Upoređivanje teksta iz Forme 1 s elementima u ListBox-u
                n = 1;
            }
         
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string listBoxItem = listBox1.Items[i].ToString();
                              
                if (string.Compare(textFromForm1.Trim(), listBoxItem.Trim()) ==0)
                {
                    return ((i + 1).ToString());
                }
            }
           
            int x = 0;
            return x.ToString();

        }
        private void img_path_btn_Click(object sender, EventArgs e)             // Određivanje putanje datoteke sa slikama
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Class1.image_file_path = folderBrowserDialog1.SelectedPath;
                                                                                // Snimanje putanje u varijablu
                
                MessageBox.Show("Selected Folder: " + Class1.image_file_path,"Alert!",MessageBoxButtons.OK, MessageBoxIcon.Information);
   
            }
        }

        private void file_path_btn_Click(object sender, EventArgs e)            // Odredjivanje putanje fajla sa rijecima
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Class1.txt_file_path = folderBrowserDialog1.SelectedPath;
                                                                                   // Snimanje putanje u varijablu 
     
                MessageBox.Show("Selected Folder: " + Class1.txt_file_path);

                Class1.full_txt_path = Class1.txt_file_path + Class1.txt_file_name;
              }
        }


        private void button1_Click(object sender, EventArgs e)                  // Upisivanje nove ponudjene rijeci
        {
            
                listBox1.Items.Add(textBox1.Text);
            for (int i = 0; i < listBox1.Items.Count; i++) {
                if (listBox1.Items[i] == string.Empty) {
                    listBox1.Items.RemoveAt(i);
                }
            }
               
            SaveTextToFile(textBox1.Text+",", "rijeci.txt");                    // Snimanje rijeci u .txt fajl
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)                  // Brisanje svih ponudjenih rijeci
        {
            listBox1.Items.Clear();
            ClearFileContent("rijeci.txt");
        }
        private void SaveTextToFile(string text, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))       // Postavljanje 'true' za dodavanje teksta
            {
                writer.WriteLine(text);
            }
        }
        private static void ClearFileContent(string filePath)
        {
            // Koristi File.WriteAllText kako bi prebriao sadrzaj sa praznim stringom
            File.WriteAllText(filePath, string.Empty);
            // Alternativno, možete koristiti File.Create kako bi kreirali praznu datoteku
            // File.Create(filePath).Close();
        }

        private void button3_Click(object sender, EventArgs e)                  // Brisanje jedne odredjene rijeci
        {
            int index2 = listBox1.SelectedIndex;
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            DeleteLineFromFile("rijeci.txt", index2);
            RemoveEmptyLinesFromFile("rijeci.txt");
            
        }
        private static void DeleteLineFromFile(string filePath, int lineNumberToDelete)
        {
            // Čitanje svih linija iz datoteke
            string[] lines = File.ReadAllLines(filePath);

            // Provera da li je linija za brisanje u granicama datoteke
           
            // Brisanje linije
                lines[lineNumberToDelete] = string.Empty;

            // Upisivanje preostalih linija nazad u datoteku
               File.WriteAllLines(filePath, lines);
            
                
              
            
        }
        private static void RemoveEmptyLinesFromFile(string filePath)
        {
            // Čitanje svih linija iz datoteke
            string[] lines = File.ReadAllLines(filePath);

            // Filtriranje nepraznih linija pomoću LINQ izraza
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

            // Upisivanje nepraznih linija nazad u datoteku
            File.WriteAllLines(filePath, nonEmptyLines);
        }

        private void Form_settings_Resize(object sender, EventArgs e)
        {
            pictureBox4.Location = new System.Drawing.Point(10, this.ClientSize.Height - 45);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set properties for the OpenFileDialog
            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            // Show the OpenFileDialog and check if the user clicked OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                ClearFileContent("rijeci.txt");


                // Get the selected file's path
                string filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath);

                // Podela teksta na reči (odvojene zarezom) i dodavanje u ListBox
                string[] words = fileContent.Split(',');
                foreach (string word in words)
                {
                    listBox1.Items.Add(word.Trim()); // Koristi Trim() za uklanjanje eventualnih praznih prostora oko reči
                }
                 ClearFileContent("rijeci.txt");
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if(listBox1.Items[i].ToString().Trim().Length == 0){ continue; }
                    SaveTextToFile(listBox1.Items[i] + ",", "rijeci.txt");
                }


            }
            else
            {
                MessageBox.Show("User canceled the operation.");
            }
        }
    }
}