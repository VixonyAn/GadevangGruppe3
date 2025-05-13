using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Helper
{
	public class BrugernavnComparer : IComparer<Bruger>
	{
		public int Compare(Bruger? x, Bruger? y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			else if (x == null)
			{
				return -1;
			}
			else if (y == null)
			{
				return 1;
			}

			return string.Compare(x.Brugernavn, y.Brugernavn);
		}
	}
}
