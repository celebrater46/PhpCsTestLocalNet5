﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhpCsTest2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Handler.URL = "http://localhost/myapps/PhpCsTest/index.php";
        }

        private static void InsertData(string data)
        {
            var values = new NameValueCollection();
            values["type"] = "t_insert_data";
            values["data"] = data;
            string result = Handler.DoPost(values);
            if (result != "")
            {
                var jobj = JObject.Parse(result);
                switch ((string)jobj["result"])
                {
                    case "success":
                        MessageBox.Show("Success!");
                        break;
                    case "failed":
                        MessageBox.Show("Failed!");
                        break;
                    default:
                        MessageBox.Show("Unknown error!");
                        break;
                }
            }
        }

        private static void GetData()
        {
            var values = new NameValueCollection();
            values["type"] = "t_get_data";
            string result = Handler.DoPost(values);
            MessageBox.Show(result);
            if (result != "")
            {
                var jobj = JObject.Parse(result);
                switch ((string)jobj["result"])
                {
                    case "success":
                        MessageBox.Show("Success!" + Environment.NewLine + "Data: " + (string)jobj["data"]);
                        break;
                    case "failed":
                        MessageBox.Show("Failed!");
                        break;
                    default:
                        MessageBox.Show("Unknown error!");
                        break;
                }
            }
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            InsertData("✟ 黒野堕天男 ✟");
        }

        private void BtnGet_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}