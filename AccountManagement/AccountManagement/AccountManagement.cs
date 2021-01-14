using AccountManagement.Dao;
using AccountManagement.DbUtils;
using AccountManagement.Domain;
using AccountManagement.Service;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountManagement
{
    public partial class AccountManagement : Form
    {
        public AccountManagement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            //1,check the text box if it is blank
            //2,get the account and password
            //3.check the inf by db data.
            int result = checkBox();
            //4.exist in db,show the msg
            //5,if do not exist ,safe the info into db and go to the account information form
            if (result == 0)
            {                //insert user into the users

                Users user = new Users()
                {
                    Id = 0,
                    userId = textBoxAccount.Text,
                    password = textBoxPassword.Text
                };
                IServiceAble<Users> service = new ServiceImpl<Users>();
   
                int rows = service.saveData(user);
                if (rows < 0)
                {
                    labelMsg.Text = "Failed to create a user, please try later.";
                    return;
                }

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
                Form form = new Accounts(textBoxAccount.Text);
                form.ShowDialog();
            }
            else
            {
                labelMsg.ForeColor = Color.FromArgb(255, 0, 0);
                if (result == -1)
                {
                    labelMsg.Text = "UserId or password can not be blank.";
                }
                else if (result > 0)
                {
                    labelMsg.Text = "UserId or password is already exist.";
                }

            }
        }

        /// <summary>
        /// user to login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //1,check the text box if it is blank
            //2,get the account and password
            //3.check the inf by db data.
            int result = checkBox();
            //4.exist in db,goto the account information form
            //5,if do not exist ,show the msg
            if (result > 1)
            {

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
             
                Form form = new Accounts(textBoxAccount.Text);

                this.Hide();
                form.ShowDialog();
                this.Close();
            }  
            else
            {
                labelMsg.ForeColor= Color.FromArgb(255, 0, 0);
                //textBoxAccount.ForeColor= Color.FromArgb(255,0,0);
                if (result == -1)
                {
                    labelMsg.Text = "UserId or password can not be blank.";
                }
                else if (result > 0)
                {
                    labelMsg.Text = "UserId or password is not crrect.";
                }

            }
 
        }

        private int checkBox()
        {
            int iRet = -1;
            if(textBoxAccount.Text =="" || textBoxPassword.Text == "")
            {
            }
            else
            {
                Users user = new Users();

                IServiceAble <Users> service = new ServiceImpl<Users>();

                List<Users> userList = service.findDataByKeyWord("userId", textBoxAccount.Text);
                //user is already exist.
                if (userList != null && userList.Count==1)
                {
                    //password is crect
                    if (textBoxPassword.Text.Equals(userList[0].password)){
                        iRet = 2;
                    }
                    //password is wrong
                    else
                    {
                        iRet = 1;
                    }
                }
                else
                //user is not exist.
                {
                    iRet = 0;
                }

            }


            return iRet;
        }

        private void AccountManagement_Load(object sender, EventArgs e)
        {
          //  DataSource.connectionPool();

        }
    }
}
