using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.FileOperate
{

    public interface IFileOperateAble<T> where T : new()
    {
         List<T> loadCsvFile<T>(string fileName) where T : new();

        int addNewDataLine<T>(string[] strArray, ref T table);
    }
}