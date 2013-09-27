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

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System;
using System.Linq;
using System.Text;

/// <summary>
/// An http POST to /ChangeDeviceState URL will be serialized as ChangeDeviceStateData and passed to
/// ChangeDeviceStateService.Post method which returns a ChangeDeviceStateData object to be serialized
/// and sent back to the caller over http.
/// </summary>
[Route("/ChangeDeviceState", "POST")]
public class ChangeDeviceStateData
{
    public long Id { get; set; }
    public DeviceState NewState { get; set; }
}

public class ChangeDeviceStateService : Service
{
    //Injected by IOC (see ApplictionHost.Configure)
    public Repository<DeviceData> Repository { get; set; }

    public ChangeDeviceStateData Post(ChangeDeviceStateData newDeviceState)
    {
        // initialize the response object
        var resultState = new ChangeDeviceStateData();

        // find the needed device
        var device = Repository.Items.FirstOrDefault(d => d.Id == newDeviceState.Id);

        if (device == null)
        {
            // device not found
            resultState.Id = 0;
            resultState.NewState = DeviceState.Undefined;
        }
        else
        {
            // change device state
            device.DeviceState = newDeviceState.NewState;

            // set new values to the response object
            resultState.Id = device.Id;
            resultState.NewState = device.DeviceState;
        }

        return resultState;
    }
}