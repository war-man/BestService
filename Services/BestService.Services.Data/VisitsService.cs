﻿namespace BestService.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class VisitsService : IVisitsService
    {
        private readonly IDeletableEntityRepository<Visit> visitRepository;

        public VisitsService(
            IDeletableEntityRepository<Visit> visitRepository)
        {
            this.visitRepository = visitRepository;
        }

        public async Task<long> IncreaseVisit(int companyId, string userId)
        {
            var visit = this.visitRepository.All().FirstOrDefault(x => x.CompanyId == companyId && x.UserId == userId);

            if (visit == null)
            {
                visit = new Visit
                {
                    CompanyId = companyId,
                    Count = 1,
                    UserId = userId,
                };

                await this.visitRepository.AddAsync(visit);
            }
            else
            {
                visit.Count = ++visit.Count;
                this.visitRepository.Update(visit);
            }

            await this.visitRepository.SaveChangesAsync();
            return this.GetCompanyVisitCount(companyId);
        }

        public long GetCompanyVisitCount(int companyId)
            => this.visitRepository.AllAsNoTracking()
                .Where(r => r.CompanyId == companyId)
                .Sum(r => r.Count);
    }
}
