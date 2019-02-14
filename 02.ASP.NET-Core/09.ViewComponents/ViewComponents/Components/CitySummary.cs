using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ViewComponents.Models;

namespace ViewComponents.Components
{
    // NOTE: View components derived from ViewComponent can omit the "ViewComponent" prefix
    public class CitySummary : ViewComponent
    {
        private readonly ICityRepository _repository;

        public CitySummary(ICityRepository repository)
        {
            _repository = repository;
        }

        public string Invoke()
        {
            return $"{_repository.Cities.Count()} cities, "
                + $"{_repository.Cities.Sum(c => c.Population)} people";
        }
    }
}
