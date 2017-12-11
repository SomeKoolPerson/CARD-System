using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            textBox2.Text += "\nPrice: " + item.getPrice();
            textBox2.Text += "\nNumber: "+ item.getCount();

            richTextBox1.ResetText();
            for (int i = 0; i < _item.getLog().Count; i++)
            {
                richTextBox1.Text += _item.getLog()[i].Item1 + ": \n" + "Price change: " + _item.getLog()[i].Item2.ToString() + "\n";
                this.chart1.Series["Value"].Points.AddXY(_item.getLog()[i].Item1, _item.getLog()[i].Item2);
            }

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
                pictureBox1.Image = Image.FromFile(ofd.FileName);
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
    }
}
