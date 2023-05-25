using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWW.Domain.Enum;
namespace WWW.Domain.Response
{
    public class BaseResponse<T>: IBaseResponse<T>
    {
        public string ErrorDescription { get; set; }
        public StatusCode StatusCode { get; set; } 
        public T Data { get; set; }
    }
    public interface IBaseResponse<T>
    {
        T Data { get; set; }
    }


}
