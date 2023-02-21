using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace dno_tes_2._1
{
    public class Note
    {
        public static string jsonpath = "C:/Program Files/DnoNotes/notedata.json";

        public string NoteName;
        public string NoteText;
        public DateTime NoteDate;

        public Note(string noteName, string noteText, DateTime noteDate)
        {
            NoteName = noteName;
            NoteText = noteText;
            NoteDate = noteDate;
        }

        static public void SaveToFile<T>(T list, string path)
        {
            if (!File.Exists(jsonpath + path))
            {
                FileStream fileStream = File.Create(jsonpath);
                fileStream.Dispose();
            }

            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(jsonpath, json);
        }

        static public List<T> ReadFromFile<T>(string path)
        {
            List<T> result;
            if (!File.Exists(jsonpath))
            {
                FileStream fileStream = File.Create(jsonpath);
                fileStream.Dispose();
            }

            string resultInfo = File.ReadAllText(jsonpath);
            result = JsonConvert.DeserializeObject<List<T>>(resultInfo);

            return result;
        }
    }
}
