using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyExample
{
    [DataContract]
    public class ServiceResponse
    {

        [DataMember]
        public string Value { get; set; }
    }
}