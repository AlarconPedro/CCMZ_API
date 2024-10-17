using CCMN_API.Models.Painel.Hospedagem.Pessoas;

namespace CCMN_API.Models.Painel.Hospedagem.Quartos;

public class QuartoPavilhao
{
    public int? QuaCodigo { get; set; }
    public string? QuaNome { get; set; }
    public int? QuaQtdcamas { get; set; }
    public int? QuaQtdcamasdisponiveis { get; set; }
    public List<PessoasNome>? PessoasQuarto { get; set; }
}
