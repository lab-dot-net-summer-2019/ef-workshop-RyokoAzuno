namespace SamuraiApp.Domain
{
    public class Katana
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SamuraiId { get; set; }
        public virtual Samurai Samurai { get; set; }
    }
}
