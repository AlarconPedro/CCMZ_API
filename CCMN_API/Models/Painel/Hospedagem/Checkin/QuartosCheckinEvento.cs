using CCMN_API.Models.Painel.Hospedagem.QuartoPessoa;

namespace CCMN_API.Models.Painel.Hospedagem.Checkin;

public class QuartosCheckinEvento
{
    public string? BloNome { get; set; }

    public List<QuartoPessoas>? QuartosCheckin { get; set; }
}
