﻿//using AccountManagement.DbUtils;
//using AccountManagement.Domain;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AccountManagement.Dao
//{

//    public class GenericDBContext<T> where T :  class

//    {
//        public GenericDBContext() 
//        {

//        }




//        public List<T> Get()

//        {
//            List<AccountInformation> list = new List<AccountInformation>();
//            using (var context = new DbEntities())
//            {
//                list = context.accountInfoDbset
//                    .AsNoTracking()
//                    .ToList();

//                //var dd =  from du in context.usersDbset
//                //          where du.userId equals (from da in context.accountInfoDbset
//                //                                 where da.userId == ""
//                //                                 select da.userId) 
                    
//                //    sel
                        
//            }

//            return list;

//        }
//        public T Get(int id)

//        {

//            return Items.Find(id);

//        }
//        public void Put(T item)

//        {

//            Items.Attach(item);

//            Entry(item).State = EntityState.Modified;

//            SaveChanges();

//        }
//        public void Post(T item)

//        {

//            Items.Add(item);

//            SaveChanges();

//        }
//        public void Delete(int id)

//        {

//            Delete(Get(id));

//        }
//        public void Delete(T item)

//        {

//            Items.Attach(item);

//            Entry(item).State = EntityState.Deleted;

//            SaveChanges();

//        }

//    }
//}
