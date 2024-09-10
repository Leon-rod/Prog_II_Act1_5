using Prog_II_Act_1.Entities;
using Prog_II_Act_1.Repositories.Contracts;
using Prog_II_Act_1.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Services
{
    public class ArticleManager
    {
        private IArticleRepository _articleRepository;
        public ArticleManager()
        {
            _articleRepository = new ArticleRepositoryADO();
        }
        public List<Article>GetArticles()
        {
            return _articleRepository.GetAll();
        }
        public bool AddArticle(Article article)
        {
            return _articleRepository.Add(article);
        }
        public bool DeleteArticle(Article article)
        {
            return _articleRepository.Delete(article);
        }
        public bool UpdateArticle(Article article)
        {
            return _articleRepository.Save(article);
        }
    }
}
