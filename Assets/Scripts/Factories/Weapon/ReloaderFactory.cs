using UnityEngine;

public static class ReloaderFactory
{
    public static IReloader Create(ReloadData reloadData, Transform parent = null)
    {
        ReloadType reloadType = reloadData.ReloadType;

        GameObject gameObject = new GameObject($"{reloadType} Reloader");

        switch (reloadType)
        {
            case ReloadType.Interruptible:
                {
                    if (parent != null)
                        gameObject.transform.SetParent(parent);

                    InterruptableReloader reloader = gameObject.AddComponent<InterruptableReloader>();
                    reloader.Setup(reloadData);

                    return reloader;
                }

            case ReloadType.NonInterruptible:
                {
                    if (parent != null)
                        gameObject.transform.SetParent(parent);

                    NonInterruptReloader reloader = gameObject.AddComponent<NonInterruptReloader>();
                    reloader.Setup(reloadData);

                    return reloader;
                }

            case ReloadType.NonReload:
                return null;

            default:
                return null;
        }
    }
}
