﻿using CCMZ_API.Models.Painel.Pessoas;

namespace CCMZ_API.Models.Painel.Quartos;

public class QuartoPavilhao
{
    public int QuaCodigo { get; set; }
    public string? QuaNome { get; set; }
    public int? QuaQtdcamas { get; set; }
    public int? QuaQtdcamasdisponiveis { get; set; }
    public List<PessoasNome>? PessoasQuarto { get; set; }
}
