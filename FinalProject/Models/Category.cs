namespace FinalProject.Models
{
    public class Category
    {
        public int ID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Director { get; set; } = string.Empty;

        public string Writer { get; set; } = string.Empty;

        public string Composer { get; set; } = string.Empty;

        public int RTS { get; set; }

        public string Runtime { get; set; } = string.Empty;

        public int Pee_Time { get; set; }

        public bool Checked { get; set; }
    }
}
