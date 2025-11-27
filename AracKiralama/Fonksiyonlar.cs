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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AracKiralama
{
    public partial class Fonksiyonlar : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; " +
           "Database=YeniAracKiralama; user ID=postgres; password=123");
        public Fonksiyonlar()
        {
            InitializeComponent();
        }

        private void Fonksiyonlar_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = (Form1)Application.OpenForms["Form1"];
            f1.Show();
            this.Close();
        }


        // FONKSİYON-1
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı aç
                baglanti.Open();
                // numericUpDown1'den arac_id alınır
                int aracId = int.Parse(numericUpDown1.Text);
                // Fonksiyonu çağıracak SQL sorgusu
                string sorgu = "SELECT fn_arac_toplam_hasar(@arac_id) AS Toplam_Hasar";
                // Komut oluştur
                NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti);
                cmd.Parameters.AddWithValue("@arac_id", aracId);
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

        // FONKSİYON-2
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı aç
                baglanti.Open();
                // Fonksiyonu çağıracak SQL sorgusu
                string sorgu = "SELECT * FROM fn_kiralama_bitimine_iki_gun()";
                // Komut oluştur
                NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti);
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

        // FONKSİYON-3
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı aç
                baglanti.Open();
                // Fonksiyonu çağıracak SQL sorgusu
                string sorgu = "SELECT * FROM UcuzAraclar() order by arac_id";
                // Komut oluştur
                NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti);
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

        // FONKSİYON-4
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı aç
                baglanti.Open();
                // numericUpDown2'den sube_id alınır
                int subeId = int.Parse(numericUpDown2.Text);
                // Fonksiyonu çağıracak SQL sorgusu
                string sorgu = "SELECT fn_sube_ortalama_ucret(@sube_id) AS ortalama_ucret";
                // Komut oluştur
                NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti);
                cmd.Parameters.AddWithValue("@sube_id", subeId);
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
