using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Test.IP_Worker.Models;

namespace Test.IP_Worker {
    public class IPValidator {
        public IPValidator() {
        }

        public List<ResultIp> ValidateIP(ResultIp ip) {
            List<ResultIp> result = new List<ResultIp>();
            IPAddress address;
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions {
                DontFragment = true
            };
            int timeout = 120;
            try {
                if (IPAddress.TryParse(ip.IP, out address)) {
                    PingReply reply = pingSender.Send(address, timeout, null, options);
                    if(reply.Status == IPStatus.Success) {
                        result.Add(new ResultIp {
                            IP = ip.IP,
                            Result = true
                        });
                    } else {
                        result.Add(new ResultIp {
                            IP = ip.IP,
                            Result = false
                        });
                    }
                } else {
                    result.Add(new ResultIp {
                        IP = ip.IP,
                        Result = false
                    });
                }

            } catch (Exception ex) {
                throw ex;
            }
           
            return result;
        }
    }
}
