using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AssessmentManagerAPI.Handlers
{
    public class CustomLogHandler : DelegatingHandler
    {
        public string strFile = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";
        private object m_LogSync = new object();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
           // Debug.WriteLine("Process request");

            // Create the response.
            var logMetadata = BuildRequestMetadata(request);
            var response = await base.SendAsync(request, cancellationToken);
            logMetadata = BuildResponseMetadata(logMetadata, response);
            await SendToLog(logMetadata);
            return response;
        }

        private LogMetadata BuildRequestMetadata(HttpRequestMessage request)
        {
            LogMetadata log = new LogMetadata
            {
                RequestMethod = request.Method.Method,
                RequestTimestamp = DateTime.Now,
                RequestUri = request.RequestUri.ToString()
            };
            return log;
        }

        private LogMetadata BuildResponseMetadata(LogMetadata logMetadata, HttpResponseMessage response)
        {
            logMetadata.ResponseStatusCode = response.StatusCode;
            logMetadata.ResponseTimestamp = DateTime.Now;
            logMetadata.ResponseContentType = response.Content.Headers.ContentType.MediaType;
            return logMetadata;
        }

        private async Task<bool> SendToLog(LogMetadata logMetadata)
        {
            // TODO: Write code here to store the logMetadata instance to a pre-configured log store...

            string logString = "Request Mehod: " + logMetadata.RequestMethod +
                                " Request Time: " + logMetadata.RequestTimestamp +
                                " Request Uri: " + logMetadata.RequestUri +
                                " Response Time: " + logMetadata.ResponseTimestamp +
                                " Response Content Type: " + logMetadata.ResponseContentType +
                                " Status " + logMetadata.ResponseStatusCode + "\n";

            //System.IO.File.AppendAllText(strFile, logString);
            lock (m_LogSync)
            {
                using (StreamWriter writer = File.AppendText(strFile))
                {
                    writer.WriteLineAsync(logString);
                }
            }

            return true;
        }
    }
}