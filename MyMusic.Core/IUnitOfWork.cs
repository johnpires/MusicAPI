using MyMusic.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace MyMusic.Core
{
	public class IUnitOfWork : IDisposable
	{
		IMusicRepository Musics { get; }
		IArtistRepository Artist { get; }

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		private Task<int> CommitAsync();
	}
}
