namespace HierarchicalView.Domain.Entity
{
    public class SubcategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryEntityId { get; set; }

    }
}
