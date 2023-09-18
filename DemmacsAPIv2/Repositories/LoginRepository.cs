using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;

namespace DemmacsAPIv2.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public LoginRepository(DemmacsdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<LoginModel>> GetAllLoginsAsync()
        {
            var logins = await _context.Logins
                .Include(logins => logins.Customer)
                .Include(logins => logins.Employee)
                .Include(employee => employee.Employee.Role)
                .ToListAsync();

            var loginModels = logins.Select((loginEntity) =>
            {
                var loginModel = new LoginModel
                {
                    Email = loginEntity.Email,
                    Password = loginEntity.Password,
                    UserType = loginEntity.UserType
                };

                if (loginEntity.UserType == 1)
                {
                    loginModel.FullName = loginEntity.Employee?.EmployeeFname + " " + loginEntity.Employee?.EmployeeSname;
                    loginModel.Role = loginEntity.Employee?.Role.RoleName;
                }
                else if (loginEntity.UserType == 0)
                {
                    loginModel.FullName = loginEntity.Customer?.CustomerFname + " " + loginEntity.Customer?.CustomerSname;
                    loginModel.Role = "Customer";
                }

                return loginModel;
            });

            return loginModels;
        }

        public async Task<LoginModel> GetLoginAsync(int id)
        {
            var loginEntity = await _context.Logins
                .Include(logins => logins.Customer)
                .Include(logins => logins.Employee)
                .Include(employee => employee.Employee.Role)
                .FirstOrDefaultAsync(login => login.LoginId == id);

            if (loginEntity == null)
            {
                return null; // Handle the case where the login is not found
            }

            var loginModel = new LoginModel
            {
                Email = loginEntity.Email,
                Password = loginEntity.Password,
                UserType = loginEntity.UserType,
            };
            if (loginEntity.UserType == 1)
            {
                loginModel.FullName = loginEntity.Employee?.EmployeeFname + " " + loginEntity.Employee?.EmployeeSname;
                loginModel.Role = loginEntity.Employee?.Role.RoleName;
            }
            else if (loginEntity.UserType == 0)
            {
                loginModel.FullName = loginEntity.Customer?.CustomerFname + " " + loginEntity.Customer?.CustomerSname;
                loginModel.Role = "Customer";
            }

            return loginModel;
        }

        public void Update(int id, LoginModelCreate login)
        {
            var query = _context.Logins.Find(id);
            if (query !=null)
            {
                query.Email = login.Email;
                query.Password = login.Password;
                query.UserType = login.UserType;
                query.CustomerId = login.CustomerId;
                query.EmployeeId = login.EmployeeId;
            }

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
