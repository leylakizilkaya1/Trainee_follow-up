using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.Data.OleDb kütüphanesinin eklenmesi
using System.Data.OleDb;

namespace StajyerTakip
{
    public partial class Form3 : Form
    {
        public Form3()  ///Form 1'de KULLANICI Seçeneğini seçersek burası açılacak!
        {
            InitializeComponent();

        }
        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim =new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0; Data Source=personel.accdb");

        private void personelleri_goster() //Bu metod sayesinde veritabanındaki tablo datagridview nesnesine yansıtılacak.
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],cinsiyet as[CİNSİYETİ],mezuniyet as[MEZUNİYETİ],dogumtarihi as[DOĞUM TARİHİ],gorevi as[GÖREVİ],gorevyeri as[GÖREV YERİ],maasi as[MAAŞI] from personeller Order By ad ASC", baglantim); //Burada sırayla veritabanında örnegin kullaniciadi ile tanımlanan veriler KULLANICI ADI olarak sırayla listelenecek!!
                DataSet dshafiza = new DataSet(); //Bellekte dshafiza isimli alan ayırdım...
                personelleri_listele.Fill(dshafiza); //Personelleri listele adlı sorgunun sonuçları bellekte oluşturduğum dshafiza adlı alana atıldı.
                dataGridView1.DataSource = dshafiza.Tables[0]; //datagridview dshafiza 'nın 0.tablosuyla doldurdum.
                baglantim.Close();
            }
            catch (Exception hatamsj) //Eğer bir hata oluşmuşsa catch bloğu çalışır.
            {
                MessageBox.Show(hatamsj.Message,"Leyla Kızılkaya Stajyer Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                baglantim.Close();
               
            }
        }
        
        private void Form3_Load(object sender, EventArgs e) 
        {
            personelleri_goster();
            this.Text = "Kullanıcı İşlemleri";
            label19.Text = Form1.adi +" "+Form1.soyadi; //Hangi Kullanıcı Form'den  giriş yaptıysa onun  ad ve soyadı gelir.
            pictureBox1.Height = 150; //Picturebox1'in yüksekliği 150 piksel olsun.
            pictureBox1.Width = 150; //Picturebox1'in genişliği 150 piksel olsun.
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; //Resmi picturebox1'a göre ayarla yani stretch yap...
            pictureBox1.BorderStyle=BorderStyle.Fixed3D;
            pictureBox2.Height = 150; pictureBox2.Width = 150; 
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage; //Resmi picturebox2'a göre ayarla yani stretch yap...
            pictureBox2.BorderStyle=BorderStyle.Fixed3D;
            try
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");
            }
            catch
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.png");
            
            }
            maskedTextBox1.Mask = "00000000000";

        }

        private void button1_Click(object sender, EventArgs e) //ARA Butonu Kodlar
        {
            bool kayit_arama_durumu = false;
            if (maskedTextBox1.Text.Length == 11) //11 haneli tc kimlik no girilmişse
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select*from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim); //maskedtextbox1'e girilen tc kimlik numarasına ilişkin tablodaki tüm kayıtları getir dedim...
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader(); //selectsorgu sonucunda gelen verileri datareader yani veri okuyucuya aktar...
                while (kayitokuma.Read()) //girilen tcye dair kayıta rastlanmışsa burası çalışır...
                {
                    kayit_arama_durumu = true;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0) + ".jpg"); //picturebox1'e hangi resmin seçileceğini belirledim,Bindeki debug klasörünün alt klasörü olan personelresimler'e gelen kaydın 0.sütuna göre getir(tcno'ya göre)


                    }
                    catch (Exception) //Resim yüklenememişse burası çalışır.
                    {

                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.png"); //resimyok gözükür.
                    }

                    label10.Text = kayitokuma.GetValue(1).ToString(); //label10 eğer veritabanındaki 1. alana(ada)eşitse stringe dönüştür getir...
                    label11.Text = kayitokuma.GetValue(2).ToString(); //label11 eğer veritabanındaki 2. alana(soyada)eşitse stringe dönüştür getir...
                    if (kayitokuma.GetValue(3).ToString() == "Bay")  //3.alan eğer baya eşitse 
                        label12.Text = "Bay"; //label 12(cinsiyete) bay yazdır.
                    else    //3.alan eğer bayana eşitse 
                        label12.Text = "Bayan";    //cinsiyete bayan yazdır.

                    label13.Text = kayitokuma.GetValue(4).ToString(); // //label13 eğer veritabanındaki 4. alana(mezuniyete)eşitse stringe dönüştür getir...
                    label14.Text = kayitokuma.GetValue(5).ToString(); //label14 eğer veritabanındaki 5. alana(dogumtarihine)eşitse stringe dönüştür getir...
                    label15.Text = kayitokuma.GetValue(6).ToString(); //label15 eğer veritabanındaki 6. alana(gorevine)eşitse stringe dönüştür getir...
                    label16.Text = kayitokuma.GetValue(7).ToString(); //label16 eğer veritabanındaki 7. alana(gorevyerine)eşitse stringe dönüştür getir...
                    label17.Text = kayitokuma.GetValue(8).ToString(); //label17 eğer veritabanındaki 8. alana(maasine)eşitse stringe dönüştür getir...
                    break;
                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Aranan kayıt maalesef bulunamadı!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();

            }
            else //Eğer tc 11 haneli girilmemişse
                MessageBox.Show("Lütfen 11 haneli TC Kimlik No giriniz!", "Leyla Kızılkaya Stajyer Takip Programı",MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

      
    }
}


