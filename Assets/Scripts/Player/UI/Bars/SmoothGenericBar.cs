using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2DScripts.UI.Bars
{
    public class SmoothGenericBar<T> : GenericBarBase<T> where T : IChangeObservable
    {
        [SerializeField] private Image _bar;

        [SerializeField] private float _barChangingSpeed = 0.25f;
        [SerializeField] private float _barChangingDelay = 0.25f;

        private Coroutine _smoothChangeBarCoroutine;
        private WaitForSecondsRealtime _delay;

        protected void Awake()
        {
            _delay = new WaitForSecondsRealtime(_barChangingDelay);
        }

        protected override void UpdateView(int current, int max)
        {
            if (_smoothChangeBarCoroutine != null)
            {
                StopCoroutine(_smoothChangeBarCoroutine);
                _smoothChangeBarCoroutine = null;
            }

            _smoothChangeBarCoroutine = StartCoroutine(SmoothChangeBar(current, max));
        }

        private IEnumerator SmoothChangeBar(int current, int max)
        {
            float targetFill = current / max;

            AdjustBar(targetFill);

            yield return _delay;

            while (Mathf.Approximately(_bar.fillAmount, targetFill) == false)
            {
                targetFill = current / max;

                AdjustBar(targetFill);

                _bar.fillAmount = Mathf.MoveTowards(_bar.fillAmount, targetFill, _barChangingSpeed * Time.unscaledDeltaTime);

                yield return null;
            }

            _smoothChangeBarCoroutine = null;
        }

        private void AdjustBar(float targetFill)
        {
            if (_bar.fillAmount < targetFill)
                _bar.fillAmount = targetFill;
        }
    }
}