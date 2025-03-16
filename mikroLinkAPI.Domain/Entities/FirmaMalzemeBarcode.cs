using mikroLinkAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class FirmaMalzemeBarcode:Entity
    {
        public   byte[] Barcode { get; set; }
    }
}
