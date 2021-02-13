using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain
{
    /// <summary>
    /// The user information who is using the app
    /// </summary>
    public class Users
    {
        /// <summary>
        /// user id
        /// </summary>
        [Key]
        public Int64 Id { get; set; }
        /// <summary>
        /// account
        /// </summary>
        public  String userId { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public  String password { get; set; }


    }
}
