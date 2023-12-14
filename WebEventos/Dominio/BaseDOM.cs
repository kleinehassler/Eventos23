using System.Net.Http.Headers;

namespace WebEventos.Dominio
{
    public class BaseDOM : HttpClient
    {
        public BaseDOM(string url_api)
        {
            this.BaseAddress = new Uri(url_api);
        }
        public BaseDOM(string url_api, string token)
        {

            this.BaseAddress = new Uri(url_api);

            this.DefaultRequestHeaders.Clear();
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!token.Equals(""))
            {
                this.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }



    }
}
