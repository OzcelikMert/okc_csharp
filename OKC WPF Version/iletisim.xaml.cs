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
using System.Diagnostics;

namespace OKC_WPF_Version
{
    /// <summary>
    /// Interaction logic for iletisim.xaml
    /// </summary>
    public partial class iletisim : Window
    {
        public iletisim()
        {
            InitializeComponent();
        }

        private void YanGrid3Label1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Anasayf();
            this.Close();
        }

        private void YanGrid4Label1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            DetaySayf();
            this.Close();
        }
        public void Anasayf() {
            Anamenu menu = new Anamenu();
            menu.Show();
        }
        public void DetaySayf() {
            Detay dty = new Detay();
            dty.Show();
        }

        private void YanGrid3Label1_MouseEnter(object sender, MouseEventArgs e) {
            YanGrid3Label1.Foreground = Brushes.LightGreen;
            //YanGrid4.Background = Brushes.SteelBlue;
           /* Border1.BorderThickness = new Thickness(4);
            Border1.BorderBrush = Brushes.Orange;*/
        }

        private void YanGrid3Label1_MouseLeave(object sender, MouseEventArgs e) {
            YanGrid3Label1.Foreground = Brushes.White;
            //YanGrid4.Background = Brushes.SteelBlue;
         //   Border1.BorderThickness = new Thickness(0);
            //Border1.BorderBrush = Brushes.Orange;
        }

        private void YanGrid4Label1_MouseEnter(object sender, MouseEventArgs e) {
            YanGrid4Label1.Foreground = Brushes.LightGreen;
            //YanGrid4.Background = Brushes.SteelBlue;
           /* Border2.BorderThickness = new Thickness(4);
            Border2.BorderBrush = Brushes.DeepSkyBlue;*/
        }

        private void YanGrid4Label1_MouseLeave(object sender, MouseEventArgs e) {
            YanGrid4Label1.Foreground = Brushes.White;
            //YanGrid4.Background = Brushes.SteelBlue;
          //  Border2.BorderThickness = new Thickness(0);
            //Border1.BorderBrush = Brushes.Orange;
        }

        private void FaceB_MouseLeave(object sender, MouseEventArgs e) {
            FaceB.Height -= 10;
            FaceB.Width -= 10;
        }

        private void FaceB_MouseEnter(object sender, MouseEventArgs e) {
            FaceB.Height += 10;
            FaceB.Width += 10;
        }

        private void Outl_MouseLeave(object sender, MouseEventArgs e) {
            Outl.Height -= 10;
            Outl.Width -= 10;
        }

        private void Outl_MouseEnter(object sender, MouseEventArgs e) {
            Outl.Height += 10;
            Outl.Width += 10;
        }

        private void Websi_MouseEnter(object sender, MouseEventArgs e) {
            Websi.Height += 10;
            Websi.Width += 10;
        }

        private void Websi_MouseLeave(object sender, MouseEventArgs e) {
            Websi.Height -= 10;
            Websi.Width -= 10;
        }

        private void Googl_MouseEnter(object sender, MouseEventArgs e) {
            Googl.Height += 10;
            Googl.Width += 10;
        }

        private void Googl_MouseLeave(object sender, MouseEventArgs e) {
            Googl.Height -= 10;
            Googl.Width -= 10;
        }

        private void Telep_MouseEnter(object sender, MouseEventArgs e) {
            Telep.Height += 10;
            Telep.Width += 10;
        }

        private void Telep_MouseLeave(object sender, MouseEventArgs e) {
            Telep.Height -= 10;
            Telep.Width -= 10;
        }

        private void FaceB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Process.Start("https://www.facebook.com/mert.ozcelik.102");
            Process.Start("https://www.facebook.com/burhan.tapar");

        }

        private void Outl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
           MessageWindow.Show("E-Posta Adresi", "Mert Özçelik : pitir.mert@hotmail.com\nAkif BAŞORMANCI : akifbasormanci@gmail.com\nBurhan TAPAR : burhantapar00@gmail.com",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void Websi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
        MessageWindow.Show("Web Site", "Daha Açılmadı", MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void Googl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Process.Start("https://plus.google.com/u/0/101834974699639192944");
            Process.Start("https://plus.google.com/u/0/107591539205351118622");

        }

        private void Telep_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
          MessageWindow.Show("Telefon Numarası", "Mert ÖZÇELİK : 0531 322 5532\nAkif BAŞORMANCI : 0538 495 5936\nBurhan TAPAR : 0537 025 0260", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private FrameworkElement _title;
       
    private void CekBirakGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this._title = (FrameworkElement)this.Template.FindName("PART_Title", this);
            if (null != this._title) {
                this._title.MouseLeftButtonDown += new MouseButtonEventHandler(CekBirakGrid_MouseLeftButtonDown);
            }
        }
    }
}
