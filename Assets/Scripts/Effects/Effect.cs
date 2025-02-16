using System.Collections;
using UnityEngine;

namespace AbstractDefence.Effects
{
    public abstract class Effect : IEffect
    {
        private Coroutine _coroutine;

        public bool IsPlaying { get; private set; }

        protected EffectEndCallback EffectEndCallback { get; private set; }
        protected MonoBehaviour CoroutineExecutor { get; private set; }

        public void Start(MonoBehaviour coroutineExecuter, float effectForce = 1, EffectEndCallback effectEndCallback = null)
        {
            TryStopCoroutine();

            CoroutineExecutor = coroutineExecuter;
            EffectEndCallback = effectEndCallback;
            IsPlaying = true;
            _coroutine = coroutineExecuter.StartCoroutine(Process(effectForce));
        }

        public void Stop()
        {
            TryStopCoroutine();          
            IsPlaying = false;
        }

        protected void TryStopCoroutine()
        {
            if (_coroutine != null)
            {
                CoroutineExecutor?.StopCoroutine(_coroutine);
                _coroutine = null;
                OnStop();
                EffectEndCallback?.Invoke(EndCause.Stopped);
            }
        }

        protected abstract IEnumerator OnStart(float effectForce = 1);

        protected virtual void OnStop() { }

        private IEnumerator Process(float effectForce = 1)
        {
            yield return OnStart(effectForce);
            EffectEndCallback?.Invoke(EndCause.Finished);
            IsPlaying = false;
        }
    }
}