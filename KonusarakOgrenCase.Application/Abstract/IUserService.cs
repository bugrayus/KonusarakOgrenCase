using KonusarakOgrenCase.Domain.Entities;
using KonusarakOgrenCase.Domain.RequestModels.User;
using KonusarakOgrenCase.Domain.ResponseModels.User;

namespace KonusarakOgrenCase.Application.Abstract;

public interface IUserService
{
    Task<User?> GetByIdAsync(int id);
    Task<LoginResponse> LoginAsync(LoginRequest request);
}