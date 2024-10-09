using API.Entities;

namespace API.DTOs
{
    public class TerminDto
    {
        public int Id { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public string Napomena { get; set; }
        public UslugaDto Usluga { get; set; }
        public ZaposlenikDto Zaposlenik { get; set; }
    }
}
