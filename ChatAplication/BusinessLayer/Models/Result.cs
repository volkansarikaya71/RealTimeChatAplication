using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ErrorMessages { get; set; }


        public Result()
        {
            ErrorMessages = new List<string>();
        }

        // Başarı durumu için constructor
        public Result(T data)
        {
            Success = true; // Başarı durumu
            Data = data;
            ErrorMessage = string.Empty;
            ErrorMessages = new List<string>(); // Hata mesajları listesini başlatıyoruz
        }


        // Hata durumu için constructor (tek hata mesajı)
        public Result(string errorMessage)
        {
            Success = false; // Hata durumu
            Data = default(T); // T'nin varsayılan değeri (string için null)
            ErrorMessage = errorMessage;
            ErrorMessages = new List<string>(); // Hata mesajları listesini başlatıyoruz
        }

        // Hata durumu için constructor (çoklu hata mesajı)
        public Result(List<string> errorMessages)
        {
            Success = false;
            Data = default(T);
            ErrorMessages = errorMessages ?? new List<string>(); // Eğer null gelirse boş liste başlatıyoruz
        }
    }

}
