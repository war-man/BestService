﻿namespace BestService.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<CompaniesInCategoryViewModel> Companies { get; set; }
    }
}
