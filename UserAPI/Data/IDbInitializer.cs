namespace UserAPI.Data;

public interface IDbInitializer
{
    void Initialize(UserApiContext context);
}
