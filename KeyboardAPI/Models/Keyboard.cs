namespace KeyboardAPI.Models
{
    public class Keyboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ReservedBy { get; set; }
    }
}
