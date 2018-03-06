using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject {
    public partial class Start : Form {
        public Start() {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            Main editor = new Main("", "");
            editor.MinimumSize = new Size(700, 400);
            editor.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Text Files (.txt)|*.txt";
            of.Title = "Open File";

            if (of.ShowDialog() == DialogResult.OK) {
                System.IO.StreamReader sr = new System.IO.StreamReader(of.FileName);
                var txt = sr.ReadToEnd();
                sr.Close();
                this.Hide();
                Main editor = new Main(txt, of.FileName);
                editor.MinimumSize = new Size(700, 500);
                editor.ShowDialog();
                this.Close();
            }
        }


    }
}
