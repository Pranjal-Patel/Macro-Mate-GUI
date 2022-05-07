using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Macro_Mate
{
    public partial class Form1 : Form
    {
        private string file_name;
        private string runner_file_name = "main.py";

        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog dialog;
        private void button2_Click(object sender, EventArgs e)
        {
            dialog = new OpenFileDialog();
            dialog.Filter = "Macro File (*.mc)|*.mc|All files (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.Title = "Select a macro file";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file_name = dialog.FileName;
                tbFile_path.Text = file_name;

                if (file_name != null) btnRun.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnRun.Enabled = false;
            progressBar1.Value = 0;
            checkBox1.Checked = false;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd";
            psi.Arguments = $"/c python {runner_file_name} {file_name}";
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;

            progressBar1.Value = 0;

            Process ps = Process.Start(psi);
            while (!ps.HasExited) { }
            on_exit();
        }

        void on_exit()
        {
            progressBar1.Value = 100;
            if (checkBox1.Checked) MessageBox.Show("Done", "Macro Mate", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
