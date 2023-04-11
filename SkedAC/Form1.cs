using System.ComponentModel;
using System.Diagnostics;
using System.Net;

namespace SkedAC
{
    public partial class Form1 : Form
    {
        Recorder recorder = new Recorder();
        public string urlMatch = "";
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button2.Visible = false;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            this.button1.Visible = false;
            this.button2.Visible = true;

            if (!recorder._isRecording)
            {
                recorder.StartRecording();
            }
            else
            {
                recorder.StopRecording();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.Visible = false;
            this.button1.Visible = true;

            recorder.StopRecording();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.button2.Visible == true)
            {
                e.Cancel = true;
                MessageBox.Show("Veuillez arrêter le processus en cours avant de fermer le programme.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }
    }
}