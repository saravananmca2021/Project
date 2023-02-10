using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Model
{
    public class Response
    {
        public string ResponseCode { get; set; }
        public string RequestId => $"{Guid.NewGuid().ToString()}";
        public string ResponseMessage { get; set; }
        public object Data { get; set; }
    }
}
