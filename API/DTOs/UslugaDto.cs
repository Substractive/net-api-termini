namespace API.DTOs
{
    public class UslugaDto
    {
        public int Id { get; set; }
        public required string Vrsta { get; set; }
        public required string Trajanje { get; set; }
        public required string Naziv { get; set; }
    }
}
