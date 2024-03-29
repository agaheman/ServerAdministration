﻿using Microsoft.Extensions.Options;
using System;

namespace ServerAdministration.Server.Slave.Extensions
{
    public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }
}
