using CCMN_API.Services.Categorias;
using CCMN_API.Services.DespesasComunidade;
using CCMN_API.Services.DespesasEvento;
using CCMN_API.Services.MovimentoProdutos;
using CCMN_API.Services.Usuarios;
using CCMZ_API;
using CCMZ_API.Services.Alocacao;
using CCMZ_API.Services.Blocos;
using CCMZ_API.Services.Comunidade;
using CCMZ_API.Services.Dashboard;
using CCMZ_API.Services.Eventos;
using CCMZ_API.Services.Pessoas;
using CCMZ_API.Services.QuartoPessoa;
using CCMZ_API.Services.Quartos;
using Microsoft.EntityFrameworkCore;

var PainelCCMN = "PainelCCMN";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: PainelCCMN, 
        policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.WithOrigins("http://localhost:3000", "http://localhost:58530");
            //policy.WithOrigins("http://painel.ccmn.org.br");
        });
});

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("CCMNConnection");

builder.Services.AddDbContext<CCMNContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPessoasService, PessoasService>();
builder.Services.AddScoped<IQuartosService, QuartosService>();
builder.Services.AddScoped<IComunidadeService, ComunidadeService>();
builder.Services.AddScoped<IEventosService, EventosService>();
builder.Services.AddScoped<IBlocosService, BlocosService>();
builder.Services.AddScoped<IAlocacaoService, AlocacaoService>();
builder.Services.AddScoped<ICheckinService, CheckinService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IDespesaEventoService, DespesaEventoService>();
builder.Services.AddScoped<IDespesaComunidadeService, DespesaComunidadeService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IMovimentoEstoqueServicecs, MovimentoEstoqueService>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(PainelCCMN);

app.UseAuthorization();

app.MapControllers();

app.Run();
