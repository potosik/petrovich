using Petrovich.Business.Data;
using System;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Repositories.Mappers;
using System.Data.Entity.Core;
using Petrovich.Business.Exceptions;

namespace Petrovich.Repositories.DataSources
{
    public class LogDataSource : ILogDataSource
    {
        private readonly ILogRepository logRepository;
        private readonly ILogMapper logMapper;

        public LogDataSource(ILogRepository logRepository, ILogMapper logMapper)
        {
            this.logRepository = logRepository;
            this.logMapper = logMapper;
        }

        public async Task<LogModel> FindAsync(Guid id)
        {
            try
            {
                var entity = await logRepository.FindAsync(id);
                return logMapper.ToLogModel(entity);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<LogModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            try
            {
                var entities = await logRepository.ListAsync(pageIndex, pageSize);
                var count = await logRepository.ListCountAsync();
                var collection = logMapper.ToLogModelCollection(entities);
                collection.TotalCount = count;
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task WriteLogAsync(LogModel entity)
        {
            try
            {
                var mappedEntity = logMapper.ToContextLog(entity);
                await logRepository.CreateAsync(mappedEntity);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
