using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model
{
    public class ProductProvider : ProductType
    {
        [JsonPropertyName("account_number")]
        public int AccountNumber { get; set; }

        [JsonPropertyName("account_beneficiary")]
        public string AccountBeneficiary { get; set; }

        [JsonPropertyName("account_ddebit")]
        public string AccountDdebit { get; set; }
    }
}