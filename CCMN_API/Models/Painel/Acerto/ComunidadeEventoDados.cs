using CCMN_API.Models.Painel.EventoDespesas;

namespace CCMN_API.Models.Painel.Acerto;

public class ComunidadeEventoDados
{
    public int ComCodigo { get; set; }
    public string? ComNome { get; set; }
    public PessoasPagantesCobrantes? PagantesCobrantes { get; set; }
}
