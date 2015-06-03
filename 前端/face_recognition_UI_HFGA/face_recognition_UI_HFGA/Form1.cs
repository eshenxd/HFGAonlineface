using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;

namespace face_recognition_UI_HFGA
{
    // 1.定义委托
    public delegate void DelReadStdOutput(string result);
    

    public partial class Form1 : Form
    {
        // 2.定义委托事件
        public event DelReadStdOutput ReadStdOutput;
        

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            //3.将相应函数注册到委托事件中
            ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*< 查找进程是否正在运行，若是则关闭进程 */
            string proName = "OnlineFaceofHFGAv1.0";//这里不能加.exe进程名中不加扩展名
            Process[] pro = Process.GetProcessesByName(proName);

            if(pro.Length>0)pro[0].Kill();

            richTextBox1.Text = null;

            /*< 开始新进程 */
            Process process = new Process();
            
            string path = "OnlineFaceofHFGAv1.0.exe";
            string args = " ";
            args = args + "1" + " ";
            ProcessStartInfo info = new ProcessStartInfo(path, args);

            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;

            process.StartInfo = info;
            process.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            process.Start();
            process.BeginOutputReadLine();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*< 查找进程是否正在运行，若是则关闭进程 */
            string proName = "OnlineFaceofHFGAv1.0";//这里不能加.exe进程名中不加扩展名
            Process[] pro = Process.GetProcessesByName(proName);

            if (pro.Length > 0) pro[0].Kill();

            richTextBox1.Text = null;
            /*< 开始新进程 */
            Process process = new Process();

            string path = "OnlineFaceofHFGAv1.0.exe";
            string args = " ";
            args = args + "2" + " ";
            ProcessStartInfo info = new ProcessStartInfo(path, args);

            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;

            process.StartInfo = info;
            //process.Start();

            process.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            process.Start();
            process.BeginOutputReadLine();
        
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                // 4. 异步调用，需要invoke
                this.Invoke(ReadStdOutput, new object[] { e.Data });
            }
        }

        private void ReadStdOutputAction(string result)
        {
            this.richTextBox1.AppendText(result + "\r\n");

            string final_path = String.Empty;
            string sub_str = "姓名：";

            int index = result.IndexOf("姓名：");
            if (index != -1) 
            {
                index = result.IndexOf(sub_str)+ sub_str.Length;
                final_path = result.Substring(index);

                string CardPath = "../Register/cardpic/" + final_path + ".jpg";
                string imagePath = "../Register/image/" + final_path + ".jpg";

                pictureBox1.ImageLocation = CardPath;
               
                pictureBox2.ImageLocation = imagePath;
               
            }

           
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string proName = "OnlineFaceofHFGAv1.0";//这里不能加.exe进程名中不加扩展名
            Process[] pro = Process.GetProcessesByName(proName);

            if (pro.Length > 0) pro[0].Kill();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
    }
}
 