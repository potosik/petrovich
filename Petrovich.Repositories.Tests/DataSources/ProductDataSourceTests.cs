﻿using Moq;
using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;
using Petrovich.Repositories.DataSources;
using Petrovich.DataSource.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Repositories.Tests.DataSources
{
    public class ProductDataSourceTests
    {
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly Mock<IProductMapper> productMapperMock;
        private readonly Mock<IFullImageDataSource> fullImageDataSourceMock;

        private readonly IProductDataSource dataSource;

        public ProductDataSourceTests()
        {
            productRepositoryMock = new Mock<IProductRepository>();
            productMapperMock = new Mock<IProductMapper>();
            fullImageDataSourceMock = new Mock<IFullImageDataSource>();

            dataSource = new ProductDataSource(productRepositoryMock.Object, productMapperMock.Object, fullImageDataSourceMock.Object);
        }

        [Fact]
        public async Task ListAsync_WhenEntityExceptionThrown_ShouldThrowDatabseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.ListAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListAsync(null, 1, 1);
            });
        }

        [Fact]
        public async Task CreateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.CreateAsync(It.IsAny<Context.Entities.Product>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.CreateAsync(new Business.Models.ProductModel());
            });
        }

        [Fact]
        public async Task FindAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.FindAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.UpdateAsync(new Business.Models.ProductModel());
            });
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.DeleteAsync(new Business.Models.ProductModel());
            });
        }

        [Fact]
        public async Task IsExistsForCategoryAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.IsExistsForCategoryAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.IsExistsForCategoryAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task IsExistsForGroupAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.IsExistsForGroupAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.IsExistsForGroupAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task SearchFastAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.SearchFastAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.SearchFastAsync(string.Empty, 0);
            });
        }

        [Fact]
        public async Task ListByCategoryIdAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.ListByCategoryIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListByCategoryIdAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task ListByGroupIdAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            productRepositoryMock.Setup(repository => repository.ListByGroupIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListByGroupIdAsync(Guid.Empty);
            });
        }
    }
}
