using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WWW.DAL.Interfaces;
using WWW.DAL.Repositories;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;
using static WWW.Domain.Enum.Articles.ArticleFilters;

namespace WWW.NUnitTests.Services
{
    [TestFixture]
    public class ArticleServiceTests
    {
        private Mock<IArticleRepository> _articleRepositoryMock;
        private Mock<IUserRepository> _userRepository;
        private Mock<ICategoryRepository> _categoryRepository;
        private Mock<EntityBaseRepository<Picture>> _pictureRepository;
        private Mock<EntityBaseRepository<Location>> _locationRepository;
        private Mock<EntityBaseRepository<EventDates>> _dateRepository;
        private IArticleService _articleService;

        [SetUp]
        public void Setup()
        {
            _articleRepositoryMock = new Mock<IArticleRepository>();
            _userRepository = new Mock<IUserRepository>() ;
            _categoryRepository = new Mock<ICategoryRepository>();
            _pictureRepository = new Mock<EntityBaseRepository<Picture>>();
            _locationRepository = new Mock<EntityBaseRepository<Location>>();
            _dateRepository = new Mock < EntityBaseRepository < EventDates >>() ;


            _articleService = new ArticleService(
                _articleRepositoryMock.Object,
                _userRepository.Object,
                _categoryRepository.Object,
                _pictureRepository.Object,
                _locationRepository.Object,
                _dateRepository.Object
                );
        }

        [Test]
        public async Task GetByCity_WithCity_ReturnsData()
        {
            // Arrange
            string city = "Toronto";
            var articles = new List<Article>
            {
                new Article { Location = new Location { City = "Toronto" } },
                new Article { Location = new Location { City = "Toronto" } },
                new Article { Location = new Location { City = "New York" } }
            };
            _articleRepositoryMock.Setup(x => x.GetALL()).Returns(articles.AsQueryable());

            // Act
            var response = await _articleService.GetByCity(city);

            // Assert
            Assert.AreEqual(StatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Data.Any());
            Assert.IsTrue(response.Data.All(a => a.Location.City == city));
        }


        [Test]
        public async Task GetByCategoryName_WithoutCatName_ReturnsAllData()
        {
            // Arrange
            var articles = new List<Article>
    {
        new Article { Category = new Category { slug = "cat1" } },
        new Article { Category = new Category { slug = "cat2" } },
        new Article { Category = new Category { slug = "cat3" } }
    };
            _articleRepositoryMock.Setup(x => x.GetALL()).Returns(articles.AsQueryable());


            // Act
            var response = await _articleService.GetByCategoryName(null);

            // Assert
            Assert.AreEqual(StatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(articles.Count, response.Data.Count());
        }

        [Test]
        public async Task GetByCategoryName_WithCatName_ReturnsFilteredData()
        {
            // Arrange
            string catName = "cat1";
            var articles = new List<Article>
                    {
                        new Article { Category = new Category { slug = "cat1" } },
                        new Article { Category = new Category { slug = "cat1" } },
                        new Article { Category = new Category { slug = "cat2" } }
                    };
            //_articleRepositoryMock.Setup(x => x.GetByCategoryName(catName)).ReturnsAsync(articles.AsQueryable());


            // Act
            var response = await _articleService.GetByCategoryName(catName);

            // Assert
            Assert.AreEqual(StatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Data.All(a => a.Category.slug == catName));
        }

        [Test]
        public async Task GetByCategoryNameFilter_WithoutCatName_ReturnsAllData()
        {
            // Arrange
            string catName = null;
            var articles = new List<Article>
    {
        new Article { Category = new Category { slug = "cat1" } },
        new Article { Category = new Category { slug = "cat2" } },
        new Article { Category = new Category { slug = "cat3" } }
    };
            IQueryable<Article> queryableArticles = articles.AsQueryable();


            // Act
            var response = await _articleService.GetByCategoryNameFilter(queryableArticles, catName);

            // Assert
            Assert.AreEqual(StatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(articles.Count, response.Data.Count());
        }

        [Test]
        public async Task GetByCategoryNameFilter_WithCatName_ReturnsFilteredData()
        {
            // Arrange
            string catName = "cat1";
            var articles = new List<Article>
                            {
                                new Article { Category = new Category { slug = "cat1" } },
                                new Article { Category = new Category { slug = "cat1" } },
                                new Article { Category = new Category { slug = "cat2" } }
                            };
            IQueryable<Article> queryableArticles = articles.AsQueryable();


            // Act
            var response = await _articleService.GetByCategoryNameFilter(queryableArticles, catName);

            // Assert
            Assert.AreEqual(StatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Data.All(a => a.Category.slug == catName));
        }

    }
}
