using UnityEngine;

namespace Assets._2DScripts.UI.Bars
{
    public abstract class GenericBarBase<T> : MonoBehaviour where T : IChangeObservable
    {
        [SerializeField] protected T _changeableValue;

        protected virtual void OnEnable()
        {
            _changeableValue.ValueChanged += UpdateView;
        }

        protected virtual void OnDisable()
        {
            _changeableValue.ValueChanged -= UpdateView;
        }

        protected abstract void UpdateView(int current, int max);
    }
}