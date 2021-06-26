using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCall_REST_API
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);

            string postString = string.Format("?name={0}&price={1}&productCategoryId={2}", name, price, 1);
            HttpWebRequest request = WebRequest.CreateHttp("http://192.168.1.238/demorestapi/api/product/" + postString);
            request.Method = "POST";
            request.ContentType = "Application/json;charset:UTF-8";
            byte[] byteArr = Encoding.UTF8.GetBytes(postString);
            request.ContentLength = byteArr.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(byteArr, 0, byteArr.Length);
            stream.Close();

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(bool));
            object responeData = jsonSerializer.ReadObject(request.GetResponse().GetResponseStream());
            bool result = (bool)responeData;
            if (result)
            {
                MessageBox.Show("Add new product success");
                LoadData();
            }
            else
            {
                MessageBox.Show("Fail to add new product");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                int Id = (int)dr.Cells[0].Value;

                string deleteString = string.Format("?Id={0}", Id);
                HttpWebRequest request = WebRequest.CreateHttp("http://192.168.1.238/demorestapi/api/product/" + deleteString);
                request.Method = "DELETE";
                request.ContentType = "Application/json;charset:UTF-8";
                byte[] byteArr = Encoding.UTF8.GetBytes(deleteString);
                request.ContentLength = byteArr.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(byteArr, 0, byteArr.Length);
                stream.Close();

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(bool));
                object responeData = jsonSerializer.ReadObject(request.GetResponse().GetResponseStream());
                bool result = (bool)responeData;
                if (result)
                {
                    MessageBox.Show("Delete product success");

                    LoadData();
                }
                else
                {
                    MessageBox.Show("Fail to delete new product");
                }
            }
        }

        private void LoadData()
        {
            HttpWebRequest request = WebRequest.Create("http://192.168.1.238/demorestapi/api/product") as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Product[]));
            object objRespone = jsonSerializer.ReadObject(response.GetResponseStream());
            Product[] products = objRespone as Product[];
            dataGridView1.DataSource = products;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                int Id = (int)dr.Cells[0].Value;

                string name = txtName.Text;
                decimal price = decimal.Parse(txtPrice.Text);

                string postString = string.Format("?Id={0}&name={1}&price={2}&productCategoryId={3}", Id, name, price, 1);
                HttpWebRequest request = WebRequest.CreateHttp("http://192.168.1.238/demorestapi/api/product/" + postString);
                request.Method = "PUT";
                request.ContentType = "Application/json;charset:UTF-8";
                byte[] byteArr = Encoding.UTF8.GetBytes(postString);
                request.ContentLength = byteArr.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(byteArr, 0, byteArr.Length);
                stream.Close();

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(bool));
                object responeData = jsonSerializer.ReadObject(request.GetResponse().GetResponseStream());
                bool result = (bool)responeData;
                if (result)
                {
                    MessageBox.Show("Update product success");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Fail to Update product");
                }
            } 
        }
    }
}
