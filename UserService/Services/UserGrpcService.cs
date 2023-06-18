using Grpc.Core;
using Proto2;
using UserService.Model;
using UserService.Core;
using UserService.Repository;

namespace UserService.Services;

public class UserGrpcService: UserGrpc.UserGrpcBase
{
    private readonly IUserRepository _userRepository;
    public UserGrpcService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserGrpcService()
    {
        _userRepository = new UserRepository(new ApplicationContext());
    }

    public override Task<UserResponse> GetUserInfo(UserRequest request, ServerCallContext context)
    {
        var user = _userRepository.Get(request.Id);
        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User with ID '{request.Id}' not found."));
        }

        var response = new UserResponse()
        {
            Surname = user.Surname,
            Email = user.Email,
            Password = user.Password,
            CityId = user.CityId,
            Enabled = user.Enabled,
            CancelCount = (int)user.cancelCount,
            Name = user.Name,
        };
        return Task.FromResult(response);
    }

}