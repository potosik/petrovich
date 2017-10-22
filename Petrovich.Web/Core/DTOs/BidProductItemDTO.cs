using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Core.DTOs
{
    public class BidProductItemDTO
    {
        [JsonProperty("product_id")]
        public Guid ProductId { get; set; }
    }
}