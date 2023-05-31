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
using System.Data;
using System.Collections;
using System.Configuration;

namespace OKC_WPF_Version {
    /// <summary>
    /// Interaction logic for Detay2.xaml
    /// </summary>
    public partial class Detay2 : Window {
        public Detay2() {
            InitializeComponent();
        }

        OleDbConnection Baglan1 = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = VeriTabani/ClanList.mdb");

        public void Goster() {

            string ConString = ConfigurationManager.ConnectionStrings["Baglan"].ConnectionString;
            string CmdString = string.Empty;
            using (OleDbConnection con = new OleDbConnection(ConString)) {
                CmdString = "SELECT NickName,VerilenMiktar,Drop FROM Detay2 where Sayi='"+Detay.GlobalSeriNo+"' ";
                OleDbCommand cmd = new OleDbCommand(CmdString, con);
                OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable("Detay2");
                sda.Fill(dt);
                dataGridView1.ItemsSource = dt.DefaultView;

            }
        }
        public void Goster2() {
            string Degerler, Degerler2, TumDegerler = "" ;
            string ConString = ConfigurationManager.ConnectionStrings["Baglan"].ConnectionString;
            string CmdString = string.Empty;
            using (OleDbConnection con = new OleDbConnection(ConString)) {
                OleDbCommand Com = new OleDbCommand("select NickName from Detay2 where Sayi = '"+Detay.GlobalSeriNo+"'", Baglan1);
                Baglan1.Open();
                OleDbDataReader dr = Com.ExecuteReader();
                while (dr.Read()) {
                    Degerler = " NickName not like '"+dr["NickName"]+"' and";
                    Degerler2 = TumDegerler;
                    TumDegerler = Degerler2 + Degerler;

                }
                string andsiz = TumDegerler.Substring(0, TumDegerler.Length - 3);
                TumDegerler = andsiz;
                Baglan1.Close();
                CmdString = "SELECT NickName From Clan_List where "+TumDegerler+""; //NuLL degeri tabloyada göstermemedsi gerekiyor 
                OleDbCommand cmd = new OleDbCommand(CmdString, con);
                OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable("Clan_List");
                sda.Fill(dt);
                dataGridView2.ItemsSource = dt.DefaultView;

            }
        }
            public void detaysayf() {

            Detay dty = new Detay();
            dty.Show();
            this.Close();

        }
        private void LookButton_Click(object sender, RoutedEventArgs e) {
             try { 
            int CikarilanMiktar = 0;
            int columnIndex = dataGridView1.SelectedIndex;
            var item = dataGridView1.Items[columnIndex];
            string Oyunisimi = (dataGridView1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            string DMiktar = (dataGridView1.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            var mesaj2 = MessageWindow.Show("Oyuncu Pay Silme", "Oyuncu İsim : " + Oyunisimi + "\nVerilmiş Olan Pay : " + DMiktar + "\nBu oyuncuyu gerçekten silmek istediğinize eminmisiniz ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mesaj2 == MessageBoxResult.Yes) {
                    Baglan1.Open();
                    OleDbCommand varmiyokmi = new OleDbCommand("Select * from Clan_List where NickName='" + Oyunisimi + "'", Baglan1);
                    OleDbDataReader read = varmiyokmi.ExecuteReader();
                    if (read.Read()) {

                        OleDbCommand Cmd = new OleDbCommand("SELECT COUNT(*) FROM Detay2 where Sayi='" + Detay.GlobalSeriNo + "'", Baglan1);
                        OleDbCommand kmt1 = new OleDbCommand("Select Pay from Clan_List Where NickName ='" + Oyunisimi + "'", Baglan1);
                        OleDbCommand kmt6 = new OleDbCommand("Select Miktar from Detay Where Sayi =" + int.Parse(Detay.GlobalSeriNo) + "", Baglan1);
                        int UyeSayisi = (int)Cmd.ExecuteScalar();
                        int NMiktar = (int)kmt1.ExecuteScalar();
                        int AnaPayMik = (int)kmt6.ExecuteScalar();
                        int DMiktar2 = Convert.ToInt32(DMiktar);
                        long LongAnaPayMik = Convert.ToInt64(AnaPayMik);
                        if (UyeSayisi > 1) {

                            CikarilanMiktar = NMiktar - DMiktar2;
                            int yenidegerbolum = AnaPayMik / (UyeSayisi - 1);
                            OleDbCommand kmt2 = new OleDbCommand("UPDATE Clan_List SET Pay = " + CikarilanMiktar + " where NickName='" + Oyunisimi + "'", Baglan1);
                            OleDbCommand kmt0 = new OleDbCommand("Delete from Detay2 where NickName ='" + Oyunisimi + "' AND Sayi = '" + Detay.GlobalSeriNo + "' ", Baglan1);
                            OleDbCommand yenideger = new OleDbCommand("update Detay2 set VerilenMiktar=" + yenidegerbolum + "  where Sayi='" + Detay.GlobalSeriNo + "' ", Baglan1);
                            kmt2.ExecuteNonQuery();
                            kmt0.ExecuteNonQuery();
                            yenideger.ExecuteNonQuery();
                        for (int i = 0; i < dataGridView1.Items.Count; i++) {

                            string isim = (dataGridView1.SelectedCells[0].Column.GetCellContent(dataGridView1.Items[i]) as TextBlock).Text;
                            if (isim==Oyunisimi) {
                                ;
                            } else {

                        OleDbCommand eskideger2 = new OleDbCommand("update Clan_List set Pay = Pay - " + DMiktar2 + "  where NickName='" + isim + "' ", Baglan1);
                                    OleDbCommand yenideger2 = new OleDbCommand("update Clan_List set Pay = Pay + " + yenidegerbolum + "  where NickName='" + isim + "' ", Baglan1);
                                    eskideger2.ExecuteNonQuery();
                                    yenideger2.ExecuteNonQuery();                              
                              }
                            }
                            Baglan1.Close();
                            Goster();
                        } else if (UyeSayisi == 1) {
                            CikarilanMiktar = NMiktar - DMiktar2;
                            OleDbCommand kmt2 = new OleDbCommand("UPDATE Clan_List SET Pay = " + CikarilanMiktar + " where NickName='" + Oyunisimi + "'", Baglan1);
                            OleDbCommand kmt0 = new OleDbCommand("Delete from Detay2 where NickName ='" + Oyunisimi + "' AND Sayi = '" + Detay.GlobalSeriNo + "'", Baglan1);
                            OleDbCommand kmt3 = new OleDbCommand("Delete from Detay where Sayi=" + int.Parse(Detay.GlobalSeriNo) + "", Baglan1);
                            OleDbCommand kmt4 = new OleDbCommand("select ToplamPay1 from ToplamPay where id = 57", Baglan1);
                            string Deger3 = (string)kmt4.ExecuteScalar();
                            long yeniDeger3 = Convert.ToInt64(Deger3);
                            long VeriToplamPay = yeniDeger3 - LongAnaPayMik;
                            OleDbCommand kmtcuk = new OleDbCommand("update ToplamPay set ToplamPay1='" + VeriToplamPay + "'  where id=57", Baglan1);
                            kmt2.ExecuteNonQuery();
                            kmtcuk.ExecuteNonQuery();
                            kmt0.ExecuteNonQuery();
                            kmt3.ExecuteNonQuery();
                            Baglan1.Close();
                            Goster();
                            detaysayf();
                        } else {
                            Baglan1.Close();
                            Goster();
                        }
                    } else {
                       var  mesaj1 = MessageWindow.Show("Kayıtsız Üye", Oyunisimi + " isiminde bir üye kayıtlı değildir.\n Silmek İstiyormusunuz ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (mesaj1 == MessageBoxResult.Yes) {
                            OleDbCommand Cmd = new OleDbCommand("SELECT COUNT(*) FROM Detay2 where Sayi='" + Detay.GlobalSeriNo + "'", Baglan1);
                            OleDbCommand kmt6 = new OleDbCommand("Select Miktar from Detay Where Sayi =" + int.Parse(Detay.GlobalSeriNo) + "", Baglan1);
                            int UyeSayisi = (int)Cmd.ExecuteScalar();
                            int AnaPayMik = (int)kmt6.ExecuteScalar();
                            int DMiktar2 = int.Parse(DMiktar);
                            int AnaMiktarDustu = AnaPayMik - DMiktar2;
                            if (UyeSayisi > 1) {
                                int yenidegerbolum = AnaPayMik / (UyeSayisi - 1);
                                OleDbCommand kmt0 = new OleDbCommand("Delete from Detay2 where (NickName ='" + Oyunisimi + "') AND (Sayi = '" + Detay.GlobalSeriNo + "')", Baglan1);
                                OleDbCommand yenideger = new OleDbCommand("update Detay2 set VerilenMiktar=" + yenidegerbolum + "  where Sayi='" + Detay.GlobalSeriNo + "' ", Baglan1);
                                kmt0.ExecuteNonQuery();
                                for (int i = 0; i < dataGridView1.Items.Count; i++) {
                                    var item3 = dataGridView1.Items[i];
                                    string ID2 = (dataGridView1.SelectedCells[0].Column.GetCellContent(item3) as TextBlock).Text;

                                    OleDbCommand eskideger2 = new OleDbCommand("update Clan_List set Pay = Pay - " + DMiktar2 + "  where NickName='" + ID2 + "' ", Baglan1);
                                    OleDbCommand yenideger2 = new OleDbCommand("update Clan_List set Pay = Pay + " + yenidegerbolum + "  where NickName='" + ID2 + "' ", Baglan1);
                                    eskideger2.ExecuteNonQuery();
                                    yenideger2.ExecuteNonQuery();
                                }
                                yenideger.ExecuteNonQuery();
                                Baglan1.Close();
                                Goster();
                            } else if (UyeSayisi == 1) {
                                OleDbCommand kmt0 = new OleDbCommand("Delete from Detay2 where (NickName ='" + Oyunisimi + "') AND (Sayi = '" + Detay.GlobalSeriNo + "')", Baglan1);
                                OleDbCommand kmt3 = new OleDbCommand("Delete from Detay where Sayi=" + int.Parse(Detay.GlobalSeriNo) + "", Baglan1);
                                kmt0.ExecuteNonQuery();
                                kmt3.ExecuteNonQuery();
                                Baglan1.Close();
                                Goster();
                                detaysayf();
                            } else {
                                ;
                            }
                        } else {
                            ;
                        }
                    }
                }

             else {
                ;
            }
              } catch (Exception hata) {
                MessageWindow.Show("Hata", "Hata : "+hata.ToString(),MessageBoxButton.OK,MessageBoxImage.Error); Baglan1.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Goster();
            dataGridView1.Columns[0].IsReadOnly = true;
            dataGridView1.Columns[1].IsReadOnly = true;
            dataGridView1.Columns[2].IsReadOnly = true;
            dataGridView2.Columns[0].IsReadOnly = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e) {
            detaysayf();
            this.Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e) {

        }

        private void PlusImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Goster2();
            dataGridView2.Visibility = Visibility.Visible;
        }

        private void AddButtonClick_Click(object sender, RoutedEventArgs e) {
            int columnIndex = dataGridView2.SelectedIndex;
            var item = dataGridView2.Items[columnIndex];
            string NickUye = (dataGridView2.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            var Mesaj1 = MessageWindow.Show("Ekleme","\""+NickUye+"\" isimli kullanıcıyı bu paya dahil etmek istediğinize eminmisiniz ? ",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (Mesaj1==MessageBoxResult.Yes) {
                Baglan1.Open();
                OleDbCommand Cmd = new OleDbCommand("SELECT COUNT(*) FROM Detay2 where Sayi='" + Detay.GlobalSeriNo + "'", Baglan1);
                OleDbCommand kmt6 = new OleDbCommand("Select Miktar from Detay Where Sayi =" + int.Parse(Detay.GlobalSeriNo) + "", Baglan1);
                OleDbCommand Com2 = new OleDbCommand("Select Drop from Detay2 Where Sayi ='" + Detay.GlobalSeriNo + "'", Baglan1);
                string Drop = (string)Com2.ExecuteScalar();
                int UyeSayisi = (int)Cmd.ExecuteScalar();
                int AnaPayMik = (int)kmt6.ExecuteScalar();
                int yenidegerbolum = AnaPayMik / (UyeSayisi + 1);
                int DMiktar2 = AnaPayMik / UyeSayisi;
                OleDbCommand Com = new OleDbCommand("select NickName from Detay2 where Sayi = '" + Detay.GlobalSeriNo + "'", Baglan1);
                OleDbDataReader dr = Com.ExecuteReader();
                while (dr.Read()) {             
                OleDbCommand eskideger2 = new OleDbCommand("update Clan_List set Pay = Pay - " + DMiktar2 + "  where NickName='" + dr["NickName"] + "' ", Baglan1);
                        OleDbCommand yenideger2 = new OleDbCommand("update Clan_List set Pay = Pay + " + yenidegerbolum + "  where NickName='" + dr["NickName"] + "' ", Baglan1);
                        eskideger2.ExecuteNonQuery();
                        yenideger2.ExecuteNonQuery(); 
                }
                OleDbCommand Com1 = new OleDbCommand("insert into Detay2 values ('"+Detay.GlobalSeriNo+"',"+yenidegerbolum+",'"+NickUye+"','"+Drop+"')",Baglan1);
                OleDbCommand yenideger3 = new OleDbCommand("update Clan_List set Pay = Pay + " + yenidegerbolum + "  where NickName='" + NickUye + "' ", Baglan1);
                OleDbCommand yenideger = new OleDbCommand("update Detay2 set VerilenMiktar=" + yenidegerbolum + "  where Sayi='" + Detay.GlobalSeriNo + "' ", Baglan1);
                Com1.ExecuteNonQuery();
                yenideger3.ExecuteNonQuery();
                yenideger.ExecuteNonQuery();
                dr.Close();
                Baglan1.Close();
                Goster();
                MessageWindow.Show("İşlem Başarılı", "\"" + NickUye + "\" isimli kullanıcı başarı ile eklendi ", MessageBoxButton.OK, MessageBoxImage.Warning);
                dataGridView2.Visibility = Visibility.Collapsed;
            } else {
                ;
            }
        }
    }
}
