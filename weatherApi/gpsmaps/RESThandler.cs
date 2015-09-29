using System;
using RestSharp;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace weatherApi
{
    public class RESThandler
    {
        //
        private string url;
        private IRestResponse response;

        public RESThandler()
        {
            url = "";
        }

        public RESThandler(string lurl)
        {
            url = lurl;
        }
        
        public async Task<Weatherdata> ExecuteRequestAsync()
        {

            var client = new RestClient(url);
            var request = new RestRequest();

            response = await client.ExecuteTaskAsync(request);

            XmlSerializer serializer = new XmlSerializer(typeof(Weatherdata));
            Weatherdata objWA;

            TextReader sr = new StringReader(response.Content);
            objWA = (Weatherdata)serializer.Deserialize(sr);
            return objWA;
        }
    }
}

