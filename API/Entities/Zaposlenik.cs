﻿namespace API.Entities
{
    public class Zaposlenik
    {
        public int Id { get; set; }
        public required string Ime { get; set; }
        public required string Prezime { get; set; }
        public required string Struka { get; set; }
    }
}