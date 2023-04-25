namespace BookStore_project.Models.Import
{
    public class ImportDetailViewModel
    {
        public int id { get; set; }
        public DateTime date_import { get; set; }
        public double Total { get; set; }
        public List<Entity.ImportDetail> lod { get; set; }
    }
}
