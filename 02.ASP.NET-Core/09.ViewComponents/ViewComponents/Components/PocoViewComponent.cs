using System.Linq;
using ViewComponents.Models;

namespace ViewComponents.Components
{
    // NOTE: View components can be placed anywhere. A view component must be a public non-abstract
    //       class, not contain any generic parameters, and either be decorated with
    //       'ViewComponentAttribute' or have a class name ending with the 'ViewComponent' suffix.
    //        A view component must not be decorated with 'NonViewComponentAttribute'.
    public class PocoViewComponent
    {
        private readonly ICityRepository _repository;

        public PocoViewComponent(ICityRepository repository)
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
