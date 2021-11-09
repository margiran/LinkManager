using System.Collections.Generic;
using System.Threading.Tasks;
using LinkManagerApi.Domain;

namespace LinkManagerApi.Services
{
    public interface ILinkService
    {
        List<Link> GetAll();

        Link GetById(int id);

        void Create(Link link);

        bool Update(Link link);
        bool Delete(int id);

    }
}