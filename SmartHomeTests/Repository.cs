/*
 * The MIT License
 *
 * Copyright 2013 Nikolai Koudelia
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

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