﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRep;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;


        public ProductsController(IGenericRepository<Product> productRepository1, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _productRepo = productRepository1;
            _productBrandRep = productBrandRepository;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet(Name = "AllProducts")]
        //[Route("All")]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepo.ListAsync(spec);

            var productDto = _mapper.Map<List<ProductToReturnDto>>(products);

            return Ok(productDto);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetSingleProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productRepo.GetEntityWithSpec(spec);

            var productDto = _mapper.Map<ProductToReturnDto>(product);

            return Ok(productDto);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrand = await _productRepo.ListAllAsync();

            return Ok(productBrand);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productType = await _productTypeRepo.ListAllAsync();

            return Ok(productType);
        }

    }
}
