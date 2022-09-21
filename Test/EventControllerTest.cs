using Api.Controllers;
using DataAccess.Entities;
using DataAccess.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Test
{
    public class EventControllerTest
    {

        [Fact]
        public async Task Test_GetById_InvalidId_EventId()
        {
            var Id = 11;
            var mock = new Mock<IBaseRepository<Event>>();
            mock.Setup(m => m.GetById(Id))
                .Returns(Task.FromResult<Event>(null));
            var _controller = new EventController(mock.Object);
            

            var result = await _controller.GetById(Id);
            var notFoundResult = result.Result as NotFoundResult;

            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Test_GetById_Valid_EventId()
        {
            var id = 11;
            var ev = new Event
            {
                Id = 11,
            };
            var mock = new Mock<IBaseRepository<Event>>();
            mock.Setup(m => m.GetById(id))
                .ReturnsAsync(ev);
            var _controller = new EventController(mock.Object);

            var result = await _controller.GetById(id);
            var okResult = result.Result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Test_GetAll()
        {
            var idOne = 1;
            var idTwo = 2;
            var returnList = new List<Event>
            {
                new()
                {
                    Id = idOne
                },
                new()
                {
                    Id = idTwo
                }
            };
            var mock = new Mock<IBaseRepository<Event>>();
            mock.Setup(m => m.GetAll())
                .ReturnsAsync(returnList);
            var _controller = new EventController(mock.Object);

            var result = await _controller.GetAll();
            var okResult = result.Result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().NotBeNull();
            var returnedList = okResult.Value as List<Event>;
            returnedList.Should().NotBeNull();
            returnedList.Count.Should().Be(returnList.Count);
        }

        [Fact]
        public async Task Test_GetServiceFeeByEventId()
        {
            var id = 1;
            var baseRepoMock = new Mock<IBaseRepository<Event>>();
            var specificRepoMock = new Mock<IEventRepository>();
            specificRepoMock.Setup(m => m.GetCorrespondingServiceFeeById(id))
                .ReturnsAsync(new ServiceFee());
            var _controller = new EventController(baseRepoMock.Object);

            var result = await _controller.GetServiceFeeByEventId(specificRepoMock.Object, id);
            var okResult = result.Result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeAssignableTo<ServiceFee>();
        }

    }
}
