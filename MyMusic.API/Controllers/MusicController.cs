using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusic.API.Resources;
using MyMusic.API.Validations;
using MyMusic.Core.Models;
using MyMusic.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusic.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MusicController : ControllerBase
	{
		private readonly IMusicService _musicService;
		private readonly IMapper _mapper;

		public MusicController(IMusicService musicService, IMapper mapper)
		{
			_musicService = musicService;
			_mapper = mapper;
		}

		[HttpGet("")]
		public async Task<ActionResult<IEnumerable<MusicResource>>> GetAllMusics()
		{
			var musics = await _musicService.GetAllWithArtist();
			var musicsResource = _mapper.Map<IEnumerable<Music>, IEnumerable<MusicResource>>(musics);
			return Ok(musicsResource);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<MusicResource>> GetMusicById(int id)
		{
			var music = await _musicService.GetMusicById(id);

			if (music == null)
			{
				return NotFound();
			}

			var musicResource = _mapper.Map<Music, MusicResource>(music);
			return Ok(musicResource);
		}

		[HttpPost("")]
		public async Task<ActionResult<MusicResource>> CreateMusic(SaveResourceMusic musicSaveResource)
		{
			var validatorMusic = new SaveMusicResourceValidatior();
			var validationResult = await validatorMusic.ValidateAsync(musicSaveResource);

			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}
			var music = _mapper.Map<SaveResourceMusic, Music>(musicSaveResource);
			var newMusic = await _musicService.CreateMusic(music);
			return Ok(newMusic);
		}
	}
}
