using Framework.Core.IUnit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Service.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataAccess.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private IDbContextTransaction transaction;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task BeginTransaction()
        {
            transaction = dbContext.Database.BeginTransaction();
            await Task.CompletedTask;
        }

        public async Task CommitTransaction()
        {
            try
            {
                await SaveChanges();
                await transaction.CommitAsync();
            }
            finally
            {
                await DisposeAsync();
                await transaction.DisposeAsync();
            }
            await Task.CompletedTask;
        }

        public async Task RollbackTransaction()
        {
            try
            {
                await transaction.RollbackAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }
    }
}
