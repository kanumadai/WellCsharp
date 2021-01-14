using AccountManagement.DbUtils;
using AccountManagement.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Dao
{
    public class DbImpl<T>: IDbInterfacedAble<T> where T : new()
    {
         private MySqlConnection conn = DataSource.getConnection();
        public List<T> findAllData()
        {
            try
            {
                T table = new T();
                List<T> tList = new List<T>();

                string tableName = table.GetType().Name;

                string sql = string.Format("select * from {0};", tableName);

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();


                //执行ExecuteReader()返回一个MySqlDataReader对象
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                //get account data
                tList = ConvDataTableToCustom<T>.FillModel(dataTable);


                conn.Close();
                return tList;
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public List<T> findDataByKeyWord(string columnName, string keyWord)
        {
            try
            {
                T table = new T();
                List<T> tList = new List<T>();

                string tableName = table.GetType().Name;

                string sql = string.Format("select * from {0} where {1} like '%{2}%';"
                                            , tableName
                                            ,columnName
                                            ,keyWord);

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();


                //执行ExecuteReader()返回一个MySqlDataReader对象
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                //get account data
                tList = ConvDataTableToCustom<T>.FillModel(dataTable);


                conn.Close();
                return tList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public T findDataById(string idName, long id)
        {

            try
            {
                T table = new T();

                string tableName = table.GetType().Name;

                string sql = string.Format("select * from {0} where {1} ={2};"
                                            , tableName
                                            , idName
                                            , id);

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                //get account data
                List<T> tList = ConvDataTableToCustom<T>.FillModel(dataTable);

                if (tList == null)
                {
                    return default(T);
                }
                else
                {
                    if (tList.Count > 0)
                    {
                        table = tList[0];
                    }
                }

                conn.Close();
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int deleteData(string tableName, string idNmae, long id)
        {
            try
            {
                string sql = string.Format("delete from {0} where {1}={2}", tableName, idNmae, id);

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();
                
                int rows = cmd.ExecuteNonQuery();

                conn.Close();
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public int saveData(List<T> tableList)
        {
            try
            {
                List<string> sqlList = new List<string>();
                foreach (T table in tableList)
                {
                    string tableName = table.GetType().Name;
                    List<string> columnList = new List<string>();
                    List<object> columnValueList = new List<object>();
                    int i = 1;
                    var columnNameInfo = table.GetType().GetProperties();
                    foreach (var mem in columnNameInfo)
                    {
                        columnList.Add(mem.Name);
                        var type = mem.PropertyType.Name;
                        //if(type is string)
                        if ("string".Equals(type.ToString().ToLower()))
                        {
                            string str = "'" + mem.GetValue(table) + "'";
                            columnValueList.Add(str);
                        }
                        else
                        {
                            columnValueList.Add(mem.GetValue(table));
                        }
                    }
                    //columnValueList.Join(",");
                    string sql = string.Format("insert into {0}({1}) value({2});"
                                                , tableName
                                                , string.Join(",", columnList)
                                                , string.Join(",", columnValueList));
                    sqlList.Add(sql);

                }
                MySqlTransaction sqlTransaction = conn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand(sqlList.ToString(), conn, sqlTransaction);
                int rows = -1;
                try
                {
                    conn.Open(); 
                    rows = cmd.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception e)
                {
                    sqlTransaction.Rollback();
                    string str = e.Message;
                    return rows;
                }
                conn.Close();
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// write to the db ,update delete insert
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int setDataToDB(string sql)
        {
            int rows = -1;
            MySqlConnection conn = DataSource.getConnection();
            conn.Open();
            MySqlTransaction sqlTransaction = conn.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand(sql, conn, sqlTransaction);
            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                sqlTransaction.Rollback();
                string str = e.Message;
                return rows;
            }

            sqlTransaction.Commit();
            return rows;
        }

        public int updateData(T table, string idName, long id)
        {
            throw new NotImplementedException();
        }
    }
}
