﻿namespace Program
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.callUpButton = new System.Windows.Forms.Button();
            this.callDownButton = new System.Windows.Forms.Button();
            this.callPictureBox = new System.Windows.Forms.PictureBox();
            this.displayPictureBox = new System.Windows.Forms.PictureBox();
            this.elevatorPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.callPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevatorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // callUpButton
            // 
            this.callUpButton.BackColor = System.Drawing.Color.Transparent;
            this.callUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.callUpButton.Location = new System.Drawing.Point(589, 297);
            this.callUpButton.Name = "callUpButton";
            this.callUpButton.Size = new System.Drawing.Size(20, 30);
            this.callUpButton.TabIndex = 3;
            this.callUpButton.UseVisualStyleBackColor = false;
            this.callUpButton.Click += new System.EventHandler(this.CallUpButton_Click);
            // 
            // callDownButton
            // 
            this.callDownButton.BackColor = System.Drawing.Color.Transparent;
            this.callDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.callDownButton.Location = new System.Drawing.Point(589, 321);
            this.callDownButton.Name = "callDownButton";
            this.callDownButton.Size = new System.Drawing.Size(20, 30);
            this.callDownButton.TabIndex = 4;
            this.callDownButton.UseVisualStyleBackColor = false;
            this.callDownButton.Click += new System.EventHandler(this.CallDownButton_Click);
            // 
            // callPictureBox
            // 
            this.callPictureBox.Image = global::Program.Properties.Resources.btn_1;
            this.callPictureBox.Location = new System.Drawing.Point(580, 280);
            this.callPictureBox.Name = "callPictureBox";
            this.callPictureBox.Size = new System.Drawing.Size(35, 70);
            this.callPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.callPictureBox.TabIndex = 2;
            this.callPictureBox.TabStop = false;
            // 
            // displayPictureBox
            // 
            this.displayPictureBox.Image = global::Program.Properties.Resources.display;
            this.displayPictureBox.Location = new System.Drawing.Point(329, 7);
            this.displayPictureBox.Name = "displayPictureBox";
            this.displayPictureBox.Size = new System.Drawing.Size(100, 50);
            this.displayPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.displayPictureBox.TabIndex = 1;
            this.displayPictureBox.TabStop = false;
            // 
            // elevatorPictureBox
            // 
            this.elevatorPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elevatorPictureBox.Image = global::Program.Properties.Resources.elevator_1;
            this.elevatorPictureBox.Location = new System.Drawing.Point(200, 60);
            this.elevatorPictureBox.Name = "elevatorPictureBox";
            this.elevatorPictureBox.Size = new System.Drawing.Size(364, 471);
            this.elevatorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.elevatorPictureBox.TabIndex = 0;
            this.elevatorPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.callDownButton);
            this.Controls.Add(this.callUpButton);
            this.Controls.Add(this.callPictureBox);
            this.Controls.Add(this.displayPictureBox);
            this.Controls.Add(this.elevatorPictureBox);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elevator";
            ((System.ComponentModel.ISupportInitialize)(this.callPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevatorPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox elevatorPictureBox;
        private System.Windows.Forms.PictureBox displayPictureBox;
        private System.Windows.Forms.PictureBox callPictureBox;
        private System.Windows.Forms.Button callUpButton;
        private System.Windows.Forms.Button callDownButton;
    }
}

