using MyMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Core.Services
{
	public interface IMusicService
	{
		Task<IEnumerable<Music>> GetAllWithArtist();
		Task<Music> GetMusicById(int id);
		Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId);
		Task<Music> CreateMusic(Music newMusic);
		Task UpdateMusic(Music musicToBeupdated, Music music);
		Task DeleteMusic(Music music);
	}
}
