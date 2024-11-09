namespace Business.Abstractions.Authentication;

public interface IJwtProvider
{
    string Generate(Guid id, string email);
}