using System;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace test1
{
    //khai bóa node
    class Node
    {
        public string value;
        public Node left;
        public Node right;
    }
    //khai báo và tạo dựng cây
    class Tree
    {

        public string ketqua;
        public string phatam;
        //khởi tạo
        public Tree()
        { }
        //hàm chèn node cho chuỗi
        public Node insert(Node root, string tudien)
        {
            if (root == null)
            {
                root = new Node();
                root.value = tudien;
            }
            else if (tudien.CompareTo(root.value) < 0)
            {
                root.left = insert(root.left, tudien);
            }
            else
            {
                root.right = insert(root.right, tudien);
            }

            return root;
        }
        //hàm tìm kiếm nghĩa theo từ nhập vào ở textbox
        public void search(Node root, string n)
        {

            if (root == null)
            {
                ketqua = "Từ điển chưa có!!!"; //
            }
            string[] listt = root.value.Split(':');
            if (n.CompareTo(listt[0]) == 0)
            {
                ketqua = listt[2]; //kết quả là chuỗi thứ 3 sau khi cắt, chuỗi này sẽ được cắt thêm lần nữa theo dấu | để hiển thị theo từng dòng
                phatam = listt[1]; //Phát âm bằng chuỗi thứ 2 sau khi cắt
            } 
            //phần Compare chuỗi để dùng tìm kiếm của cây nhị phân
            else if (n.CompareTo(listt[0]) < 0)
            {
                search(root.left, n);
            }
            else if (n.CompareTo(listt[0]) > 0)
            {
                search(root.right, n);
            }
        }
        //hàm bool kiểm tra để kiểm tra xem từ có nằm trong cây hay không
        public bool check(Node root, string n)
        {
            if (root == null)
            {
                return false;
            }
            string[] listt = root.value.Split(':'); //cắt và so sánh phần đầu tiên với từ nhập ở textbox
            if (n.CompareTo(listt[0]) == 0)
            {
                return true;
            }
            else if (n.CompareTo(listt[0]) < 0)
            {
                return check(root.left, n);
            }
            else if (n.CompareTo(listt[0]) > 0)
            {
                return check(root.right, n);
            }
            return true;
        }
        //hàm chỉnh sửa node trong cây, sửa nghĩa theo từ tiếng anh nhập vào
        public void edit(Node root, string n, string m)
        {
            string[] listt = root.value.Split(':'); //cắt chuổi và so sánh với phần đầu
            if (n.CompareTo(listt[0]) == 0)
            {
                root.value = listt[0] +"::"+ m; //sửa chuỗi mới bằng cộng phần đầu tiên bị cắt với phần nghĩa đã nhập ở sau (không thêm phần phát âm nên hai dấu : gần nhau,listt[1]= null
                return;
            }
            else if (n.CompareTo(listt[0]) < 0)
            {
                edit(root.left, n, m);
            }
            else if (n.CompareTo(listt[0]) > 0)
            {
                edit(root.right, n, m);
            }
        }
        //hàm tìm node để xóa
        public void del(Node root, string n)
        {
            string[] listt = root.value.Split(':'); //cắt chuỗi và chỉ so sánh với phần đầu tiên
            if (n.CompareTo(listt[0]) == 0)
            {
                delNode(root);
            }
            else if (n.CompareTo(listt[0]) < 0)
            {
                del(root.left, n);
            }
            else if (n.CompareTo(listt[0]) > 0)
            {
                del(root.right, n);
            }
        }
        //hàm xóa node
        public void delNode(Node root)
        {
            //không có con, xóa node
            if (root.left == null && root.right == null)
            {
                root = null;
            }
            //chỉ có con phải, con trái bằng null
            else if (root.left == null)
            {
                Node temp = root;
                root = root.right;
                temp = null;
            }
            //chỉ có con trái, con phải bằng null
            else if (root.right == null)
            {
                Node temp = root;
                root = root.left;
                temp = null;
            }
            //có cả 2 con, tìm giá trị trái nhất của cây bên phải bằng hàm findmin
            else
            {
                Node min = new Node();
                min = findmin(root.right);
                root.value = min.value; //gán giá trị nút bị xóa thành giá trị trái nhất
            }
        }
        //tìm giá trị của node trái nhất của một node root
        public Node findmin(Node root)
        {

            if (root.left == null)
            {
                Node min = root;
                delNode(root);
                return min;
            }
            else return findmin(root.left);
        }
    }
}