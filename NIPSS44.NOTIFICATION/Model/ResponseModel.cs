using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44.NOTIFICATION.Model
{
    public class ResponseModel
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
