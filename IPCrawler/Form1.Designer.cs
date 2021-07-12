
namespace IPCrawler
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.inputFilePickerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.outputDirecrotyPickerButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.GenerateCSVButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(360, 23);
            this.textBox1.TabIndex = 0;
            // 
            // inputFilePickerButton
            // 
            this.inputFilePickerButton.Location = new System.Drawing.Point(378, 34);
            this.inputFilePickerButton.Name = "inputFilePickerButton";
            this.inputFilePickerButton.Size = new System.Drawing.Size(32, 23);
            this.inputFilePickerButton.TabIndex = 1;
            this.inputFilePickerButton.Text = "...";
            this.inputFilePickerButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Input file path:";
            // 
            // outputDirecrotyPickerButton
            // 
            this.outputDirecrotyPickerButton.Location = new System.Drawing.Point(379, 94);
            this.outputDirecrotyPickerButton.Name = "outputDirecrotyPickerButton";
            this.outputDirecrotyPickerButton.Size = new System.Drawing.Size(32, 23);
            this.outputDirecrotyPickerButton.TabIndex = 3;
            this.outputDirecrotyPickerButton.Text = "...";
            this.outputDirecrotyPickerButton.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 95);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(359, 23);
            this.textBox2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Output file path:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // GenerateCSVButton
            // 
            this.GenerateCSVButton.Location = new System.Drawing.Point(155, 139);
            this.GenerateCSVButton.Name = "GenerateCSVButton";
            this.GenerateCSVButton.Size = new System.Drawing.Size(105, 23);
            this.GenerateCSVButton.TabIndex = 6;
            this.GenerateCSVButton.Text = "Generate CSV";
            this.GenerateCSVButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 185);
            this.Controls.Add(this.GenerateCSVButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.outputDirecrotyPickerButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputFilePickerButton);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button inputFilePickerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button outputDirecrotyPickerButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button GenerateCSVButton;
    }
}

