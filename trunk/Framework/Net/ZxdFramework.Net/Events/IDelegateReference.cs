using System;

namespace ZxdFramework.Events
{
    public interface IDelegateReference
    {
        /// <summary>
        /// Gets the referenced <see cref="Delegate" /> object.
        /// </summary>
        /// <value>A <see cref="Delegate"/> instance if the target is valid; otherwise <see langword="null"/>.</value>
        Delegate Target { get; }
    }
}