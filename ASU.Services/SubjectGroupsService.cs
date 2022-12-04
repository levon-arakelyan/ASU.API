using ASU.Core.Database.Entities;
using ASU.Core.Database;
using ASU.Core.Services;
using AutoMapper;
using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Services.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ASU.Services
{
    public class SubjectGroupsService : ISubjectGroupsService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseTable<SubjectGroup> _subjectGroupsTable;
        private readonly PagedItemsListUtility<SubjectGroup, SubjectGroupDTO> _pagedItemsListUtility;

        public SubjectGroupsService(IDatabaseTable<SubjectGroup> subjectGroupsTable, IMapper mapper)
        {
            _subjectGroupsTable = subjectGroupsTable;
            _mapper = mapper;
            _pagedItemsListUtility = new PagedItemsListUtility<SubjectGroup, SubjectGroupDTO>(
                _mapper,
                GetQuery(),
                new string[] { "Name", "SubjectGroup.Name" },
                new string[] { "Name", "SubjectGroup.Name" }
            );
        }

        public async Task Add(NewSubjectGroup subjectGroupDto)
        {
            if (subjectGroupDto == null)
            {
                throw new ArgumentNullException(nameof(subjectGroupDto));
            }

            var subjectGroup = _mapper.Map<NewSubjectGroup, SubjectGroup>(subjectGroupDto);
            await _subjectGroupsTable.AddAsync(subjectGroup);
            await _subjectGroupsTable.CommitAsync();
        }

        public PagedItemsList<SubjectGroupDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            return _pagedItemsListUtility.GetPaged(page, pageSize, orderBy, direction, filter);
        }

        public async Task<ICollection<SubjectGroupDTO>> GetAll()
        {
            var subjectGroups = await GetQuery().ToListAsync();
            return _mapper.Map<ICollection<SubjectGroup>, ICollection<SubjectGroupDTO>>(subjectGroups);
        }

        private IQueryable<SubjectGroup> GetQuery()
        {
            return _subjectGroupsTable
                .Queryable()
                .Include(x => x.Subjects);
        }
    }
}
