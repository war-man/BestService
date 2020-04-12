﻿namespace BestService.Web.Controllers
{
    using System.Diagnostics;

    using BestService.Common;
    using BestService.Services.Data;
    using BestService.Web.ViewModels;
    using BestService.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICompaniesService companiesService;

        public HomeController(
            ICategoriesService categoriesService,
            ICompaniesService companiesService)
        {
            this.categoriesService = categoriesService;
            this.companiesService = companiesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Categories = this.categoriesService.GetAll<IndexCategoryViewModel>(),
                MostRecentCompanies = this.companiesService.GetRecentlyAdded<IndexCompanyDetailsViewModel>(3),
                MostCommentedCompanies = this.companiesService.GetMostCommented<IndexCompanyDetailsViewModel>(3),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult HttpError(int? statusCode)
        {
            if (statusCode != null)
            {
                var statusMessage = string.Empty;

                switch (statusCode)
                {
                    case 400:
                        statusMessage = GlobalConstants.ErrorStatusBadRequest;
                        break;
                    case 403:
                        statusMessage = GlobalConstants.ErrorStatusForbidden;
                        break;
                    case 404:
                        statusMessage = GlobalConstants.ErrorStatusPageNotFound;
                        break;
                    case 408:
                        statusMessage = GlobalConstants.ErrorStatusTimeout;
                        break;
                    case 500:
                        statusMessage = GlobalConstants.ErrorStatusInternalServerError;
                        break;
                    default:
                        statusMessage = GlobalConstants.ErrorStatusOther;
                        break;
                }

                string reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode.Value);

                var viewModel = new CustomErrorViewModel
                {
                    StatusCodeNumber = statusCode.Value,
                    ReasonPhrase = reasonPhrase,
                    StatusMessage = statusMessage,
                };

                return this.View(viewModel);
            }

            return this.RedirectToAction(nameof(this.Error));
        }
    }
}
