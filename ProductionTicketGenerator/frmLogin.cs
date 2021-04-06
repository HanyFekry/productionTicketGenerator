﻿using DBLayer;
using ProductionTicketGenerator.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionTicketGenerator
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string message;
            string _Role = "";
            if (ValidateInputs(ref _Role, out message))
            {
                if (string.IsNullOrEmpty(_Role))
                {
                    MessageBox.Show(message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (_Role == SD.AutomotiveUser)
                {
                    Form form = new frmAutomotivePrint();// (txtUserName.Text);
                    this.Hide();
                    form.Show();
                }
                if (_Role == SD.QualityAdmin || _Role == SD.QualityUser)
                {
                    Form form = new frmAutomotivePrint();
                    this.Hide();
                    form.Show();
                }
            }
            else
            {
                MessageBox.Show(message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool ValidateInputs(ref string _Role, out string message)
        {
            message = string.Empty;
            if (!string.IsNullOrWhiteSpace(txtUserName.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                string userName = txtUserName.Text;
                string password = txtPassword.Text;
                using (var context = new ManufacturingContext())
                {
                    var user = context.Members.Include("Role").FirstOrDefault(u => u.Name == userName && u.Password == password);
                    if (null != user && user.Id > 0)
                    {
                        Global.UserId = user.Id;
                        _Role = user.Role.EnName;
                        return true;
                    }
                    else
                    {
                        message = "إسم المستخدم غير صحيح أو كلمة المرور غير صحيحة!";
                        return false;
                    }
                }
            }
            else
            {
                message = "خطأ في إسم المستخدم أو كلمة المرور!";
                return false;
            }
        }
    }
}
