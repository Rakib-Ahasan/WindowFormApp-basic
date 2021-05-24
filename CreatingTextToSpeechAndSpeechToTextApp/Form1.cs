using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace CreatingTextToSpeechAndSpeechToTextApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SpeechSynthesizer ss=new SpeechSynthesizer();
            //ss.Volume = trackBar1.Value;
            //ss.Speak(textBox1.Text);

            SpeechRecognitionEngine s=new SpeechRecognitionEngine();
            Grammar word=new DictationGrammar();
            s.LoadGrammar(word);
            try
            {
                s.SetInputToDefaultAudioDevice();
                RecognitionResult result = s.Recognize();
                textBox1.Text = result.Text;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                s.UnloadAllGrammars();
            }
        }
    }
}
