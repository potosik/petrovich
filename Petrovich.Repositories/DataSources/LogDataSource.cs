using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models.Enumerations;
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
            this.logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            this.logMapper = logMapper ?? throw new ArgumentNullException(nameof(logMapper));
        }

        public async Task<Log> FindAsync(int id)
        {
            try
            {
                var entity = await logRepository.FindAsync(id);
                if (entity == null)
                {
                    throw new LogNotFoundException(id);
                }

                return logMapper.ToBusinessEntity(entity);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<LogCollection> ListAsync(int pageIndex, int pageSize)
        {
            try
            {
                var entities = await logRepository.ListAsync(pageIndex, pageSize);
                return logMapper.ToBusinessEntityCollection(entities);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task WriteLogAsync(Log entity)
        {
            try
            {
                var mappedEntity = logMapper.ToContextEntity(entity);
                await logRepository.CreateAsync(mappedEntity);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
