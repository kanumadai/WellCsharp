using AccountManagement.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Service
{
    class ServiceImpl<T> : IServiceAble<T> where T : new()
    {
        private DbImpl<T> DbImpl = new Dao.DbImpl<T>();
        public int deleteData(string tableName, string idName, long id)
        {
            return DbImpl.deleteData(tableName, idName,id);
        }

        public List<T> findAllData()
        {
            return DbImpl.findAllData();
        }

        public T findDataById(string idName, long id)
        {
            return DbImpl.findDataById(idName,id);
        }


        public List<T> findDataByKeyWord(string columnName, string keyWord)
        {
            return DbImpl.findDataByKeyWord(columnName, keyWord);
        }

        public int saveData(List<T> tableList)
        {
            return DbImpl.saveData(tableList);
        }

        public int updateData(T table, string idName, long id)
        {
            return DbImpl.updateData(table,idName,id);
        }
    }
}
