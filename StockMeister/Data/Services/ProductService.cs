﻿using Microsoft.AspNetCore.Identity;
using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;
using StockMeister.Models.ViewModels;
using System.Security.Claims;

namespace StockMeister.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string loginUserId;

        public ProductService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public async Task<int> AddUpdate(ProductVM model)
        {
            if (model != null && model.Id > 0)
            {
                // Update
                var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == model.Id, "Category");

                product.ProductName = model.ProductName;
                product.ProductPrice = model.ProductPrice;
                product.CategoryId = model.CategoryId;

                _unitOfWork.Product.Update(product);
                await _unitOfWork.SaveAsync();
                return 1;
            }
            else
            {
                // Create
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                var category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == model.CategoryId);
                var company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == user.CompanyId);
                Product product = new Product()
                {
                    ProductName = model.ProductName,
                    ProductPrice = model.ProductPrice,
                    CategoryId = category.Id,
                    CompanyId = company.Id,
                };

                _unitOfWork.Product.Add(product);
                await _unitOfWork.SaveAsync();
                return 2;
            };
        }
        public async Task<int> DeleteProduct(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            _unitOfWork.Product.Remove(product);
            await _unitOfWork.SaveAsync();

            return 1;
        }
        public Product GetById(int id)
        {
            return _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id,"Category");
        }
    }
}
