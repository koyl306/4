namespace pract9
{
    public class Experiment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string UsedMaterial { get; set; }
        public string Result { get; set; }
    }
}
