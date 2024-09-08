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
    public partial class ŞifreUnuttum : Form
    {
        public ŞifreUnuttum()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source = LAPTOP-VBT65R4T; Initial Catalog = market_otomasyonu; Integrated Security = True");

        private void ŞifreUnuttum_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (textBox1.Text == string.Empty)
              hata = 1;
            if (textBox2.Text == string.Empty)
                hata = 1;
            if (hata ==1)
            {
                MessageBox.Show("alanları doldurun", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                baglanti.Open();
                SqlCommand kmt2 = new SqlCommand("Select sifre from kullanıcılar where ad = '"+textBox1.Text+"' and soyad ='"+textBox2.Text+"'", baglanti);
                SqlDataReader dr2 = kmt2.ExecuteReader();

                if(dr2.Read())
                {
                    string islem = dr2["sifre"].ToString();
                    MessageBox.Show(islem, "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                    textBox1.Clear();
                    textBox2.Clear();
                }
                else
                {
                    MessageBox.Show("hatalı bilgi girdiniz", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    baglanti.Close();
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
        }
    }
}
