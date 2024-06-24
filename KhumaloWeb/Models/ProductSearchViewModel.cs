namespace KhumaloWeb.Models
{
    public class ProductSearchViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string SelectedCategory { get; set; }
    }
}
