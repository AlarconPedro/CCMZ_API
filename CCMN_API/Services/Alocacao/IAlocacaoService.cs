﻿namespace CCMZ_API.Services.Alocacao;

using CCMN_API;
using CCMN_API.Models;
using CCMN_API.Models.Painel.Hospedagem.Bloco;
using CCMN_API.Models.Painel.Hospedagem.Comunidade;
using CCMN_API.Models.Painel.Hospedagem.Evento;
using CCMN_API.Models.Painel.Hospedagem.Pessoas;

public interface IAlocacaoService
{
    //GET
    Task<IEnumerable<EventoDadosBasicos>> GetEventos(int filtro);
    Task<IEnumerable<Comunidade>> GetComunidades(int codigoEvento);
    Task<IEnumerable<PessoasNome>> GetPessoasComunidade(int codigoEvento, int codigoComunidde);
    Task<IEnumerable<BlocoNome>> GetBlocos(int codigoEvento);
    Task<IEnumerable<PessoasAlocadas>> GetPessoasQuarto(int codigoQuarto);
    Task<IEnumerable<PessoasNome>> GetPessoasTotal(int codigoEvento, int codigoComunidde);
    Task<TbQuartoPessoa> GetPessoaAlocada(int codigoPessoa);
    //INSERT
/*    Task AddPessoaQuarto(int codigoQuarto, int codigoPessoa);*/    
    Task AddPessoaQuarto(TbQuartoPessoa quartoPessoa);
    //UPDATE
    Task AtualizarPessoaQuarto(TbQuartoPessoa quartoPessoa);
    //DELETE
    Task RemoverPessoaQuarto(TbQuartoPessoa quartoPessoa);
    Task LimpaPessoasAlocadas(int codigoQuarto);
}
