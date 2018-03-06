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
    public partial class Replace : Form {
        private Main Editor;
        public Replace(Main ctx) {
            Editor = ctx;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            Main mainWindow = new Main("", "");
            try {
                Editor.replaceFirstWord(textBox1.Text, textBox2.Text);
                button1.Text = "Next";
            } catch (Exception ex) {
                Console.WriteLine("Oops an error occurred: '{0}'", ex);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            Main mainWindow = new Main("", "");
            try {
                Editor.replaceAll(textBox1.Text, textBox2.Text);
            } catch (Exception ex) {
                Console.WriteLine("Oops an error occurred: '{0}'", ex);
            }
        }
    }
}
