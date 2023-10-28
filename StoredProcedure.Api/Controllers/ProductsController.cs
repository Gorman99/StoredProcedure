using System.Net;
using Microsoft.AspNetCore.Mvc;
using StoredProcedure.Api.Models.Entities;
using StoredProcedure.Api.Repositories;

namespace StoredProcedure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController:ControllerBase
{

    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductsController> _logger;
    
    public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var data = await _productRepository.GetProducts();
        return Ok(data);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var data = await _productRepository.GetProductByIdAsync(id);

        var result = data.FirstOrDefault();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Post( [FromBody]Product product)
    {
        try
        {
            var result = await _productRepository.AddProductAsync(product);
            if (result < 1)
            {
                return BadRequest();
            }

            return StatusCode((int)HttpStatusCode.Created, product);
        }
        catch (Exception e)
        {
          _logger.LogError("Error occured ",e);
          return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

        }
    } 
    
    [HttpPut]
    public async Task<ActionResult> Put( [FromBody]Product product)
    {
        try
        {
            var result = await _productRepository.UpdateProductAsync(product);
            if (result < 1)
            {
                return BadRequest();
            }

            return StatusCode((int)HttpStatusCode.Created, product);
        }
        catch (Exception e)
        {
          _logger.LogError("Error occured ",e);
          return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

        }
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var data = await _productRepository.DeleteProductAsync(id);

        return Ok();
    }
    
    
    
}