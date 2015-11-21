using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Generic in-memory item repository for maintaining application state.
/// Each item (of type T) is identified by an ID (IRepositoryItem).
/// </summary>
/// <typeparam name="T">Type of the item stored in the repository.</typeparam>
public class Repository<T> where T: IRepositoryItem
{
    private List<T> items = new List<T>();

    public List<T> Items
    {
        get { return items; }
    }

    public T Store(T item)
    {
        if (!items.Any(x => x.Id == item.Id))
        {
            // this is a new item -> get next id
            item.Id = items.Count > 0 ? items.Max(x => x.Id) + 1 : 1;
        }

        items.Add(item); // add or replace the item

        return item;
    }
}