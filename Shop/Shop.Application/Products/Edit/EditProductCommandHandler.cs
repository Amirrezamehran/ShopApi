using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.ProductAggregate;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.ProductAggregate.Services;


namespace Shop.Application.Products.Edit
{
    public class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductDomainService _productDomainService;
        private readonly IFileService _fileService;

        public EditProductCommandHandler(IProductRepository productRepository, IProductDomainService productDomainService, IFileService fileService)
        {
            _productRepository = productRepository;
            _productDomainService = productDomainService;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetTracking(request.ProductId);
            if (product == null)
            {
                return OperationResult.NotFound();
            }

            product.Edit(request.Title, request.Description, request.CategoryId, request.SubCategoryId
                       ,request.SecondarySubCategoryId, request.Slug, request.SeoData, _productDomainService);

            string oldImage = product.ImageName;

            if (request.ImageFile != null)
            {
                var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
                product.SetProductImage(imageName);
            }

            var specifications = new List<ProductSpecification>();
            request.Specifications.ToList().ForEach(input =>
            {
                specifications.Add(new ProductSpecification(input.Key, input.Value));
            });

            product.SetSpecification(specifications);
            await _productRepository.Save();
            RemoveOldImage(request.ImageFile, oldImage);
            return OperationResult.Success();
        }

        private void RemoveOldImage(IFormFile? imageFile, string oldImageName)
        {
            if (imageFile != null)
            {
                _fileService.DeleteFile(Directories.ProductImages, oldImageName);
            }
        }
    }

}
