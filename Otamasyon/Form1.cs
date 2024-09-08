using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otamasyon
{
    public partial class Form1 : Form
    {
        private object Interaction;

        public ListBox ListBoxForm1
        {
            get { return lbUrunler; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
            Dictionary<string, double> urunler = new Dictionary<string, double>();

            // Örnek ürünler ekleyin
            urunler["Elma"] = 25;
            urunler["Portakal"] = 23;
            urunler["çilek"] = 30;
            urunler["domates"] = 20;
            urunler["salata"] = 20;
            urunler["erik"] = 30;
            urunler["Marul"] = 25;
            urunler["patlıcan"] = 20;
            urunler["soğan"] = 20;
            urunler["patates"] = 25;
            urunler["mandalina"] = 27;
            urunler["kivi"] = 25;
            urunler["armut"] = 20;
            urunler["kavun"] = 30;
            urunler["Ispanak"] = 26;

            // Toplam fiyatı hesaplayın
            double toplamFiyat = urunler.Sum(urun => urun.Value);

            // ListBox'a öğeleri ekle
            foreach (var urun in urunler)
            {
                lbUrunler.Items.Add($"{urun.Key} - {urun.Value:C2}");
            }

            // Toplam fiyatı da ListBox'a ekleyin
            lbUrunler.Items.Add($"Toplam Fiyat: {toplamFiyat:C2}");
        }

        private void ürünEkleToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            
        }

        private void ürünSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var c = new UrunSil
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false
            };

            c.StartPosition = FormStartPosition.CenterParent;
            c.ShowDialog(this);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            var c = new Giris
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false
            };

            c.StartPosition = FormStartPosition.CenterParent;
            c.ShowDialog(this);
        }

        private void kullanıcıEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var c = new KullanıcıEkle
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false
            };

            c.StartPosition = FormStartPosition.CenterParent;
            c.ShowDialog(this);
        }

        private void meyveSebzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var c = new UrunEkle
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false
            };

            c.StartPosition = FormStartPosition.CenterParent;
            c.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in lbUrunler.SelectedItems)
            {
                listBox2.Items.Add(selectedItem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {          
            double toplamFiyat = 0;

            // listBox2'deki her bir öğeyi işleyin
            foreach (var item in listBox2.Items)
            {
                // Her öğenin fiyatını alarak toplama ekleyin
                string[] parts = item.ToString().Split('-');
                double fiyat = double.Parse(parts[1].Trim().TrimStart('₺')); // Fiyat kısmını al ve başındaki para birimi işaretini kaldır
                toplamFiyat += fiyat;
            }

            // Toplam fiyatı Label4'e yazdır
            label4.Text = $"Toplam Fiyat: {toplamFiyat:C2}";
        }

    

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void GetProductsAndTotalPrice()
        {
            // Veritabanı bağlantı dizesi
            string connectionString = "Data Source=server_adı;Initial Catalog=veritabanı_adı;Integrated Security=True";

            // Ürünlerin ve toplam fiyatın saklanacağı liste
            List<string> products = new List<string>();
            double totalPrice = 0;

            // Veritabanı bağlantısını oluştur
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Veritabanı bağlantısını aç
                connection.Open();

                // Veritabanından ürünleri çekmek için sorgu
                string query = "SELECT urun_adi, urun_fiyati FROM Urunler";

                // Sorguyu yürüt ve sonuçları al
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Her bir satırı oku ve ürünleri ve toplam fiyatı hesapla
                        while (reader.Read())
                        {
                            string urunAdi = reader["urun_adi"].ToString();
                            double urunFiyati = Convert.ToDouble(reader["urun_fiyati"]);

                            products.Add($"{urunAdi} - {urunFiyati:C2}");
                            totalPrice += urunFiyati;
                        }
                    }
                }
            }

            // Ürünleri ListBox1'e ekle
            foreach (string product in products)
            {
                lbUrunler.Items.Add(product);
            }

            // Toplam fiyatı ListBox1'e ekle
            lbUrunler.Items.Add($"Toplam Fiyat: {totalPrice:C2}");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Fiş içeriği
            string fis = "--------------------------\n";
            fis += "Alışveriş Fişi\n";
            fis += "--------------------------\n";

            // Seçili ürünleri fişe ekle
            foreach (var item in listBox2.Items)
            {
                fis += item.ToString() + "\n";
            }

            // Toplam fiyatı fişe ekle
            double toplamFiyat = 0;
            foreach (var item in listBox2.Items)
            {
                string[] parts = item.ToString().Split('-');
                double fiyat = double.Parse(parts[1].Trim().TrimStart('₺'));
                toplamFiyat += fiyat;
            }
            fis += "--------------------------\n";
            fis += $"Toplam Fiyat: {toplamFiyat:C2}\n";
            fis += "--------------------------";

            // Fişi göster
            MessageBox.Show(fis, "Fiş", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ürünlerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (lbUrunler.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili ürünün bilgilerini al
            string seciliUrun = lbUrunler.SelectedItem.ToString();
            string[] urunBilgileri = seciliUrun.Split('-'); // Ürün adı ve stok miktarını al
            string urunAdi = urunBilgileri[0].Trim(); // Ürün adını al

            // Stok miktarını almak için Regex ifadesini kullan
            int stokMiktari = 0;
            Match match = Regex.Match(seciliUrun, @"Stok: (\d+)");
            if (match.Success)
            {
                stokMiktari = int.Parse(match.Groups[1].Value);
            }
            else
            {
                MessageBox.Show("Stok miktarı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Seçilen ürünün stok miktarını göster
            MessageBox.Show($"\"{urunAdi}\" ürününün stok miktarı: {stokMiktari}", "Stok Miktarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    }


