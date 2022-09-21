using Api.Controllers;
using DataAccess.Entities;
using DataAccess.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Test
{
    public class ProductControllerTest
    {
        [Fact]
        public async Task Test_GetProductById_InvalidId()
        {
            var id = 1;
            var baseRepoMock = new Mock<IBaseRepository<Product>>();
            baseRepoMock.Setup(x => x.GetById(id))
                .Returns(Task.FromResult<Product>(null));

            var _controller = new ProductController(baseRepoMock.Object);


            var result = await _controller.GetProductById(id);
            var notFoundResult = result.Result as NotFoundResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Test_GetProductById_Invalid()
        {
            var id = 1;
            var baseRepoMock = new Mock<IBaseRepository<Product>>();
            baseRepoMock.Setup(x => x.GetById(id))
                .ReturnsAsync(new Product());

            var _controller = new ProductController(baseRepoMock.Object);


            var result = await _controller.GetProductById(id);
            var okResult = result.Result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeAssignableTo<Product>();
        }
    }
}
