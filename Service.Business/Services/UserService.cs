using AutoMapper;
using Framework.Core.comman;
using Framework.Core.IUnit;
using Microsoft.Extensions.Logging;
using Service.Core.IRepo;
using Service.Core.IServices;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.User;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILogger<UserService> logger;

        public UserService(IUserRepository userRepository,
            IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        private async Task<User> UserValidation(int id)
        {
            var query = await userRepository.GetById(id);

            if (query is null)
            {
                logger.LogWarning("No exist User with #id: {id}", id);
                throw new ItemNotFoundException("Item not found");
            }

            return query;
        }

        public async Task<IList<UserVM>> GetAll()
        {
            var query = await userRepository.GetAll();

            if (query is null || query.Count == 0)
            {
                logger.LogWarning("No exist any Users");
                throw new ItemNotFoundException("Items not found");
            }

            var map = mapper.Map<IList<UserVM>>(query);

            return map;
        }

        public async Task<IList<UserVM>> Paginate(int pageIndex, int pageSize)
        {
            var page = await userRepository.Pagination(pageIndex, pageSize);

            if (page is null || page.Count == 0)
            {
                logger.LogWarning("No data to display");
                throw new ItemNotFoundException("Items not found");
            }

            var map = mapper.Map<IList<UserVM>>(page);

            return map;
        }

        public async Task<UserVM> GetById(int id)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", id);

            var query = await UserValidation(id);

            var map = mapper.Map<UserVM>(query);

            return map;
        }

        public async Task<int> Add(RegisterUserVM user)
        {
            if (user is null)
            {
                logger.LogWarning("Data can not be null");
                throw new BadRequestException("Bad data sent");
            }

            var map = mapper.Map<User>(user);

            await userRepository.AddEntity(map);
            await unitOfWork.SaveChanges();

            return map.Id;
        }

        public async Task Update(UpdateUserVM user)
        {
            logger.LogWarning("Now, you try accessing with #Id: {id}", user.Id);

            var query = await UserValidation(user.Id);

            mapper.Map(user, query);

            await unitOfWork.SaveChanges();
        }

        public async Task Delete(int usrId)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", usrId);

            var query = await UserValidation(usrId);

            await userRepository.DeleteEntity(query);
            await unitOfWork.SaveChanges();
        }
        
        public async Task<UserVM> AuthenticateUser(string email,string password)
        {
            var query= await userRepository.FirstOrDefault(x=> x.Email == email && x.Password == password);

            if(query == null)
            {
                logger.LogWarning("User is UnAuthenticated");
                throw new UnAuthorizedException("User is UnAuthenticated");
            }

            var map= mapper.Map<UserVM>(query);

            return map;
        }
    }
}
