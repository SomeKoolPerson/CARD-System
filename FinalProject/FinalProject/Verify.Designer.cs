namespace FinalProject
{
    partial class Verify
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
            this.verificationLabel = new System.Windows.Forms.Label();
            this.verCodeTextBox = new System.Windows.Forms.TextBox();
            this.enterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // verificationLabel
            // 
            this.verificationLabel.AutoSize = true;
            this.verificationLabel.Font = new System.Drawing.Font("Times New Roman Uni", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verificationLabel.Location = new System.Drawing.Point(71, 9);
            this.verificationLabel.Name = "verificationLabel";
            this.verificationLabel.Size = new System.Drawing.Size(169, 33);
            this.verificationLabel.TabIndex = 0;
            this.verificationLabel.Text = "Verification Code:";
            // 
            // verCodeTextBox
            // 
            this.verCodeTextBox.Location = new System.Drawing.Point(36, 54);
            this.verCodeTextBox.Name = "verCodeTextBox";
            this.verCodeTextBox.Size = new System.Drawing.Size(240, 20);
            this.verCodeTextBox.TabIndex = 1;
            this.verCodeTextBox.TextChanged += new System.EventHandler(this.verCodeTextBox_TextChanged);
            // 
            // enterButton
            // 
            this.enterButton.Font = new System.Drawing.Font("Times New Roman Uni", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enterButton.Location = new System.Drawing.Point(91, 87);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(135, 48);
            this.enterButton.TabIndex = 2;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // Verify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 147);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.verCodeTextBox);
            this.Controls.Add(this.verificationLabel);
            this.Name = "Verify";
            this.Text = "Verify";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label verificationLabel;
        private System.Windows.Forms.TextBox verCodeTextBox;
        private System.Windows.Forms.Button enterButton;
    }
}