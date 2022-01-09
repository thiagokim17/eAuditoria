namespace eAuditoria.Models
{
    public class LocacaoModelView
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }
        public string NomeCliente { get; set; } = "";

        public int IdFilme { get; set; }
        public string TituloFilme { get; set; } = "";

        public string DataLocacao { get; set; } = "";

        public string DataDevolucao { get; set; } = "";

    }
}
