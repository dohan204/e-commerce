using api.Helpers.Dtos;
using application.cases.Commands.Vouchers;
using application.cases.Queries.Vouchers;
using domain.entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;

namespace api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class VoucherController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VoucherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Voucher), StatusCodes.Status200OK, contentType: "application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVoucherById(int id)
        {
            var voucher = await _mediator.Send(new GetVoucherQueryId { Id = id });
            return Ok(new ApiResponse<Voucher>
            {
                Message = "ok",
                Data = voucher
            });
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVouchers()
        {
            var vouchers = await _mediator.Send(new GetVouchersQuery());
            if (!vouchers.Any())
            {
                return Ok(new ApiResponse<IReadOnlyCollection<Voucher>>
                {
                    Message = "Chưa có dữ liệu",
                    Data  = vouchers
                });
            }

            return Ok(new ApiResponse<IReadOnlyCollection<Voucher>>
            {
                Message = "Lấy dữ liệu thành công",
                Data = vouchers
            });
        }
        [HttpPost]
        [ProducesResponseType(typeof(Voucher),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateVoucherCommand command)
        {
            var newVoucher = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetVoucherById), new { id = newVoucher }, command);
        }
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCount()
        {
            var count = await _mediator.Send(new GetVoucherCountActive {});
            return Ok(new ApiResponse<int>
            {
                Message = "lấy dữ liệu thành công",
                Data = count
            });
        }
    }
}