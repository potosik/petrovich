﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models
{
    public class PagedListViewModel<TViewModel> : IPagedListViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public int StartItem { get; set; }
        public int LastItem { get; set; }

        public IEnumerable<TViewModel> Items { get; set; }

        public PagedListViewModel(IEnumerable<TViewModel> items, int currentPage, int totalCount, int pageSize)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            StartItem = (currentPage - 1) * pageSize + 1;
            LastItem = StartItem - 1 + items.Count();
        }
    }
}