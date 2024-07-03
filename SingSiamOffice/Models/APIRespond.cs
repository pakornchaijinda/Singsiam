namespace SingSiamOffice.Models
{
    public class APIRespond<T>
    {
        public string result { get; set; }
        public string message { get; set; }
        public int respondCode { get; set; }

        public T data { get; set; }

        public APIRespond()
        {
            respondCode = 200;
            result = "ok";
        }
    }
}
