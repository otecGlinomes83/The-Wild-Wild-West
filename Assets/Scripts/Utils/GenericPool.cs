using UnityEngine;
using UnityEngine.Pool;

public class GenericPool<T> where T : Component, IPooled
{
    private ObjectPool<T> _pool;

    public void Setup(Transform parent, string name)
    {
        _pool = new ObjectPool<T>
            (
            createFunc: () => Create(parent, name),
            actionOnGet: objectToGet => OnGet(objectToGet),
            actionOnRelease: objectToRelease => OnRelease(objectToRelease)
            );
    }

    public T Get() =>
         _pool.Get();

    public void Release(T objectToRelease) =>
        _pool.Release(objectToRelease);

    private void OnRelease(T objectToRelease)
    {
        objectToRelease.Reset();
        objectToRelease.gameObject.SetActive(false);
    }

    protected void OnGet(T objectToGet)
    {
        objectToGet.gameObject.SetActive(true);
    }

    protected T Create(Transform parent, string name)
    {
        GameObject gameObject = new GameObject(name);
        gameObject.transform.SetParent(parent, false);

        T createdObject = gameObject.AddComponent<T>();
        gameObject.SetActive(false);

        return createdObject;
    }
}