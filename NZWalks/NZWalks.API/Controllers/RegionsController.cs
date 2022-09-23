using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

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
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);   
        }
    }
}
