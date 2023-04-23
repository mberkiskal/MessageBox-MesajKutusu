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
using System.Security.Cryptography.X509Certificates;

namespace p11ChatApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string numara;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-23T2RIK\\SQLEXPRESS;Initial Catalog=p11Mesajlasma;Integrated Security=True");
        
        void gelen()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select  MESAJID, (AD+' '+SOYAD) as GONDEREN,BASLIK,ICERIK from Table_Mesajlar inner join Table_Kisiler on Table_Mesajlar.GONDEREN=Table_Kisiler.NUMARA where ALICI=" + numara,con);
            DataTable dt = new DataTable();
            da1.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void gecmis()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("select  MESAJID, (AD+' '+SOYAD) as ALICI,BASLIK,ICERIK from Table_Mesajlar inner join Table_Kisiler on Table_Mesajlar.ALICI=Table_Kisiler.NUMARA where GONDEREN=" + numara, con);
            DataTable d2 = new DataTable();
            da2.Fill(d2);
            dataGridView2.DataSource = d2;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'p11MesajlasmaDataSet.p11gonderen' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.p11gonderenTableAdapter.Fill(this.p11MesajlasmaDataSet.p11gonderen);
            lblNumara.Text = numara;
            gelen();
            gecmis();
           
            //AD SOYAD ÇEKME
            con.Open();
            SqlCommand cmd = new SqlCommand("select AD,SOYAD from Table_Kisiler where NUMARA=" + numara, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("insert into Table_Mesajlar (GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)", con);
            cmd1.Parameters.AddWithValue("@p1", numara);
            cmd1.Parameters.AddWithValue("@p2", mskAlici.Text);
            cmd1.Parameters.AddWithValue("@p3", txtBaslik.Text);
            cmd1.Parameters.AddWithValue("@p4", rchIcerik.Text);
            cmd1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Mesaj Başarıyla Gönderildi!","İşlem Başarılı!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            gecmis();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mskAlici.Text = "";
            txtBaslik.Text = "";
            rchIcerik.Text = "";
        }
    }
}
