﻿namespace BestService.Web.ViewModels.RecentCompanies
{
    using System;
    using System.Collections.Generic;

    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class RecentCompanyViewModel : IMapFrom<Company>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public double ArverageStars => Math.Round(this.Rating, 1, MidpointRounding.AwayFromZero);

        public string LogoImage { get; set; }

        public string LogoImagePath => GlobalConstants.CloudinaryUploadDir + this.LogoImage;

        public DateTime CreatedOn { get; set; }

        public string CreatedShort => this.CreatedOn.ToShortDateString();

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserName => this.User.UserName;
    }
}
