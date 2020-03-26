using Entities;
using System;
using System.IO;
using System.Linq;

namespace DataLayer
{
    public class Data
    {
        public static int GetId(string filePath)
        {
            string lastLine = File.ReadLines(filePath).LastOrDefault(x => x.Length > 0);
            int id = lastLine == null ? 0 : int.Parse(lastLine.Split(Constant.Delimiter)[0]);
            return id;
        }
        public static string CreateFile(string fileName)
        {
            string filePath = fileName + ".csv";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            return filePath;
        }
    }
}
