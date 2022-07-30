namespace ProductShop.DTOs.User
{
    using Newtonsoft.Json;
    using System.Linq;

    [JsonObject]
    public  class UserCountAndAllProductsInfoDTO
    {
        public UserCountAndAllProductsInfoDTO(ExportUserProductsDTO[] users)
        {
            Users = users;
        }

        [JsonProperty("usersCount")]
        public int UsersCount => this.Users.Any() 
                                 ? Users.Length
                                 : 0;

        [JsonProperty("users")]
        public ExportUserProductsDTO[] Users { get; set; }
    }
}
