using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KPI.Parameter;
using KPI.Models;

namespace KPI.Class
{
   public class FileIO
    {
        public List<string> errorline = new List<string>();
        private string filename = Paths.Errorlogfile;

        public FileIO()
        {

        }

        public void CreateFile()
        {
            bool isFileExists = File.Exists(filename);
            if (isFileExists == false)
            {
                using (StreamWriter sw = new StreamWriter(filename)) { }

            }

        }

        private void AppendFile(DateTime dt, string userId, string errorcode, string functionname, string decription)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string str = string.Format("{0},{1},{2},{3},{4}", dt.ToString("yyyy-MM-dd HH:mm:ss"), userId, errorcode, filename, decription);

                    sw.WriteLine(str);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }

        public bool ReadFile()
        {
            bool isFileExists = File.Exists(filename);
            if (isFileExists)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        string allline = sr.ReadToEnd();
                        char[] charTrim = { '\n', '\r' };

                        string[] linearray = allline.Split('@');
                        foreach (var line in linearray)
                        {
                            string msg = line.Trim(charTrim);
                            errorline.Add(msg); // Console.WriteLine(line);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    FileIO.AppendErrorLogFile("0x0030", "FileIoClass:ReadTextFile", ex.Message);
                    MessageBox.Show(new Form() { TopMost = true }, "Error code = 0x0030 , Message : " + ex.ToString() + " (LOOP  FileIoClass:ReadTextFile)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string[] ReadFile(string filename)
        {
            string[] result = new string[]
            {
                "",""
            };
            bool isFileExists = File.Exists(filename);
            if (isFileExists)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        string allline = sr.ReadToEnd();
                     //   char[] charTrim = { '\n', '\r' };

                        string[] linearray = allline.Split(',');
                        //foreach (var line in linearray)
                        //{
                        //    string msg = line.Trim(charTrim);
                        //    errorline.Add(msg); // Console.WriteLine(line);
                        //}
                        result = linearray;
                    }
                    return result;
                }
                catch 
                {
                    return result;
                }
            }
            else
            {
                return result;
            }
        }




        public bool DeleteSpecifiedFile(string folder)
        {
            string[] filearray = Directory.GetFiles(folder);
            try
            {
                foreach (var file in filearray)
                {
                    File.Delete(file);
                }
                return true;
            }
            catch (Exception ex)
            {
                FileIO.AppendErrorLogFile("0x0031", "FileIoClass:DeleteSpecifiedFile", ex.Message);
                MessageBox.Show(new Form() { TopMost = true }, "Error code = 0x0031 , Message : " + ex.ToString() + " (LOOP FileIoClass DeleteLogFile)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

        }

        public void DeleteFile()
        {
            bool isFileExists = File.Exists(filename);
            if (isFileExists == true)
            {
                File.Delete(filename);
            }

        }

        public static void AppendErrorLogFile(string errorcode, string functionname, string decription)
        {
            string dtstr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string userId = User.ID;
            string filename = Paths.Errorlogfile;
            if (File.Exists(filename) == false)
            {
                using (StreamWriter sw = new StreamWriter(filename)) { }
            }

            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string str = string.Format("{0}|{1}|{2}|{3}|{4}@", dtstr, userId, errorcode, functionname, decription);

                    sw.WriteLine(str);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }



        }



        public static void WriteToCSV(List<ProductionSummay> mlrecordlist)
        {
            var str = new StringBuilder();
            string path = $"C:\\zdebug\\mlRecord{DateTime.Now:yyyy_MM_dd}-{DateTime.Now:HHmmss}.csv";
            str.Append("run,MachineId,CurrentDateTime,PartNumber,OKNG,MTTime,Unknow,UnknowNoHT,Speed,SpeedM,Idling,,MTminTime,HTminTime \r\n");
            foreach (var i in mlrecordlist)
            {
                str.AppendFormat($"{i.RegistDate},{i.Partnumber},{i.Qty}\r\n");
            }
            File.WriteAllText(path, str.ToString());

        }


    }


    //public class ReadFileModel
    //{
    //    public string  Data1 { get; set; }
    //}
}
