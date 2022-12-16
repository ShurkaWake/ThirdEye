namespace ThirdEye.Back.Responses.User
{
    public sealed record UserViewModel(
        string Id,
        string Email,
        bool IsEmailConfirmed,
        string FirstName,
        string LastName,
        string Patronymic);
}
