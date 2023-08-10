using System;
using System.Collections;
using UnityEngine;

internal static class AnimationExtensions
{
	public static IEnumerator Play(this Animation animation, string clipName, bool useTimeScale, Action<bool> onComplete)
	{
		if (!useTimeScale)
		{
			AnimationState _currState = animation[clipName];
			bool isPlaying = true;
			float _progressTime = 0f;
			animation.Play(clipName);
			float _timeAtLastFrame = Time.realtimeSinceStartup;
			while (isPlaying)
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				float num = realtimeSinceStartup - _timeAtLastFrame;
				_timeAtLastFrame = realtimeSinceStartup;
				_progressTime += num;
				_currState.normalizedTime = _progressTime / _currState.length;
				animation.Sample();
				if (_progressTime >= _currState.length)
				{
					if ((int)_currState.wrapMode != 2)
					{
						isPlaying = false;
					}
					else
					{
						_progressTime = 0f;
					}
				}
				yield return (object)new WaitForEndOfFrame();
			}
			yield return null;
			onComplete?.Invoke(obj: true);
		}
		else
		{
			animation.Play(clipName);
		}
	}

	public static IEnumerator Reverse(this Animation animation, string clipName, bool useTimeScale, Action<bool> onComplete)
	{
		if (useTimeScale)
		{
			yield break;
		}
		AnimationState _currState = animation[clipName];
		bool isPlaying = true;
		float _progressTime = 0f;
		animation.Play(clipName);
		float _timeAtLastFrame = Time.realtimeSinceStartup;
		while (isPlaying)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num = realtimeSinceStartup - _timeAtLastFrame;
			_timeAtLastFrame = realtimeSinceStartup;
			_progressTime += num;
			animation.Play();
			_currState.normalizedTime = 1f - _progressTime / _currState.length;
			animation.Sample();
			animation.Stop();
			if (_progressTime >= _currState.length)
			{
				if ((int)_currState.wrapMode != 2)
				{
					isPlaying = false;
				}
				else
				{
					_progressTime = 0f;
				}
			}
			yield return (object)new WaitForEndOfFrame();
		}
		yield return null;
		onComplete?.Invoke(obj: true);
	}
}
