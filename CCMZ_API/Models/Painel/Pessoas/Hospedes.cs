﻿namespace CCMZ_API.Models.Painel.Pessoas;

public class Hospedes
{
    public int PesCodigo { get; set; }
    public string? PesNome { get; set; }
    public string? PesGenero { get; set; }
    public string? Comunidade { get; set; }
    public bool? Pagante { get; set; }
    public bool? Cobrante { get; set; }
}
