using API.Entities;

namespace API.DTOs
{
    public class TerminUpdateDto
    {
        public string Napomena { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public required int UslugaId { get; set; } // FK
        public required int ZaposlenikId { get; set; } // FK
    }
}
