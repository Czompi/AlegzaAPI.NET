using AlegzaCRM.AlegzaAPI.Util;
using System;
using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model;

public class Person : AlegzaModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("created_at")]
    [JsonConverter(typeof(JsonStringDateTimeConverter))]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    [JsonConverter(typeof(JsonStringDateTimeConverter))]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("workplace")]
    public string Workplace { get; set; }

    [JsonPropertyName("relationship_state")]
    public int RelationshipState { get; set; }

    [JsonPropertyName("partner_name")]
    public string PartnerName { get; set; }

    [JsonPropertyName("partner_profession")]
    public string PartnerProfession { get; set; }

    [JsonPropertyName("partner_phone")]
    public string PartnerPhone { get; set; }

    [JsonPropertyName("partner_age")]
    public string PartnerAge { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("housing_state")]
    public int HousingState { get; set; }

    [JsonPropertyName("helper_person_state")]
    public int HelperPersonState { get; set; }

    [JsonPropertyName("deleted_at")]
    [JsonConverter(typeof(JsonStringDateTimeConverter))]
    public DateTime? DeletedAt { get; set; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; }

    [JsonPropertyName("othername")]
    public string Othername { get; set; }

    [JsonPropertyName("birth_place")]
    public string BirthPlace { get; set; }

    [JsonPropertyName("birthday")]
    public string Birthday { get; set; }

    [JsonPropertyName("taxnumber")]
    public string Taxnumber { get; set; }

    [JsonPropertyName("nationality")]
    public string Nationality { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("adoazonosito_jel")]
    public string AdoazonositoJel { get; set; }

    [JsonPropertyName("category_id")]
    public string CategoryId { get; set; }

    [JsonPropertyName("company_id")]
    public string CompanyId { get; set; }

    [JsonPropertyName("birth_name")]
    public string BirthName { get; set; }

    [JsonPropertyName("postal_code")]
    public int PostalCode { get; set; }

    [JsonPropertyName("doc_idcard_number")]
    public string DocIdcardNumber { get; set; }

    [JsonPropertyName("doc_idcard_expiry")]
    public string DocIdcardExpiry { get; set; }

    [JsonPropertyName("doc_address_number")]
    public string DocAddressNumber { get; set; }

    [JsonPropertyName("doc_driver_number")]
    public string DocDriverNumber { get; set; }

    [JsonPropertyName("doc_driver_expiry")]
    public string DocDriverExpiry { get; set; }

    [JsonPropertyName("doc_bankacc_number")]
    public string DocBankaccNumber { get; set; }

    [JsonPropertyName("mothername")]
    public string Mothername { get; set; }
}
