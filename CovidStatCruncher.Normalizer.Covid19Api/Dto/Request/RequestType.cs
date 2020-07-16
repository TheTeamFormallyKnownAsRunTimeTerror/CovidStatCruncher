using System.Runtime.Serialization;

namespace CovidStatCruncher.Normalizer.Covid19Api.Dto.Request
{
    public enum RequestType
    {
        [EnumMember(Value = "Default")]
        Default,
        [EnumMember(Value = "Summary")]
        Summary,
        [EnumMember(Value = "Countries")]
        Countries,
        [EnumMember(Value = "ByCountryAllStatus")]
        ByCountryAllStatus,
        [EnumMember(Value = "LiveByCountryAllStatus")]
        LiveByCountryAllStatus,
        [EnumMember(Value = "Stats")]
        Stats,
        [EnumMember(Value = "ByCountryAllStatusRange")]
        ByCountryAllStatusRange
    }
}
