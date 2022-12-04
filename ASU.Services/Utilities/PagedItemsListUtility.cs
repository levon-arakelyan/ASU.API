using ASU.Core.Enums;
using ASU.Core.Models;
using AutoMapper;

namespace ASU.Services.Utilities
{
    public class PagedItemsListUtility<TEntity, TDto> where TEntity : class, new()
    {
        private readonly IMapper _mapper;
        private readonly string[] _keysToFilter;
        private readonly string[] _keysToOrder;
        private IQueryable<TEntity> _query;

        public PagedItemsListUtility(
            IMapper mapper,
            IQueryable<TEntity> query,
            string[] keysToFilter,
            string[] keysToOrder

        )
        {
            _mapper = mapper;
            _query = query;
            _keysToFilter = keysToFilter;
            _keysToOrder = keysToOrder;
        }

        public PagedItemsList<TDto> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var dividers = filter.Where(x => char.IsWhiteSpace(x) || char.IsPunctuation(x)).Distinct()
                    .ToArray();
                var keywords = filter.Split(dividers).Distinct()
                    .ToArray();
                _query = keywords.Aggregate(_query, FilterItems);
            }

            var totalRecords = _query.Count();

            _query = OrderItems(_query, orderBy, direction);

            var allItems = _query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mappedItems = _mapper.Map<ICollection<TEntity>, ICollection<TDto>>(allItems);

            return new PagedItemsList<TDto>()
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                Order = new PagedListOrder() { Direction = direction, OrderBy = orderBy },
                Data = mappedItems,
            };
        }

        private IQueryable<TEntity> OrderItems(IQueryable<TEntity> query, string orderBy, OrderDirection direction)
        {
            var key = _keysToOrder.FirstOrDefault(x => x.ToLower() == orderBy.ToLower());
            if (string.IsNullOrEmpty(key))
                key = "id";

            var enumerable = query.AsEnumerable();
            
            return (direction == OrderDirection.Ascending
                ? enumerable.OrderBy(p => GetValue(p, key))
                : enumerable.OrderByDescending(p => GetValue(p, key))).AsQueryable<TEntity>();
        }

        private IQueryable<TEntity> FilterItems(IQueryable<TEntity> query, string filter)
        {
            var enumerable = query.AsEnumerable();
            return enumerable.Where(x => GetFilter(0, filter.ToLower(), x)).AsQueryable<TEntity>();
        }

        private bool GetFilter(int i, string filter, TEntity x)
        {
            if (i >= _keysToFilter.Length)
                return false;

            return GetValue(x, _keysToFilter[i]).ToLower().Contains(filter) || GetFilter(i + 1, filter, x);
        }

        private string GetValue(object obj, string propertyName)
        {
            foreach (var prop in propertyName.Split('.').Select(s => obj.GetType().GetProperty(s)))
            {
                if (prop == null)
                    return "";

                object? newObj = prop.GetValue(obj, null);
                if (newObj == null)
                    return "";

                obj = newObj;
            }

            if (obj == null)
                return "";

            var res = obj.ToString();
            if (res == null)
                return "";

            return res;
        }
    }
}
