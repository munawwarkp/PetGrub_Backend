using AutoMapper;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Repositories.Usr;

namespace PetGrubBakcend.Services.Usr
{
    public class UserService:IUserService
    {
        private readonly IMapper _mapper;
        private readonly  IUserRepository repository;
        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            repository = userRepository;
            _mapper = mapper;
        }

       public async Task<ApiResponse<List<UserListDto>>> GetUsers()
        {
            try
            {
                var users = await repository.ListUsers();
                //_mapper.Map<List<UserListDto>>(users);

                var userDtos = users.Select(user => new UserListDto
                {
                    UserID = user.Id,
                    Name = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    IsBlocked = user.IsBlocked,
                }).ToList();

                return new ApiResponse<List<UserListDto>>
                {
                    StatusCode = 200,
                    Message = "succesfully listed users",
                    Data = userDtos
                };


            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserListDto>>
                {
                    StatusCode = 500,
                    Message = $"error occured while listing users : {ex.Message}",

                };
            }
        }

        public async Task<ApiResponse<UserListDto>> GetUserById(int id)
        {
            try
            {
               var user = await repository.GetUser(id);
                if(user == null)
                {
                    return new ApiResponse<UserListDto>
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }

                var data = new UserListDto
                {
                    UserID = user.Id,
                    Name = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    IsBlocked = user.IsBlocked,
                };

                return new ApiResponse<UserListDto>
                {
                    StatusCode = 200,
                    Data = data
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse<UserListDto>
                {
                    StatusCode = 500,
                    Message = $"error occured while fetching user by id : {ex.Message}"
                };
            }
        }


        public async Task<ApiResponse<object>> BolckOUnblockUserService(int id)
        {
            try
            {
                var user = await repository.BolckOUnblockUser(id);
                if(user == null)
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Message = "user is null"
                    };
                }

                if(user.IsBlocked == false)
                {
                    user.IsBlocked = true;
                }
                else
                {
                    user.IsBlocked = false;
                }

                  await  repository.ImplementBlcokOUnblockUser();

                //var data = _mapper.Map<UserListDto> (user);

                var data = new UserListDto
                {
                    UserID = user.Id,
                    Name = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    IsBlocked = user.IsBlocked,
                };
                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = $"{(data.IsBlocked == false ? "user unblocked" : "user blocked")}",
                    Data = data
                    
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = $"error occured at blockingOrUnblocking section : {ex.Message}"
                };
            }
        }


    }
}
