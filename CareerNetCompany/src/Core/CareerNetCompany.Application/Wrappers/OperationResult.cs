namespace CareerNetCompany.Application.Common
{
    /// <summary>
    /// İşlemin sonucunu temsil eden sınıf. Başarı durumu, mesaj ve işlem sonucunda dönecek veri içerir.
    /// IsSuccess,Message ve T Data değerleri sadece bu sınıf içerisindeki metodlardan değiştirilsin diye private set işaretlenir.
    /// </summary>
    public class OperationResult<T>
    {
        /// <summary>
        /// İşlemin başarılı olup olmadığını belirtir.
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// İşlem sonucu döndürülecek mesaj.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// İşlem sonucunda dönecek veri.
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// Başarılı bir işlem sonucu için yapılandırıcı metot.
        /// </summary>
        public static OperationResult<T> Success(T data, string message = "")
        {
            return new OperationResult<T> { IsSuccess = true, Data = data, Message = message };
        }

        /// <summary>
        /// Başarısız bir işlem sonucu için yapılandırıcı metot.
        /// </summary>
        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T> { IsSuccess = false, Message = message };
        }
    }
}
