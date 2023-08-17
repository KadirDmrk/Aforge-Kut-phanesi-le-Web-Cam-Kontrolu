using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace Aforge_Kütüphanesi_İle_Web_Cam_Kontrolü
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FilterInfoCollection fico; // Bilgisayara bağlı kameraları tutan dizi
        VideoCaptureDevice vcd;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            fico = new FilterInfoCollection(FilterCategory.VideoInputDevice);// Bilgisayara bağlı kameraları getiriyor. 
            foreach (FilterInfo f in fico)
            {
                comboBox1.Items.Add(f.Name); // Kamera ismini ekle 
                comboBox1.SelectedIndex = 0; // seçili olan değer 0 index olsun, kamerayı seçmeden direkt kendi seçiyor. 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vcd = new VideoCaptureDevice(fico[comboBox1.SelectedIndex].MonikerString);
            vcd.NewFrame += Vcd_NewFrame; // Bu olsuşan metot kameraya çerçeveyi tanıtıcaz 
            vcd.Start();
            //timer1.Start(); 
        }

        private void Vcd_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone(); // Bitmap sınıfından ilgili pictureboxa kameradan aldıgımız çerceveyi aktarıyoruz. 
        }
        //  Kameredan fotoğraf Yakalama işlemi 
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "(*.jpg)|*.jpg";
            DialogResult dr = s.ShowDialog(); //Dosya  açtırma işlemi 
            if (dr == DialogResult.OK)
            {
                pictureBox1.Image.Save(s.FileName);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*
            if (pictureBox1 != null)
            {
                BarcodeReader brd = new BarcodeReader();
                Result sonuc = brd.Decode((Bitmap)pictureBox1.Image);
                if (sonuc != null)
                {
                    richTextBox1.Text = sonuc.ToString();
                    timer1.Stop();
                }
            }
            */ 
            // Yukarıdaki işlem telefondan barkod gösterdiğimiz zaman barkodu okuyor ve richtextboza aktarıyor fakat null değer diye hata alıyorum o yüzden kapatattım . 
        }
    }
}
