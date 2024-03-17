namespace Test.Models.ViewModel
{
    public class CreateSubcategoryModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }


        public void Validation()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(Name, "Укажите название");
            }
        }
    }
}
