using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalksAPI.CustomActionFiltes;
using NZWalksAPI.IRepository;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using System.Data;

namespace NZWalksAPI.Controllers
{
    // https://localhost:1234/api/
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        public readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //  GET All regions
        // GET https://loclhost:1234/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? FilterOn, string? FilterQuery)
        {
            //Get data from database - Domain models
            var regiondomain = await regionRepository.GetAllAsync(FilterOn, FilterQuery);
            //  Map Domain models to Dto
            //var regiondto = new List<RegionDto>();
            //foreach (var regionDomain in regiondomain)
            //{
            //    regiondto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Name = regionDomain.Name,
            //        Code = regionDomain.Code,
            //        RegionImageUrl = regionDomain.RegionImageUrl,
            //    });
            //}

            //  Map Domain models to Dto
            return Ok(mapper.Map<List<RegionDto>>(regiondomain));

            // Return Dto
            //return Ok(regiondto);
        }


        // Get Single region by id (get region by id)
        //  https://loclhost:1234/api/regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);

            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //  Map Domain models to Dto
            //var regiondto = new List<RegionDto>();

            //regiondto.Add(new RegionDto()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl,
            //});
            // Return back to Dto to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpGet]
        [Route("{code}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Code == code);
            if (region == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(region);
            }
        }


        // POST To Creat New Region
        // POST: https://loclhost:1234/api/regions

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or convert  Dto to Domain Model

            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            //var regionDomainModel = new Region
            //{

            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            //};
            // Use Domain Model to create Region

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map domain model back to dto

            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,

            //};
            //Map domain model back to dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update region
        // PUT : https://localhot:1234/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {

            // Map Dto to Domain model
            //var regionDomainModel = new Region()
            //{
            //   Code = updateRegionDto.Code,
            //   Name = updateRegionDto.Name,
            //   RegionImageUrl = updateRegionDto.RegionImageUrl,
            //};
            var regionDomainModel = mapper.Map<Region>(updateRegionDto);

            // checks if region exists
            var regionDomain = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // convet domain model to dto

            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //};

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // Delete region
        // PUT : https://localhot:1234/api/regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Delete Region

            // return deleted region back
            // map domain model to dto 

            //var regiondto = new RegionDto()
            //{ 
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

        }
    }

}
