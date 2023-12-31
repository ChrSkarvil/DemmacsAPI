﻿using AutoMapper;
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
                    UserType = loginEntity.UserType,
                    CustomerId = loginEntity.CustomerId,
                    EmployeeId = loginEntity.EmployeeId
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

        public async Task<LoginModel> GetLoginAsync(string email)
        {
            var loginEntity = await _context.Logins
                .Include(logins => logins.Customer)
                .Include(logins => logins.Employee)
                .Include(employee => employee.Employee.Role)
                .FirstOrDefaultAsync(login => login.Email == email);

            if (loginEntity == null)
            {
                return null;
            }

            var loginModel = new LoginModel
            {
                Email = loginEntity.Email,
                Password = loginEntity.Password,
                UserType = loginEntity.UserType,
                CustomerId = loginEntity.CustomerId,
                EmployeeId = loginEntity.EmployeeId
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

        public async Task<Login> FindLogin(string email)
        {
            IQueryable<Login> query = _context.Logins;
            // Query It
            query = query
                .Where(l => l.Email == email);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
