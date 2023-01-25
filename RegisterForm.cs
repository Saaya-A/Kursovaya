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
using Курсовая;

namespace Курсовая
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            userNameField.Text = "Name";
            userNameField.ForeColor = Color.Gray;
            userSurnameField.Text = "Surname";
            userSurnameField.ForeColor = Color.Gray;
            loginField.Text = "Login";
            loginField.ForeColor = Color.Gray;
            passField.Text = "Password";
            passField.ForeColor = Color.Gray;
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void userNameField_Enter(object sender, EventArgs e)
        {
            if (userNameField.Text == "Name")
            {
                userNameField.Text = "";
                userNameField.ForeColor = Color.Black;
            }
        }
        private void userNameField_Leave(object sender, EventArgs e)
        {
            if (userNameField.Text == "")
            {
                userNameField.Text = "Name";
                userNameField.ForeColor = Color.Gray;
            }
        }
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if(userNameField.Text == "Name")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (userSurnameField.Text == "Surname")
            {
                MessageBox.Show("Введите фамилию");
                return;
            }
            if (loginField.Text == "Login")
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (passField.Text == "Password")
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (isUserExists(loginField.Text))
            {
                MessageBox.Show("Логин уже используется, введите другой");
                return;
            }
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) VALUES (@login, @pass, @name, @surname)", db.GetConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userNameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userSurnameField.Text;
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт создан");
                Form LoginForm = new LoginForm();
                LoginForm.Show();
                this.Hide();
            }
            else MessageBox.Show("Ошибка. Аккаунт не создан");
            db.closeConnection();
        }
        public Boolean isUserExists(string login)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void registerLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "Surname")
            {
                userSurnameField.Text = "";
                userSurnameField.ForeColor = Color.Black;
            }
        }
        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "")
            {
                userSurnameField.Text = "Surname";
                userSurnameField.ForeColor = Color.Gray;
            }
        }
        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Login")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }
        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "Login";
                loginField.ForeColor = Color.Gray;
            }
        }
        private void passField_Enter_1(object sender, EventArgs e)
        {
            if (passField.Text == "Password")
            {
                passField.Text = "";
                passField.ForeColor = Color.Black;
            }
        }
        private void passField_Leave_1(object sender, EventArgs e)
        {
            if (passField.Text == "")
            {
                passField.Text = "Password";
                passField.ForeColor = Color.Gray;
            }
        }
    }
}

