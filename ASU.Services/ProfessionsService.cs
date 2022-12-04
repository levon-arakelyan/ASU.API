using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using ASU.Services.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASU.Services
{
    public class ProfessionsService : IProfessionsService
    {
        private readonly IDatabaseTable<Profession> _professionsTable;
        private readonly IMapper _mapper;
        private readonly PagedItemsListUtility<Profession, ProfessionDTO> _pagedItemsListUtility;

        public ProfessionsService(IDatabaseTable<Profession> professionsTable, IMapper mapper)
        {
            _professionsTable = professionsTable;
            _mapper = mapper;
            _pagedItemsListUtility = new PagedItemsListUtility<Profession, ProfessionDTO>(
               _mapper,
               GetQuery(),
               new string[] { "Name", "Department.Name" },
               new string[] { "Name", "Department.Name" }
           );
        }

        public async Task Add(NewProfession newProfession)
        {
            if (newProfession == null)
            {
                throw new ArgumentNullException(nameof(newProfession));
            }

            var profession = _mapper.Map<NewProfession, Profession>(newProfession);
            await _professionsTable.AddAsync(profession);
            await _professionsTable.CommitAsync();
        }

        public async Task<ICollection<ProfessionDTO>> GetAll()
        {
            var professions = await GetQuery().ToListAsync();
            return _mapper.Map<ICollection<Profession>, ICollection<ProfessionDTO>>(professions);
        }

        public PagedItemsList<ProfessionDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            return _pagedItemsListUtility.GetPaged(page, pageSize, orderBy, direction, filter);
        }

        private IQueryable<Profession> GetQuery()
        {
            return _professionsTable
                .Queryable()
                .Include(x => x.Department)
                .Include(x => x.Courses);
        }
    }
}
