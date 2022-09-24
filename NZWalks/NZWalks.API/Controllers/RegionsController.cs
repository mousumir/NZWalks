using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        public readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //[HttpGet]
        //public IActionResult GetAllRegions()
        //{
        //    // static data 

        //    //var regions = new List<Region>
        //    //{
        //    //    new Region
        //    //    {
        //    //      Id = Guid.NewGuid(),
        //    //      Name =  "Wellington",
        //    //      Code= "WLG",
        //    //      Area = 227755,
        //    //      Lat = 1.8822,
        //    //      Long= 299.88,
        //    //      Population= 500000, 

        //    //    },
        //    //    new Region
        //    //    {
        //    //      Id = Guid.NewGuid(),
        //    //      Name =  "Auckland",
        //    //      Code= "Auck",
        //    //      Area = 227755,
        //    //      Lat = 1.8822,
        //    //      Long= 299.88,
        //    //      Population= 500000,

        //    //    }
        //    //};
        //    var regions = regionRepository.GetAll();

        //    ///returns DTO regions
        //    //var regionsDTO = new List<Models.DTO.Region>();
        //    //regions.ToList().ForEach(region =>
        //    //{ 
        //    //    var regionDTO = new Models.DTO.Region()
        //    //    { 
        //    //        Id= region.Id,
        //    //        Code = region.Code,
        //    //        Name = region.Name,
        //    //        Area = region.Area,
        //    //        Lat =  region.Lat,
        //    //        Long = region.Long,
        //    //        Population = region.Population,
        //    //    };            
        //    //});

        //    var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
        //    return Ok(regionsDTO);   
        //}

        //For Asynchronous code
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);   
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        { 
            var region = await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);   
        }

        [HttpPost]       
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        { 
            //Request(DTO) to domain Model
            var region = new Models.Domain.Region()
            { 
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,

            };
            //Pass details to Repository

            region = await regionRepository.AddAsync(region);

            //Convert back to DTO
            var regionsDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionsDTO.Id }, regionsDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var region = await regionRepository.DeleteAsync(id);
            //If null NotFound
            if (region == null)
            { 
                return NotFound();  
            }
            //Convert reponse back to DTO
            var regionsDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };

            //Return OK response
            return Ok(regionsDTO);  

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Convert  DTO to Domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population,

            };

            //Update Region using Repository

            region = await regionRepository.UpdateAsync(id, region);

            //If Null then NotFound

            if (region==null)
            {
                return NotFound();
            }
            //Convert back to DTO
            var regionsDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };
            //Return OK Response
            return Ok(region);
        }

    }
}
 