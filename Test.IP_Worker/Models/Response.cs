using System;
using System.Collections.Generic;

namespace Test.IP_Worker.Models {
    public class Response {
        public bool Status { get; set; }
        public string Error { get; set; }
        public List<ResultIp> ResultIps { get; set; }
    }
}
