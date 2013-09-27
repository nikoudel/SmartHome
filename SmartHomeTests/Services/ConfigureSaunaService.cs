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
/// An http POST to /ConfigureSauna URL will be serialized as SaunaConfigurationData and passed to
/// ConfigureSaunaService.Post method which returns a SaunaConfiguration object to be serialized
/// and sent back to the caller over http.
/// 
/// Note: SaunaConfigurationData is inherited from a domain-side object SaunaConfiguration in order
/// to keep domain- and test code separated in different assemblies. Route attribute
/// can't be added to SaunaConfiguration without adding ServiceStack references to domain-side
/// assembly SmartHomeDomain where SaunaConfiguration resides.
/// </summary>
[Route("/ConfigureSauna", "POST")]
public class SaunaConfigurationData : SaunaConfiguration { }

class ConfigureSaunaService : Service
{
    public SaunaConfiguration Post(SaunaConfigurationData conf)
    {

        CreateRoomService rooms = GetResolver().TryResolve<CreateRoomService>();

        if(rooms == null)
        {
            throw new ApplicationException("Failed resolving CreateRoomService!");
        }

        // find the needed sauna
        Sauna sauna = (Sauna)rooms.Repository.Items.FirstOrDefault(room => room.Id == conf.Sauna);

        if (sauna == null)
        {
            throw new ApplicationException("Room not found: " + conf.Sauna);
        }

        // configure the sauna with given parameters
        return sauna.AddConfiguration(conf);
    }
}