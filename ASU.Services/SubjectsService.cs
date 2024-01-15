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
    public class SubjectsService : ISubjectsService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseTable<Subject> _subjectsTable;
        private readonly PagedItemsListUtility<Subject, SubjectDTO> _pagedItemsListUtility;

        public SubjectsService(IDatabaseTable<Subject> subjectsTable, IMapper mapper)
        {
            _subjectsTable = subjectsTable;
            _mapper = mapper;
            _pagedItemsListUtility = new PagedItemsListUtility<Subject, SubjectDTO>(
                _mapper,
                GetQuery(),
                new string[] { "Name", "SubjectGroup.Name" },
                new string[] { "Name", "SubjectGroup.Name" }
            );
        }

        public async Task Add(NewSubject subjectDto)
        {
            if (subjectDto == null)
            {
                throw new ArgumentNullException(nameof(subjectDto));
            }

            var subject = _mapper.Map<NewSubject, Subject>(subjectDto);
            await _subjectsTable.AddAsync(subject);
            await _subjectsTable.CommitAsync();
        }

        public PagedItemsList<SubjectDTO> GetPaged(int page, int pageSize, string orderBy = "id", OrderDirection direction = OrderDirection.Descending, string? filter = "")
        {
            return _pagedItemsListUtility.GetPaged(page, pageSize, orderBy, direction, filter);
        }

        public async Task<SubjectDTO> Get(int subjectId)
        {
            var subject = await GetQuery().FirstOrDefaultAsync(x => x.Id == subjectId);
            if (subject == null)
            {
                return null;
            }
            return _mapper.Map<Subject, SubjectDTO>(subject);
        }

        public async Task<ICollection<SubjectDTO>> GetAll()
        {
            var subjects = await GetQuery().ToListAsync();
            return _mapper.Map<ICollection<Subject>, ICollection<SubjectDTO>>(subjects);
        }

        private IQueryable<Subject> GetQuery()
        {
            return _subjectsTable
                .Queryable();
        }
    }
}
