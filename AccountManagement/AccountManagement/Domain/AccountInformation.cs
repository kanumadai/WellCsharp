using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain
{
    /// <summary>
    /// Account informations
    /// </summary>
    public class AccountInformation
    {
        public Int64 accountId { get; set; }
        public string userId { get; set; }
        public string webSite { get; set; }
        public string loginAccount { get; set; }
        public string loginPassword { get; set; }
        public string payAccount { get; set; }
        public string payPassword { get; set; }
        public string emailAddr { get; set; }
        public string telephone { get; set; }
        public string validDate { get; set; }
        public string creditSafeCode { get; set; }
        public string safeQuestion1 { get; set; }
        public string safeQuestionAnswer1 { get; set; }
        public string safeQuestion2 { get; set; }
        public string safeQuestionAnswer2 { get; set; }
        public string safeQuestion3 { get; set; }
        public string safeQuestionAnswer3 { get; set; }
        public string emergencyContact1 { get; set; }
        public string emergencyContactPhone1 { get; set; }
        public string emergencyContact2 { get; set; }
        public string emergencyContactPhone2 { get; set; }
        public string address { get; set; }
        public string accountType { get; set; }
        public string companyName { get; set; }
        public string companyCode { get; set; }
        public string shopName { get; set; }
        public string shopCode { get; set; }


    }
}
