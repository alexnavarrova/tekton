﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tekton.Application.Features.Product.dtos;
using Tekton.Application.Features.Product.Queries;

namespace Tekton.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get Products.
    /// </summary>
    /// <returns>List of products.</returns>
    [HttpGet("GetProducts")]
    public Task<IEnumerable<ProductDto>> GetProducts() => _mediator.Send(new ProductGetAllQuery());


    /// <summary>
    /// Get Product by id.
    /// </summary>
    /// <param name="productId">The param.</param>
    /// <returns>Product.</returns>
    [HttpGet("GetById/{productId}")]
    public Task<ProductDto> GetByIdAsync(Guid productId) => _mediator.Send(new ProductGetByIdQuery() { ProductId = productId });
}
