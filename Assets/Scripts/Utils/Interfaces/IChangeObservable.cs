using System;

public interface IChangeObservable
{
    public event Action<int,int> ValueChanged;
}
