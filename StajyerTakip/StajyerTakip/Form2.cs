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
//System.Text.RegularExpression(Regex) kütüphanesinin eklenmesi
using System.Text.RegularExpressions; //güvenli parola için kullanılır.
//Giriş-çıkış işlemlerine ilişkin kütüphanesinin eklenmesi
using System.IO;



namespace StajyerTakip
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //Veritabanı dosya yolu ve Provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");
        private void kullanicilari_goster() //veritabanındaki tabloyu datagrid nesnesine getirmeye yarayan metot
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],yetki AS[YETKİ],kullaniciadi AS[KULLANICI ADI],parola AS[PAROLA] from kullanicilar Order By ad ASC", baglantim);//Burada sırayla veritabanında örnegin kullaniciadi ile tanımlanan veriler KULLANICI ADI olarak sırayla listelenecek!!

                DataSet dshafiza = new DataSet();  //bellekte bir alan oluşturuldu.
                kullanicilari_listele.Fill(dshafiza); //sorgunun sonuçlarıyla o alan dolduruldu.
                dataGridView1.DataSource = dshafiza.Tables[0]; //Sonuçta gelen ilk tabloyu datasource aktar.
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();

            }
        }
        private void personelleri_goster() //veritabanındaki tabloyu datagrid nesnesine getirmeye yarayan metot
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],cinsiyet AS[CİNSİYET],mezuniyet AS[MEZUNİYETİ],dogumtarihi AS[DOĞUM TARİHİ],gorevi AS[GÖREVİ],gorevyeri AS[GÖREV YERİ],maasi AS[MAAŞI] from personeller Order By ad ASC", baglantim);
                DataSet dshafiza = new DataSet();  //bellekte bir alan oluşturuldu.
                personelleri_listele.Fill(dshafiza); //sorgunun sonuçlarıyla o alan dolduruldu.
                dataGridView2.DataSource = dshafiza.Tables[0]; //Sonuçta gelen ikinci tabloyu datasource aktar.
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //FORM2 AYARLARI
            pictureBox1.Height = 150;
            pictureBox1.Width = 150; //resmin genişliği 150 yüksekliği 150 olsun yani kare resim...
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; //Resmi picturebox'a göre ayarla yani stretch yap...

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".png");
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.png");
            }
            //KULLANICI İŞLEMLERİ SEKMESİ AYARLARIM
            this.Text = "YÖNETİCİ İŞLEMLERİ";
            label11.ForeColor = Color.DarkRed;
            label11.Text = Form1.adi + " " + Form1.soyadi;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "TC Kimlik No 11 Karakter Olmalı!"); //uyarı mesajı verildi...
            radioButton1.Checked = true;
            textBox2.CharacterCasing = CharacterCasing.Upper; //textbox2 karakter kontrolü yapıldı... //küçük harfde yazılsa büyük harfe çevirir.
            textBox3.CharacterCasing = CharacterCasing.Upper; //textbox3 karakter kontrolü yapıldı... //küçük harfde yazılsa büyük harfe çevirir.
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100; //progress bar'ı 100'e böl.
            kullanicilari_goster();

            //PERSONEL İŞLEMLERİ SEKMESİ AYARLARIM
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100; pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            maskedTextBox1.Mask = "00000000000";
            maskedTextBox2.Mask = "LL????????????????????"; //Burada bir regex ifadesi kullandım,en fazla 22 karakter girilebilsin fakat "LL" ile belirtilen iki karakter zorunlu olsun...
            maskedTextBox3.Mask = "LL????????????????????";
            maskedTextBox4.Mask = "0000"; //1000 ile 10.000 arası maaş zorunlu...
            maskedTextBox4.Text = "0";
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();

            comboBox1.Items.Add("İlköğretim"); comboBox1.Items.Add("Ortaöğretim"); comboBox1.Items.Add("Lise"); comboBox1.Items.Add("Üniversite");
            comboBox2.Items.Add("Yönetici"); comboBox2.Items.Add("Memur");
            comboBox3.Items.Add("ARGE"); comboBox3.Items.Add("Bilgi İşlem"); comboBox3.Items.Add("Muhasebe"); comboBox3.Items.Add("Üretim"); comboBox3.Items.Add("Donanım");

            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy")); //yıl alındı ve int türünde değişkende saklandı.
            int ay = int.Parse(zaman.ToString("MM")); //ay alındı ve int türünde değişkende saklandı.
            int gun = int.Parse(zaman.ToString("dd")); //gün alındı ve int türünde değişkende saklandı.

            dateTimePicker1.MinDate = new DateTime(1960, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gun); //18 yaşından küçük alınamaz yil-18...
            dateTimePicker1.Format = DateTimePickerFormat.Short; //kısa tarih görünsün dedim.
            radioButton3.Checked = true; //cinsiyet bay da seçili hale gelsin.
            personelleri_goster();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)
                errorProvider1.SetError(textBox1, "TC Kimlik No 11 karakter olmalı!"); //11 rakamdan fazla girilirse uyarı verir...
            else
                errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8) //eğer klavyeden basılan tuş (48-57 arası yani 0 ile 9 rakam arası olacak) ve backspace tuşuna da basılabilir.(8 backspace tuşuna karşılık gelir... ) //tcnoya sadece rakam girebiliriz.
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;  //klavyeden basılan tuş eger harfse,backspace tuşuna basıldıysa boşluğa da basıldıysa tuşları aktif et...    
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;  //klavyeden basılan tuş eger harfse,backspace tuşuna basıldıysa boşluğa da basıldıysa tuşları aktif et...  

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 8)
                errorProvider1.SetError(textBox4, "Kullanıcı Adı 8 karakter olmalı!");
            else
                errorProvider1.Clear(); // Eğer kullanıcı adı uzunluğumuz 8 karakter olmamışsa uyarı belirt,uyarı da "Kullanıcı Adı 8 karakter olmalı!" diyorum.
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true; //Burada eğer harfe,backspace tuşuna veya sayıya basılmışsa bunlara izin verdim..."
        }
        int parola_skoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)  //kullanıcının oluşturduğu parolaya göre bu skor değişkeni çeşitli değer alacak...
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text; //PAROLANIN GÜVENLİ OLUP OLMADIĞI REGEX KÜTÜPHANESİNİ KULLANARAK BELİRLENİR. //Regex kütüphanesi İngilizce karakterleri baz aldığı için,Türkçe karakterlerde sorun yaşamamak için şifre string ifadesindeki Türkçe karakterleri İngilizce karakterlere dönüştürmek gerekir...
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre; //duzeltilmiş sifreyi sifreye atadım.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I'); //Büyük İ harfi büyük I harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i'); // Küçük ı harfi küçük i harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C'); //Büyük Ç harfi büyük C harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c'); // Küçük ç harfi küçük c harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S'); //Büyük Ş harfi büyük S harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's'); // Küçük ş harfi küçük s harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G'); //Büyük Ğ harfi büyük G harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g'); // Küçük ğ harfi küçük g harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U'); //Büyük Ü harfi büyük U harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u'); // Küçük ü harfi küçük u harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O'); //BÜyük Ö harfi büyük O harfine dönüştürüldü.
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o'); //Küçük ö harfi küçük o harfine dönüştürüldü.

            if (sifre != duzeltilmis_sifre) //değişiklik yapıldıysa
            {
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!"); //Örneğin parolaya "Murat" yazacağız uygulamam direkt uyarı verecek,çünkü ingilizcede "u" harfi yok!!!
            }
            //Eğer parolada 1 tane küçük harf varsa 10 puan,2 ve üzeri ise 20 puan verilecek(Skor değişkenime)
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length; //sifre string ifadesinden küçük harf olanları sil. Şifrenin toplam uzunluğundan küçük harf olanların uzunluğu silinirse sadece küçük harf olanların uzunluğu kalır!
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10; //örneğin 1 küçük harf kullanırsa;1*10=10 old için skor 10;2 küçük harf kullanırsak 2*10=20 skor olur...

            //Eğer parolada 1 tane büyük harf varsa 10 puan,2 ve üzeri ise 20 puan verilecek(Skor değişkenime)
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length; //sifre string ifadesinden büyük harf olanları sil. Şifrenin toplam uzunluğundan büyük harf olanların uzunluğu silinirse sadece büyük harf olanların uzunluğu kalır!
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10; //örneğin 1 büyük harf kullanırsa;1*10=10 old için skor 10;2 büyük harf kullanırsak 2*10=20 skor olur...

            //Eğer parolada 1 tane rakam varsa 10 puan,2 ve üzeri ise 20 puan verilecek(Skor değişkenime)
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length; //sifre string ifadesinden rakam olanları sil. Şifrenin toplam uzunluğundan rakam olan ifadelerin uzunluğu silinirse sadece rakam uzunluğu kalır!
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10; //örneğin 1 rakam kullanırsa;1*10=10 old için skor 10;2 rakam kullanırsak 2*10=20 skor olur...

            //Eğer parolada 1 tane sembol varsa 10 puan,2 ve üzeri ise 20 puan verilecek(Skor değişkenime)
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi; //sifre string ifadesinden küçük harf,büyük harf rakam olanları sil. Şifrenin toplam uzunluğundan bunlar silinirse sadece sembol uzunluğu kalır!
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;//örneğin 1 sembol kullanırsa;1*10=10 old için skor 10;2 rakam kullanırsak 2*10=20 skor olur...

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru; //eğer hepsinde 2 ve üzeri girersek her birinden skor 20 olacak ve 20*4 =80 olacak,skor 100 üzerinden hesaplandığı için 100'e tamamlamak için bir if bloğu oluşturuyoruz...

            if (sifre.Length == 9) //Eğer sifre uzunluğum 9 karaktere eşitse parola skoruna 10 puan  ekle.
                parola_skoru += 10;
            else if (sifre.Length == 10) ////Eğer sifre uzunluğum 10 karaktere eşitse parola skoruna 20 puan ekle.
                parola_skoru += 20;

            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0) //Eğer girilen parolada hiç küçük harf,büyük harf ve rakam kullanılmamışsa kullanıcıya mesaj verdim.
                label22.Text = "Büyük harf,küçük harf ve rakam mutlaka kullanmalısınız!";
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0) //Eğer küçük harf,büyük harf ve rakam kullanılmışsa ;
                label22.Text = ""; //label22 text kısmını silecek,uyarı ortadan kaldırılmış olur.

            if (parola_skoru < 70) //EĞER PAROLA SKORUMUZ 70 DEĞERİNDEN KÜÇÜKSE UYGULAMAMIN VERDİĞİ MESAJ ""GİRİLEN ŞİFRE KABUL EDİLEMEZ!" OLUR.
                parola_seviyesi = "GİRİLEN ŞİFRE KABUL EDİLEMEZ!";
            else if (parola_skoru == 70 || parola_skoru == 80) //EĞER PAROLA SKORUMUZ 70 DEĞERİNE EŞİT VE 80'E EŞİTSE UYGULAMAMIN VERDİĞİ MESAJ ""GİRİLEN ŞİFREGİRİLEN ŞİFRE GÜÇLÜ!" OLUR.
                parola_seviyesi = "GİRİLEN ŞİFRE GÜÇLÜ!";
            else if (parola_skoru == 90 || parola_skoru == 100) //EĞER PAROLA SKORUMUZ 90 DEĞERİNE EŞİT VE 100'E EŞİTSE UYGULAMAMIN VERDİĞİ MESAJ ""GİRİLEN ŞİFRE ÇOK GÜÇLÜ" OLUR.
                parola_seviyesi = "GİRİLEN ŞİFRE ÇOK GÜÇLÜ!";

            label9.Text = "%" + Convert.ToString(parola_skoru); //label9'a(SKOR) parola skorunu yazdır.
            label10.Text = parola_seviyesi;  //label10'a(SEVİYE) parola seviyesini yazdır.
            progressBar1.Value = parola_skoru; //Ben progress bar'ı 100 parçaya bölmüştüm yani 100 üzerinden değerlendirme olacaktı.parola skorunun ne kadar güçlü olduğunu kullanıcı bu barda görecek...

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text) //parola ile parola tekrar kısmı eşleşmiyorsa;
                errorProvider1.SetError(textBox6, "PAROLA TEKRARI EŞLEŞMİYOR,KONTROL EDİNİZ!"); //textbox6 'nın hemen yanına bir hata ver ve bu hata mesajı kırmızı ile yazılan olsun...
            else
                errorProvider1.Clear(); //Eğer eşleşiyorsa bu hatayı sil dedim...

        }

        private void topPage1_temizle() //TEXTBOX'ların içini temizle dedim.
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
        private void topPage2_temizle()
        {
            pictureBox2.Image = null;
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            maskedTextBox4.Clear();
            comboBox1.SelectedIndex = -1; //seçili olan indexin içini temizle dedim...
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

        }
        private void button1_Click(object sender, EventArgs e) //KAYDET BUTONUNA YAZDIĞIM KODLAR
        {
            string yetki = "";
            bool kayitkontrol = false; //Daha önceden kullanıcı kaydı var mı diye kontrol etmek için false atadık...

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select*from kullanicilar where tcno='" + textBox1.Text + "'", baglantim); //Kullanıcılar tablosundan kayıt al dedim bu kayıt da tcnosu textBox1'e eşit olan kayıtları getir dedim...

            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();

            if (kayitkontrol == false) //bu durumda kayıt yaptırılır veya hatalı yerler kullancıya gösterilir.
            {
                //Girilen TC Kimlik No Kontrolü
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red; //TC Kimlik No 11 karakterden küçük girilirse label1 yani "tc kimlik no" yazısı kırmızı olur...
                else
                    label1.ForeColor = Color.Black; //TC Kimlik No 11 karakter olarak girilirse label1 yani "tc kimlik no" yazısı siyah olur...

                //Adı kısmı veri kontrolü
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;

                //Soyadı kısmı veri kontrolü
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;

                //Kullanıcı adı kısmı veri kontrolü
                if (textBox4.Text.Length != 8 || textBox4.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;

                //Parola kısmı veri kontrolü
                if (textBox5.Text == "" || parola_skoru < 70)
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;


                //Parola tekrar kısmı veri kontrolü 
                if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70) //textbox1 11 karakterden oluşuyorsa ve boş değilse,textbox2 boş değilse ve textbox2 'nin uzunluğu 1 karakterden büyükse ...bu böylece text6'ya kadar gidecek.textbox5(parola) ile textbox6 (parola tekrarı) birbirine eşitse ve parola skoru 70ten büyükse kayıt işlemleri gerçekleşicek.
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)

                        yetki = "Kullanıcı";

                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanicilar values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "', '" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')", baglantim); //tabloya insert komutu ile veri ekledik.

                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();



                    }


                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message); //EĞER HER ŞEY DÜZGÜN İSE KAYIT GERÇEKLEŞTİRİLİR.
                        baglantim.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları tekrar gözden geçiriniz!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Girilen Tc Kimlik Numarası ile daha önceden kayıt yapılmıştır,yeniden deneyiniz!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)  //Ara butonu için kodlar yazdım.
        {
            bool kayit_arama_durumu = false;
            if (textBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select*from kullanicilar where tcno='" + textBox1.Text + "'", baglantim); //kullanıcılar tablosundaki tüm alanları seç ve acces veritabanındaki tcno alanı textbox1'e eşit olan kayıtları getir dedim.
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader(); //select sorgusunun sonucunu kayitokuma isimli datareader nesnesine tanımla.

                while (kayitokuma.Read()) //eğer herhangi bir kayıt gelmişse;while döngüsü çalışır..

                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString(); //tablodaki isim bilgisinin text box2'ye yazılmasını sağladım.
                    textBox3.Text = kayitokuma.GetValue(2).ToString(); //tablodaki isim bilgisinin text box2'ye yazılmasını sağladım.
                    if (kayitokuma.GetValue(3).ToString() == "Yönetici") //tablodaki ücüncü değer yani yetki yöneticiye eşit ise
                        radioButton1.Checked = true; //radiobutton1'i seç yani yönetici yi işaretle...
                    else
                        radioButton2.Checked = true; //yöneticiye eşit değilse radiobutton2'i seç yani kullanıcıyı işaretle...
                    textBox4.Text = kayitokuma.GetValue(4).ToString(); // tablodaki kullaniciadi kısmını(4.) textbox4'e yazdır...
                    textBox5.Text = kayitokuma.GetValue(5).ToString(); // tablodaki parola kısmını(5.) textbox5'e yazdır...
                    textBox6.Text = kayitokuma.GetValue(5).ToString(); // tablodaki parola kısmını(5.) textbox6'e yazdır...
                    break;
                }
                if (kayit_arama_durumu == false) //eğer herhangi bir kayıt gelmemişse hata mesajı verdim...

                    MessageBox.Show("Aramaya çalıştığınız kayıt bulunamadı!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }
            else
            {
                MessageBox.Show("Lütfen 11 haneli bir Tc Kimlik No giriniz! ", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();
            }

        }

        private void button3_Click(object sender, EventArgs e) //Kayıt Güncelleme için Kodlar
        {
            string yetki = "";


            //Girilen TC Kimlik No Kontrolü
            if (textBox1.Text.Length < 11 || textBox1.Text == "")
                label1.ForeColor = Color.Red; //TC Kimlik No 11 karakterden küçük girilirse label1 yani "tc kimlik no" yazısı kırmızı olur...
            else
                label1.ForeColor = Color.Black; //TC Kimlik No 11 karakter olarak girilirse label1 yani "tc kimlik no" yazısı siyah olur...

            //Adı kısmı veri kontrolü
            if (textBox2.Text.Length < 2 || textBox2.Text == "")
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.Black;

            //Soyadı kısmı veri kontrolü
            if (textBox3.Text.Length < 2 || textBox3.Text == "")
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.Black;

            //Kullanıcı adı kısmı veri kontrolü
            if (textBox4.Text.Length != 8 || textBox4.Text == "")
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.Black;

            //Parola kısmı veri kontrolü
            if (textBox5.Text == "" || parola_skoru < 70)
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.Black;


            //Parola tekrar kısmı veri kontrolü 
            if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                label7.ForeColor = Color.Red;
            else
                label7.ForeColor = Color.Black;

            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70) //textbox1 11 karakterden oluşuyorsa ve boş değilse,textbox2 boş değilse ve textbox2 'nin uzunluğu 1 karakterden büyükse ...bu böylece text6'ya kadar gidecek.textbox5(parola) ile textbox6 (parola tekrarı) birbirine eşitse ve parola skoru 70ten büyükse kayıt işlemleri gerçekleşicek.
            {
                if (radioButton1.Checked == true)
                    yetki = "Yönetici";
                else if (radioButton2.Checked == true)

                    yetki = "Kullanıcı";

                try
                {
                    baglantim.Open();
                    OleDbCommand güncellekomutu = new OleDbCommand("update kullanicilar set ad='" + textBox2.Text + "' ,soyad='" + textBox3.Text + "',yetki='" + yetki + "',kullaniciadi='" + textBox4.Text + "',parola='" + textBox5.Text + "'where tcno='" + textBox1.Text + "'", baglantim); //sırasıyla update komutu ile güncelleme gerçekleştirildi.Örneğin kullanici adi text box 4 e güncellendi.// tcno alanı textbox1'e eşit olan kayıtlar güncellenecek!! //EĞER WHERE YAZMAZSAK BİR KAYDI DEĞİŞİNCE HEPSİ DEĞİŞİR...

                    güncellekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    kullanicilari_goster();

                }


                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error); //EĞER HER ŞEY DÜZGÜN İSE KAYIT GERÇEKLEŞTİRİLİR.
                    baglantim.Close();
                }

            }
            else
            {
                MessageBox.Show("Yazı rengi kırmızı olan alanları tekrar gözden geçiriniz!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button4_Click(object sender, EventArgs e)  //SİL BUTONU KODLARI
        {
            if (textBox1.Text.Length == 11) //TEXTBOX1 uzunluğu 11'e eşit ise sil..
            {
                bool kayit_arama_durumu = false;
                baglantim.Open();
                OleDbCommand secmesorgusu = new OleDbCommand("select*from kullanicilar where tcno='" + textBox1.Text + "'", baglantim); //tc kimlik nosu textbox1'e eşit olan kayıtlar kullanicilar tablosundan gelir...
                OleDbDataReader kayitokuma = secmesorgusu.ExecuteReader(); //sorgunun sonuçlarını kayitokuma data reader'ine attim...

                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true; //Kayıt varsa
                    OleDbCommand deletesorgusu = new OleDbCommand("select*from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                    deletesorgusu.ExecuteNonQuery(); //veritablosundaki değişiklikler yapılır(sorguyla alakalı)...
                    MessageBox.Show("Kullanıcı kaydı silindi!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    kullanicilari_goster();
                    topPage1_temizle();
                    break;
                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Silinecek kayıt bulunamadı!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                topPage1_temizle();
            }
            else
                MessageBox.Show("Lütfen 11 karakterden oluşan bir TC KİMLİK NO giriniz!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void button5_Click(object sender, EventArgs e)  //Formu temizle butonu kodları
        {

            topPage1_temizle(); //Kullanıcı herhangi bir durumda bütün formun temizlenmesini isteyebilir.O zaman bu kod çalışır...
        }

        private void button6_Click(object sender, EventArgs e)  //Gözat Butonu Kodları
        {
            OpenFileDialog resimsec = new OpenFileDialog(); //resim seçme nesnesi oluşturuldu.
            resimsec.Title = "Lütfen bir personel resmi seçiniz."; //Resim seç nesnesiyle bir resim seçek istediğimizde ekrana gelen başlık bu şekildedir...
            resimsec.Filter = "JPG Dosyalar(*.jpg) | *.jpg"; //Sadece jpg uzantılı resim dosyalarını ekrana getirmemizi sağlar...

            if (resimsec.ShowDialog() == DialogResult.OK) //Eğer bu işlem olmuşsa
            {

                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile());  //Seçtiğimiz resmin picturebox2'ye basılmasını sağladım...


            }

        }

        private void button8_Click(object sender, EventArgs e)  //Personel işlemleri sekmesi kaydet butonu kodları
        {
            string cinsiyet = "";
            bool kayitkontrol = false;
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select*from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim); //veritabanındaki tcno alanlar maskedtextbox1'e eşit olanlar gelsin...
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())  //kayıtlı personel varsa döngü çalışır.
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();

            if (kayitkontrol == false)  //kayıtlı personel yoksa
            {
                if (pictureBox2.Image == null) //picture box boşşa resim yüklenmemişse
                    button6.ForeColor = Color.Red; //resim yüklenmemişse gözat(button6) butonunu kırmızı yap dedim...
                else
                    button6.ForeColor = Color.Black; //yüklenmişse siyah olsun...

                if (maskedTextBox1.MaskCompleted == false) //masked tcno  tamamlanmamışsa kurala uyulmamışsa
                    label13.ForeColor = Color.Red; //kırmızı yap...
                else
                    label13.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (maskedTextBox2.MaskCompleted == false) //masked2 ad tamamlanmamışsa kurala uyulmamışsa
                    label14.ForeColor = Color.Red; //kırmızı yap...
                else
                    label14.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (maskedTextBox3.MaskCompleted == false) //masked3 soyad alanı tamamlanmamışsa kurala uyulmamışsa
                    label15.ForeColor = Color.Red; //kırmızı yap...
                else
                    label15.ForeColor = Color.Black;  //yüklenmişse siyah olsun..


                if (comboBox1.Text == "") //mezuniyet alanı boşsa
                    label17.ForeColor = Color.Red; //kırmızı yap...
                else
                    label17.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (comboBox2.Text == "") //görevi alanı
                    label19.ForeColor = Color.Red; //kırmızı yap...
                else
                    label19.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (comboBox3.Text == "") //görev yeri
                    label20.ForeColor = Color.Red; //kırmızı yap...
                else
                    label20.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (maskedTextBox4.MaskCompleted == false)  //maas alanı
                    label21.ForeColor = Color.Red; //kırmızı yap...
                else
                    label21.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (int.Parse(maskedTextBox4.Text) < 1000)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false) //resim yüklenmişse ve 11 karakterli tcno girilmesi kuralına uyulmuşsa ve diğer tüm kurallara uyulmuşsa
                {
                    if (radioButton3.Checked == true)
                        cinsiyet = "Bay";
                    else if (radioButton4.Checked == true)
                        cinsiyet = "Bayan";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into personeller values('" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "', '" + cinsiyet + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + maskedTextBox4.Text + "')", baglantim); //böylece veritabanındaki alanlara teker teker yazdırdım...
                        eklekomutu.ExecuteNonQuery(); //sorgunun sonuçları veritabanına işlendi...
                        baglantim.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\personelresimler")) //Application.StartupPath her zaman bin'deki debug klasörünü tanımlar!! bunun içinde personel resimler klasörü yoksa dedim...
                            Directory.CreateDirectory(Application.StartupPath + "\\personelresimler"); //eğer böyle bir klasör yoksa oluşturulsun dedim...
                        else
                            pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\" + maskedTextBox1.Text + ".jpg"); //eğer böyle bir resim varsa debug daki personel resimler klasörüne tcno'ya ve jpg uzantılı olmasına göre kopyaladım...

                        MessageBox.Show("Yeni personel kaydı başarıyla oluşturuldu.", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        personelleri_goster();
                        topPage2_temizle();
                        maskedTextBox4.Text = "0";


                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }

                }
                else  //Eğer kayıt için if blogundaki sartlar saglanmamışsa burası çalışacak...
                {
                    MessageBox.Show("Lütfen yazı rengi kırmızı olan alanları yeniden gözden geçiriniz!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else //Kayitkontrol true olmuşsa yani daha önceden kayit oluşmuşsa burası çalışır.
            {
                MessageBox.Show("Girilen TC Kimlik Numarası daha önceden kayıtlıdır,lütfen tekrar kontrol ediniz!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void button7_Click(object sender, EventArgs e)  //ARA BUTONU KODLARI
        {
            bool kayit_arama_durumu = false;

            if (maskedTextBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select*from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader(); //sorgu sonuçları bellekte kayitokuma adında saklanacak!
                while (kayitokuma.Read()) //Eğer kayıt bulunmuşsa
                {
                    kayit_arama_durumu = true;
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0).ToString() + ".jpg");  //Eğer kayıt varsa hedef yolu yazdık debug klasörüne baktık sonra personel resimler klasörüne baktık daha sonra tablonun 0. alanı(tc no) ile eşleşen alanları string'e çevirip pictureBox2 'ye bastırdım...


                    }
                    catch
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.png"); //kayıt yoksa resim yok isimli fotoğraf gelir.
                    }

                    maskedTextBox2.Text = kayitokuma.GetValue(1).ToString(); //masked text box2 eğer veritabanındaki 1. alana(ada)eşitse stringe dönüştür getir...
                    maskedTextBox3.Text = kayitokuma.GetValue(2).ToString(); ////masked text box3 eğer veritabanındaki . alana(soyada)eşitse stringe dönüştür getir...
                    if (kayitokuma.GetValue(3).ToString() == "Bay") //veritabanındaki 3. alan(cinsiyet) baya eşitse
                        radioButton3.Checked = true; //radioButton3'ü (Bay seçeneğini) seç dedim...
                    else //veritabanındaki 3. alan(cinsiyet) bayana eşitse
                        radioButton4.Checked = true;  //radioButton4'ü (Bayan seçeneğini) seç dedim..

                    comboBox1.Text = kayitokuma.GetValue(4).ToString(); //mezuniyet alanını çektim...
                    dateTimePicker1.Text = kayitokuma.GetValue(5).ToString(); //Kişinin doğum tarihi de datetimePicker1'de görünür...
                    comboBox2.Text = kayitokuma.GetValue(6).ToString(); //Kişinin görevini de comboBox2'de görüntülenmesini sağladım,veritabanımdan 6.sütunu çektim...
                    comboBox3.Text = kayitokuma.GetValue(7).ToString(); //Kişinin görev yerinin de comboBox3'de görüntülenmesini sağladım,veritabanımdan 7.sütunu çektim...
                    maskedTextBox4.Text = kayitokuma.GetValue(8).ToString(); ////Kişinin maaşının da maskedtextbox4'de görüntülenmesini sağladım,veritabanımdan 8.sütunu çektim...
                    break;


                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Aradığınız kayıt bulunamadı,lütfen tekrar deneyiniz!", "Leyla Kızılkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();


            }
            else
            {
                MessageBox.Show("Lütfen 11 haneli TC Kimlik No girip,yeniden deneyiniz!", "Leyla Kıızlkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }



        }

        private void button9_Click(object sender, EventArgs e) //GÜNCELLE BUTONU KODLARI
        {
            string cinsiyet = "";
            
                if (pictureBox2.Image == null) //picture box boşşa resim yüklenmemişse
                    button6.ForeColor = Color.Red; //resim yüklenmemişse gözat(button6) butonunu kırmızı yap dedim...
                else
                    button6.ForeColor = Color.Black; //yüklenmişse siyah olsun...

                if (maskedTextBox1.MaskCompleted == false) //masked tcno  tamamlanmamışsa kurala uyulmamışsa
                    label13.ForeColor = Color.Red; //kırmızı yap...
                else
                    label13.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (maskedTextBox2.MaskCompleted == false) //masked2 ad tamamlanmamışsa kurala uyulmamışsa
                    label14.ForeColor = Color.Red; //kırmızı yap...
                else
                    label14.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (maskedTextBox3.MaskCompleted == false) //masked3 soyad alanı tamamlanmamışsa kurala uyulmamışsa
                    label15.ForeColor = Color.Red; //kırmızı yap...
                else
                    label15.ForeColor = Color.Black;  //yüklenmişse siyah olsun..


                if (comboBox1.Text == "") //mezuniyet alanı boşsa
                    label17.ForeColor = Color.Red; //kırmızı yap...
                else
                    label17.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (comboBox2.Text == "") //görevi alanı
                    label19.ForeColor = Color.Red; //kırmızı yap...
                else
                    label19.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (comboBox3.Text == "") //görev yeri
                    label20.ForeColor = Color.Red; //kırmızı yap...
                else
                    label20.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (maskedTextBox4.MaskCompleted == false)  //maas alanı
                    label21.ForeColor = Color.Red; //kırmızı yap...
                else
                    label21.ForeColor = Color.Black;  //yüklenmişse siyah olsun..

                if (int.Parse(maskedTextBox4.Text) < 1000) //1000'den küçük maaş girilişi yapılamaz dedim...
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

            if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false) //resim yüklenmişse ve 11 karakterli tcno girilmesi kuralına uyulmuşsa ve diğer tüm kurallara uyulmuşsa
            {
                if (radioButton3.Checked == true) //radiobutton3 seçiliyse
                    cinsiyet = "Bay"; //bay'ı seç.
                else if (radioButton4.Checked == true) //radiobutton4 seçiliyse
                    cinsiyet = "Bayan"; //bayanı seç.
                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu = new OleDbCommand("update personeller set ad='" + maskedTextBox2.Text + "',soyad='" + maskedTextBox3.Text + "', cinsiyet='" + cinsiyet + "',mezuniyet='" + comboBox1.Text + "',dogumtarihi='" + dateTimePicker1.Text + "',gorevi='" + comboBox2.Text + "',gorevyeri='" + comboBox3.Text + "',maasi='" + maskedTextBox4.Text + "'where tcno='" + maskedTextBox1.Text + "'", baglantim); //böylece veritabanındaki alanlara teker teker güncelleme işlemini gerçekleştirdim,kayıtı güncellenecek verinin kriteri belirlendi...
                    guncellekomutu.ExecuteNonQuery(); //sorgunun sonuçları veritabanına işlendi...
                    baglantim.Close();
                    personelleri_goster();
                    topPage2_temizle();
                    maskedTextBox4.Text = "0";

                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "Leyla Kıızlkaya Stajyer Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }

            }
        }

        private void button10_Click(object sender, EventArgs e) //SİL Butonu Kodlar
        {
            if(maskedTextBox1.MaskCompleted==true) //tc kuralına uyulmuşsa (yani 11 hane tcno girilmişse bu if çalışır,yoksa else çalışır...)
            {
                bool kayit_arama_durumu = false; //girilen tcnoya ilişkin kayıt olmasın diyelim...
                baglantim.Open();
                OleDbCommand arama_sorgusu=new OleDbCommand("select*from personeller where tcno='"+maskedTextBox1.Text+"'",baglantim); //kayıt olup olmadığını sınadım.
                OleDbDataReader kayitokuma=arama_sorgusu.ExecuteReader(); //arama sorgusunda gelen sonuçlar kayitokuma adlı datareader nesnesine atadım...
                while(kayitokuma.Read()) //sonuçta kayıt gelirse
                {
                    kayit_arama_durumu = true;
                    OleDbCommand delectsorgu=new OleDbCommand("delete from personeller where tcno='"+maskedTextBox1.Text +"'",baglantim); //eğer where demezsek tüm şeyler silinir.Kriter bildirdim ve kayıtları sildim.
                    delectsorgu.ExecuteNonQuery(); //sorgu sonuçlarını veritabanına işle.
                    break;
                }
                if(kayit_arama_durumu==false) //kayıt gelmezse
                {
                    MessageBox.Show("Silinecek Kayıt Bulunamadı!","Leyla Kıızlkaya Stajyer Takip Prpgramı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                baglantim.Close();
                personelleri_goster();
                topPage2_temizle();
                maskedTextBox4.Text = "0";

            }
            else // tc kuralına uyulmamışsa (yani 11 hane tcno girilmemişse bu else çalışır...)
            {
                MessageBox.Show("Lütfen 11 karakterden oluşaan bir TC Kimlik No giriniz!","Leyla Kızılkaya Stajyer Takip Programı",MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage2_temizle();
                maskedTextBox4.Text="0";
            }
        }

        private void button11_Click(object sender, EventArgs e) //TEMİZLE Butonu Kodlar
        {
            topPage2_temizle(); 
        }
    }
}



       

