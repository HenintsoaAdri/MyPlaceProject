using MyPlaceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPlaceProject.Services
{
    public class BaseModelPagination<T>
    {
        public ICollection<T> liste { get; set; }
        public int page { get; set; }
        public int maxResult { get; set; }
        public int totalResult { get; set; }
        public BaseModelPagination(int page, int maxResult)
        {
            this.page = page;
            this.maxResult = maxResult;
        }
        public Type getType()
        {
            return typeof(T);
        }
        public int getNbPage()
        {
            return (int)Math.Ceiling((double)totalResult / maxResult);
        }
        public int offset()
        {
            return (page - 1) * maxResult;
        }
    }
}