using Dapper;
using Npgsql;

namespace ExampleWebApiDapper.Repository;
public class MovieRepository : IMovieRepository
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;

    public MovieRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
        this.connectionString = configuration.GetConnectionString("SqlConnection");
    }

    public async Task<bool> AddAsync(MovieRequest request)
    {
        string query = @"INSERT INTO tb_filme(nome,ano)
                         Values(@Nome,@Ano)";

        using var con = new NpgsqlConnection(connectionString);

        return await con.ExecuteAsync(query,request) > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        string query = @"DELETE FROM tb_filme 
                          WHERE id = @Id";

        var parms = new DynamicParameters();
        parms.Add("Id", id);

        using var con = new NpgsqlConnection(connectionString);

        return await con.ExecuteAsync(query, parms) > 0;
    }

    public async Task<IEnumerable<MovieResponse>> GetAllAsync()
    {
        string sql = @"SELECT * FROM tb_filme";

        using var con = new NpgsqlConnection(connectionString);
        return await con.QueryAsync<MovieResponse>(sql);

    }

    public async Task<MovieResponse> GetAsync(int id)
    {
        string sql = @"SELECT * FROM tb_filme where Id = @Id";

        using var con = new NpgsqlConnection(connectionString);
        
        return await con.QueryFirstOrDefaultAsync<MovieResponse>(sql, new { Id = id});
        
    }

    public async Task<bool> UpdateAsync(MovieRequest request, int id)
    {
        string query = @"UPDATE tb_filme SET
                             nome = @Nome,
                             ano = @Ano
                          WHERE id = @Id";

        var parms = new DynamicParameters();
        parms.Add("Id", id);
        parms.Add("Ano",request.Ano);
        parms.Add("Nome",request.Nome);

        using var con = new NpgsqlConnection(connectionString);

        return await con.ExecuteAsync(query, parms) > 0;
    }
}
