using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Bid
{
    public class CreateBidParametersViewModel
    {
        private const int DefatulBidDays = 1;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CreateBidParametersViewModel()
        {
            StartDate = DateTime.UtcNow.Date;
            EndDate = DateTime.UtcNow.AddDays(DefatulBidDays).Date;
        }
    }
}