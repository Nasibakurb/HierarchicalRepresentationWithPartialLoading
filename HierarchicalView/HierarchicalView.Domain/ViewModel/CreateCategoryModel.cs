using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalView.Domain.ViewModel
{
    public class CreateCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public void Validation()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(Name, "Укажите название");
            }
        }
    }
}
