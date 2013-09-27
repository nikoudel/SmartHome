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

using Funq;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// ServiceStack application host.
/// </summary>
public class ApplictionHost : AppHostHttpListenerBase
{
    public ApplictionHost() : base("SmartHome Testing Service", typeof(ApplictionHost).Assembly) { }

    public override void Configure(Container container)
    {
        // The following item repositories will be available to ServiceStack
        // services directly through IOC injection (e.g DeviceService) or indirectly
        // by resolving a service with direct access (e.g. GetSaunaStateService).
        // The repositories preserve the application's state.

        container.Register(new Repository<Room>());
        container.Register(new Repository<DeviceData>());
    }
}