namespace HospitalAppointment.WebApi.Models.ReturnModels
{
    public class ReturnModel <T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ReturnModel(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public override string ToString()
        {
            return $"Success: {Success}, Message: {Message}, Data: {Data}";
        }
    }
}
