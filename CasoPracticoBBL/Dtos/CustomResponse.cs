namespace CasoPracticoBLL.Dtos
{
    public class CustomResponse<T>
    {
        public T Data { get; set; }           // El dato que devuelve (puede ser objeto, lista, bool, etc.)
        public bool EsError { get; set; }     // Indica si hubo error
        public string Mensaje { get; set; }   // Mensaje de error o información

        // 🔹 Constructor genérico que acepta cualquier tipo de T
        public CustomResponse(T data)
        {
            Data = data;
            EsError = false;
            Mensaje = string.Empty;
        }

        // 🔹 Constructor opcional para inicializar vacío si se necesita
        public CustomResponse()
        {
            Data = default!;
            EsError = false;
            Mensaje = string.Empty;
        }
    }
}
