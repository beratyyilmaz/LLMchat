using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LLMChatbot
{
    /// <summary>
    /// Ana form sınıfı. Kullanıcı arayüzü ve olay yönetimini içerir.
    /// </summary>
    public partial class Form1 : Form
    {
        // OpenAI API servisini kullanmak için nesne
        private OpenAIService _openAIService;
        
        // Sohbet geçmişini tutmak için liste
        // Bu liste, API'ye gönderilirken tüm konuşma bağlamını içerir
        private List<ChatMessage> _conversationHistory;

        /// <summary>
        /// Form1 sınıfının yapıcı metodu.
        /// Form başlatıldığında çağrılır.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            
            // OpenAI servisini başlatıyoruz
            _openAIService = new OpenAIService();
            
            // Sohbet geçmişi listesini oluşturuyoruz
            _conversationHistory = new List<ChatMessage>();
            
            // Başlangıç mesajını gösteriyoruz
            AddMessageToChat("Sistem", "LLM Sohbet Botu'na hoş geldiniz! Mesajınızı yazıp 'Gönder' butonuna basabilirsiniz.", Color.Blue);
        }

        /// <summary>
        /// "Gönder" butonuna tıklandığında çalışan olay metodu.
        /// Kullanıcının mesajını alır, API'ye gönderir ve yanıtı gösterir.
        /// </summary>
        private async void buttonSend_Click(object sender, EventArgs e)
        {
            // Kullanıcının yazdığı mesajı alıyoruz
            string userMessage = textBoxMessage.Text.Trim();
            
            // Eğer mesaj boşsa, işlem yapmıyoruz
            if (string.IsNullOrEmpty(userMessage))
            {
                return;
            }

            // Mesajı sohbet alanına ekliyoruz (kullanıcı mesajı olarak)
            AddMessageToChat("Sen", userMessage, Color.Black);
            
            // Mesaj kutusunu temizliyoruz
            textBoxMessage.Clear();
            
            // Gönder butonunu pasif hale getiriyoruz (çift tıklamayı önlemek için)
            buttonSend.Enabled = false;
            
            // Durum etiketini güncelliyoruz ("Yazıyor..." mesajı)
            labelStatus.Text = "Yazıyor...";
            labelStatus.ForeColor = Color.Gray;
            
            try
            {
                // API'ye mesajı gönderiyoruz ve yanıtı bekliyoruz
                // async/await kullanıldığı için UI donmayacak
                string response = await _openAIService.SendMessageAsync(userMessage, _conversationHistory);
                
                // API'den gelen yanıtı sohbet alanına ekliyoruz (bot yanıtı olarak)
                AddMessageToChat("Bot", response, Color.DarkGreen);
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi veriyoruz
                AddMessageToChat("Sistem", $"Bir hata oluştu: {ex.Message}", Color.Red);
            }
            finally
            {
                // İşlem tamamlandığında (başarılı veya hatalı) butonu tekrar aktif ediyoruz
                buttonSend.Enabled = true;
                
                // Durum etiketini temizliyoruz
                labelStatus.Text = "";
                
                // Mesaj kutusuna odaklanıyoruz (kullanıcı hemen yeni mesaj yazabilsin)
                textBoxMessage.Focus();
            }
        }

        /// <summary>
        /// Mesaj kutusunda Enter tuşuna basıldığında çalışan olay metodu.
        /// Enter tuşu ile mesaj gönderilir, Ctrl+Enter ile yeni satır eklenir.
        /// </summary>
        private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            // Eğer Enter tuşuna basıldıysa ve Ctrl tuşu basılı değilse
            if (e.KeyCode == Keys.Enter && !e.Control)
            {
                // Enter tuşunun varsayılan davranışını (yeni satır ekleme) engelliyoruz
                e.SuppressKeyPress = true;
                
                // Gönder butonuna tıklama olayını tetikliyoruz
                buttonSend_Click(sender, e);
            }
            // Ctrl+Enter kombinasyonu yeni satır eklemek için kullanılabilir
        }

        /// <summary>
        /// Mesajı sohbet alanına (RichTextBox) ekleyen yardımcı metod.
        /// Farklı renklerle farklı göndericileri ayırt eder.
        /// </summary>
        /// <param name="sender">Mesajı gönderen (Sen, Bot, Sistem)</param>
        /// <param name="message">Gönderilecek mesaj içeriği</param>
        /// <param name="color">Mesajın rengi</param>
        private void AddMessageToChat(string sender, string message, Color color)
        {
            // RichTextBox'ın seçili metninin sonuna ekleme yapıyoruz
            richTextBoxChat.SelectionStart = richTextBoxChat.TextLength;
            richTextBoxChat.SelectionLength = 0;
            
            // Mesajın rengini önce ayarlıyoruz (metni eklemeden önce)
            richTextBoxChat.SelectionColor = color;
            
            // Gönderen adını ve mesajı formatlıyoruz
            string formattedMessage = $"[{DateTime.Now:HH:mm:ss}] {sender}: {message}\n\n";
            
            // Mesajı seçili alana ekliyoruz (ayarlanan renkle eklenir)
            richTextBoxChat.SelectedText = formattedMessage;
            
            // Seçimi kaldırıyoruz
            richTextBoxChat.SelectionStart = richTextBoxChat.TextLength;
            richTextBoxChat.SelectionLength = 0;
            
            // Scroll'u en alta kaydırıyoruz (yeni mesaj görünsün)
            richTextBoxChat.ScrollToCaret();
        }

        /// <summary>
        /// Form kapatıldığında çalışan metod.
        /// Kaynakları temizler (HttpClient gibi).
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // OpenAI servisindeki HttpClient'ı temizliyoruz
            _openAIService?.Dispose();
            
            base.OnFormClosing(e);
        }
    }
}

