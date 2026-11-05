using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Interfaces;

// Löfte för en klass som pratar med ett API över http.
// Task = metoden är asynkron: den väntar på nätet utan att frysa fönstret.
// T byts mot Product, Customer osv, så en klass räcker för alla typer.
public interface IHttpService<T> where T : class
{
    Task Delete(string resource, string id);   // ta bort ett objekt, id är _id från restdb
    Task<List<T>> Get(string resource);   // hämta alla, t ex "products"

    // samma namn men en parameter till = överlagring (overload). predicate är ett sökvillkor
    // i json, t ex {"itemNumber":"1000"}, och då får vi tillbaka ETT objekt i stället för en lista
    Task<T> Get(string resource, string predicate);
    Task Post(string resource, T data);    // skicka ett nytt objekt
    Task Put(string resource, T data, string objectId);   // uppdatera ett objekt som redan finns
}
