using NUnit.Framework;
using System.Linq;
using WWW.Domain.Entity;
using WWW.Domain.Enum;
using WWW.Service.Implementations;
using WWW.Service.Interfaces;

namespace WWW.NUnitTests.Servises
{
    [TestFixture]
    public class ArticleServiceTests
    {
        private IArticleService _articleService;

        [SetUp]
        public void Setup(IArticleService articleService)
        {
            _articleService=articleService;
        }

        [Test]
        public void GetAll_ShouldReturnAllArticles()
        {
            // Arrange

            // Act
            var result = _articleService.GetAll().Result;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Greater(result.Data.Count(), 0);
        }

        [Test]
        public void GetBySlug_ExistingSlug_ShouldReturnArticle()
        {
            // Arrange
            string slug = "example-article-slug";

            // Act
            var result = _articleService.GetBySlug(slug);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.AreEqual(slug, result.Data.slug);
        }
    }
}
