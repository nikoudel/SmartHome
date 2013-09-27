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
/// An http POST to /GetSaunaState URL will be serialized as SaunaStateData and passed to
/// GetSaunaStateService.Post method which returns a SaunaState object to be serialized
/// and sent back to the caller over http.
/// 
/// Note: SaunaStateData is inherited from a domain-side object SaunaState in order
/// to keep domain- and test code separated in different assemblies. Route attribute
/// can't be added to SaunaState without adding ServiceStack references to domain-side
/// assembly SmartHomeDomain where SaunaState resides.
/// </summary>
[Route("/GetSaunaState", "POST")]
public class SaunaStateData : SaunaState { }

/// <summary>
/// ServiceStack service object handling POST requests with SaunaStateData.
/// </summary>
public class GetSaunaStateService : Service
{
    public SaunaState Post(SaunaStateData item)
    {
        CreateRoomService service = GetResolver().TryResolve<CreateRoomService>();

        // find the room by a given room ID (SaunaStateData.Sauna)
        Room sauna = service.Repository.Items.Where(s => s.Id == item.Sauna).FirstOrDefault();

        if (sauna == null)
        {
            throw new ApplicationException("Room not found: " + item.Sauna);
        }

        // return the room state on the given DateTime
        return ((Sauna)sauna).GetState(item.DateTime);
    }
}
