using System;
using System.Windows.Forms;

namespace LLMChatbot
{
    /// <summary>
    /// Uygulamanın giriş noktası (entry point).
    /// Program başladığında ilk bu metod çalışır.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Uygulama başlatıldığında bu metod çağrılır.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Windows Forms uygulamaları için görsel stilleri etkinleştiriyoruz
            // Bu, modern Windows görünümü için gereklidir
            Application.EnableVisualStyles();
            
            // Metin renderlaması için yüksek kalite ayarlarını etkinleştiriyoruz
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Ana formu (Form1) başlatıp çalıştırıyoruz
            // Run metodu, form kapatılana kadar uygulamanın çalışmasını sağlar
            Application.Run(new Form1());
        }
    }
}

