using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Domain.Models.Api;
using WWW.Jobs.Jobs;
using WWW.Service.Helpers;
using WWW.Service.Helpers.Api;
using WWW.Service.Interfaces;
using static WWW.Domain.Models.Api.TicketApi;

namespace WWW.NUnitTests.Jobs
{
    [TestFixture]
    public class EventApiJob_ParseToDbTests
    {
        private EventApiJob_ParseToDb _job;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IRestApiRequest> _restApiRequestMock;
        private Mock<ILogger<EventApiJob_ParseToDb>> _loggerMock;
        private Mock<IArticleRepository> _articleRepositoryMock;
        private Mock<IUserRepository> _accountRepositoryMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<DownloadService> _downloadServiceMock;
        private Mock<EntityBaseRepository<Domain.Entity.Location>> _locationRepositoryMock;
        private Mock<EntityBaseRepository<EventDates>> _dateRepositoryMock;
        private Mock<EntityBaseRepository<Picture>> _pictureRepositoryMock;

        [SetUp]
        public void Setup()
        {

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _restApiRequestMock = new Mock<IRestApiRequest>();
            _loggerMock = new Mock<ILogger<EventApiJob_ParseToDb>>();
            _articleRepositoryMock = new Mock<IArticleRepository>();
            _accountRepositoryMock = new Mock<IUserRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _downloadServiceMock = new Mock<DownloadService>();
            _locationRepositoryMock = new Mock<EntityBaseRepository<Domain.Entity.Location>>();
            _dateRepositoryMock = new Mock<EntityBaseRepository<EventDates>>();
            _pictureRepositoryMock = new Mock<EntityBaseRepository<Picture>>();

            _job = new EventApiJob_ParseToDb(
                _restApiRequestMock.Object,
                _loggerMock.Object,
                _articleRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _downloadServiceMock.Object,
                _locationRepositoryMock.Object,
                _dateRepositoryMock.Object,
                _pictureRepositoryMock.Object,
                _httpContextAccessorMock.Object
            );
        }

        //[Test]
        //public async Task ExecuteAsync_ShouldSetCityFromSession()
        //{
        //    // Arrange
        //    string expectedCity = "Toronto";
        //    _httpContextAccessorMock.Setup(x => x.HttpContext.Session.GetString("City")).Returns(expectedCity);
        //    //_httpContextAccessorMock.Setup(session => SessionExtensions.GetString((ISession)session, "City")).Returns(expectedCity);

        //    // Act
        //    await _job.ExecuteAsync();

        //    // Assert
        //    _httpContextAccessorMock.Verify(x => x.HttpContext.Session.GetString("City"), Times.Once);
        //    _restApiRequestMock.Verify(x => x.ApiSelector(It.IsAny<string>()), Times.Once);
        //    _restApiRequestMock.Verify(x => x.GetDataAsync<Rootobject>(It.Is<Dictionary<string, string>>(d => d.ContainsKey("city") && d["city"] == expectedCity)), Times.Once);
        //}

        [Test]
        public async Task ExecuteAsync_ShouldCreateArticleAndRelatedEntities()
        {
            // Arrange
            string cityName = "City1";
            string eventName = "Event1";
            string venueName = "Venue1";

            var apiData = new Rootobject
            {
                page = new Page
                {
                    totalPages = 1,
                    size = 1
                },
                _embedded = new _Embedded
                {
                    events = new[]
                    {
                        new Event
                        {
                            name = eventName,
                            _embedded = new _Embedded1
                            {
                                venues = new[]
                                {
                                    new Venue1
                                    {
                                        name = venueName,
                                        city = new City
                                        {
                                            name = cityName
                                        }
                                    }
                                }
                            },
                            classifications = new[]
                            {
                                new Classification1
                                {
                                    segment = new Segment1
                                    {
                                        name = "Segment1"
                                    }
                                }
                            },
                            dates = new Dates
                            {
                                status = new Status
                                {
                                    code = "Status1"
                                },
                                start = new Start
                                {
                                    localTime = "2023-05-17T10:00:00"
                                }
                            },
                            images = new[]
                            {
                                new Image2
                                {
                                    url = "http://example.com/image.jpg"
                                }
                            }
                        }
                    }
                }
            };

            _httpContextAccessorMock.Setup(x => x.HttpContext.Session.GetString("City")).Returns(cityName);
            _restApiRequestMock.Setup(x => x.GetDataAsync<Rootobject>(It.IsAny<Dictionary<string, string>>())).ReturnsAsync(apiData);
            _articleRepositoryMock.Setup(x => x.GetALL()).Returns(new List<Article>().AsQueryable());

            _articleRepositoryMock.Setup(x => x.Create(It.IsAny<Article>())).ReturnsAsync(true);

            _accountRepositoryMock.Setup(x => x.GetALL()).Returns(new List<User>().AsQueryable());
            _categoryRepositoryMock.Setup(x => x.GetALL()).Returns(new List<Category>().AsQueryable());
            _categoryRepositoryMock.Setup(x => x.Create(It.IsAny<Category>())).ReturnsAsync(true);
            _locationRepositoryMock.Setup(x => x.GetALL()).Returns(new List<Domain.Entity.Location>().AsQueryable());
            _locationRepositoryMock.Setup(x => x.Create(It.IsAny<Domain.Entity.Location>())).ReturnsAsync(true);
            _dateRepositoryMock.Setup(x => x.Create(It.IsAny<EventDates>())).ReturnsAsync(true);
            _downloadServiceMock.Setup(x => x.DownloadJpgPictAsync(It.IsAny<string>())).ReturnsAsync(new Picture());

            // Act
            await _job.ExecuteAsync();

            // Assert
            _articleRepositoryMock.Verify(x => x.Create(It.Is<Article>(a => a.Title == eventName)), Times.Once);
            _categoryRepositoryMock.Verify(x => x.Create(It.Is<Category>(c => c.Name == "Segment1")), Times.Once);
            _locationRepositoryMock.Verify(x => x.Create(It.Is<Domain.Entity.Location>(l => l.location == venueName && l.City == cityName)), Times.Once);
            _dateRepositoryMock.Verify(x => x.Create(It.IsAny<EventDates>()), Times.Once);
            _downloadServiceMock.Verify(x => x.DownloadJpgPictAsync(It.IsAny<string>()), Times.Once);
            _pictureRepositoryMock.Verify(x => x.Create(It.IsAny<Picture>()), Times.Once);
        }
    }
}
