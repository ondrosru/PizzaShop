namespace PizzaShopAdmin.Dto.Error
{
    public class ErrorDto
    {
        public string Error { get; set; }
        public string Message { get; set; }

        public ErrorDto(string error, string message)
        {
            this.Error = error;
            this.Message = message;
        }
    }
}
