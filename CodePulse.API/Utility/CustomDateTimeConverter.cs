using System.Runtime.Serialization;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CodePulse.API.Utility
{
    public class CustomDateTimeConverter: IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.SSSZ"; // Specify your expected format here
        }
    }
}
