using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderJaMa
{
    [Serializable]
    class Highscore
    {
        public static Highscore Instance;

        private static string path = "";

        public List<int> highscores;

        public Highscore()
        {
            if(Instance == null)
            {
                Instance = this;

                path = Environment.CurrentDirectory + "\\hs.bin";
                highscores = new List<int>();
                try
                {
                    ReadFromFile();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void AddToList(int value)
        {
            Highscore.Instance.highscores.Add(value);
            Highscore.Instance.highscores.Sort();
            Highscore.Instance.highscores.Reverse();
            WriteToFile();
        }

        public static void WriteToFile()
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, Highscore.Instance.highscores);
            fs.Close();
        }

        public static void ReadFromFile()
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            Highscore.Instance.highscores = (List<int>) bf.Deserialize(fs);
            fs.Close();
        }

    }
}
