using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalView.Domain.Enum
{
    public enum StatusCode
    {
        EntityIsHasAlready = 1, // Уже имеется
        EntitykNotFound = 2,

        Ok = 200,
        InternalServerError = 500

    }
}
