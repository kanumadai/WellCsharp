using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Dao
{
    interface IDbInterfacedAble<T>
    {
        /// <summary>
        /// get all rows in the table T
        /// </summary>
        /// <returns></returns>
        List<T> findAllData();
        /// <summary>
        ///  get one row in the table T ,where column == id
        /// </summary>
        /// <param name="idName">id column name</param>
        /// <param name="id">id</param>
        /// <returns>T</returns>
        T findDataById(string idName,long id);
        /// <summary>
        /// get rows in the table T ,where columns contain keyword
        /// </summary>
        /// <param name="columnNames"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        List<T> findDataByKeyWord(string columnName, string keyWord);

        int updateData(T table,string idName, long id);

        int saveData(List<T> tableList);

        int deleteData(string tableName, string idName,long id);

    }
}
