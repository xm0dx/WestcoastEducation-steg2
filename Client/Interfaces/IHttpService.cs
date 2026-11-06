using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Interfaces;

public interface IHttpService<T> where T : class
{
    Task Delete(string resource, string id);   // ta bort ett objekt, id är _id från restdb
    Task<List<T>> Get(string resource);   // hämta alla, t ex "products"

    Task<T> Get(string resource, string predicate);
    Task Post(string resource, T data);    // skicka ett nytt objekt
    Task Put(string resource, T data, string objectId);   // uppdatera ett objekt som redan finns
}
