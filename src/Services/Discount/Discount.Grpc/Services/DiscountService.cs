using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountDbContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.AsNoTracking().FirstOrDefaultAsync(f => f.ProductName == request.ProductName);

        if(coupon == null) coupon = new Models.Coupon { Amount =0, ProductName="None", Description = "None"};

        logger.LogInformation("Discount is retrieved for \"ProductName: \" {productName}, \"Amount: \" {amount} ", coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Adapt<Coupon>();
        if (coupon is { ProductName : null }) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request."));
        await dbContext.Coupons.AddAsync(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is Created Successfully for \"ProductName: \" {productName}, \"Amount: \" {amount} ", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {

        var coupon = request.Adapt<Coupon>();
        if (coupon is { ProductName: null }) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request."));
        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is Updated Successfully for \"ProductName: \" {productName}, \"Amount: \" {amount} ", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {

        var coupon = await dbContext.Coupons.AsNoTracking().FirstOrDefaultAsync(f => f.ProductName == request.ProductName);

        if (coupon is null) throw new RpcException(new Status(StatusCode.NotFound, "Not Found."));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount Deleted Successfully for \"ProductName: \" {productName}, \"Amount: \" {amount} ", coupon.ProductName, coupon.Amount);

        return new DeleteDiscountResponse { Success = true };
    }
}
