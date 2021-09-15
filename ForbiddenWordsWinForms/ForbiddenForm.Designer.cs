
namespace ForbiddenWordsWinForms
{
    partial class ForbiddenWordForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForbiddenWordForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.filePathTb = new System.Windows.Forms.TextBox();
            this.bestTb = new System.Windows.Forms.TextBox();
            this.penaltyTb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.findBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(776, 475);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 534);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "Введите имя файла:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(451, 531);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 38);
            this.label3.TabIndex = 2;
            this.label3.Text = "Best:";
            // 
            // filePathTb
            // 
            this.filePathTb.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.filePathTb.Location = new System.Drawing.Point(288, 531);
            this.filePathTb.Name = "filePathTb";
            this.filePathTb.Size = new System.Drawing.Size(150, 45);
            this.filePathTb.TabIndex = 3;
            this.filePathTb.TextChanged += new System.EventHandler(this.filePathTb_TextChanged);
            // 
            // bestTb
            // 
            this.bestTb.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bestTb.Location = new System.Drawing.Point(621, 537);
            this.bestTb.Name = "bestTb";
            this.bestTb.Size = new System.Drawing.Size(150, 45);
            this.bestTb.TabIndex = 4;
            // 
            // penaltyTb
            // 
            this.penaltyTb.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.penaltyTb.Location = new System.Drawing.Point(621, 594);
            this.penaltyTb.Name = "penaltyTb";
            this.penaltyTb.Size = new System.Drawing.Size(150, 45);
            this.penaltyTb.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(444, 594);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 38);
            this.label4.TabIndex = 6;
            this.label4.Text = "Штраф Best";
            // 
            // findBtn
            // 
            this.findBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.findBtn.Location = new System.Drawing.Point(51, 594);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(150, 58);
            this.findBtn.TabIndex = 7;
            this.findBtn.Text = "Найти Best";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clearBtn.Location = new System.Drawing.Point(288, 595);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(150, 57);
            this.clearBtn.TabIndex = 8;
            this.clearBtn.Text = "Очистить";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // forbiddenWordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(800, 709);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.penaltyTb);
            this.Controls.Add(this.bestTb);
            this.Controls.Add(this.filePathTb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ForbiddenWordForm";
            this.Text = "Запрещённые слова";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox filePathTb;
        private System.Windows.Forms.TextBox bestTb;
        private System.Windows.Forms.TextBox penaltyTb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Button clearBtn;
    }
}

