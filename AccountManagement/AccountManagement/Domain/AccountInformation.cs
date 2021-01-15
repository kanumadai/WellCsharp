using AccountManagement.FileOperate;
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
    public class AccountInformation : CsvFileImpl
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


        public override object addNewDataLine(string str)
        {
            AccountInformation account = new AccountInformation();
            string[] strArray = str.Split(',');
            if(strArray.Length> 27)
            {
                return null;
            }
            //account.accountId = strArray[0];
            account.userId = strArray[1];
            account.loginAccount = strArray[2];
            account.loginPassword = strArray[3];
            account.payAccount = strArray[4];
            account.payPassword = strArray[5];
            account.emailAddr = strArray[6];
            account.telephone = strArray[7];
            account.validDate = strArray[8];
            account.creditSafeCode = strArray[9];
            account.safeQuestion1 = strArray[10];
            account.safeQuestionAnswer1 = strArray[11];
            account.safeQuestion2 = strArray[12];
            account.safeQuestionAnswer2 = strArray[13];
            account.safeQuestion3 = strArray[14];
            account.safeQuestionAnswer3 = strArray[15];
            account.emergencyContact1 = strArray[16];
            account.emergencyContactPhone1 = strArray[17];
            account.emergencyContact2 = strArray[18];
            account.emergencyContactPhone2 = strArray[19];
            account.address = strArray[20];
            account.accountType = strArray[21];
            account.companyName = strArray[22];
            account.companyCode = strArray[23];
            account.shopName = strArray[24];
            account.shopCode = strArray[25];
            account.webSite =strArray[26];
            if(account.userId=="" || account.loginAccount == "")
            {
                return null;
            }

            return account;
        }
    }
}
