namespace ExampleWebApiDapper.Repository;
public interface IMovieRepository
{
    Task<IEnumerable<MovieResponse>> GetAllAsync();
    Task<MovieResponse> GetAsync(int id);
    Task<bool> AddAsync(MovieRequest request);
    Task<bool> UpdateAsync(MovieRequest request, int id);
    Task<bool> DeleteAsync(int id);
}
