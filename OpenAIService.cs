using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LLMChatbot
{
    /// <summary>
    /// OpenAI API ile iletişim kurmak için kullanılan servis sınıfı.
    /// Bu sınıf, sohbet mesajlarını API'ye gönderir ve yanıtları alır.
    /// </summary>
    public class OpenAIService
    {
        // OpenAI API endpoint adresi
        private const string API_URL = "https://api.openai.com/v1/chat/completions";
        
        // API Key buraya gelecek (güvenlik için değişkende tutuluyor)
        private const string API_KEY = "BURAYA_API_KEY_GELECEK";
        
        // HTTP istekleri için HttpClient nesnesi
        private readonly HttpClient _httpClient;

        /// <summary>
        /// OpenAIService sınıfının yapıcı metodu.
        /// HttpClient nesnesini başlatır ve gerekli header'ları ayarlar.
        /// </summary>
        public OpenAIService()
        {
            _httpClient = new HttpClient();
            
            // API Key'i Authorization header'ına ekliyoruz
            // OpenAI API, Bearer token formatında API Key bekliyor
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");
            
            // İstek içeriğinin JSON formatında olduğunu belirtiyoruz
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        /// <summary>
        /// Kullanıcı mesajını OpenAI API'ye gönderir ve yanıtı alır.
        /// Bu metod asenkron çalışır, böylece UI donmaz.
        /// </summary>
        /// <param name="userMessage">Kullanıcının gönderdiği mesaj</param>
        /// <param name="conversationHistory">Önceki sohbet geçmişi (isteğe bağlı)</param>
        /// <returns>API'den gelen yanıt metni</returns>
        public async Task<string> SendMessageAsync(string userMessage, List<ChatMessage> conversationHistory = null)
        {
            try
            {
                // Eğer sohbet geçmişi yoksa, yeni bir liste oluşturuyoruz
                if (conversationHistory == null)
                {
                    conversationHistory = new List<ChatMessage>();
                }

                // Kullanıcının yeni mesajını geçmişe ekliyoruz
                conversationHistory.Add(new ChatMessage
                {
                    Role = "user",
                    Content = userMessage
                });

                // API'ye gönderilecek istek nesnesini oluşturuyoruz
                var requestBody = new
                {
                    model = "gpt-3.5-turbo", // Kullanılacak model (gpt-3.5-turbo veya gpt-4)
                    messages = conversationHistory // Sohbet geçmişi
                };

                // Request body'yi JSON formatına çeviriyoruz
                string jsonContent = JsonConvert.SerializeObject(requestBody);
                
                // JSON içeriğini HTTP içeriğine dönüştürüyoruz
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // API'ye POST isteği gönderiyoruz ve yanıtı bekliyoruz
                HttpResponseMessage response = await _httpClient.PostAsync(API_URL, content);

                // HTTP yanıt kodunu kontrol ediyoruz
                if (!response.IsSuccessStatusCode)
                {
                    // Hata durumunda hata mesajını okuyoruz
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Hatası: {response.StatusCode} - {errorContent}");
                }

                // Başarılı yanıtı JSON formatında okuyoruz
                string responseContent = await response.Content.ReadAsStringAsync();
                
                // JSON yanıtını parse ediyoruz
                var apiResponse = JsonConvert.DeserializeObject<OpenAIResponse>(responseContent);

                // API yanıtından mesaj içeriğini çıkarıyoruz
                if (apiResponse?.Choices != null && apiResponse.Choices.Count > 0)
                {
                    string assistantMessage = apiResponse.Choices[0].Message.Content;
                    
                    // Asistan yanıtını da geçmişe ekliyoruz (sonraki mesajlar için)
                    conversationHistory.Add(new ChatMessage
                    {
                        Role = "assistant",
                        Content = assistantMessage
                    });

                    return assistantMessage;
                }

                throw new Exception("API'den geçerli bir yanıt alınamadı.");
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya anlaşılır bir mesaj döndürüyoruz
                return $"Hata oluştu: {ex.Message}";
            }
        }

        /// <summary>
        /// HttpClient nesnesini temizlemek için kullanılan metod.
        /// Uygulama kapanırken çağrılmalıdır.
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    /// <summary>
    /// Sohbet mesajlarını temsil eden sınıf.
    /// OpenAI API, mesajları role (user/assistant) ve content (içerik) ile bekliyor.
    /// </summary>
    public class ChatMessage
    {
        [JsonProperty("role")]
        public string Role { get; set; } // "user" veya "assistant"

        [JsonProperty("content")]
        public string Content { get; set; } // Mesaj içeriği
    }

    /// <summary>
    /// OpenAI API'den gelen yanıtı temsil eden sınıf.
    /// JSON deserialization için kullanılır.
    /// </summary>
    public class OpenAIResponse
    {
        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; }
    }

    /// <summary>
    /// API yanıtındaki choice (seçenek) nesnesini temsil eder.
    /// </summary>
    public class Choice
    {
        [JsonProperty("message")]
        public ChatMessage Message { get; set; }
    }
}

