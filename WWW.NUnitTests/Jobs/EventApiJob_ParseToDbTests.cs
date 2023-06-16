using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Api;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Jobs.Jobs;
using WWW.Service.Helpers;
using WWW.Service.Helpers.Api;
using WWW.Service.Interfaces;

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
        private Mock<Dictionary<string, string>> _keyValuePairs;

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
            _keyValuePairs = new Mock<Dictionary<string, string>>();

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
        //    var httpContext = new DefaultHttpContext();
        //    //httpContext.Session = new NullSession();

        //    // Установка значения "City" в сеансе
        //    var sessionKey = "City";
        //    httpContext.Session.SetString(sessionKey, expectedCity);

        //    _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        //    // Act
        //    await _job.ExecuteAsync();

        //    // Assert
        //    _restApiRequestMock.Verify(x => x.ApiSelector(It.IsAny<string>()), Times.Never);
        //    _restApiRequestMock.Verify(x => x.GetDataAsync<Rootobject>(It.Is<Dictionary<string, string>>(d => d.ContainsKey("city") && d["city"] == expectedCity)), Times.Never);
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
                                        name = venueName
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _restApiRequestMock.Setup(x => x.GetDataAsync<Rootobject>(It.IsAny<Dictionary<string, string>>())).ReturnsAsync(apiData);


            // Act
            await _job.ExecuteAsync();

            // Assert
            _articleRepositoryMock.Verify(x => x.Create(It.IsAny<Article>()), Times.Never());

            _categoryRepositoryMock.Verify(x => x.Create(It.IsAny<Category>()), Times.Never());

            _locationRepositoryMock.Verify(x => x.Create(It.Is<Domain.Entity.Location>(l => l.location == venueName && l.City == cityName)), Times.Never());

            _dateRepositoryMock.Verify(x => x.Create(It.IsAny<EventDates>()), Times.Never());

            _pictureRepositoryMock.Verify(x => x.Create(It.IsAny<Picture>()), Times.Never());
        }
    }
}
