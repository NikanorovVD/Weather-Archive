﻿using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ServiceLayer.Services
{
    public class DatabaseInitService : BackgroundService
    {
        private readonly AppDbContext _dbContext;

        public DatabaseInitService(IServiceScopeFactory serviceScopeFactory)
        {
            IServiceScope scope = serviceScopeFactory.CreateScope();
            _dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _dbContext.Database.MigrateAsync(cancellationToken: stoppingToken);
        }
    }
}
