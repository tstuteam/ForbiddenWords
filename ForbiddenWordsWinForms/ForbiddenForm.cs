using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ForbiddenWordsLib;

namespace ForbiddenWordsWinForms
{
    public partial class forbiddenWordForm : Form
    {
        public forbiddenWordForm()
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
                try
                {
                    var forbiddenWords = WorkFile.ReadFile(path, out int M);
                    try
                    {
                        bestWord = fWUtils.MakeBestWord(M, forbiddenWords.Count, forbiddenWords);
                    }
                    catch (Exception)
                    {
                        throw;
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
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    var result = MessageBox.Show(message, caption, buttons);
                }
                catch (ArgumentOutOfRangeException)
                {
                    var message = "The content of the file is incorrect.\n" +
                        "Read the terms of the problem, check the contents of the file.";
                    var caption = "Error";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    var result = MessageBox.Show(message, caption, buttons);
                }
                catch (ArgumentException argumentException)
                {
                    var message = "File path not entered.\n" + argumentException.Message;
                    var caption = "Error Detected in Input";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    var result = MessageBox.Show(message, caption, buttons);
                }
                catch (FormatException formatException)
                {
                    var message = "The content of the file is incorrect.\n" +
                        "Read the terms of the problem, check the contents of the file.";
                    var caption = formatException.Message;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    var result = MessageBox.Show(message, caption, buttons);
                }
            }
            catch (Exception)
            {
                throw;
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
