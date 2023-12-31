﻿using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProductModel[]>> GetProducts()
        {
            try
            {
                var allProducts = await _repository.GetAllProductsAsync();
                return _mapper.Map<ProductModel[]>(allProducts);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchCart(int? productId, int? categoryId)
        {
            //Search by productId
            if (productId.HasValue)
            {
                var product = await _repository.GetProductAsync(productId.Value);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                var productModel = _mapper.Map<ProductModel>(product);
                return Ok(productModel);
            }
            //Search by categoryId
            else if (categoryId.HasValue)
            {
                var products = await _repository.GetProductsByCategoryAsync(categoryId.Value);
                if (!products.Any())
                {
                    return NotFound("No products found for the category.");
                }

                var productModels = _mapper.Map<IEnumerable<ProductModel>>(products);
                return Ok(productModels);
            }
            else
            {
                return BadRequest("Please enter either ProductId or CategoryId.");
            }
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductModelCreate>> PostProduct([FromForm] ProductModelCreate model, [FromForm] IFormFile? ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await ImageFile.CopyToAsync(stream);
                        model.Image = stream.ToArray(); // Convert the image to byte[]
                    }
                }

                var product = _mapper.Map<Product>(model);

                _repository.Add(product);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/Product/{product.ProductId}", _mapper.Map<ProductModelCreate>(product));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductModelCreate>> PutProduct(int id, [FromForm] ProductModelCreate model, [FromForm] IFormFile? ImageFile)
        {
            try
            {
                var oldProduct = await _repository.GetProductAsync(id);

                // Check if the product exists
                if (oldProduct == null)
                {
                    return NotFound($"Could not find product {id}");
                }

                // If an image file is provided, update the image and filename
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await ImageFile.CopyToAsync(stream);
                        oldProduct.Image = stream.ToArray(); // Update the image with the new content
                    }
                }

                _mapper.Map(model, oldProduct);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<ProductModelCreate>(oldProduct);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var oldProduct = await _repository.GetProductAsync(id);
                if (oldProduct == null) return NotFound();

                _repository.Delete(oldProduct);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted Product {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The Product");
        }
    }
}
