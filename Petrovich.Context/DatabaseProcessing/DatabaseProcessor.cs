using Petrovich.Business.Exceptions;
using Petrovich.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context.DatabaseProcessing
{
    public class DatabaseProcessor<TModel> :
        IDatabaseReadModel<TModel>
        where TModel : class, IDisposable
    {
        private TModel model;

        private DatabaseProcessor(Func<TModel> modelFactory)
        {
            Guard.NotNullArgument(modelFactory, nameof(modelFactory));

            model = modelFactory();
        }

        public async Task<TResult> QueryAsync<TResult>(IDatabaseQuery<TModel, TResult> query)
        {
            Guard.NotNullArgument(model, nameof(model));
            Guard.NotNullArgument(query, nameof(query));

            try
            {
                return await query.ExecuteAsync(model).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException(ex);
            }
            finally
            {
                DisposeModel();
            }
        }

        public async Task<TResult> QueryAsync<TResult>(Func<TModel, TResult> query)
        {
            Guard.NotNullArgument(model, nameof(model));
            Guard.NotNullArgument(query, nameof(query));

            return await Task.Run(() =>
            {
                try
                {
                    return query(model);
                }
                catch (Exception ex)
                {
                    throw new DatabaseOperationException(ex);
                }
                finally
                {
                    DisposeModel();
                }
            }).ConfigureAwait(false);
        }

        public async Task DoAsync(IDatabaseOperation<TModel> command)
        {
            Guard.NotNullArgument(model, nameof(model));
            Guard.NotNullArgument(command, nameof(command));

            try
            {
                await command.RunAsync(model).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException(ex);
            }
            finally
            {
                DisposeModel();
            }
        }

        public async Task<TResult> DoAsync<TResult>(IDatabaseOperation<TModel, TResult> command)
        {
            Guard.NotNullArgument(model, nameof(model));
            Guard.NotNullArgument(command, nameof(command));

            try
            {
                return await command.RunAsync(model).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException(ex);
            }
            finally
            {
                DisposeModel();
            }
        }
        
        private void DisposeModel()
        {
            if (model != null)
            {
                model.Dispose();
                model = default(TModel);
            }
        }

        public static DatabaseProcessor<TModel> Model(Func<TModel> modelFactory)
        {
            return new DatabaseProcessor<TModel>(modelFactory);
        }
    }
}
