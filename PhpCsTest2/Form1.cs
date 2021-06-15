using Newtonsoft.Json.Linq;
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

namespace datatest
{
    public partial class Form1 : Form
    {
        private Label lb;
        private Button btPut;
        private Button btGet;
        
        public Form1()
        {
            InitializeComponent();
            
            this.Text = "サンプル";
            this.Width = 250;
            this.Height = 200;

            lb = new Label();
            lb.Text = "C#よりPHPを召喚ッ！！";
            lb.Width = 150;
            
            btPut = new Button();
            btPut.Text = "データの挿入";
            btPut.Top = this.Top + lb.Height;
            btPut.Width = lb.Width;

            btGet = new Button();
            btGet.Text = "データの呼び出し";
            btGet.Top = this.Top + lb.Height + btPut.Height;
            btGet.Width = lb.Width;

            btPut.Parent = this;
            btGet.Parent = this;
            lb.Parent = this;

            // Handler.URL = "http://localhost:8080/testPhpCs/index.php"; // Docker
            Handler.URL = "http://localhost/myapps/PhpCsTest2/index.php"; // XAMPP
            btPut.Click += new EventHandler(BtnInsert_Click);
            btGet.Click += new EventHandler(BtnGet_Click);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Handler.URL = "http://localhost/myapps/PhpCsTest2/index.php";
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
                        MessageBox.Show("Success!" + Environment.NewLine + "Name: " + (string)jobj["name"]);
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