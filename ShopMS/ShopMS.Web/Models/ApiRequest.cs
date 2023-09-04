using static ShopMS.Web.SD;

namespace ShopMS.Web.Models
{
    public class ApiRequest
    {

        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get; set; } = "https://localhost:7166";

        public object Data { get; set; }

        public string AccessToken { get; set; }

    }
}
