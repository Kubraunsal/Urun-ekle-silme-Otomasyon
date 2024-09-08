using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otamasyon
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source = LAPTOP-VBT65R4T; Initial Catalog = market_otomasyonu; Integrated Security = True");

        private void button1_Click(object sender, EventArgs e)
        {
            String adi = textBox1.Text;
            String Sifree = textBox2.Text;

            baglanti.Open();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT * FROM kullanıcılar WHERE ad = '" + adi + "' AND sifre = '" + Sifree + "'";
            komut.ExecuteNonQuery();
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("hatalı giriş yaptınız");
            }

            baglanti.Close();   
    }


        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult sonuc;
            sonuc = MessageBox.Show("çıkış yapmak istediğinizden emin misiniz", "ÇIKIŞ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); 
            if(sonuc == DialogResult.No)
            {

            }
            if (sonuc == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            var c = new ŞifreUnuttum
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false
            };

            c.StartPosition = FormStartPosition.CenterParent;
            c.ShowDialog(this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
