﻿namespace UserService.Core
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        void Dispose();
    }
}
