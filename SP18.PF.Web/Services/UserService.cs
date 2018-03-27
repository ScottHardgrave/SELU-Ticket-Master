using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Shared;
using SP18.PF.Core.Features.Users;
using SP18.PF.Web.Areas.Api.Models.Users;
using SP18.PF.Web.Helpers;
using SP18.PF.Web.Models.Users;

namespace SP18.PF.Web.Services
{
    public class UserService
    {
        private readonly DbContext dbContext;
        private readonly IMapper mapper;
        private readonly IValidator<IRegisterUser> userValidator;
        private readonly IValidator<Address> addressValidator;

        public UserService(
            DbContext dbContext,
            IMapper mapper,
            IValidator<IRegisterUser> userValidator,
            IValidator<Address> addressValidator)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userValidator = userValidator;
            this.addressValidator = addressValidator;
        }

        public async Task<ServiceResponse<UserDto>> Login(UserLoginDto loginDto)
        {
            var user = await dbContext.Set<User>().SingleOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user == null || !CryptoHelpers.VerifyPassword(loginDto.Password, user.Password))
            {
                return new ServiceResponse<UserDto>
                {
                    Errors = { { "email", new[] { "invalid username or password" } } }
                };
            }
            return await GetUserByEmail(loginDto.Email);
        }

        public async Task<ServiceResponse<UserDto>> Register(UserRegisterDto registerDto)
        {
            var validationResult = await userValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<UserDto>(validationResult);
            }
            var newUser = new User
            {
                BillingAddress = registerDto.BillingAddress,
                Email = registerDto.Email,
                Password = CryptoHelpers.HashPassword(registerDto.Password),
                Role = Roles.Customer
            };
            dbContext.Add(newUser);
            await dbContext.SaveChangesAsync();
            var userDto = await GetUserByEmail(newUser.Email);
            return userDto;
        }

        public async Task<ServiceResponse<UserDto>> UpdateAddress(ClaimsPrincipal user, Address address)
        {
            var validationResult = await addressValidator.ValidateAsync(address);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<UserDto>(validationResult);
            }

            var userEmail = user?.Identity?.Name;
            var userEntitity = await dbContext.Set<User>()
                .SingleOrDefaultAsync(x => x.Email == userEmail);
            if (userEntitity == null)
            {
                return new ServiceResponse<UserDto>
                {
                    Errors = { { "user", new[] { "No such user" } } }
                };
            }
            mapper.Map(address, userEntitity.BillingAddress);
            await dbContext.SaveChangesAsync();
            var userDto = await GetUserByEmail(userEntitity.Email);
            return userDto;
        }

        private async Task<ServiceResponse<UserDto>> GetUserByEmail(string email)
        {
            var result = await dbContext.Set<User>()
                .Where(x => x.Email == email)
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
            return new ServiceResponse<UserDto>
            {
                Data = result
            };
        }

        public Task<UserDtoProfile[]> GetUser(ClaimsPrincipal user, Expression<Func<User, bool>> filter = null)
        {
            filter = filter ?? (x => true);
            var userEmail = user?.Identity?.Name;
            var users = dbContext.Set<User>()
                .Where(x => x.Email == userEmail)
                .Where(filter)
                .ProjectTo<UserDtoProfile>(mapper.ConfigurationProvider)
                .ToArrayAsync();
            return users;
        }
    }
}