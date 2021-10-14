using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppServices.Services;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AirportWazeAPI.Controllers
{
    public class EdgeController : AppBaseController
    {
        IEdgeService eService;
        public EdgeController(IEdgeService eService)
        {
            this.eService = eService;
        }

        //הצגת כל הקשתות מטבלת קשתות
        //api/Graph/getAll
        [HttpGet("getAll")]
        public List<CEdge> GetAll()
        {
            return eService.GetListEdges();
        }


    }
}
