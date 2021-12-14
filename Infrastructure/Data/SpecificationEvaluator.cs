using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> sepc)
        {
            var query = inputQuery;

            if(sepc.Criteria != null)
            {
                query = query.Where(sepc.Criteria); // p => p.ProductTypeId == id
            }

            query = sepc.Includes.Aggregate(query,(current,include) => current.Include(include));

            return query;
        }
    }
}