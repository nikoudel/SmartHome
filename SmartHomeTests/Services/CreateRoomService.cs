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

using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;

/// <summary>
/// An http POST to /CreateRoom URL will be serialized as RoomData and passed to
/// CreateRoomService.Post method which returns a RoomData object to be serialized
/// and sent back to the caller over http.
/// </summary>
[Route("/CreateRoom", "POST")]
public class RoomData
{
    public long Id { get; set; }
    public RoomType RoomType { get; set; }
}

public class CreateRoomService : Service
{
    //Injected by IOC (see ApplictionHost.Configure)
    public Repository<Room> Repository { get; set; }

    /// <summary>
    /// Create and add a new Room into the repository.
    /// </summary>
    public RoomData Post(RoomData data)
    {
        // Note: repository creates a Room, not RoomData.
        var room = Repository.Store(Room.createInstance(data.RoomType));

        // set room ID back to the data object
        data.Id = room.Id;

        return data;
    }
}
