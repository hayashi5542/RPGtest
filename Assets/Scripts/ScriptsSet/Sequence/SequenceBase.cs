using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogamelib
{
    [System.Serializable]
    public class SequenceConfig
    {
        public bool Active = true;
        [Range(0, 100)]
        public float Chance = 100f;
    }
    [AddComponentMenu("")]
    [System.Serializable]
    public abstract class SequenceBase : MonoBehaviour
    {
        [HideInInspector]
        public string Label = "Sequence Base";
        public GameObject Owner { get; set; }
        protected bool _initialized = false;
        public SequenceConfig Config;
        public SequenceTiming Timing;
        protected float _lastPlayTimestamp = -1f;
        protected int _playsLeft;

        protected WaitForSeconds _initialDelayWaitForSeconds;
        protected WaitForSeconds _betweenDelayWaitForSeconds;
        protected WaitForSeconds _sequenceDelayWaitForSeconds;

        protected Coroutine _playCoroutine;
        protected Coroutine _infinitePlayCoroutine;
        protected Coroutine _sequenceCoroutine;
        protected Coroutine _repeatedPlayCoroutine;

        public virtual YieldInstruction Pause { get { return null; } }
        public virtual bool HoldingPause { get { return false; } }
        public virtual bool LooperPause { get { return false; } }
        public virtual bool LooperStart { get { return false; } }
#if UNITY_EDITOR
        public virtual Color FeedbackColor { get { return Color.white; } }
#endif

        public virtual bool InCooldown { get { return (Timing.CooldownDuration > 0f) && (SequenceTime - _lastPlayTimestamp < Timing.CooldownDuration); } }
        public float SequenceTime
        {
            get
            {
                if (Timing.TimescaleMode == TimescaleModes.Scaled)
                {
                    return Time.time;
                }
                else
                {
                    return Time.unscaledTime;
                }
            }
        }
        public virtual float SequenceStartedAt { get { return _lastPlayTimestamp; } }
        public virtual float SequenceDuration { get { return 0f; } }
        public virtual bool SequencePlaying { get { return ((SequenceStartedAt > 0f) && (Time.time - SequenceStartedAt < SequenceDuration)); } }

        public virtual void Initialization(GameObject owner)
        {
            _initialized = true;
            Owner = owner;
            _playsLeft = Timing.NumberOfRepeats + 1;

            SetInitialDelay(Timing.InitialDelay);
            SetDelayBetweenRepeats(Timing.DelayBetweenRepeats);

            CustomInitialization(owner);
        }

        public virtual void SetInitialDelay(float delay)
        {
            Timing.InitialDelay = delay;
            if (Timing.InitialDelay > 0f)
            {
                _initialDelayWaitForSeconds = new WaitForSeconds(Timing.InitialDelay);
            }
        }
        public virtual void SetDelayBetweenRepeats(float delay)
        {
            Timing.DelayBetweenRepeats = delay;
            if (Timing.DelayBetweenRepeats > 0f)
            {
                _betweenDelayWaitForSeconds = new WaitForSeconds(Timing.DelayBetweenRepeats + SequenceDuration);
            }
        }
        public virtual void ResetSequence()
        {
            _playsLeft = Timing.NumberOfRepeats + 1;
            CustomReset();
        }

        public virtual IEnumerator Play(Vector3 position, float attenuation = 1.0f)
        {
            if (!Config.Active)
            {
                yield break;
            }

            if (!_initialized)
            {
                Debug.LogWarning("The " + this + " sequence is being played without having been initialized. Call Initialization() first.");
            }

            // we check the cooldown
            if (InCooldown)
            {
                yield break;
            }

            if (Timing.InitialDelay > 0f)
            {
                _playCoroutine = StartCoroutine(PlayCoroutine(position, attenuation));
            }
            else
            {
                // 基本はここ
                _lastPlayTimestamp = SequenceTime;
                yield return StartCoroutine(RegularPlay(position, attenuation));
            }
        }

        protected virtual IEnumerator PlayCoroutine(Vector3 position, float attenuation = 1.0f)
        {
            yield return _initialDelayWaitForSeconds;
            _lastPlayTimestamp = SequenceTime;
            StartCoroutine(RegularPlay(position, attenuation));
        }

        protected virtual IEnumerator RegularPlay(Vector3 position, float attenuation = 1.0f)
        {
            if (Config.Chance == 0f)
            {
                yield break;
            }
            if (Config.Chance != 100f)
            {
                // determine the odds
                float random = Random.Range(0f, 100f);
                if (random > Config.Chance)
                {
                    yield break;
                }
            }

            if (Timing.RepeatForever)
            {
                _infinitePlayCoroutine = StartCoroutine(InfinitePlay(position, attenuation));
                yield break;
            }
            else if (Timing.NumberOfRepeats > 0)
            {
                _repeatedPlayCoroutine = StartCoroutine(RepeatedPlay(position, attenuation));
                yield break;
            }
            else
            {
                yield return CustomPlaySequence(position, attenuation);
            }

        }

        protected virtual IEnumerator InfinitePlay(Vector3 position, float attenuation = 1.0f)
        {
            while (true)
            {
                _lastPlayTimestamp = SequenceTime;
                CustomPlaySequence(position, attenuation);
                yield return _betweenDelayWaitForSeconds;
            }
        }
        protected virtual IEnumerator RepeatedPlay(Vector3 position, float attenuation = 1.0f)
        {
            while (_playsLeft > 0)
            {
                _lastPlayTimestamp = SequenceTime;
                _playsLeft--;
                CustomPlaySequence(position, attenuation);
                yield return _betweenDelayWaitForSeconds;
            }
            _playsLeft = Timing.NumberOfRepeats + 1;
        }

        public virtual void Stop(Vector3 position, float attenuation = 1.0f)
        {
            if (_playCoroutine != null) { StopCoroutine(_playCoroutine); }
            if (_infinitePlayCoroutine != null) { StopCoroutine(_infinitePlayCoroutine); }
            if (_repeatedPlayCoroutine != null) { StopCoroutine(_repeatedPlayCoroutine); }
            if (_sequenceCoroutine != null) { StopCoroutine(_sequenceCoroutine); }

            _lastPlayTimestamp = 0f;
            _playsLeft = Timing.NumberOfRepeats + 1;
            CustomStopSequence(position, attenuation);
        }
        public virtual void ResetFeedback()
        {
            _playsLeft = Timing.NumberOfRepeats + 1;
            CustomReset();
        }

        protected virtual void CustomInitialization(GameObject owner) { }
        protected abstract IEnumerator CustomPlaySequence(Vector3 position, float attenuation = 1.0f);
        protected virtual void CustomReset() { }
        protected virtual void CustomStopSequence(Vector3 position, float attenuation = 1.0f) { }

    }
}




