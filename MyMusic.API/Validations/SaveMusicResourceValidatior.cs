using FluentValidation;
using MyMusic.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusic.API.Validations
{
	public class SaveMusicResourceValidatior : AbstractValidator<SaveResourceMusic>
	{
		public SaveMusicResourceValidatior()
		{
			RuleFor(m => m.Name)
				.NotEmpty()
				.MaximumLength(50);

			RuleFor(m => m.ArtistId)
				.NotEmpty()
				.WithMessage("'Artist Id' must not be 0.");
		}
	}
}
