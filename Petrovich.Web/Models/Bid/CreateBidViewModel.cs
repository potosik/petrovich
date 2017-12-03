using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petrovich.Business.Models;
using System.Web.Mvc;

namespace Petrovich.Web.Models.Bid
{
    public class CreateBidViewModel
    {
        public CreateBidClientViewModel Client { get; set; }
        public CreateBidParametersViewModel Parameters { get; set; }

        public IList<BidProductViewModel> Products { get; set; }

        public CreateBidViewModel()
        {
            Client = new CreateBidClientViewModel();
            Parameters = new CreateBidParametersViewModel();
            Products = new List<BidProductViewModel>();
        }
    }
}