using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Test.API.IP.Models;

namespace Test.API.IP.Interfaces {
    public interface IValidator {

        public Task<Response> IPValidator(List<string> IPS);
        public Task<Response> PingIPs(List<IPAddress> IPS);
        public Task<ResultIp> SendPingIP(IPAddress ip);
    }
}
