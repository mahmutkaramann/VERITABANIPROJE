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
    public partial class Form2 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; " +
          "Database=YeniAracKiralama; user ID=postgres; password=123");
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void Form2_Load(object sender, EventArgs e)
        {
            //commoBox arac: 
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from arac where durum = 'Müsait' " +
                "order by arac_id", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "arac_id";
            comboBox1.ValueMember = "arac_id";
            comboBox1.DataSource = dt;
            baglanti.Close();
        }


        //EKLEME İŞLEMİ
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into musteri(arac_id, musteri_ad," +
                " musteri_soyad, tc_no, telefon, e_posta, adres, ehliyet_no)" +
                "values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(comboBox1.Text));
            komut1.Parameters.AddWithValue("@p2", textBox2.Text);
            komut1.Parameters.AddWithValue("@p3", textBox3.Text);
            komut1.Parameters.AddWithValue("@p4", textBox4.Text);
            komut1.Parameters.AddWithValue("@p5", textBox5.Text);
            komut1.Parameters.AddWithValue("@p6", textBox6.Text);
            komut1.Parameters.AddWithValue("@p7", textBox7.Text);
            komut1.Parameters.AddWithValue("@p8", textBox8.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri ekleme işlemi başarılı olarak gerçekleştirildi.");
        }

        // Müsait Araçlar
        private void button1_Click(object sender, EventArgs e) 
        {
            string sorgu = "select arac.arac_id, arac_marka.marka_ad, arac_model.model_ad,  " +
                "arac.model_yili, arac.kilometre," +
                " durum, kira_ucreti from arac join arac_model on arac.model_id = arac_model.model_id" +
                " join arac_marka on arac_model.marka_id = arac_marka.marka_id where arac.durum = 'Müsait' " +
                "order by arac_id ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = (Form1)Application.OpenForms["Form1"];
            f1.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
