using ASU.Core.DTO;
using ASU.Core.Enums;
using ASU.Core.Models;
using ASU.Core.Services;
using ASU.Core.Services.Utilities;
using AutoMapper;

namespace ASU.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly ITeachersService _teachersService;
        private readonly IStudentsService _studentsService;
        private readonly IMapper _mapper;
        private readonly IAuthUtility _authUtility;

        private const string ErrorNoAccountFound = "Email or password are invalid.";
        private const string ErrorNoUserIdRole = "User id or/and role are not provided.";
        private const string ErrorStudentNotFound = "Student with id={0} not found.";
        private const string ErrorTeacherNotFound = "Teacher with id={0} not found.";

        public AccountsService(
            ITeachersService teachersService,
            IStudentsService studentsService,
            IAuthUtility authUtility,
            IMapper mapper
        )
        {
            _teachersService = teachersService;
            _studentsService = studentsService;
            _authUtility = authUtility;
            _mapper = mapper;
        }
        public async Task<TokenModel> Login(LoginModel loginModel)
        {
            var teacher = await _teachersService.Get(null, loginModel.Email, includePassword: true);
            if (teacher != null && BCrypt.Net.BCrypt.Verify(loginModel.Password, teacher.Password))
            {
                return _authUtility.GenerateToken(teacher.Id, teacher.Email, UserRole.Teacher);
            }

            var student = await _studentsService.Get(null, loginModel.Email, includePassword: true);
            if (student != null && BCrypt.Net.BCrypt.Verify(loginModel.Password, student.Password))
            {
                return _authUtility.GenerateToken(student.Id, student.Email, UserRole.Student);
            }

            throw new Exception(ErrorNoAccountFound);
        }

        public async Task<AuthenticatedUser> Get(int? userId, UserRole? role)
        {
            if (userId == null || role == null)
            {
                throw new ArgumentNullException(ErrorNoUserIdRole);
            }

            if (role == UserRole.Student)
            {
                var student = await _studentsService.Get(userId);
                if (student == null)
                {
                    throw new Exception(string.Format(ErrorStudentNotFound, userId));
                }
                return _mapper.Map<StudentDTO, AuthenticatedUser>(student);
            }
            else
            {
                var teacher = await _teachersService.Get(userId);
                if (teacher == null)
                {
                    throw new Exception(string.Format(ErrorTeacherNotFound, userId));
                }
                return _mapper.Map<TeacherDTO, AuthenticatedUser>(teacher);
            }
        }
    }
}
