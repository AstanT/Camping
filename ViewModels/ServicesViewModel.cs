namespace Camping.ViewModels
{
    public class ServicesViewModel
    {
        public long IdService { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public short Rating { get; set; }
        public decimal Prise { get; set; }
        public bool IsActive { get; set; }
        public string Photo { get; set; }
    }

}