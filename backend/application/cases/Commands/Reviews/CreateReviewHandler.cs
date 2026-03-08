using MediatR;
using application.interfaces;
using Microsoft.AspNetCore.Mvc;
using application.exceptions;
using FluentValidation;
using domain.entities;
using Serilog;
namespace application.cases.Commands.Reviews
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Unit>
    {
        private readonly IValidator<CreateReviewCommand> _validator;
        private readonly ICurrentUser _currentUser;
        private readonly IProductRepository _productRepository;
        private readonly IReviewRepository _reviewRepository;
        public CreateReviewHandler(ICurrentUser currentUser, 
                                    IProductRepository productRepository,
                                    IReviewRepository reviewRepository,
                                    IValidator<CreateReviewCommand> validator)
        {
            _currentUser = currentUser;
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateReviewCommand command, CancellationToken token)
        {   
            Log.Information("Bắt đầu Tìm kiếm người dùng");
            if(!Guid.TryParse(_currentUser.UserId, out var userId))
            {
                Log.Warning($"Người dùng không hợp lệ: {userId}");
                throw new UnauthorizeException("Người dùng không hợp lệ");
            }
            Log.Information($"tìm thấy người dùng với id: {userId}");
            Log.Information("Lấy ra sản phẩm để tạo đánh giá");
            var product = await _productRepository.GetProductById(command.ProductEntityId);
            if(product is null)
            {
                Log.Warning("Không tìm thấy sản phẩm cần đánh giá");
                throw new NotFoundException($"Sản phẩm với Mã: {command.ProductEntityId} Không được tìm thấy ");
            }
            Log.Information($"tìm thấy sản phẩm với id: {product.Id}");
            var validationReview = await _validator.ValidateAsync(command);
            if(!validationReview.IsValid) 
                throw new BadRequestException(
                    string.Join(",", validationReview.Errors)
                );
            Log.Information("Đầu vào hợp lệ, tiến hành tạo đánh giá");
            var review = Review.Create(userId, product.Id, command.Ratings , command.Comments);
            await _reviewRepository.CreateAsync(review);
            return Unit.Value;
        }
    }

    public class ErrorView
    {
        public int StatusCode {get; set;}
        public string Message {get; set;} = string.Empty; 
    }
}