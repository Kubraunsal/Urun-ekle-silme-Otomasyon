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
    public partial class KullanıcıEkle : Form
    {
        public KullanıcıEkle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source = LAPTOP-VBT65R4T; Initial Catalog = market_otomasyonu; Integrated Security = True");

        private void KullanıcıEkle_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (textBox1.Text == string.Empty) 
                hata = 1;
            if (textBox2.Text == string.Empty)
                hata = 1;
            if (textBox3.Text == string.Empty)
                hata = 1;

            if (hata == 1)
            {
                MessageBox.Show("bütün alanları doldurunuz", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
            }
            else
            {
                baglanti.Open();
                SqlCommand ekle = new SqlCommand("INSERT INTO kullanıcılar(Ad, Soyad, Sifre) values('" + textBox1.Text+ "', '"+textBox2.Text+ "', '"+textBox3.Text+"')", baglanti);
                int basari = ekle.ExecuteNonQuery();
                baglanti.Close();
                if(basari == 1)
                {
                    MessageBox.Show("kayıt eklendi");
                    Close();
                }

                else
                {
                    MessageBox.Show("kayıt eklenmedi");
                }
            }
        }
    }
}
