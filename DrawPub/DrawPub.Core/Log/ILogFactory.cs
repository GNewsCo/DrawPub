using System;

namespace DrawPub.Core.Log
{
    public interface ILogFactory
    {
        ILogBase CreateLog(Type type);
    }
}