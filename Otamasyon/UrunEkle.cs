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
    public partial class UrunEkle : Form
    {
        public UrunEkle()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection("Data Source = LAPTOP-VBT65R4T; Initial Catalog = market_otomasyonu; Integrated Security = True");
        //internal decimal Price;

        private void UrunEkle_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int hata = 0;

            // Alanların doluluk kontrolü...

            if (hata == 1)
            {
                MessageBox.Show("Bütün alanları doldurunuz", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    // Veritabanına ekleme işlemi...

                    // Form1'deki ListBox'a eklenen ürünü aktarma
                    if (Application.OpenForms["Form1"] is Form1 form1)
                    {
                        form1.ListBoxForm1.Items.Add(textBox1.Text);
                    }

                    MessageBox.Show("Kayıt eklendi");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }

            }
            }
        }
    }

