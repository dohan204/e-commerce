using MediatR;

namespace application.cases.Commands.Product
{
    public class UpdateProductImageCommand : IRequest<string>
    {
        public int ProductId {get; set;}
        public Stream ImageUrl {get; set;} = null;
        public string FileName {get; set;} = string.Empty;
    }
}