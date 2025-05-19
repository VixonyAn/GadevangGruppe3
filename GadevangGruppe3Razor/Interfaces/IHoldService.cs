using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;

namespace GadevangGruppe3Razor.Interfaces
{
	public interface IHoldService
	{
		Task<List<Hold>> GetAllHoldAsync();
		Task<bool> CreateHoldAsync(Hold hold);
		Task<Hold?> DeleteHoldAsync(int holdId);
		Task<Hold> GetHoldByIdAsync(int holdId);
	}
}
