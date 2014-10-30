namespace Lab_4
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hintBox = new System.Windows.Forms.CheckBox();
            this.cleanBoard = new System.Windows.Forms.Button();
            this.topMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hintBox
            // 
            this.hintBox.AutoSize = true;
            this.hintBox.Location = new System.Drawing.Point(12, 14);
            this.hintBox.Name = "hintBox";
            this.hintBox.Size = new System.Drawing.Size(50, 17);
            this.hintBox.TabIndex = 0;
            this.hintBox.Text = "Hints";
            this.hintBox.UseVisualStyleBackColor = true;
            this.hintBox.CheckedChanged += new System.EventHandler(this.hintBox_CheckedChanged);
            // 
            // cleanBoard
            // 
            this.cleanBoard.Location = new System.Drawing.Point(100, 10);
            this.cleanBoard.Name = "cleanBoard";
            this.cleanBoard.Size = new System.Drawing.Size(75, 23);
            this.cleanBoard.TabIndex = 1;
            this.cleanBoard.Text = "Clear";
            this.cleanBoard.UseVisualStyleBackColor = true;
            this.cleanBoard.Click += new System.EventHandler(this.cleanBoard_Click);
            // 
            // topMessage
            // 
            this.topMessage.AutoSize = true;
            this.topMessage.Location = new System.Drawing.Point(258, 14);
            this.topMessage.Name = "topMessage";
            this.topMessage.Size = new System.Drawing.Size(35, 13);
            this.topMessage.TabIndex = 2;
            this.topMessage.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.topMessage);
            this.Controls.Add(this.cleanBoard);
            this.Controls.Add(this.hintBox);
            this.Name = "Form1";
            this.Text = "Eight Queens by Raymond Li";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox hintBox;
        private System.Windows.Forms.Button cleanBoard;
        private System.Windows.Forms.Label topMessage;

    }
}

