namespace CCMN_API.Models.Painel.Promocao;

public class ListarGanhadorCupom
{
    public int ParCodigo { get; set; }

    public string? ParNome { get; set; }

    public string? ParFone { get; set; }

    public string? ParCidade { get; set; }

    public string? ParUf { get; set; }

    public int CupCodigo { get; set; }

    public string CupNumero { get; set; } = null!;

}
