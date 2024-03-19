using HierarchicalView.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalView.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>  // Cоздание шаблона ответа от сервера 
    {
        public string Description { get; set; }

        public T Data { get; set; }

        public StatusCode StatusCode { get; set; }
    }
    public interface IBaseResponse<T>
    { // Создание свойств "ответа"
        string Description { get; }
        StatusCode StatusCode { get; }
        T Data { get; }

    }
}
