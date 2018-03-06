using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FinalProject {
    public partial class Main : Form {
        string NAME = "New File";
        int INDEX;
        private Search searchForm;
        private Replace replaceForm;

        public Main(string txt, string name) {
            NAME = name;
            InitializeComponent();
            richTextBox1.Text = txt;
            button1.TabStop = false;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button2.TabStop = false;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            NAME = "New file";
            toolStripStatusLabel1.Text = "New file";
            toolStripStatusLabel2.Text = DateTime.Now.ToShortDateString();
        }

        //Declare all buttons in menu strip
        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            newFile(); 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text) == false) {
                if (MessageBox.Show("Do you want to save changes to your text?", "Warning!",
                    MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    if (NAME == "New file") {
                        saveFileAs();
                    } else {
                        saveFileAs();
                    }
                }
            }
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            saveFileAs();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.SelectAll();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e) {
            fontDialog1.ShowColor = true;
            fontDialog1.Color = richTextBox1.SelectionColor;
            fontDialog1.Font = richTextBox1.SelectionFont;
       
            if (fontDialog1.ShowDialog() != DialogResult.Cancel) {
                richTextBox1.SelectionFont = fontDialog1.Font;
                richTextBox1.SelectionColor = fontDialog1.Color;
            }
        }

        private void dateAndTimeToolStripMenuItem_Click(object sender, EventArgs e) {
            richTextBox1.Text += DateTime.Now.ToLongTimeString() + DateTime.Now.ToShortDateString();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            Author autorBox = new Author();
            autorBox.MinimumSize = new Size(100, 100);
            autorBox.StartPosition = FormStartPosition.CenterScreen;
            autorBox.ShowDialog();
        }

        private void searchToolStripMenuItem2_Click(object sender, EventArgs e) {
            INDEX = 0;
            Search search = new Search(this);
            searchForm = search;
            search.MinimumSize = new Size(240, 110);
            search.StartPosition = FormStartPosition.CenterScreen;
            search.Show(this);
        }

        private void findReplaceToolStripMenuItem_Click(object sender, EventArgs e) {
            Replace replace = new Replace(this);
            replaceForm = replace;
            replace.MinimumSize = new Size(250, 180);
            replace.StartPosition = FormStartPosition.CenterScreen;
            replace.Show(this);
        }
        //End of declaration of all buttons in menu strip

        //Custom functions
        private void newFile() {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text) == false) {
                if (MessageBox.Show("Do you want to save changes to " + NAME + " ?", "Warning!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                    if (NAME == "New file") {
                        saveFileAs();
                    } else {
                        saveFile();
                    }
                    richTextBox1.Clear();
                } else {
                    richTextBox1.Clear();
                }
            }
            NAME = "New file";
            toolStripStatusLabel1.Text = "New file";
        }
        public void openFile() {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Text Files (.txt)|*.txt";
            of.Title = "Open File";
            of.FileName = "*.txt";

            if (of.ShowDialog() == DialogResult.OK) {
                try {
                    NAME = of.FileName;
                    toolStripStatusLabel1.Text = NAME;
                    System.IO.StreamReader sr = new System.IO.StreamReader(of.FileName);
                    richTextBox1.Text = sr.ReadToEnd();
                    sr.Close();
                } catch(Exception e) {
                    Console.WriteLine("Oops an error occurred: '{0}'", e);
                }
            }
        }
        public void saveFile() {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(NAME);
            try {
                sw.WriteLine(richTextBox1.Text);
            } catch (Exception e) {
                Console.WriteLine("Oops an error occurred: '{0}'", e);
            } finally {
                sw.Close();
            }
        }

        public void saveFileAs() {
            SaveFileDialog sfa = new SaveFileDialog();
            sfa.Filter = "Text Files (.txt)|*.txt";
            sfa.Title = "Save File";
            sfa.FileName = "*.txt";
            
            if (sfa.ShowDialog() == DialogResult.OK) {
                NAME = sfa.FileName;
                try {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(NAME);
                    sw.WriteLine(richTextBox1.Text);
                    sw.Close();
                } catch (Exception e) {
                    Console.WriteLine("Oops an error occurred: '{0}'", e);
                }
            }
        }
        public void searchWord(string word) {
            int i = INDEX;
            if (richTextBox1.Text.Contains(word)) {
                if ((i = richTextBox1.Text.IndexOf(word, (i + 1))) != -1) {
                    richTextBox1.Select(i, word.Length);
                    this.Focus();
                    i++;
                }
            } else {
                MessageBox.Show("There are no other occurances of searched word!", "Warning!", MessageBoxButtons.OK);
                searchForm.Close();
            }
            INDEX = i;
        }

        public void replaceFirstWord(string word, string replace) {
            if (richTextBox1.Text.Contains(word)) {
                Regex rgx = new Regex(word);
                richTextBox1.Text = rgx.Replace(richTextBox1.Text, replace, 1);
            } else {
                MessageBox.Show("There are no other occurances of searched word!", "Warning!", MessageBoxButtons.OK);
                replaceForm.Close();
            }
        }
        public void replaceAll(string word, string replace) {
            if (richTextBox1.Text.Contains(word)) {
                Regex rgx = new Regex(word);
                richTextBox1.Text = rgx.Replace(richTextBox1.Text, replace);
            } else {
                MessageBox.Show("There are no other occurances of searched word!", "Warning!", MessageBoxButtons.OK);
                replaceForm.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            richTextBox1.MinimumSize = this.Size;
            richTextBox1.MaximumSize = this.Size;

        }


    }
}
