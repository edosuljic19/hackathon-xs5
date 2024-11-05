using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_recognizer_UI
{
    public class Class1
    {
        public static string image_file_path   = "";
        public static string image_file_name = @"\slika";
        public static string full_img_path = "";
        public static string txt_file_path = "";
        public static string txt_file_name = @"\rijeci.txt";
        public static string full_txt_path = "";
        public static string api_key = "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";

        public static int counter = 0;


        public static void merge_img_path() {

            Class1.full_img_path = Class1.image_file_path + Class1.image_file_name + Convert.ToString(Class1.counter) + ".jpg";


        }
  

    }
}
