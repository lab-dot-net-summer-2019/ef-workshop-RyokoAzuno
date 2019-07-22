using System.Collections.Generic;

namespace SamuraiApp.Domain
{
    public class Samurai
    {
        public Samurai()
        {
            //SecretIdentity = new SecretIdentity();
            SecretIdentity = new SecretIdentity();
            Quotes = new List<Quote>();
            Katanas = new List<Katana>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Quote> Quotes { get; set; }
        public virtual List<SamuraiBattle> SamuraiBattles { get; set; }
        public virtual List<Katana> Katanas { get; set; }
        public virtual SecretIdentity SecretIdentity { get; set; }
    }
}
