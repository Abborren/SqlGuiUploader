using System;
using System.Diagnostics;
using System.IO;
using System.Text;
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
            this.pathField = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.StatusField = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Path
            // 
            this.pathField.Location = new System.Drawing.Point(13, 13);
            this.pathField.Name = "Path";
            this.pathField.ReadOnly = true;
            this.pathField.Size = new System.Drawing.Size(532, 20);
            this.pathField.TabIndex = 1;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(235, 226);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 0;
            this.submitButton.Text = "Upload";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new EventHandler(this.submitButton_Click);

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
            this.Controls.Add(this.pathField);
            this.Controls.Add(this.submitButton);
            this.Name = "Form1";
            this.Text = "SQLUploader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathField;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.TextBox StatusField;
        // The Output array that should be sent to DB.
        private string[] outputArr;
        private bool fileIsValid = false;
        #region GuiDrag
        
        private new void DragEnter(object sender, DragEventArgs e)
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
        private new void DragDrop(object sender, DragEventArgs e)
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
                    if (FileValid(files[0]))
                    {
                        fileIsValid = true;
                        
                        var readFile = ReadFile(files[0]);
                        
                        outputArr = ProccessFile(readFile);
                        // This shows the file path 
                        pathField.Text = files[0];
                        StatusField.Text = "File is Valid.";

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            this.Invalidate();
        }

        #endregion

        #region FileProccessing
        
        private string[] ProccessFile(string[] fileParts)
        {
            try
            {
                int arrayOffset = 0;
                string[] items = {"Mj�lkningar senaste 24 tim",
                    "Mj�lkm�ngd senaste 24 tim", 
                    "Genomsn. mj�lkm�ngd per mj�lkning senaste 24 tim"};
                string[] output = new string[items.Length];
                // Splits string at tab.
                string[] firstLine = fileParts[0].Split('\t');
                // First Parses Away unkown characters and replaces , with .  then splits at tab
                string[] secondLine = ParseUnkownChar(fileParts[1]).Split('\t');
                for (int i = 0; i < firstLine.Length; i++)
                {
                    for (int j = 0; j < items.Length; j++)
                    {
                        if (firstLine[i] == items[j])
                        {
                            output[arrayOffset] = secondLine[i];
                            arrayOffset++;
                            
                            break;
                        }
                    }
                    if (arrayOffset >= 3 )
                    {
                        break;
                    }
                }
                return output;
            }
            catch (Exception e)
            {   
                throw new IndexOutOfRangeException("Index ran away");
            }
            
        }
        
        private string[] ReadFile(string filePath)
        {
            try
            {
                string[] file = {"",""};
                var i = 0;
                using (StreamReader sr = new StreamReader(filePath)) 
                {
                    while (sr.Peek() >= 0)
                    {
                        file[i] = sr.ReadLine();
                        i++;
                    }
                }
                MessageBox.Show(file.ToString());
                return file;
            } catch (Exception e) 
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                throw new FileLoadException("File Failed loading.");
            }
        }

        private string ParseUnkownChar(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            
            for (int i = 0; i < s.Length; i++)
            {
                if (sb[i].Equals('�'))
                {
                    sb[i] = ' '; // index starts at 0!
                } else if (sb[i].Equals(','))
                {
                    sb[i] = '.';
                }
            }
            return sb.ToString();
        }
        private bool FileValid(string file)
        {
            if (file.EndsWith(".txt"))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region buttonClick

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (fileIsValid)
            {
                StatusField.Text = "Sending File...";
                bool uploadSuccesful = new Sql().Upload(outputArr);
                if (uploadSuccesful)
                {
                    StatusField.Text = "Upload successfull.";
                }
                else
                {
                    StatusField.Text = "Upload Failed.";
                }

                pathField.Text = "";
            }
            
            
            //this.Close();
        }

        #endregion
    }
    
        
}

