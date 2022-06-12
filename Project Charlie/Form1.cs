using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;





namespace Project_Charlie
{
    public partial class Form1 : Form
    {
        public string constring = "server=127.0.0.1;" +
                                         "uid=root;" +
                                         "pwd=SQLMaster;" +
                                         "database=visitorinfo";
        public Form1()
        {
            InitializeComponent();
            try
            {
                //"datasource=localhost;port=3306;username=root;password=SQLMaster;database=visitorinfo;"
                MySqlConnection connection = new MySqlConnection(constring);
                connection.Open();
                if (connection.State == ConnectionState.Open)
                { 
                    MessageBox.Show("Connected");
                    
                }
                else
                {
                    label1.Text = "Not Connected";
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string q = "insert into visitor(VisitId,Name,Surname,Mobile,Email,Meetingdate,Meetingtime,StaffId,MeetingWith,MeetingAim) values('" + textBox5.Text + "','" + textBox1.Text + "','" + textBox3.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + dateTimePicker1.Value.ToString() + "','" + numericUpDown1.Value + numericUpDown2.Value + "','" + textBox6.Text + "','" + comboBox1.SelectedItem + "','" + button2.Text + "')";
                MySqlCommand cmd = new MySqlCommand(q, connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data insereted successfully");
                label1.Text = "Not Connected";
                label1.ForeColor = Color.Red;
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
                label1.Text = "Connected";
                label1.ForeColor = Color.Green;


            }
        }



            private void label4_Click(object sender, EventArgs e)
        {   

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

       



        private void button2_Click(object sender, EventArgs e)
        {
            buttonwasclicked = true;
            
            Form2 sv = new Form2();
            if (sv.ShowDialog() == DialogResult.OK)
            {
                if(sv.radioButton1.Checked==true)
                button2.Text = sv.radioButton1.Text;
                else if(sv.radioButton2.Checked==true)
                button2.Text = sv.radioButton2.Text;
                else if (sv.radioButton3.Checked == true)
                    button2.Text = sv.radioButton3.Text;
                else if (sv.radioButton4.Checked == true)
                    button2.Text = sv.radioButton4.Text;

            }
        }

        private void ResetFields()
        {
            textBox1.Clear();
            textBox3.Clear();
            textBox2.Clear();
            textBox4.Clear();
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            comboBox1.ResetText();
            button2.Text = "Meeting Aim";

        }
        bool buttonwasclicked = false; //using for button validation
        private void Button1_Click(object sender, EventArgs e)
        {
                         int num = 0; //using for number validation
            if (textBox1.Text == "") //Empty Validation
            {
                MessageBox.Show("Firstname can't be empty");
                textBox1.BackColor = Color.Red;
                textBox1.Focus();// it will bring cursor back to the textbox 1
            }
            else if (textBox3.Text == "")

            {
                textBox1.BackColor = Color.White;
                MessageBox.Show("Surname can't be empty");
                textBox3.BackColor = Color.Red;
                textBox3.Focus();
            }
            else if (int.TryParse(textBox2.Text, out num) == false) //number validation
            {
                textBox3.BackColor = Color.White;
                MessageBox.Show("Enter Digits only:");
                textBox2.BackColor = Color.Red;
                textBox2.Focus();
            }
            else if (textBox2.Text.Length < 9)
            {
                MessageBox.Show("Enter Proper 9 digit NZ phone number:");

            }


            else if (textBox4.Text == "")

            {
                textBox2.BackColor = Color.White;
                MessageBox.Show("Email can't be empty");
                textBox4.BackColor = Color.Red;
                textBox4.Focus();
            }
            else if (textBox4.Text.IndexOf('@') == -1|| textBox4.Text.IndexOf(".com")==-1 || textBox4.Text.IndexOf(".com") < textBox4.Text.IndexOf("@")+1)
            {
                MessageBox.Show("Must be proper email with '@' and .com in it!");
            }


            else if (numericUpDown1.Value == 0) //Numeric UpDown validation
            {

                textBox4.BackColor = Color.White;
                MessageBox.Show("Please enter the time of meeting:");
            }
            else if (comboBox1.SelectedIndex == -1) //Combo Box validation
            {
                MessageBox.Show("Please select any item");
            }
            else if (buttonwasclicked == false) //buttom validation
            {
                MessageBox.Show("Please click the meeting aim button");
            }


            else

            {
                listBox1.Items.Add("First Name: " + textBox1.Text + " ");
                listBox1.Items.Add("Last Name: " + textBox3.Text + " ");
                listBox1.Items.Add("Full Name: " + textBox1.Text + " " + textBox3.Text);
                listBox1.Items.Add("Mobile number: " + textBox2.Text + "");
                listBox1.Items.Add("Email is: " + textBox4.Text + "");
                listBox1.Items.Add("Calander date is:" + dateTimePicker1.Value);
                listBox1.Items.Add("Time: " + numericUpDown1.Value + ":" + ((numericUpDown2.Value < 10) ? "0" : "") + numericUpDown2.Value);
                listBox1.Items.Add("Meeting with: " + comboBox1.SelectedItem);
                listBox1.Items.Add("Meeting Aim; " + button2.Text);
                listBox1.Items.Add("=====================================");
                ResetFields();
            }
            
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1 != null)

            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            MySqlConnection connection = new MySqlConnection(constring);
            connection.Open();
            if (connection.State == ConnectionState.Open)

            {
                string q = "Delete From visitor WHERE VisitId ='" + textBox5.Text + "'";
                MySqlCommand cmd = new MySqlCommand(q, connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Deleted successfully");
                ResetFields();
            }
            else
            {
                MessageBox.Show("Connection not established");
            }
        }
       //private void Edit()
       //{
            //MySqlConnection con = new MySqlConnection(mySqlConString);
            //con.Open();
            //
           // if (con.State == ConnectionState.Open) // to ensure connection is open ornot
          //  {
           //     string query = "Update Visitors set FirstName = '" + textBox1.Text + "', Surname = '" + textBox3.Text + "', Mobile = '" + mobile.Text + "', Email = '" + email.Text + "', MeetingAim = '" + button1.Text + "' Where VisitorId = " + ID.Text;
            //    MySqlCommand cmd = new MySqlCommand(query, con);
            //    cmd.ExecuteNonQuery();
             //   MessageBox.Show("Data Updated successfully");
             //   ResetFields();
           // }
           // else
          //  {
           //     MessageBox.Show("Connection not established");
          //  }
       // }

    }
    }

