using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.OleDb kütüphanesinin eklenmesi
using System.Data.OleDb;
namespace StajyerTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0; Data Source=personel.accdb");
        //Formlar arası veri aktarımında kullanılacak değişkenler
        public static string tcno, adi, soyadi, yetki;

        private void button1_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select*from kullanicilar", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString(); //eğer tcno veritabanında 0. alana eşitse değeri al.
                            adi = kayitokuma.GetValue(1).ToString(); //eğer adi veritabanında 1. alana eşitse değeri al.
                            soyadi = kayitokuma.GetValue(2).ToString(); //eğer soyadi veritabanında 2. alana eşitse değeri al.
                            yetki = kayitokuma.GetValue(3).ToString();   //eğer yetki veritabanında 3. alana eşitse değeri al.
                            this.Hide(); //başarılı old. için formu gizle
                            Form2 frm2 = new Form2();
                            frm2.Show(); //frm2 nesnesini göster.
                            break;
                        }
                    }
                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString(); //eğer tcno veritabanında 0. alana eşitse değeri al.
                            adi = kayitokuma.GetValue(1).ToString(); //eğer adi veritabanında 1. alana eşitse değeri al.
                            soyadi = kayitokuma.GetValue(2).ToString(); //eğer soyadi veritabanında 2. alana eşitse değeri al.
                            yetki = kayitokuma.GetValue(3).ToString();   //eğer yetki veritabanında 3. alana eşitse değeri al.
                            this.Hide(); //başarılı old. için formu gizle
                            Form3 frm3 = new Form3();
                            frm3.Show(); //frm3 nesnesini göster.
                            break;
                        }
                    }
                }
                if (durum == false)
                    hak--;
                baglantim.Close();
            }
            label5.Text=Convert.ToString(hak);
            if (hak == 0) 
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş Hakkı Kalmadı!","Leyla Kızılkaya Stajyer Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
        }

        //Yerel yalnizca sadece bu formda kullanılacak değişkenler
        int hak = 3; bool durum = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi...";
            this.AcceptButton = button1; this.CancelButton = button2; //enter tuşuna basıldığında button1 (giriş),esc ye basıldığında button2(çıkış) tuşu çalışır.
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen; //ekranın merkezinde gelmesi
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow; //Tam ekran ve simge durumuna küçültme tuşları pasif hale gelsin

        }
    }
}

    