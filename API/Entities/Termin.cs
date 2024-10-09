namespace API.Entities
{
    public class Termin
    {
        public int Id { get; set; }
        public string Napomena { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public required Usluga Usluga { get; set; }
        public required Zaposlenik Zaposlenik { get; set; }
    }
}
