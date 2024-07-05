namespace ProgettoS3L5SabrinaCinque.Models
{
    public class Verbale
    {
        public int IdVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string Nominativo_Agente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }

        public Anagrafica Anagrafica { get; set; }
        public TipoViolazione TipoViolazione { get; set; }
    }
}
