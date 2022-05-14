using KonusarakOgrenCase.Application.Abstract;
using KonusarakOgrenCase.Application.Common;
using KonusarakOgrenCase.Domain.Entities;
using KonusarakOgrenCase.Domain.RequestModels.User;
using KonusarakOgrenCase.Domain.ResponseModels.User;
using KonusarakOgrenCase.Persistence.Abstract;

namespace KonusarakOgrenCase.Application.Concrete;

public class UserService : IUserService
{
    private readonly Token _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, Token tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    #region GetByIdAsync

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    #endregion

    #region LoginAsync

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByMailAsync(request.Mail);
        if (user == null)
            throw new ApiException(new Error
            {
                Message = $"No user found by mail {request.Mail}"
            });

        var hashedPassword = PasswordHasher.HashPassword(request.Password, user.Salt).Item2;
        var token = _tokenGenerator.GenerateToken(user);
        if (hashedPassword == user.HashedPassword)
            return new LoginResponse
            {
                Id = user.Id,
                Token = token,
                Email = user.Mail
            };

        throw new ApiException(new Error
        {
            Message = $"Wrong password {request.Mail}"
        });
    }

    #endregion
}