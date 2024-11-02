namespace Banga.Domain.DTOs
{
    public sealed record SendMessageDto(
        int UserId,
        int ToUserId,
        string Message);
}
