namespace CCMN_API.Models.Painel.Estoque;

public class MovimentoEstoque
{
    public int MovCodigo { get; set; }

    public int? ProCodigo { get; set; }

    public string? ProNome { get; set; }

    public int? MovQuantidade { get; set; }

    public DateTime? MovData { get; set; }

    public string? MovTipo { get; set; }
}