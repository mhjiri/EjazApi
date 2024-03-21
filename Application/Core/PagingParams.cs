using Microsoft.Extensions.Configuration;

namespace Application.Core
{
    public class PagingParams
    {
        
        
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 50000;
        public string OrderBy { get; set; } = string.Empty;
        public string OrderAs { get; set; } = "ASC";
        public int PageMaxSize { get; set; } = 50000;


        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > PageMaxSize) ? PageMaxSize : value;
        }
    }
}
