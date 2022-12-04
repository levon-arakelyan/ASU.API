using ASU.Core.Database;
using ASU.Core.Database.Entities;
using ASU.Core.DTO;
using ASU.Core.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASU.Services
{
    public class DepartmentHeadsService : IDepartmentHeadsService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseTable<DepartmentHead> _departmentHeadsTable;

        private const string ErrorDepartmentHeadNotExist = "{0} ամբիոնը չունի վարիչ";
        private const string ErrorDepartmentHasHead = "{0} ամբիոնը արդեն ունի վարիչ.";
        private const string ErrorNoDepartmentWithProvidedHead = "{0} ամբիոնի վարիչը {0}-ը չէ";

        public DepartmentHeadsService(IDatabaseTable<DepartmentHead> departmentHeadsTable, IMapper mapper)
        {
            _departmentHeadsTable = departmentHeadsTable;
            _mapper = mapper;
        }

        public async Task TryEdit(int departmentId, int teacherId, bool add)
        {
            var departmentHead = GetQuery().FirstOrDefault(x => x.DepartmentId == departmentId);

            if (departmentHead == null && add)
            {
                await Add(departmentId, teacherId);
            }
            else if (departmentHead != null && !add)
            {
                await Remove(departmentId, teacherId);
            }
        }

        private async Task Add(int departmentId, int teacherId)
        {
            var head = new DepartmentHead()
            {
                DepartmentId = departmentId,
                TeacherId = teacherId
            };
            await _departmentHeadsTable.AddAsync(head);
            await _departmentHeadsTable.CommitAsync();
        }

        private async Task Remove(int departmentId, int teacherId)
        {
            var facultyHead = GetQuery().FirstOrDefault(x => x.DepartmentId == departmentId && x.TeacherId == teacherId);
            if (facultyHead != null)
            {
                await _departmentHeadsTable.DeleteAsync(facultyHead);
            }
        }

        private IQueryable<DepartmentHead> GetQuery()
        {
            return _departmentHeadsTable
                .Queryable()
                .Include(x => x.Department)
                .Include(x => x.Head);
        }
    }
}
