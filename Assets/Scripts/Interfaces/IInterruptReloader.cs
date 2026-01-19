using System;

public interface IInterruptReloader : IReloader
{
    public event Action AmmoLoaded;
    public void Interrupt();
}
