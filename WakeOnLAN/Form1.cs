using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WakeOnLAN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //textBox1.Text = "‎10050143306E";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string MACADDRESS = textBox1.Text;
            if (MACADDRESS.Length==13)
            {
                MACADDRESS = MACADDRESS.Substring(1, 12);
            }
            WOLClass client = new WOLClass();
            client.Connect(new
               IPAddress(0xffffffff),  //255.255.255.255  i.e broadcast
               0x2fff); // port=12287 let's use this one 
            client.SetClientToBrodcastMode();
            //set sending bites
            int counter = 0;
            //buffer to be send
            byte[] bytes = new byte[1024];   // more than enough :-)
                                             //first 6 bytes should be 0xFF
            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            //now repeate MAC 16 times
            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    bytes[counter++] =
                        byte.Parse(MACADDRESS.Substring(i, 2),
                        NumberStyles.HexNumber);
                    i += 2;
                }
            }

            //now send wake up packet
            int reterned_value = client.Send(bytes, 1024);

        }
    }
}
