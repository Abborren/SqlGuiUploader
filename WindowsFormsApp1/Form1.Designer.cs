using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApp1
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
            this.Path = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.StatusField = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Path
            // 
            this.Path.Location = new System.Drawing.Point(13, 13);
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            this.Path.Size = new System.Drawing.Size(532, 20);
            this.Path.TabIndex = 1;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(235, 226);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 0;
            this.submitButton.Text = "Upload";
            this.submitButton.UseVisualStyleBackColor = true;
            // 
            // StatusField
            // 
            this.StatusField.Location = new System.Drawing.Point(13, 53);
            this.StatusField.Name = "StatusField";
            this.StatusField.ReadOnly = true;
            this.StatusField.Size = new System.Drawing.Size(111, 20);
            this.StatusField.TabIndex = 2;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            ((Control) this).DragDrop += new DragEventHandler(this.DragDrop);
            ((Control) this).DragEnter += new DragEventHandler(this.DragEnter);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 261);
            this.Controls.Add(this.StatusField);
            this.Controls.Add(this.Path);
            this.Controls.Add(this.submitButton);
            this.Name = "Form1";
            this.Text = "SQLUploader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Path;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.TextBox StatusField;
        
        
        private void DragDrop(object sender, DragEventArgs e)
        {
            Console.Write("Drop triggered");
            // Handle FileDrop data.
            if(e.Data.GetDataPresent(DataFormats.FileDrop) )
            {
                // Assign the file names to a string array, in 
                // case the user has selected multiple files.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    Path.Text = files[0];
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            this.Invalidate();
        }
        private void DragEnter(object sender, DragEventArgs e)
        {
            // If the data is a file or a bitmap, display the copy cursor.
            if (e.Data.GetDataPresent(DataFormats.Text) || 
                e.Data.GetDataPresent(DataFormats.FileDrop) ) 
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
    
}

