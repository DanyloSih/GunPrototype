using System;
using UnityEngine;

namespace AbstractDefence.Effects
{
    public enum EndCause
    {
        Finished,
        Stopped
    }

    public delegate void EffectEndCallback(EndCause endCause);

    public interface IEffect
    {
        public bool IsPlaying { get; }

        public void Start(MonoBehaviour coroutineExecuter, float effectForce = 1, EffectEndCallback effectEndCallback = null);

        public void Stop();
    }
}