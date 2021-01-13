using MyMusic.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace MyMusic.Core
{
	public class IUnitOfWork : IDisposable
	{
		IMusicRepository Musics { get; }
		IArtistRepository Artist { get; }

		Task<int> CommitAsync { get; }

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
