using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace sterHeli
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
            comboBox1.SelectedItem = "COM6";

            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;

            trackBar1.Value = 63;
            trackBar2.Value = 63;
            trackBar3.Value = 0;
            trackBar4.Value = 63;

            textBox1.Text += trackBar1.Value;
            textBox2.Text += trackBar2.Value;
            textBox3.Text += trackBar3.Value;
            textBox4.Text += trackBar4.Value;

            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            trackBar3.Enabled = false;
            trackBar4.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
            serialPort1.BaudRate = 9600;
            try
            {
                serialPort1.Open();

                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;

                trackBar1.Enabled = true;
                trackBar2.Enabled = true;
                trackBar3.Enabled = true;
                trackBar4.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Nie można otworzyć portu.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;

            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            trackBar3.Enabled = false;
            trackBar4.Enabled = false;
        }

        private byte[] konwersja(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        private void wyslijWartosc(string pierwsze, int drugie)
        {
            String stringtosend;
            if (drugie < 16)
                stringtosend = pierwsze + "0" + drugie.ToString("X");
            else
                stringtosend = pierwsze + drugie.ToString("X");
            byte[] bytestosend = konwersja(stringtosend);
            serialPort1.Write(bytestosend, 0, bytestosend.Length);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            wyslijWartosc("00", trackBar1.Value);
            textBox1.Clear();
            textBox1.Text += trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            wyslijWartosc("01", trackBar2.Value);
            textBox2.Clear();
            textBox2.Text += trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            wyslijWartosc("02", trackBar3.Value);
            textBox3.Clear();
            textBox3.Text += trackBar3.Value;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            wyslijWartosc("03", trackBar4.Value);
            textBox4.Clear();
            textBox4.Text += trackBar4.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 63;
            wyslijWartosc("00", trackBar1.Value);
            textBox1.Clear();
            textBox1.Text += trackBar1.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trackBar2.Value = 63;
            wyslijWartosc("01", trackBar2.Value);
            textBox2.Clear();
            textBox2.Text += trackBar2.Value;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trackBar3.Value = 0;
            wyslijWartosc("02", trackBar3.Value);
            textBox3.Clear();
            textBox3.Text += trackBar3.Value;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            trackBar4.Value = 63;
            wyslijWartosc("03", trackBar4.Value);
            textBox4.Clear();
            textBox4.Text += trackBar4.Value;
        }

    }
}
