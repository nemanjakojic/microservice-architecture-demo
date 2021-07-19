using System.ComponentModel;

namespace Demo.Microservice.Core.Common.Enum
{
    public enum Order
    {
        [Description("Ascending")]
        Asc = 0,

        [Description("Descending")]
        Desc = 1
    }
}
