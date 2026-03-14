using application.interfaces;

namespace Integration.Testing.Mocks
{
    public class MockEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message, bool ishtml) 
        => Task.CompletedTask; // Không làm gì cả, chỉ trả về Task đã hoàn thành
}

}