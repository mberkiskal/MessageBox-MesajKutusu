using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace p11ChatApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-23T2RIK\\SQLEXPRESS;Initial Catalog=p11Mesajlasma;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Table_Kisiler where NUMARA=@p1 and SIFRE=@p2", con);
            cmd.Parameters.AddWithValue("@p1", mskNumara.Text);
            cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Form2 form2 = new Form2();
                form2.numara = mskNumara.Text;
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Giriş Bilgileriniz Hatalı Lütfen Bilgilerinizi Kontrol Ediniz!", "HATALI GİRİŞ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }
    }
}
