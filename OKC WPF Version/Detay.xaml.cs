using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.IO;

namespace OKC_WPF_Version {
    /// <summary>
    /// Interaction logic for Detay.xaml
    /// </summary>
    public partial class Detay : Window {
        public Detay() {
            InitializeComponent();
        }
        OleDbConnection Baglan1 = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = VeriTabani/ClanList.mdb");

        public void Goster() {

            string ConString = ConfigurationManager.ConnectionStrings["Baglan"].ConnectionString;
            string CmdString = string.Empty;
            using (OleDbConnection con = new OleDbConnection(ConString)) {
                CmdString = "SELECT Tarih,Miktar FROM Detay";
                OleDbCommand cmd = new OleDbCommand(CmdString, con);
                OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable("Detay");
                sda.Fill(dt);
                dataGridView1.ItemsSource = dt.DefaultView;

            }
        }

        private void YanGrid3Label1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Anasayf();
            this.Close();
        }

        private void YanGrid4Label1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ;
        }

        private void YanGrid5Label1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Iletisimsayf();
            this.Close();
        }

        private void YanGrid6Label1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

        }
        public void Anasayf() {
            Anamenu menu = new Anamenu();
            menu.Show();
        }
        public void Iletisimsayf() {
            iletisim ile = new iletisim();
            ile.Show();
        }

        private void YanGrid5Label1_MouseEnter(object sender, MouseEventArgs e) {
            YanGrid5Label1.Foreground = Brushes.LightGreen;
            //YanGrid4.Background = Brushes.SteelBlue;
           // Border3.BorderThickness = new Thickness(4);
            //Border3.BorderBrush = Brushes.Crimson;
        }

        private void YanGrid5Label1_MouseLeave(object sender, MouseEventArgs e) {
            YanGrid5Label1.Foreground = Brushes.White;
            //YanGrid4.Background = Brushes.SteelBlue;
            //Border3.BorderThickness = new Thickness(0);
            //Border1.BorderBrush = Brushes.Orange;
        }

        private void YanGrid3Label1_MouseEnter(object sender, MouseEventArgs e) {
            YanGrid3Label1.Foreground = Brushes.LightGreen;
            //YanGrid4.Background = Brushes.SteelBlue;
         /*   Border1.BorderThickness = new Thickness(4);
            Border1.BorderBrush = Brushes.Orange;*/
        }

        private void YanGrid3Label1_MouseLeave(object sender, MouseEventArgs e) {
            YanGrid3Label1.Foreground = Brushes.White;
            //YanGrid4.Background = Brushes.SteelBlue;
            //Border1.BorderThickness = new Thickness(0);
            //Border1.BorderBrush = Brushes.Orange;
        }
        private FrameworkElement _title;
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                dataGridView1.Columns[0].IsReadOnly = true;
                dataGridView1.Columns[1].IsReadOnly = true;
                Goster();
                this._title = (FrameworkElement)this.Template.FindName("PART_Title", this);
                if (null != this._title) {
                    this._title.MouseLeftButtonDown += new MouseButtonEventHandler(CekBirakGrid_MouseLeftButtonDown);
                }
            } catch (Exception hata){

                MessageWindow.Show("Hata", "Hata : " + hata.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static string GlobalSeriNo;
        private void LookButton_Click(object sender, RoutedEventArgs e) {
            try {
                    int columnIndex = dataGridView1.SelectedIndex;
                    var item = dataGridView1.Items[columnIndex];
                    string Tarih = (dataGridView1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                Baglan1.Open();
                OleDbCommand komut = new OleDbCommand("Select Sayi from Detay where Tarih = '"+Tarih+"'",Baglan1);
                string Sayicik =Convert.ToString( (int)komut.ExecuteScalar());
                    GlobalSeriNo = Sayicik;
                    Detay2 dty2 = new Detay2();
                    dty2.Show();
                Baglan1.Close();
                    this.Close();
               
                
            } catch(Exception hata) {

                MessageWindow.Show("Hata", "Hata : " + hata.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BarImage_MouseLeave(object sender, MouseEventArgs e) {
            //   BarImage.Opacity = 1.0;
            BarImage.Height -= 5;
            BarImage.Width -= 5;
        }

        private void BarImage_MouseEnter(object sender, MouseEventArgs e) {
            BarImage.Height += 5;
            BarImage.Width += 5;
            // BarImage.Opacity = 0.7;
        }
        string MDegeri, SBDegeri, GBDegeri, CDegeri, NoktaDegeri1, NoktaDegeri2, NoktaDegeri3, NoktaDegeri4, Deger3;

        private void YedekText_MouseLeave(object sender, MouseEventArgs e) {
            YedekText.Foreground = Brushes.FloralWhite;
        }

        private void YedekText_MouseEnter(object sender, MouseEventArgs e) {
            YedekText.Foreground = Brushes.Brown;
        }

        private void CekBirakGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void YedekText_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            try {
                var Mesajcik = MessageWindow.Show("Veritabanı Yedekleme","Veritabanını gerçekten yedeklemek istediğinize eminmisiniz ?",MessageBoxButton.YesNo,MessageBoxImage.Question);
                if (Mesajcik==MessageBoxResult.Yes) {
                if (Directory.Exists("C:\\OKC Yedek")) {
                    Directory.Delete(@"C:\\OKC Yedek", true);
                    string klasorYeri = "C:\\";
                    string klasorAdi = "OKC Yedek";
                    string klasorOlustur = klasorYeri + @"\" + klasorAdi;
                    Directory.CreateDirectory(klasorOlustur);
                    File.Copy("VeriTabani/ClanList.mdb", "C:\\OKC Yedek\\" + "ClanList" + ".mdb");
                    MessageWindow.Show("İşlem Başarılı", "Veritabanı C:\\OKC Yedek  klasorunun içine yedeklenmiştir", MessageBoxButton.OK,MessageBoxImage.Warning);
                } else {
                    string klasorYeri = "C:\\";
                    string klasorAdi = "OKC Yedek";
                    string klasorOlustur = klasorYeri + @"\" + klasorAdi;
                    Directory.CreateDirectory(klasorOlustur);
                     File.Copy("VeriTabani/ClanList.mdb", "C:\\OKC Yedek\\" + "ClanList" + ".mdb");
                    MessageWindow.Show("İşlem Başarılı", "Veritabanı C:\\OKC Yedek  klasorunun içine yedeklenmiştir", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                } else {
                    ;
                }

            } catch (Exception a) {
                MessageWindow.Show("HATA!!", "Yedekleme alınırken bir hata meydana geldi. Hata Sebebi : " + a.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetText_MouseLeave(object sender, MouseEventArgs e) {
            ResetImage.Source = (ImageSource)FindResource("ImageWhite");
            ResetText.FontSize = 20;
            ResetText.Foreground = Brushes.White;
        }

        private void ResetText_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            try { 
            var mesaj9 = MessageWindow.Show("Sıfırlama", "Tüm kayıt bilgilerindeki pay oranları 0'lanacaktır.\nDevam etmek istediğinize eminmisiniz ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mesaj9 == MessageBoxResult.Yes) {
                byte Sıfırlanma = 0;
                Baglan1.Open();
                OleDbCommand komut1 = new OleDbCommand("UPDATE Clan_List SET Pay = " + Sıfırlanma + "", Baglan1);
                komut1.ExecuteNonQuery();
                OleDbCommand komut2 = new OleDbCommand("Delete From Detay", Baglan1);
                komut2.ExecuteNonQuery();
                OleDbCommand komut3 = new OleDbCommand("Delete From Detay2", Baglan1);
                komut3.ExecuteNonQuery();
                Baglan1.Close();
                Goster();
            var  mesaj3 = MessageWindow.Show("Toplam Pay Sıfırlama", "Tüm kayıt bilgilerindeki pay oranları 0'landı.\nToplam pay oranıda 0'lansınmı ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mesaj3 == MessageBoxResult.Yes) {
                    char Sıfırlanma2 = '0';
                    Baglan1.Open();
                    OleDbCommand komut4 = new OleDbCommand("UPDATE ToplamPay SET ToplamPay1 = '" + Sıfırlanma2 + "'", Baglan1);
                    komut4.ExecuteNonQuery();
                    Baglan1.Close();
                        Goster();
                } else {
                    ;
                }
            } else {
                ;
            }
            }
            catch(Exception hata) {
                MessageWindow.Show("Hata", "Hata : " + hata.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetText_MouseEnter(object sender, MouseEventArgs e) {
            ResetImage.Source = (ImageSource)FindResource("ImageBlack");
            ResetText.FontSize = 24;
            ResetText.Foreground = Brushes.Red;
        }

        private void BarImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Baglan1.Open();
            OleDbCommand kmt4 = new OleDbCommand("select ToplamPay1 from ToplamPay where id=57", Baglan1);
            Deger3 = (string)kmt4.ExecuteScalar();
            Baglan1.Close();
            if (Deger3.Length > 5 && Deger3.Length < 7) {
                NoktaDegeri1 = Deger3.Substring(0, 3);
                NoktaDegeri2 = Deger3.Substring(3, 3);
              MessageWindow.Show("Bilgi", NoktaDegeri1 + "." + NoktaDegeri2 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 6 && Deger3.Length < 8) { // 1.4.0.1.125
                MDegeri = Deger3.Substring(0, 1);
                CDegeri = Deger3.Remove(0, 1);
                NoktaDegeri2 = Deger3.Substring(1, 3);
                NoktaDegeri3 = Deger3.Substring(4, 3);
              MessageWindow.Show("Bilgi", MDegeri + " M \n" + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 7 && Deger3.Length < 9) {
                SBDegeri = Deger3.Substring(0, 1);
                MDegeri = Deger3.Substring(1, 1);
                CDegeri = Deger3.Remove(0, 2);
                NoktaDegeri2 = Deger3.Substring(2, 3);//
                NoktaDegeri3 = Deger3.Substring(5, 3);
              MessageWindow.Show("Bilgi", SBDegeri + " Silver Bar \n" + MDegeri + " M \n" + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 8 && Deger3.Length < 10) {
                GBDegeri = Deger3.Substring(0, 1);
                SBDegeri = Deger3.Substring(1, 1);
                MDegeri = Deger3.Substring(2, 1);
                CDegeri = Deger3.Remove(0, 3);
                NoktaDegeri2 = Deger3.Substring(3, 3);
                NoktaDegeri3 = Deger3.Substring(6, 3);
              MessageWindow.Show("Bilgi", GBDegeri + " Gold Bar \n" + SBDegeri + " Silver Bar \n" + MDegeri + " M \n" + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 9 && Deger3.Length < 11) {
                GBDegeri = Deger3.Substring(0, 2);
                SBDegeri = Deger3.Substring(2, 1);
                MDegeri = Deger3.Substring(3, 1);
                CDegeri = Deger3.Remove(0, 4);
                NoktaDegeri2 = Deger3.Substring(4, 3); //1.100.100.100
                NoktaDegeri3 = Deger3.Substring(7, 3);
               MessageWindow.Show("Bilgi", GBDegeri + " Gold Bar \n" + SBDegeri + " Silver Bar \n" + MDegeri + " M \n" + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 10 && Deger3.Length < 12) {
                GBDegeri = Deger3.Substring(0, 3);
                SBDegeri = Deger3.Substring(3, 1);
                MDegeri = Deger3.Substring(4, 1);
                CDegeri = Deger3.Remove(0, 5);//1.049.999.999
                NoktaDegeri2 = Deger3.Substring(5, 3);//10.100.100.100
                NoktaDegeri3 = Deger3.Substring(8, 3);
               MessageWindow.Show("Bilgi", GBDegeri + " Gold Bar \n" + SBDegeri + " Silver Bar \n" + MDegeri + " M \n" + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else {
                MessageWindow.Show("Bilgi", Deger3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CoinsImage_MouseLeave(object sender, MouseEventArgs e) {
            CoinsImage.Height -= 5;
            CoinsImage.Width -= 5;
        }

        private void CoinsImage_MouseEnter(object sender, MouseEventArgs e) {
            CoinsImage.Height += 5;
            CoinsImage.Width += 5;
          
        }

        private void CoinsImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Baglan1.Open();
            OleDbCommand kmt4 = new OleDbCommand("select ToplamPay1 from ToplamPay where id=57", Baglan1);
            Deger3 = (string)kmt4.ExecuteScalar();
            Baglan1.Close();
            if (Deger3.Length > 5 && Deger3.Length < 7) {
                NoktaDegeri1 = Deger3.Substring(0, 3);
                NoktaDegeri2 = Deger3.Substring(3, 3);
            MessageWindow.Show("Bilgi", NoktaDegeri1 + "." + NoktaDegeri2 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 6 && Deger3.Length < 8) { // 1.4.0.1.125
                NoktaDegeri1 = Deger3.Substring(0, 1);
                NoktaDegeri2 = Deger3.Substring(1, 3);
                NoktaDegeri3 = Deger3.Substring(4, 3);
              MessageWindow.Show("Bilgi", NoktaDegeri1 + "." + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 7 && Deger3.Length < 9) {
                NoktaDegeri1 = Deger3.Substring(0, 2);
                NoktaDegeri2 = Deger3.Substring(2, 3);//
                NoktaDegeri3 = Deger3.Substring(5, 3);
             MessageWindow.Show("Bilgi", NoktaDegeri1 + "." + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 8 && Deger3.Length < 10) {
                NoktaDegeri1 = Deger3.Substring(0, 3);
                NoktaDegeri2 = Deger3.Substring(3, 3);
                NoktaDegeri3 = Deger3.Substring(6, 3);
             MessageWindow.Show("Bilgi", NoktaDegeri1 + "." + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 9 && Deger3.Length < 11) {
                NoktaDegeri4 = Deger3.Substring(0, 1);
                NoktaDegeri1 = Deger3.Substring(1, 3);
                NoktaDegeri2 = Deger3.Substring(4, 3); //1.100.100.100
                NoktaDegeri3 = Deger3.Substring(7, 3);
              MessageWindow.Show("Bilgi", NoktaDegeri4 + "." + NoktaDegeri1 + "." + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (Deger3.Length > 10 && Deger3.Length < 12) {
                NoktaDegeri4 = Deger3.Substring(0, 2);
                NoktaDegeri1 = Deger3.Substring(2, 3);
                NoktaDegeri2 = Deger3.Substring(5, 3);//10.100.100.100
                NoktaDegeri3 = Deger3.Substring(8, 3);
              MessageWindow.Show("Bilgi", NoktaDegeri4 + "." + NoktaDegeri1 + "." + NoktaDegeri2 + "." + NoktaDegeri3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            } else {
              MessageWindow.Show("Bilgi", Deger3 + " Coins", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
