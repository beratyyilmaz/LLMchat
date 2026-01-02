namespace LLMChatbot
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
            // Form ve kontroller için gerekli bileşenleri başlatıyoruz
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // 
            // richTextBoxChat - Sohbet geçmişinin görüntüleneceği alan
            // 
            this.richTextBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richTextBoxChat.Location = new System.Drawing.Point(12, 12);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true; // Sadece okunabilir
            this.richTextBoxChat.Size = new System.Drawing.Size(776, 400);
            this.richTextBoxChat.TabIndex = 0;
            this.richTextBoxChat.Text = "";
            
            // 
            // textBoxMessage - Kullanıcının mesaj yazacağı alan
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxMessage.Location = new System.Drawing.Point(12, 430);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(650, 50);
            this.textBoxMessage.TabIndex = 1;
            // Enter tuşuna basıldığında mesaj gönderilsin (Ctrl+Enter ile yeni satır)
            this.textBoxMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxMessage_KeyDown);
            
            // 
            // buttonSend - Mesaj gönderme butonu
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonSend.Location = new System.Drawing.Point(668, 430);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(120, 50);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Gönder";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            
            // 
            // labelStatus - Durum göstergesi ("Yazıyor..." mesajı için)
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelStatus.ForeColor = System.Drawing.Color.Gray;
            this.labelStatus.Location = new System.Drawing.Point(12, 490);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 15);
            this.labelStatus.TabIndex = 3;
            
            // 
            // Form1 - Ana form ayarları
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.richTextBoxChat);
            this.MinimumSize = new System.Drawing.Size(600, 400); // Minimum pencere boyutu
            this.Name = "Form1";
            this.Text = "LLM Sohbet Botu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // UI kontrollerini tanımlıyoruz
        private System.Windows.Forms.RichTextBox richTextBoxChat; // Sohbet geçmişi
        private System.Windows.Forms.TextBox textBoxMessage; // Mesaj girişi
        private System.Windows.Forms.Button buttonSend; // Gönder butonu
        private System.Windows.Forms.Label labelStatus; // Durum etiketi
    }
}

