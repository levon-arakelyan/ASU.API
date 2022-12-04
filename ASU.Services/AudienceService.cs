using ASU.Core.Database.Entities;
using ASU.Core.Database;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using ASU.Services.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace ASU.Services
{
    public class AudienciesService : IAudienciesService
    {
        private readonly IDatabaseTable<Audience> _audienciesTable;
        private readonly IMapper _mapper;
        private readonly PagedItemsListUtility<Audience, AudienceDTO> _pagedItemsListUtility;

        private const string ErrorAudienceExists = "{0} համարով լսարան արդեն գոյություն ունի";

        public AudienciesService(IDatabaseTable<Audience> audienciesTable, IMapper mapper)
        {
            _audienciesTable = audienciesTable;
            _mapper = mapper;
            _pagedItemsListUtility = new PagedItemsListUtility<Audience, AudienceDTO>(
               _mapper,
               GetQuery(),
               new string[] { "Number" },
               new string[] { "Number" }
           );
        }

        public async Task Add(NewAudience newAudience)
        {
            if (newAudience == null)
            {
                throw new ArgumentNullException(nameof(newAudience));
            }

            var audienceWithNumber = await _audienciesTable.Queryable().FirstOrDefaultAsync(x => x.Number == newAudience.Number);
            if (audienceWithNumber != null)
            {
                throw new Exception(string.Format(ErrorAudienceExists, newAudience.Number));
            }

            var audience = _mapper.Map<NewAudience, Audience>(newAudience);
            await _audienciesTable.AddAsync(audience);
            await _audienciesTable.CommitAsync();
        }

        public PagedItemsList<AudienceDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            return _pagedItemsListUtility.GetPaged(page, pageSize, orderBy, direction, filter);
        }

        public async Task<ICollection<AudienceDTO>> GetAll()
        {
            var audiencies = await GetQuery().ToListAsync();
            return _mapper.Map<ICollection<Audience>, ICollection<AudienceDTO>>(audiencies);
        }

        private IQueryable<Audience> GetQuery()
        {
            return _audienciesTable
                .Queryable()
                .Include(x => x.Schedule);
        }
    }
}
