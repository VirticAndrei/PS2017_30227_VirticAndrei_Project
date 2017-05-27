using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using FoodDelivery.Properties;
using FoodDelivery.Controller;
using FoodDelivery.View;

namespace FoodDelivery
{
    public partial class StartView : Form, IStartView
    {
        private StartController _startController;
        private int timer = 0, imageIndex = 1;
        private string[] imageList = {"slide1.jpg","slide2.jpg"};
        private ResourceManager rm = Resources.ResourceManager;
        public StartView()
        {
            InitializeComponent();
        }

        private void StartView_Load(object sender, EventArgs e)
        {
            timer1.Start();
            logoutBtn.Visible = false;
        }

        public void Open()
        {
            this.Visible = true;
        }

        public void CloseView()
        {
            this.Visible = false;
        }

        public void SetController(StartController controller)
        {
            _startController = controller;
        }

        public void ValidateLog(string errorMsg)
        {
            if (errorMsg == "Correct!")
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                logoutBtn.Visible = true;
            }
            else
                errorLog.Text = errorMsg;
        }

        public void ValidateSign(string errorMsg)
        {
            if (errorMsg == "Correct!")
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                logoutBtn.Visible = true;
            }
            else
                errorSign.Text = errorMsg;
        }

        public void Logout()
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            logoutBtn.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer++;
            switch (imageIndex)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.slide1;
                    break;
                case 2:
                    pictureBox1.Image = Properties.Resources.slide2;
                    break;
                case 3:
                    pictureBox1.Image = Properties.Resources.slide3;
                    break;
            }

            if (timer == 20)
            {
                imageIndex++;
                if (imageIndex == 4)
                    imageIndex = 1;
                timer = 0;
            }
            
        }

        private void loginButton_MouseEnter(object sender, EventArgs e)
        {
            loginOrSiginButton.ForeColor = Color.DarkBlue;
        }

        private void loginButton_MouseLeave(object sender, EventArgs e)
        {
            loginOrSiginButton.ForeColor = Color.White;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.DarkBlue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.DarkBlue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.DarkBlue;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.White;
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.DarkBlue;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.DarkBlue;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.White;
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            label8.ForeColor = Color.DarkBlue;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.White;
        }

        private void label9_MouseEnter(object sender, EventArgs e)
        {
            label9.ForeColor = Color.DarkBlue;
        }

        private void label9_MouseLeave(object sender, EventArgs e)
        {
            label9.ForeColor = Color.White;
        }

        private void label10_MouseEnter(object sender, EventArgs e)
        {
            label10.ForeColor = Color.DarkBlue;
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            label10.ForeColor = Color.White;
        }

        private void label11_MouseEnter(object sender, EventArgs e)
        {
            label11.ForeColor = Color.DarkBlue;
        }

        private void label11_MouseLeave(object sender, EventArgs e)
        {
            label11.ForeColor = Color.White;
        }

        private void label12_MouseEnter(object sender, EventArgs e)
        {
            label12.ForeColor = Color.DarkBlue;
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            label12.ForeColor = Color.White;
        }

        private void label13_MouseEnter(object sender, EventArgs e)
        {
            label13.ForeColor = Color.DarkBlue;
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            label13.ForeColor = Color.White;
        }

        private void label14_MouseEnter(object sender, EventArgs e)
        {
            label14.ForeColor = Color.DarkBlue;
        }

        private void label14_MouseLeave(object sender, EventArgs e)
        {
            label14.ForeColor = Color.White;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            _startController.OpenAccountView();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Menu");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Aperitive");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Bauturi");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Pizza");
        }

        private void label6_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Supe");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Salate");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Paste");
        }

        private void label9_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Fel principal");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Burger");
        }

        private void label11_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Peste");
        }

        private void label12_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Garnituri");
        }

        private void label13_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Desert");
        }

        private void label14_Click(object sender, EventArgs e)
        {
            _startController.OpenMeniu("Sosuri");
        }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (showPassword.Checked == true)
            {
                passwordTxt.UseSystemPasswordChar = false;
            }
            else
            {
                passwordTxt.UseSystemPasswordChar = true;
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            errorLog.Text = "";
            _startController.Login(userTxt.Text,passwordTxt.Text);
        }

        private void signinBtn_Click(object sender, EventArgs e)
        {
            errorSign.Text = "";
            _startController.Signin(fnameTxt.Text,lnameTxt.Text,passTxt.Text,confirmPassTxt.Text,emailTxt.Text);
        }

        private void StartView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _startController.Disconnect();
            Environment.Exit(0);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            if (logoutBtn.Visible == true)
                _startController.Logout();
        }


        
    }
}
