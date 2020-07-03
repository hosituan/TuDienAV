using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace test1
{
    //phần giao diện
    public partial class Form1 : Form
    {
        //khai báo cây
        Node root = null;
        Tree bst = new Tree();

        private void Form1_Load()
        {
        }
        public Form1()
        {
            InitializeComponent();
        }
        //Câu lệnh cho button Translate
        //Thông báo sẽ có màu đỏ
        public void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear(); //clear richtexBox
            //kiểm tra từ nhập vào có trong cơ sở dữ liệu chưa
            if (bst.check(root,textBox1.Text)==false)
            {
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Từ điển chưa có!!!\nBạn vui lòng nhập từ khác, hoặc có thể sử dụng tính năng thêm từ!!!";
            }
            //có
            else
            {
                bst.search(root, textBox1.Text);
                label3.Text = bst.phatam;
                string[] listnghia = bst.ketqua.Split('|'); //cắt chuỗi của phần kết quả thành nhiều phần qua dấu |, để hiển thị ở richtextBox nhiều dòng
                foreach (string str in listnghia) //hiển thị từng dòng ở richtextBox
                {
                    string str_ = str;
                    richTextBox1.Text += str_ += "\n";
                    richTextBox1.ForeColor = Color.Green; //hiển thị nghĩa màu xanh
                }
            }
        }
        //câu lệnh cho button thêm từ
        private void button2_Click(object sender, EventArgs e)
        {
            //kiểm tra đã có từ trong cơ sở dữ liệu chưa
            if (bst.check(root,tb_anh_add.Text)==true)
            {
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Từ điển đã có rồi không cần thêm!!!"; //thông báo
            }
            else
            {
                string add_word = tb_anh_add.Text + "::" + tb_viet_add.Text; //tạo chuỗi theo format để dùng hàm insert node đưa từ vào cây
                bst.insert(root, add_word);
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Từ điển đã được thêm từ '" + tb_anh_add.Text + "' !!!"; //thông báo sau khi thêm từ
            }

        }
        //câu lệnh cho button sửa - edit
        private void bt_edit_Click(object sender, EventArgs e)
        {
            //kiểm tra
            if (bst.check(root, tb_anh_edit.Text) == false)
            {
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Từ điển chưa có từ này để sửa!!!";
            }
            else
            {
                bst.edit(root, tb_anh_edit.Text, tb_viet_edit.Text);
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Đã sửa nghĩa của từ " + tb_anh_edit.Text + "!!!";
            }
        }
        //button xóa
        private void bt_del_Click(object sender, EventArgs e)
        {
            if (bst.check(root, tb_del.Text) == false)
            {
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Từ điển chưa có từ này để xóa!!!";
            }
            else
            {
                bst.del(root, tb_del.Text);
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Đã xóa!!!";
            }
        }


        //button load từ
        private void load_dic_Click(object sender, EventArgs e)
        {
            //đọc file
            progressBar1.Value = 1; //giá trị đầu progress
            FileStream fs = new FileStream("av.dict", FileMode.Open);
            StreamReader rd = new StreamReader(fs, Encoding.UTF8);//đọc file
            StreamWriter rw = new StreamWriter(fs, Encoding.Unicode); //khai báo để sau khi bấm vào button exit, thì sẽ ghi vào file gốc những từ đã thêm, sửa xóa cập nhật cho file từ điển
            progressBar1.Step = 1; //step của progress
            long SIZE = 10000;
            long j = 0;
            SIZE =Convert.ToInt64(soluongtu.Text); //đọc só lượng từ từ textbox, convert sang kiểu long
            for (long i = 0; i < SIZE; i++)
            {
                root = bst.insert(root, rd.ReadLine()); //đọc theo từng dòng
                progressBar1.PerformStep(); //giá trị thay đổi
                j++;
                label5.Text = Convert.ToString(j) + "/103,480"; //số lượng từ
            }
            progressBar1.Value = 100; //giá trị cuối
            this.load_dic.Enabled = false;
            richTextBox1.Text = "Đã Load xong file từ điển!!!";
        }
        //hàm auto clear giá trị có sẵn trong Textbox nhập từ khi click vào để nhập từ khác mà không cần xóa từ cũ
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }


    }
}
