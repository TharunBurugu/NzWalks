using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFiltes;
using NZWalksAPI.IRepository;
using NZWalksAPI.Mapping;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{   // api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        public IMapper _mapper;
        private IWalkRepository WalkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            WalkRepository = walkRepository;
        }



        // Create Walk
        //POST : /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {  
                // Map dto to domainModel --- 

                var walksDomainModel = _mapper.Map<Walks>(addWalkRequestDto);

                await WalkRepository.CreateAsync(walksDomainModel);

            // Map DomainModel to Dto
            return Ok(_mapper.Map<WalkDto>(walksDomainModel));
        }

        // Get Walks
        // GET : /api/walks?filterOn=name$FilterQuery=Track$SortBy=Name&IsAscending=true&PageNumber=1&Pagesize=5
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery ,
            [FromQuery]string? sortBy , [FromQuery] bool? isAscending , [FromQuery] int pageNumber = 1 , int pageSize = 1000)
        {
            var walksdomainmodel = await WalkRepository.GetAllAsync( filterOn, filterQuery, sortBy,isAscending ?? true , pageNumber,pageSize);

            // we didn't pass domain model back

            // map dto to domainModel

           return Ok(_mapper.Map<List<WalkDto>>(walksdomainmodel));
        }

        // Get walk by id
        // GET :/api/walks/id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
        var walkdomainmodel = await WalkRepository.GetByIdAsync(id);
            if (walkdomainmodel == null)
            {
                return NotFound();
            }
            // Map doamin model to DTO
            return Ok(_mapper.Map<WalkDto>(walkdomainmodel));
        }

        // Update Walk By Id
        // PUT :/api/walks/{id}
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateWalkDto updateWalkDto)
        {

           
                // Map Dto to DomainModel
                var walkdomainModel = _mapper.Map<Walks>(updateWalkDto);

                var walkDomain = await WalkRepository.UpdateAsync(id, walkdomainModel);
                if (walkDomain == null)
                {
                    return NotFound();
                }
                //map DomainModel to Dto 
                return Ok(_mapper.Map<WalkDto>(walkDomain));
            
          
        }
        // Update Walk By Id
        // DELETE :/api/walks/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteByID([FromRoute] Guid Id)
        {
          // map DomanModel to Dto

            var deletedWalkDomainModel =     await WalkRepository.DeleteAsync(Id);

            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            // Dto To DomainModel

             return Ok(_mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
}
