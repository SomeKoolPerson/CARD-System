using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class View_Item : Form
    {
        private string _name;
        private string _desc;
        private Item _item;


        public View_Item(Item item)
        {
            _name = item.getName();
            _desc = item.getDesc();
            _item = item;

            InitializeComponent();

            textBox1.Text = _name;
            textBox2.Text = _desc;
            textBox2.Text += "\r\nPrice: $" + item.getPrice();
            textBox2.Text += "\r\nNumber: "+ item.getCount();

            richTextBox1.ResetText();
            for (int i = 0; i < _item.getLog().Count; i++)
            {
                richTextBox1.Text += _item.getLog()[i].Item1 + ": \r\n" + "Price change: " + _item.getLog()[i].Item2.ToString() + "\r\n";
                this.chart1.Series["Value"].Points.AddXY(_item.getLog()[i].Item1, _item.getLog()[i].Item2);
            }

            if(_item.getPrice() > 7000) { chart1.ChartAreas[0].AxisY.Minimum = 7000; }

        }

        OpenFileDialog ofd = new OpenFileDialog();


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "All|*.*|JPEG|*.jpeg|PNG|*.png|JPG|*.jpg";
            ofd.FilterIndex = 4;
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image picture = Image.FromFile(ofd.FileName);
                picture = ResizeImage(picture,pictureBox1.Width,pictureBox1.Height);
                pictureBox1.Image = picture;

            }
        }



        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            String newText = textBox2.Text;
            _item.setDesc(newText);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
