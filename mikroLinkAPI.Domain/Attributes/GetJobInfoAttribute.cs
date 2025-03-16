using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikroLinkAPI.Domain.Attributes
{
    public class GetJobInfoAttribute : Attribute
    {
        public string Value { get; }

        public GetJobInfoAttribute(string value)
        {
            Value = value;
        }
    }
}
