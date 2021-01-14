using MyMusic.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace MyMusic.Core
{
	public interface IUnitOfWork : IDisposable
	{
		IMusicRepository Musics { get; }
		IArtistRepository Artists { get; }

		Task<int> CommitAsync();
	}
}
