using System;

public interface IReloader
{
    public bool IsReloading { get; }

    public event Action ReloadStarted;
    public event Action ReloadFinished;

    public void TryReload(Magazine magazineToReload);
}
