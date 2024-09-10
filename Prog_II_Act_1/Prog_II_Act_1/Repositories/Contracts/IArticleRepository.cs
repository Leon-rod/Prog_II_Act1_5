using Prog_II_Act_1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Repositories.Contracts
{
    internal interface IArticleRepository
    {
        bool Add(Article article);
        List<Article> GetAll();
        bool Save(Article article);
        bool Delete(Article article);
    }
}
