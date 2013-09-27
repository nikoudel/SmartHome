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
using ServiceStack.ServiceHost;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// An http POST to /CreateDevice URL will be serialized as DeviceData and passed to
/// DeviceService.Post method which returns a DeviceData object to be serialized
/// and sent back to the caller over http.
/// 
/// An http GET to /GetDevices URL will be serialized as DeviceData and passed to
/// DeviceService.Get method which returns a DeviceData object to be serialized
/// and sent back to the caller over http.
/// </summary>
[Route("/CreateDevice", "POST")]
[Route("/GetDevices", "GET")]
public class DeviceData : IRepositoryItem
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public DeviceType DeviceType { get; set; }
    public DeviceState DeviceState { get; set; }
}

public class DeviceService : Service
{
    //Injected by IOC (see ApplictionHost.Configure)
    public Repository<DeviceData> Repository { get; set; } 

    /// <summary>
    /// Add a new DeviceData into the repository.
    /// </summary>
    public DeviceData Post(DeviceData item)
    {
        return Repository.Store(item);
    }

    /// <summary>
    /// Get a list of devices based on a given criteria.
    /// </summary>
    public List<DeviceData> Get(DeviceData crit)
    {
        var query = from device in Repository.Items select device;

        if(crit.Id > 0)
        {
            query = query.Where(device => device.Id == crit.Id);
        }

        if(crit.RoomId > 0)
        {
            query = query.Where(device => device.RoomId == crit.RoomId);
        }
        
        if(crit.DeviceType != DeviceType.Undefined)
        {
            query = query.Where(device => device.DeviceType == crit.DeviceType);
        }

        if (crit.DeviceState != DeviceState.Undefined)
        {
            query = query.Where(device => device.DeviceState == crit.DeviceState);
        }

        return query.ToList<DeviceData>();
    }
}
