namespace ClienteAPI.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string NomCliente { get; set; } = null!;

    public virtual ICollection<ClientesDocumento> ClientesDocumentos { get; } = new List<ClientesDocumento>();
}
