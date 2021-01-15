using AccountManagement.DbUtils;
using AccountManagement.Domain;
using AccountManagement.FormComm;
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


                return tList;
            }
            catch(Exception e)
            {
                CommLog.loger.Error(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
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


                return tList;
            }
            catch (Exception e)
            {
                CommLog.loger.Error(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public T findDataById(string idName, long id)
        {
            T table = new T();
            try
            {
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
                return table;
            }
            catch (Exception e)
            {
                CommLog.loger.Error(e.Message);
                return default(T);
            }
            finally
            {
                conn.Close();
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

                return rows;
            }
            catch (Exception e)
            {
                CommLog.loger.Error(e.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }


        public int saveData(List<T> tableList)
        {
            int rows = -1;
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
                conn.Open(); 
                MySqlTransaction sqlTransaction = conn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand(string.Join(" ",sqlList), conn, sqlTransaction);
               
                try
                {
                    rows = cmd.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception e)
                {
                    sqlTransaction.Rollback();
                    string str = e.Message;
                    return rows;
                }
            }
            catch (Exception e)
            {
                CommLog.loger.Error(e.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public int updateData(T table, string idName, long id)
        {
            int rows = -1;
            try
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
                conn.Open();
                MySqlTransaction sqlTransaction = conn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand(sql, conn, sqlTransaction);

                try
                {
                    rows = cmd.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception e)
                {
                    sqlTransaction.Rollback();
                    string str = e.Message;
                    return rows;
                }
            }
            catch (Exception e)
            {
                CommLog.loger.Error(e.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
            return rows;

        }
    }
}
