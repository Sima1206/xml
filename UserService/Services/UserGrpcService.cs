using Grpc.Core;
using Proto2;
using UserService.Model;
using UserService.Core;
using UserService.Repository;

namespace UserService.Services;

public class UserGrpcService: UserGrpc.UserGrpcBase
{
    private readonly IUserService _userRepository;
  public UserGrpcService(IUserService userRepository)
    {
        _userRepository = userRepository;
    }

  public override Task<UserResponse> GetUserInfo(UserRequest request, ServerCallContext context)
    {
        var user = _userRepository.GetUserByID(request.Request.Id);
        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User with ID '{request.Request.Id}' not found."));
        }

        ++user.cancelCount;
        var response = new UserResponse();
        _userRepository.UpdateProfile(user);
        return Task.FromResult(response);
    }

}