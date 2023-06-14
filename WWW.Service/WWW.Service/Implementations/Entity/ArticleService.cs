using WWW.Domain.Entity;
using WWW.Domain.Response;
using WWW.Service.Interfaces;
using WWW.DAL.Interfaces;
using WWW.Domain.Enum;
using Microsoft.EntityFrameworkCore;

using WWW.DAL.Repositories;
using WWW.Domain.ViewModels.Article;
using WWW.Domain.Api;
using System;
using WWW.Domain.Response;
using Location = WWW.Domain.Entity.Location;
using System.Linq;
using WWW.Domain.Enum.Articles;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Castle.Components.DictionaryAdapter.Xml;
using System.Text.RegularExpressions;

namespace WWW.Service.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly EntityBaseRepository<Picture> _pictureRepository;
        private readonly EntityBaseRepository<Location> _locationRepository;
        private readonly EntityBaseRepository<EventDates> _dateRepository;


        public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, EntityBaseRepository<Picture> pictureRepository, EntityBaseRepository<Location> locationRepository, EntityBaseRepository<EventDates> dateRepository)
        {

            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _pictureRepository = pictureRepository;
            _locationRepository = locationRepository;
            _dateRepository = dateRepository;
        }

        public async Task<BaseResponse<IEnumerable<Article>>> GetAll()
        {
            var BaseResponse = new BaseResponse<IEnumerable<Article>>();
            try {
                var Articles = await _articleRepository.GetAll();
                if (Articles.Count() == 0)
                {
                    BaseResponse.ErrorDescription = "Found 0 elements";
                    BaseResponse.StatusCode = (StatusCode)StatusCode.OK;
                }
                else
                {
                    BaseResponse.Data = Articles;
                    BaseResponse.StatusCode = (StatusCode)StatusCode.OK;
                }
                return BaseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Article>>()
                {
                    ErrorDescription = $"[Articles.GetAll]:{ex.Message}",
                };
            }
        }
        public async Task<BaseResponse<IQueryable<Article>>> GetByCategoryName(string CatName=null)
        {
            if (CatName == null) return new BaseResponse<IQueryable<Article>>() { Data = _articleRepository.GetALL(), StatusCode= StatusCode.OK};
            BaseResponse<IQueryable<Article>> BaseResponse = new BaseResponse<IQueryable<Article>>();
            try
            {
                BaseResponse.Data = await _articleRepository.GetByCategoryName(CatName);
                BaseResponse.StatusCode = StatusCode.OK;
            }
            catch(Exception ex) { 
                BaseResponse.ErrorDescription += ex.Message;
                BaseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return BaseResponse;
        }
        public async Task<BaseResponse<IQueryable<Article>>> GetByCity(string? City)
        {
            IQueryable<Article> data = null;
            if (City != null)
                data = _articleRepository.GetALL().Where(a => a.Location.City == City);


            if (data != null && data.Any())
                return new BaseResponse<IQueryable<Article>>() { Data = data, StatusCode = StatusCode.OK };
            else
                return new BaseResponse<IQueryable<Article>>() { Data = _articleRepository.GetALL(), StatusCode = StatusCode.OK };

        }

        public async Task<IQueryable<Article>> SearchByTitle(IQueryable<Article> articles, string searchTerm)
        {
            //return _articleRepository.SearchByTitle(articles, searchTerm);
            return articles.Where(a => a.Title.ToLower().Replace(" ", "").Contains(searchTerm.ToLower().Replace(" ", "")));

        }


        public async Task<BaseResponse<IQueryable<Article>>> GetByCategoryNameFilter(IQueryable<Article> articles, string? CatName)
        {

            if (CatName == null) return new BaseResponse<IQueryable<Article>>() { Data = articles, StatusCode = StatusCode.OK };

            BaseResponse<IQueryable<Article>> BaseResponse = new();
            try
            {
                BaseResponse.Data = articles.Where(a=>a.Category.slug==CatName);
                BaseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                BaseResponse.ErrorDescription += ex.Message;
                BaseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return BaseResponse;
        }
        public async Task<BaseResponse<Article>> GetById(int id)
        {
            try
            {
                var data = await _articleRepository.GetALL().FirstOrDefaultAsync(a => a.Id == id);
                return new BaseResponse<Article>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>()
                {
                    ErrorDescription = ex.Message,
                };
            }


        }

        public async Task<bool> Create(ArticleCreateViewModal entity)
        {

            //  Location
            Location loc = await _locationRepository.GetALL().FirstOrDefaultAsync(l => l.location == entity.Location);
            if (loc == null)
            {
                loc = new Location()
                {
                    location = entity.Location,
                    City = entity.City,
                    Building = entity.Building,
                    CountryCode = entity.CountryCode,
                    PostalCode = entity.PostalCode,
                    Timezone = entity.Timezone
                };
                await _locationRepository.Create(loc);
            }

            Article newArticle = new Article(entity)
            {
                Category = await _categoryRepository.GetValueByID(entity.Category),
                IsFavorite = true,
                Autor = await _userRepository.GetALL().FirstAsync(u => u.Id == entity.UserId),
                Status = ArticleStatus.Onsale,
                Location = loc,
                Published = entity.Published,

            };
            //  slug
            string slug = Regex.Replace(entity.Title, @"[^a-zA-Zа-яА-Я\s]", "");
            slug = Regex.Replace(slug, "-+", "-");
            if (!_articleRepository.GetALL().Any())
            {

                newArticle.slug = slug.ToLower().Replace(' ', '-') + "-" + (0);
            }
            else
            {
                newArticle.slug = slug.ToLower().Replace(' ', '-') + "-" + (_articleRepository.GetALL().OrderBy(a => a.Id).Last().Id + 1);
            }
            await _articleRepository.Create(newArticle);
            // Picture
            if (entity.Picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await entity.Picture.CopyToAsync(memoryStream);
                    await _pictureRepository.Create(newArticle.Picture = new Picture()
                    {
                        Article = newArticle,
                        picture = memoryStream.ToArray(),
                        Type = entity.Picture.GetType().ToString(),
                        Name = entity.Picture.Name,
                    });
                }
                
            }
            // Data 
            await _dateRepository.Create(new EventDates()
            {
                Article = newArticle,
                Date_of_Creation = DateTime.Now,
                Date_Of_Start = entity.DateOfEvent,
                Date_Of_Updated = DateTime.Now,
            });


            return true;
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddTag(Article article,Tags tags)
        {
            await _articleRepository.AddTags(article, tags);
            return true;
        }

        public Task<bool> Create(Article category)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IQueryable<Article>>> OrderBy(IQueryable<Article> articles, ArticleSortOption SortOption)
        {
            switch (SortOption)
            {   
                case ArticleSortOption.None:
                    return new BaseResponse<IQueryable<Article>>(){ Data = articles, StatusCode=StatusCode.OK };
                case ArticleSortOption.ByTitle:
                    return new BaseResponse<IQueryable<Article>>() { Data = articles.OrderBy(a => a.Title), StatusCode = StatusCode.OK };
                case ArticleSortOption.ByDateAscending:
                    return new BaseResponse<IQueryable<Article>>() { Data = articles.OrderBy(a => a.Date), StatusCode = StatusCode.OK };
                case ArticleSortOption.ByDateDescending:
                    return new BaseResponse<IQueryable<Article>>() { Data = articles.OrderByDescending(a => a.Date), StatusCode = StatusCode.OK };
                //case ArticleSortOption.UserFavorites:
                //    return new BaseResponse<IQueryable<Article>>() { Data = (await _userRepository.GetValueByID(UserId)).FavEvent.AsQueryable(), StatusCode = StatusCode.OK };
                case ArticleSortOption.Popular:
                    return new BaseResponse<IQueryable<Article>>(){ Data = articles.OrderByDescending(a => a.User.Count()),  StatusCode = StatusCode.OK};
                default:
                    return new BaseResponse<IQueryable<Article>>() { Data = articles, StatusCode = StatusCode.OK };
            }
        }

        public async Task<BaseResponse<IQueryable<Article>>> Filter(IQueryable<Article> articles, Dictionary<string,string> filters )
        {
            if (filters.ContainsKey("Status") && filters["Status"]!=null)
            {
                articles = articles.Where(a => a.Status == Enum.Parse<ArticleStatus>(filters["Status"]));
            }
            if (filters.ContainsKey("Date") && filters["Date"] != null)
            {
                DateTime now = DateTime.Now;
                DayOfWeek currentDayOfWeek = now.DayOfWeek;
                int daysUntilNextWeekend = ((int)DayOfWeek.Saturday - (int)currentDayOfWeek + 7) % 7;


                DateTime[] nextWeekend = { now.AddDays(daysUntilNextWeekend), now.AddDays(daysUntilNextWeekend+1) };
                DateTime[] thisWeek = { now, now.AddDays(daysUntilNextWeekend + 1) };

                switch (filters["Date"])
                {
                    case "Today":
                        articles = articles.Where(a => a.Date.Date_Of_Start.Date == now.Date);
                        break;
                    case "This_Week":
                        articles = articles.Where(a => a.Date.Date_Of_Start.Date >= thisWeek[0].Date && a.Date.Date_Of_Start < thisWeek[1].Date);
                        break;

                    case "This_Weekends":
                        articles = articles.Where(a => a.Date.Date_Of_Start.Date >= nextWeekend[0].Date && a.Date.Date_Of_Start < nextWeekend[1].Date);
                        break;

                }
                //articles = articles.Where(a => a.Date == Enum.Parse<ArticleFilters.date>(filters["Status"]));
            }

            return new BaseResponse<IQueryable<Article>>() { Data = articles, StatusCode = StatusCode.OK };

        }

        public BaseResponse<Article> GetBySlug(string slug)
        {
            try
            {
                var data = _articleRepository.GetALL().FirstOrDefault(a => a.slug == slug);

                if (data == null)
                {
                    return new BaseResponse<Article>() { StatusCode = StatusCode.NotFound };
                }

                return new BaseResponse<Article>() {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>() { StatusCode= StatusCode.InternalServerError, ErrorDescription = ex.Message };
                
            }
        }

        
    }
}
