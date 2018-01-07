using BigBeer.WinForm.GraphicRecognition.Do;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BigBeer.WinForm.GraphicRecognition
{
    public partial class 图文识别 : Form
    {
        public 图文识别()
        {
            InitializeComponent();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            //判断用户是否正确的选择了文件
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(fileDialog.FileName);
                pictureBox1.Image = img;
                //获取用户选择文件的后缀名
                string extension = Path.GetExtension(fileDialog.FileName);
                //声明允许的后缀名
                string[] str = new string[] { ".gif", ".jpge", ".jpg", ".png", ".ico" };
                if (!((IList)str).Contains(extension))
                {
                    MessageBox.Show("仅能上传gif,jpge,jpg,png格式的图片！");
                }
                else
                {
                   var data =  GeneralBasic.General(fileDialog.FileName);
                    richTextBox1.Text =data;
                    ////获取用户选择的文件，并判断文件大小不能超过20K，fileInfo.Length是以字节为单位的
                    //FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                    //if (fileInfo.Length > 20480)
                    //{
                    //    MessageBox.Show("上传的图片不能大于20K");
                    //}
                    //else
                    //{
                    //    //在这里就可以写获取到正确文件后的代码了
                    //}
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text.Contains("http"))
            //{
            //    MessageBox.Show("请输入正确的图片地址");
            //}
                    var img = GetImage.Page_Load(textBox1.Text);
                    pictureBox1.Image = img;
                    var data = GeneralBasic.GeneralBasicUrl(textBox1.Text);
                    richTextBox1.Text = data;

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
