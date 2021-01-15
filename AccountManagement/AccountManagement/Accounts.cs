using AccountManagement.Dao;
using AccountManagement.DbUtils;
using AccountManagement.Domain;
using AccountManagement.FileOperate;
using AccountManagement.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountManagement
{
    public partial class Accounts : Form
    {
        private string currentUser;
        /// <summary>
        /// 1,new one account
        /// 2,modify one account
        /// 3,insert one or more fie's operations
        /// 4,delete one account
        /// </summary>
        private List<AccountInformation> _accountList = new List<AccountInformation>();
        private AccountInformation _account = new AccountInformation();
        private IServiceAble<AccountInformation> _service = new ServiceImpl<AccountInformation>();
        
        private enum _viewStatus {S_NOMAL, S_DELETE,S_UPDATE,S_NEW,S_UPLOAD};

        public Accounts(string user)
        {
            currentUser = user;
            InitializeComponent();
        }

        /// <summary>
        /// get all account information at current user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllAccounts_Click(object sender, EventArgs e)
        {

            if (_accountList != null)
            {
                comboBoxResults.Items.Clear();
                _accountList.Clear();
            }
            List<AccountInformation> accountFoundList =  _service.findAllData();

            resultsCheck(accountFoundList);

            //if (accountFoundList == null)
            //{
            //    MessageBox.Show("0 result has been found.");
            //    return;
            //}


            //if (accountFoundList.Count > 0)
            //{
            //    //get account data
            //    foreach (var item in accountFoundList)
            //    {
            //        if (currentUser.Equals(item.userId))
            //        {
            //            _accountList.Add(item);
            //            comboBoxResults.Items.Add(item.companyName + "_" + item.loginAccount);

            //        }
            //    }
            //}

            //if (_accountList.Count > 0)
            //{
            //    MessageBox.Show(string.Format("{0} results has been found.", _accountList.Count));
            //    comboBoxResults.SelectedIndex = 0;
            //}
        }


        /// <summary>
        /// new a account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if ("New".Equals(btnNew.Text))
            {
                //comboBoxResults.SelectedIndex = 0;
                clearTextBoxInfo();
                setViewStatus(_viewStatus.S_NEW);
                //  enableTextBox();

            }
            else
            {
                setViewStatus(_viewStatus.S_NOMAL);
                // disableTextBox();

            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if ("Save".Equals(btnUpload.Text))
            {
                List<AccountInformation> accountList = new List<AccountInformation>();

                AccountInformation account = getTextBoxInfo();
                
                if (account.loginAccount == "")
                {
                    MessageBox.Show("LoginAccount must to input account info.");
                    return;
                }
                accountList.Add(account);
               int row= _service.saveData(accountList);
                //failed to delete
                if (row < 1)
                {
                    MessageBox.Show("Failed to new a account.");
                    return;
                }

                MessageBox.Show("Succeed to new a account.");
                setViewStatus(_viewStatus.S_NOMAL);
                //  enableTextBox();

            }
            else
            {
                if (_accountList.Count > 0)
                {
                    int row = _service.saveData(_accountList);
                    //failed to delete
                    if (row < 1)
                    {
                        MessageBox.Show("Failed to upload accounts.");
                        return;
                    }

                    MessageBox.Show(string.Format("Succeed to save {0} account.",row));

                }

                setViewStatus(_viewStatus.S_NOMAL);
                // disableTextBox();

            }

        }


        /// <summary>
        /// update current account info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (comboBoxResults.SelectedIndex < 0)
            {
                return;
            }

            AccountInformation account = getTextBoxInfo();
           // disableTextBox();

            if (account == null)
            {
                MessageBox.Show("Account information is not currect.");
                return;
            }
            int row = _service.updateData(account, account.accountId.GetType().Name, account.accountId);
            //failed to delete
            if (row < 1)
            { 
                MessageBox.Show("Failed to update current account.");
                return;
            }
        }
      
        /// <summary>
        /// delete curretn account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (comboBoxResults.SelectedIndex < 0)
            {
                return;
            }


            AccountInformation account = getTextBoxInfo();
            disableTextBox();

            if (account == null)
            {
                MessageBox.Show("Account information is not currect.");
                return;
            }
            int row = _service.deleteData(account.GetType().Name, account.accountId.GetType().Name, account.accountId);
            //success to delete
            if (row > 0)
            {
                _accountList.Remove(account);
                comboBoxResults.Items.RemoveAt(comboBoxResults.SelectedIndex);
                comboBoxResults.SelectedIndex = 0;
            }
            //failed to delete
            else
            {
                MessageBox.Show("Failed to delete current account.");
                return;
            }

        }


        /// <summary>
        /// get all the text of the account 
        /// </summary>
        /// <returns></returns>
        private AccountInformation getTextBoxInfo()
        {
            if(textBoxLoginAccount.Text == "")
            {
                return null;
            }

            return new AccountInformation
            {
                webSite = textBoxWebSite.Text,
                userId = currentUser,
                loginAccount = textBoxLoginAccount.Text,
                loginPassword = textBoxLoginPassword.Text,
                payAccount = textBoxPayAccount.Text,
                payPassword = textBoxPayPassword.Text,
                emailAddr = textBoxEmailAddr.Text,
                validDate = textBoxValidDate.Text,
                telephone = textBoxTelephone.Text,
                creditSafeCode = textBoxCreditSafeCode.Text,
                safeQuestion1 = textBoxSafeQuestion1.Text,
                safeQuestionAnswer1 = textBoxSafeQuestionA1.Text,
                safeQuestion2 = textBoxSafeQuestion2.Text,
                safeQuestionAnswer2 = textBoxSafeQuestionA2.Text,
                safeQuestion3 = textBoxSafeQuestion3.Text,
                safeQuestionAnswer3 = textBoxSafeQuestionA3.Text,
                emergencyContact1 = textBoxEmergencyContact1.Text,
                emergencyContactPhone1 = textBoxEmergencyContactPhone1.Text,
                emergencyContact2 = textBoxEmergencyContact2.Text,
                emergencyContactPhone2 = textBoxEmergencyContactPhone2.Text,
                address = textBoxAddress.Text,
                accountType = textBoxAccountType.Text,
                companyName = textBoxCompnayName.Text,
                companyCode = textBoxCompanyCode.Text,
                shopName = textBoxShopName.Text,
                shopCode = textBoxShopCode.Text
            };
        }
       
        /// <summary>
        /// set account info to the textbox.
        /// </summary>
        /// <param name="listBoxIndex"></param>
        private void setTextBoxInfo(int listBoxIndex)
        {
            if (_accountList[listBoxIndex] == null)
            {
                return;
            }
            AccountInformation account = _accountList[listBoxIndex];


            textBoxWebSite.Text = account.webSite;
            textBoxLoginAccount.Text = account.loginAccount;
            textBoxLoginPassword.Text = account.loginPassword;
            textBoxPayAccount.Text = account.payAccount;
            textBoxPayPassword.Text = account.payPassword;
            textBoxEmailAddr.Text = account.emailAddr;
            textBoxValidDate.Text = account.validDate;
            textBoxTelephone.Text = account.telephone;
            textBoxCreditSafeCode.Text = account.creditSafeCode;
            textBoxSafeQuestion1.Text = account.safeQuestion1;
            textBoxSafeQuestionA1.Text = account.safeQuestionAnswer1;
            textBoxSafeQuestion2.Text = account.safeQuestion2;
            textBoxSafeQuestionA2.Text = account.safeQuestionAnswer2;
            textBoxSafeQuestion3.Text = account.safeQuestion3;
            textBoxSafeQuestionA3.Text = account.safeQuestionAnswer3;
            textBoxEmergencyContact1.Text = account.emergencyContact1;
            textBoxEmergencyContactPhone1.Text = account.emergencyContactPhone1;
            textBoxEmergencyContact2.Text = account.emergencyContact2;
            textBoxEmergencyContactPhone2.Text = account.emergencyContactPhone2;
            textBoxAddress.Text = account.address;
            textBoxAccountType.Text = account.accountType;
            textBoxCompnayName.Text = account.companyName;
            textBoxCompanyCode.Text = account.companyCode;
            textBoxShopName.Text = account.shopName;
            textBoxShopCode.Text = account.shopCode;

        }

        private void clearTextBoxInfo()
        {


            textBoxWebSite.Text = "";
            textBoxLoginAccount.Text = "";
            textBoxLoginPassword.Text = "";
            textBoxPayAccount.Text = "";
            textBoxPayPassword.Text = "";
            textBoxEmailAddr.Text = "";
            textBoxValidDate.Text = "";
            textBoxTelephone.Text = "";
            textBoxCreditSafeCode.Text = "";
            textBoxSafeQuestion1.Text = "";
            textBoxSafeQuestionA1.Text = "";
            textBoxSafeQuestion2.Text = "";
            textBoxSafeQuestionA2.Text = "";
            textBoxSafeQuestion3.Text = "";
            textBoxSafeQuestionA3.Text = "";
            textBoxEmergencyContact1.Text = "";
            textBoxEmergencyContactPhone1.Text = "";
            textBoxEmergencyContact2.Text = "";
            textBoxEmergencyContactPhone2.Text = "";
            textBoxAddress.Text = "";
            textBoxAccountType.Text = "";
            textBoxCompnayName.Text = "";
            textBoxCompanyCode.Text = "";
            textBoxShopName.Text = "";
            textBoxShopCode.Text = "";

        }

        /// <summary>
        /// disabale text box
        /// </summary>
        private void disableTextBox()
        {

            textBoxWebSite.Enabled = false;
            textBoxLoginAccount.Enabled = false;
            textBoxLoginPassword.Enabled = false;
            textBoxPayAccount.Enabled = false;
            textBoxPayPassword.Enabled = false;
            textBoxEmailAddr.Enabled = false;
            textBoxValidDate.Enabled = false;
            textBoxTelephone.Enabled = false;
            textBoxSafeQuestion1.Enabled = false;
            textBoxSafeQuestionA3.Enabled = false;
            textBoxEmergencyContactPhone1.Enabled = false;
            textBoxEmergencyContact2.Enabled = false;
            textBoxEmergencyContactPhone2.Enabled = false;
            textBoxAddress.Enabled = false;
            textBoxAccountType.Enabled = false;
            textBoxCompnayName.Enabled = false;
            textBoxCompanyCode.Enabled = false;
            textBoxShopName.Enabled = false;
            textBoxCreditSafeCode.Enabled = false;
            textBoxSafeQuestionA1.Enabled = false;
            textBoxSafeQuestion2.Enabled = false;
            textBoxSafeQuestionA2.Enabled = false;
            textBoxSafeQuestion3.Enabled = false;
            textBoxEmergencyContact1.Enabled = false;
            textBoxShopCode.Enabled = false;
        }

        /// <summary>
        /// enable text box
        /// </summary>
        private void enableTextBox()
        {
            textBoxWebSite.Enabled = true;
            textBoxLoginAccount.Enabled = true;
            textBoxLoginPassword.Enabled = true;
            textBoxPayAccount.Enabled = true;
            textBoxPayPassword.Enabled = true;
            textBoxEmailAddr.Enabled = true;
            textBoxValidDate.Enabled = true;
            textBoxTelephone.Enabled = true;
            textBoxSafeQuestion1.Enabled = true;
            textBoxSafeQuestionA3.Enabled = true;
            textBoxEmergencyContactPhone1.Enabled = true;
            textBoxEmergencyContact2.Enabled = true;
            textBoxEmergencyContactPhone2.Enabled = true;
            textBoxAddress.Enabled = true;
            textBoxAccountType.Enabled = true;
            textBoxCompnayName.Enabled = true;
            textBoxCompanyCode.Enabled = true;
            textBoxShopName.Enabled = true;
            textBoxCreditSafeCode.Enabled = true;
            textBoxSafeQuestionA1.Enabled = true;
            textBoxSafeQuestion2.Enabled = true;
            textBoxSafeQuestionA2.Enabled = true;
            textBoxSafeQuestion3.Enabled = true;
            textBoxEmergencyContact1.Enabled = true;
            textBoxShopCode.Enabled = true;
        }

        /// <summary>
        /// combo box select event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTextBoxInfo(comboBoxResults.SelectedIndex);
        }

        /// <summary>
        /// form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accounts_Load(object sender, EventArgs e)
        {
          var memInfo=  _account.GetType().GetProperties();
            foreach(var mem in memInfo) {
                comboBoxTableCol.Items.Add(mem.Name);
            }
            comboBoxTableCol.SelectedIndex = 0;
        }


        /// <summary>
        /// search botton event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {

            IServiceAble<AccountInformation> _service = new ServiceImpl<AccountInformation>();
            if (comboBoxTableCol.SelectedIndex < 0)
            {
                MessageBox.Show("Please chose search condition.");
                return;
            }
            if (textBoxSearch.Text=="")
            {
                MessageBox.Show("Please input search key word.");
                return;
            }

            if (_accountList != null)
            {
                comboBoxResults.Items.Clear();
                _accountList.Clear();
            }
            List<AccountInformation> accountFoundList = _service.findDataByKeyWord(comboBoxTableCol.SelectedItem.ToString(), textBoxSearch.Text);

            resultsCheck(accountFoundList);

            //if(accountFoundList == null)
            //{
            //    MessageBox.Show("0 results has been found.");
            //    return;
            //}            

            //if (accountFoundList.Count > 0)
            //{
            //    foreach (var item in accountFoundList)
            //    {
            //        if (currentUser.Equals(item.userId))
            //        {
            //            _accountList.Add(item);
            //            comboBoxResults.Items.Add(item.companyName + "_" + item.loginAccount);

            //        }
            //    }
            //}

            //if (_accountList.Count > 0)
            //{
            //    MessageBox.Show(string.Format("{0} results has been found.", _accountList.Count));
            //    comboBoxResults.SelectedIndex = 0;
            //}  
        }

        private void setViewStatus(_viewStatus status){
            switch (status)
            {
                case _viewStatus.S_NOMAL:
                    btnNew.Text = "New";
                    btnUpload.Text = "Upload";
                    btnReadFile.Text = "Read from file";
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnAllAccounts.Enabled = true;
                    btnSearch.Enabled = true;
                    btnReadFile.Enabled = true;
                    btnNew.Enabled = true;
                    break;
                case _viewStatus.S_NEW:
                    btnNew.Text = "Cancle";
                    btnUpload.Text = "Save";
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                    btnAllAccounts.Enabled = false;
                    btnSearch.Enabled = false;
                    btnReadFile.Enabled = false;
                    break;
                case _viewStatus.S_DELETE:
                    break;
                case _viewStatus.S_UPDATE:
                    break;
                case _viewStatus.S_UPLOAD:
                    btnReadFile.Text = "Cancle";
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                    btnAllAccounts.Enabled = false;
                    btnSearch.Enabled = false;
                    btnNew.Enabled = false;
                    break;
            }
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            if ("Cancle".Equals(btnReadFile.Text))
            {

                setViewStatus(_viewStatus.S_NOMAL);
                _accountList.Clear();
                comboBoxResults.Items.Clear();
                return;
            }
            setViewStatus(_viewStatus.S_UPLOAD);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please Chose a CSV file...";
            openFileDialog.InitialDirectory = @"./";
            openFileDialog.Filter = "|*.csv|csv file|*.*|all file|";
            openFileDialog.ShowDialog();

            string fileName = openFileDialog.FileName;

            List<object> accountInformationList = _account.loadCsvFile(fileName);
            if (_accountList != null)
            {
                comboBoxResults.Items.Clear();
                _accountList.Clear();
            }

            if (accountInformationList == null)
            {
                MessageBox.Show("0 results has been found.");
                return;
            }

            if (accountInformationList.Count > 0)
            {
                foreach (var item in accountInformationList)
                {
                    AccountInformation acc = (AccountInformation)item;
                    if (currentUser.Equals(acc.userId))
                    {
                        _accountList.Add(acc);
                        comboBoxResults.Items.Add(acc.companyName + "_" + acc.loginAccount);

                    }
                }
            }

            if (_accountList.Count > 0)
            {
                MessageBox.Show(string.Format("{0} results has been found.", _accountList.Count));
                comboBoxResults.SelectedIndex = 0;
            }

        }

        private void resultsCheck(List<AccountInformation> accountFoundList)
        {
            if (accountFoundList == null)
            {
                MessageBox.Show("0 results has been found.");
                return;
            }

            if (accountFoundList.Count > 0)
            {
                foreach (var item in accountFoundList)
                {
                    if (currentUser.Equals(item.userId))
                    {
                        _accountList.Add(item);
                        comboBoxResults.Items.Add(item.companyName + "_" + item.loginAccount);

                    }
                }
            }

            if (_accountList.Count > 0)
            {
                MessageBox.Show(string.Format("{0} results has been found.", _accountList.Count));
                comboBoxResults.SelectedIndex = 0;
            }
        }
    }
}
