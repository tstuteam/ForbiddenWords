using System;
using System.IO;
using System.Windows.Forms;
using ForbiddenWordsLib;

namespace ForbiddenWordsWinForms
{
    public partial class ForbiddenWordForm : Form
    {
        public ForbiddenWordForm()
        {
            InitializeComponent();
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            var fWUtils = new ForbiddenWordUtils();
            var path = filePathTb.Text ?? string.Empty;
            var bestWord = new ForbiddenWord();

            try
            {
                var forbiddenWords = WorkFile.ReadFile(path, out var m);
                try
                {
                    bestWord = fWUtils.MakeBestWord(m, forbiddenWords.Count, forbiddenWords);
                }
                finally
                {
                    bestTb.Text = bestWord.Word;
                    penaltyTb.Text = bestWord.Penalty.ToString();
                }
            }
            catch (IOException ioException)
            {
                var message = ioException.Message;
                var caption = "Error Detected in Input";
                var buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            catch (ArgumentOutOfRangeException)
            {
                var message = "The content of the file is incorrect.\n" +
                              "Read the terms of the problem, check the contents of the file.";
                var caption = "Error";
                var buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            catch (ArgumentException argumentException)
            {
                var message = "File path not entered.\n" + argumentException.Message;
                var caption = "Error Detected in Input";
                var buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            catch (FormatException formatException)
            {
                var message = "The content of the file is incorrect.\n" +
                              "Read the terms of the problem, check the contents of the file.";
                var caption = formatException.Message;
                var buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void filePathTb_TextChanged(object sender, EventArgs e)
        {
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            filePathTb.Clear();
            bestTb.Clear();
            penaltyTb.Clear();
        }
    }
}
