using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ImagesHackDotNet.HttpExtensions
{
    public class HttpPostMultipartFormDataSender
    {

        private readonly HttpWebRequest _request;
        private readonly LinkedList<IMultipartParameter> _parameters = new LinkedList<IMultipartParameter>();
        private HttpWebRequest Request
        {
            get { return _request; }
        }

        public HttpPostMultipartFormDataSender(string uri)
        {
            _request = (HttpWebRequest)WebRequest.Create(uri);
        }

        public HttpPostMultipartFormDataSender(Uri uri)
        {
            _request = (HttpWebRequest)WebRequest.Create(uri);
        }

        public HttpPostMultipartFormDataSender WithData(string name, string data)
        {
            return WithCustomField(new BasicMultipartParameter(name, data));
        }

        public HttpPostMultipartFormDataSender WithFileData(string name, string path, string contentType)
        {
            return WithCustomField(new FileMultipartParameter(path, name, contentType));
        }

        public HttpPostMultipartFormDataSender WithCustomField(IMultipartParameter parameter)
        {
            _parameters.AddLast(parameter);
            return this;
        }

        public HttpWebResponse GetResponse()
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            _request.ContentType = "multipart/form-data; boundary=" + boundary;
            _request.Method = "POST";
            
            var requestStream = _request.GetRequestStream();

            requestStream.Write(boundary + "\r\n", Encoding.ASCII);

            foreach (var multipartParameter in _parameters)
            {
                multipartParameter.Write(requestStream);
                requestStream.Write(boundarybytes, 0, boundarybytes.Length);

                
            }
            requestStream.Close();
            return _request.GetResponse() as HttpWebResponse;
        }
    }
}
