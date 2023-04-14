namespace ExampleWebApiDapper
{
    public class MovieRequest
    {
        public string? Nome { get; set; }

        public int? Ano { get; set; }

    }

    public class MovieResponse
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }

        public int? Ano { get; set; }

    }
}