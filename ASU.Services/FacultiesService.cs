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
    public class FacultiesService : IFacultiesService
    {
        private readonly IDatabaseTable<Faculty> _facultiesTable;
        private readonly IMapper _mapper;
        private readonly PagedItemsListUtility<Faculty, FacultyDTO> _pagedItemsListUtility;

        public FacultiesService(IDatabaseTable<Faculty> facultiesTable, IMapper mapper)
        {
            _facultiesTable = facultiesTable;
            _mapper = mapper;
            _pagedItemsListUtility = new PagedItemsListUtility<Faculty, FacultyDTO>(
               _mapper,
               GetQuery(),
               new string[] { "Name", "FacultyHead.Head.FullName" },
               new string[] { "Name", "FacultyHead.Head.FullName" }
           );
        }

        public async Task Add(NewFaculty newFaculty)
        {
            if (newFaculty == null)
            {
                throw new ArgumentNullException(nameof(newFaculty));
            }

            var faculty = _mapper.Map<NewFaculty, Faculty>(newFaculty);
            await _facultiesTable.AddAsync(faculty);
            await _facultiesTable.CommitAsync();
        }

        public PagedItemsList<FacultyDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            return _pagedItemsListUtility.GetPaged(page, pageSize, orderBy, direction, filter);
        }

        public async Task<ICollection<FacultyDTO>> GetAll()
        {
            var faculties = await GetQuery().ToListAsync();
            return _mapper.Map<ICollection<Faculty>, ICollection<FacultyDTO>>(faculties);
        }

        private IQueryable<Faculty> GetQuery()
        {
            return _facultiesTable
                .Queryable()
                .Include(x => x.Departments)
                .Include(x => x.FacultyHead)
                .ThenInclude(x => x.Head)
                .AsNoTracking();
        }
    }
}
