using ExampleWebApiDapper.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExampleWebApiDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        public MoviesController(IMovieRepository repository)
        {
            this._repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await _repository.GetAllAsync();
            return movies.Any() ? Ok(movies)  : NoContent() ;
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var movie = await _repository.GetAsync(id);
            return movie !=null ? Ok(movie) : NotFound("Filme n�o encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Post(MovieRequest request)
        {
            if (string.IsNullOrEmpty(request.Nome))
                return BadRequest("Nome do filme n�o foi informado!");

            if (request.Ano < 1930 || request.Ano > 9999)
                return BadRequest("Ano inv�lido");

            var add = await _repository.AddAsync(request);
            return add  ? Ok("Filme Cadastrado com Sucesso!") : BadRequest("Ocorreu um erro e o filme n�o p�de ser cadastrado!");
        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(MovieRequest request, int id)
        {
            if (id<=0)
                return BadRequest("C�digo de filme inv�lido");
            
            var movie= await _repository.GetAsync(id);

            if (movie == null) return NotFound("Filme n�o encontrado");

            if (string.IsNullOrEmpty(request.Nome))
                return BadRequest("Nome do filme n�o foi informado!");

            if (request.Ano < 1930 || request.Ano > 9999)
                return BadRequest("Ano inv�lido");

            var upd = await _repository.UpdateAsync(request,id);

            return upd ? Ok("Filme Atualizado com Sucesso!") : BadRequest("Ocorreu um erro e o filme n�o p�de ser atualizado!");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete( int id)
        {
            if (id <= 0)
                return BadRequest("C�digo de filme inv�lido");

            var movie = await _repository.GetAsync(id);

            if (movie == null) return NotFound("Filme n�o encontrado");            

            var upd = await _repository.DeleteAsync( id);

            return upd ? Ok("Filme Deletado com Sucesso!") : BadRequest("Ocorreu um erro e o filme n�o p�de ser deletado!");
        }
    }
}