using CCMZ_API.Models.Painel.QuartoPessoa;

namespace CCMN_API.Models.Painel.Checkin;

public class QuartosCheckinEvento
{
    public string? BloNome { get; set; }

    public List<QuartoPessoas>? QuartosCheckin { get; set; }
}
