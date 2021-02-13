using AccountManagement.FormComm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.FileOperate
{
    public  class CsvFileImpl 
    {
        public List<object> loadCsvFile(string fileName)
        {
            int iRet = -1;
            if (fileName == null)
            {

                CommLog.loger.Error("File name is null!");
                return null;
            }
            else if (!File.Exists(fileName))
            {
                CommLog.loger.Error("File is not exist!");
                return null;
            }

            StreamReader sr = new StreamReader(fileName, Encoding.UTF8);

            object newLine = new object();
            List<object> tableList = new List<object>();

            int lineNumb = 0;
            string fileDataLine;
            while (true)
            {
                lineNumb++;
                fileDataLine = sr.ReadLine();
                if (fileDataLine == null)
                {
                    break;
                }
                if (fileDataLine != "")
                {
                    if (!fileDataLine.StartsWith("#"))
                    {
                        string[] strArray = fileDataLine.Split(',');
                      //  iRet = addNewDataLine(strArray, ref newLine);
                        newLine = addNewDataLine(fileDataLine);
                        if (newLine == null)
                        {
                            CommLog.loger.Error(string.Format("Csv file ({0} ,line ({1})) data is not currect.", fileName, lineNumb));
                            tableList = null;
                            break;

                        }
                        tableList.Add(newLine);
                    }
                }

            }
            sr.Close();
            return tableList;

        }

        //public virtual T addNewDataLine<T>(string[] strArray, ref T table) 
        //{

        //    //return t.GetType().GetMethod("addNewDataLine")(strArray,ref table);
        //    return null;
        //}

        public virtual object addNewDataLine(string strArray)
        {

            //return t.GetType().GetMethod("addNewDataLine")(strArray,ref table);
            return null;
        }

    }
}
