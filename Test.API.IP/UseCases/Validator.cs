using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyNetQ;
using Newtonsoft.Json;
using Test.API.IP.Interfaces;
using Test.API.IP.Models;

namespace Test.API.IP.UseCases {
    public class Validator : IValidator {
        public Validator() {
        }

        /// <summary>
        /// Validate if the IP is in correct format, if so it will ping the connectiviyt trough the internet
        /// </summary>
        /// <param name="IPS"></param>
        /// <returns></returns>
        public async Task<Response> IPValidator(List<string> IPS) {
            List<IPAddress> ipsValidated = new List<IPAddress>();
            try {
                foreach (string ip in IPS) {
                    IPAddress address;
                    if (IPAddress.TryParse(ip, out address)) {
                        ipsValidated.Add(address);
                    } else {
                        continue;
                    }
                }
                return await PingIPs(ipsValidated);
                
            } catch (Exception ex) {
                throw ex;
            }
        }


        /// <summary>
        /// Create async calls to verify if one or more IP's make a successful ping.
        /// </summary>
        /// <param name="IPS"></param>
        /// <returns></returns>
        public async Task<Response> PingIPs(List<IPAddress> IPS) {
            List<Task<ResultIp>> tasks = new List<Task<ResultIp>>();
            Response response = new Response();
            List<ResultIp> responseIps = new List<ResultIp>();
            try {
                foreach (IPAddress ip in IPS) {
                    Task<ResultIp> t = Task.Run(() => SendPingIP(ip));
                    tasks.Add(t);
                }
                Task.WaitAll(tasks.ToArray());
                foreach (Task<ResultIp> t in tasks) {
                    if (!t.IsFaulted) {
                        responseIps.Add(new ResultIp { IP = t.Result.ToString(), Result = true }) ;
                    } else {
                        responseIps.Add(new ResultIp { IP = t.Result.ToString(), Result = false });
                    }

                }
                response.Status = true;
                response.ResultIps = responseIps;
                return response;

            } catch (Exception ex) {
                response.Status = false;
                response.Error = ex.ToString();
                return response;
            }
        }

        public async Task<ResultIp> SendPingIP(IPAddress ip) {
            try {

                ResultIp res_ip = new ResultIp() { IP = ip.ToString(), Result = false };

                var messageBus = RabbitHutch.CreateBus("host=localhost");

                var response = messageBus.Request<ResultIp, Response>(res_ip);

                return response.ResultIps[0];
            } catch (Exception ex) {
                throw ex;
            }
        }

    }
}
