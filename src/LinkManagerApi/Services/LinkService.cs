using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkManagerApi.Domain;

namespace LinkManagerApi.Services
{
    public class LinkService : ILinkService
    {
        private readonly List<Link> _links;

        public LinkService()
        {
            _links =new List<Link> ();
            for (var i = 0; i < 5; i++)
            {
                _links.Add (new Link{
                    Id=i,
                    Name= $"link number: {i}"
                });
            }
        }
        public void Create(Link link)
        {
            var rnd=new Random();
            link.Id=rnd.Next(5,1000);
            _links.Add(link);
        }

        public bool Delete(int id)
        {
            var link = GetById(id);
            if (link == null)
                return false;

            _links.Remove(link);
            return true;
        }

        public List<Link> GetAll()
        {
            return  _links;
        }

        public Link GetById(int id)
        {
            return _links.SingleOrDefault(l => l.Id== id);
        }

        public bool Update(Link link)
        {
            var exist = GetById(link.Id) != null;

            if (!exist) 
                return false;
            
            var index= _links.FindIndex(l=>l.Id== link.Id);

            _links[index]= link;
            return true;
        }
    }
}