using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Service
{
    interface IServiceAble<T>
    {
        List<T> findAllData();
        T findDataById(string idName, long id);
        List<T> findDataByKeyWord(string columnName, string keyWord);

        int updateData(T table, string idName, long id);

        int saveData(List<T> tableList);

        int deleteData(string tableName, string idName, long id);

    }
}
