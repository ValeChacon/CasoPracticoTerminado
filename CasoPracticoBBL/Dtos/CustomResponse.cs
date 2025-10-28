namespace CasoPracticoBLL.Dtos
{
    public class CustomResponse<T>
    {
        public T Data { get; set; }           
        public bool EsError { get; set; }      
        public string Mensaje { get; set; }    

        public CustomResponse(T data)
        {
            Data = data;
            EsError = false;
            Mensaje = string.Empty;
        }

        public CustomResponse()
        {
            Data = default!;
            EsError = false;
            Mensaje = string.Empty;
        }
    }
}
