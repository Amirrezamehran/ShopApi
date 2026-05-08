using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.ProductAggregate;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application.Products.Create
{
    internal class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductDomainService _productDomainService;
        private readonly IFileService _fileService;

        public CreateProductCommandHandler(IProductRepository productRepository, IProductDomainService productDomainService
            , IFileService fileService)
        {
            _productRepository = productRepository;
            _productDomainService = productDomainService;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
            var product = new Product(request.Title, imageName, request.Description, request.CategoryId
                                ,request.SubCategoryId, request.SecondarySubCategoryId, request.Slug, request.SeoData
                                ,_productDomainService);

            await _productRepository.AddAsync(product);

            var specifications = new List<ProductSpecification>();
            request.Specifications.ToList().ForEach(input =>
            {
                specifications.Add(new ProductSpecification(input.Key, input.Value));
            });

            product.SetSpecification(specifications);
            await _productRepository.Save();
            return OperationResult.Success();
        }
    }
}
