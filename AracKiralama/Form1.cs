using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralama
{
    public partial class Form1 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; " +
           "Database=YeniAracKiralama; user ID=postgres; password=123");
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Fonksiyonlar sayfasına yönlendir
        private void button5_Click(object sender, EventArgs e)
        {
            Fonksiyonlar f3 = new Fonksiyonlar();
            f3.Show();
            this.Hide(); // Form2 kapanır
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       //LİSTELE
        private void button1_Click(object sender, EventArgs e) 
        {
            string sorgu = "select * from musteri order by musteri_id" ;
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        // GÜNCELLE
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sql = "UPDATE arac SET kilometre=@km, kira_ucreti=@ucret WHERE arac_id=@id";
            using (var komut3 = new NpgsqlCommand(sql, baglanti))
            {
                komut3.Parameters.AddWithValue("@km", (int)numericUpDown2.Value);
                komut3.Parameters.AddWithValue("@ucret", (int)numericUpDown3.Value);
                komut3.Parameters.AddWithValue("@id", (int)numericUpDown1.Value);

                komut3.ExecuteNonQuery();
            }
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi başarılı.");

        }

        // SİLME İŞLEMİ
        private void button3_Click(object sender, EventArgs e) 
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("Delete From musteri where musteri_id =@p9", baglanti);
            komut2.Parameters.AddWithValue("@p9", int.Parse(numericUpDown4.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün silme işlemi başarılı olarak gerçekleşti.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide(); // Form2 kapanır
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // TÜM ARAÇLARI LİSTELE
        private void button1_Click_1(object sender, EventArgs e)
        {
            string sorgu = "select arac.arac_id, arac_marka.marka_ad, arac_model.model_ad,  " +
                "arac.model_yili, arac.kilometre," +
                " durum, kira_ucreti from arac join arac_model on arac.model_id = arac_model.model_id" +
                " join arac_marka on arac_model.marka_id = arac_marka.marka_id order by arac_id ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        //  ARAMA İŞLEMİ
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı aç
                baglanti.Open();
                // numericUpDown4'ten musteri_id alınır
                int musteriId = int.Parse(numericUpDown4.Text);
                // Musteriyi çağıracak SQL sorgusu
                string sorgu = "SELECT * from musteri where musteri_id = @musteri_id";
                // Komut oluştur
                NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti);
                cmd.Parameters.AddWithValue("@musteri_id", musteriId);
                // Sonucu DataTable'a al
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                // DataGridView'e ata
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }
    }
}
