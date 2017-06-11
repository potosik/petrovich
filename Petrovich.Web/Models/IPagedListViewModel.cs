using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models
{
    public interface IPagedListViewModel
    {
        int CurrentPage { get; set; }
        int TotalCount { get; set; }
        int TotalPages { get; set; }

        int StartItem { get; set; }
        int LastItem { get; set; }
    }
}