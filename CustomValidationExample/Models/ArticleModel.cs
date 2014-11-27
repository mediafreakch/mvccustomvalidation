using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomValidationExample.Models
{
    public class ArticleModel
    {
        public string Name { get; set; }

        [DisplayName("Start price")]
        public decimal StartPrice { get; set; }

        [DisplayName("BuyNow price")]
        [DecimalGreaterThan("StartPrice", "BuyNow price must be higher than the Start price")]
        public decimal BuyNowPrice { get; set; }
    }
}