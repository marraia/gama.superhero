using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application.AppHero.Input;
using SuperHero.Application.AppHero.Interfaces;

namespace SuperHero.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : BaseController
    {
        private readonly IHeroAppService _heroAppService;

        public HeroController(INotificationHandler<DomainNotification> notification,
                                IHeroAppService heroAppService)
            : base (notification)
        {
            _heroAppService = heroAppService;
        }

        [Authorize(Roles = "Produtor")]
        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] HeroInput input)
        {
            var item = await _heroAppService
                                .InsertAsync(input)
                                .ConfigureAwait(false);

            return CreatedContent("", item);
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            var item = _heroAppService.Get();
            return Ok(item);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] int id)
        { 
            var item = await  _heroAppService
                                 .GetByIdAsync(id)
                                 .ConfigureAwait(false);
            return Ok(item);
        }
    }
}
