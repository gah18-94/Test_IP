using System;
using System.Collections.Generic;

namespace Test.API.IP.Models {
    public class Response {
        public bool Status { get; set; }
        public string Error { get; set; }
        public List<ResultIp> ResultIps { get; set; }
    }
}
