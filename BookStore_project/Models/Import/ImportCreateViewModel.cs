using Entity;

namespace BookStore_project.Models.Import
{
    public class ImportCreateViewModel
    {
        public int import_id { get; set; }
        public DateTime date_import { get; set; }
        public int numberofproduct { get; set; }
        
        public List<Entity.ImportDetail>? lod { get; set; }
    }
}
