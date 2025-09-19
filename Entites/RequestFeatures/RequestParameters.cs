using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPage = 15;
        public int PageNumber { get; set; }
        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value>maxPage ? maxPage:value; }
        }

        public string? SearchTerm { get; set; }
    }
}
