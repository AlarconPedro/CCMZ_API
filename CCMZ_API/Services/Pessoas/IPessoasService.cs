﻿namespace CCMZ_API.Services.Pessoas;
using CCMZ_API.Models;
using CCMZ_API.Models.Painel.Pessoas;

public interface IPessoasService
{
    //GET
    Task<IEnumerable<Pessoas>> GetPessoas();
    Task<TbPessoa> GetPessoaId(int idPessoa);
    Task<bool> GetPessoaPodeExcluir(int idPessoa);
    Task<PessoaDetalhes> GetPessoaDetalhe(int idPessoa);
    //POST
    Task PostPessoas(TbPessoa tbPessoa);
    //PUT
    Task PutPessoas(TbPessoa tbPessoa);
    //DELETE
    Task DeletePessoas(TbPessoa tbPessoa);
}
