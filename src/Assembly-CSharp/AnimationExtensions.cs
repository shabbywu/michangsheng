using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000661 RID: 1633
internal static class AnimationExtensions
{
	// Token: 0x060028C6 RID: 10438 RVA: 0x0001FCB5 File Offset: 0x0001DEB5
	public static IEnumerator Play(this Animation animation, string clipName, bool useTimeScale, Action<bool> onComplete)
	{
		if (!useTimeScale)
		{
			AnimationState _currState = animation[clipName];
			bool isPlaying = true;
			float _progressTime = 0f;
			float _timeAtLastFrame = 0f;
			animation.Play(clipName);
			_timeAtLastFrame = Time.realtimeSinceStartup;
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
					if (_currState.wrapMode != 2)
					{
						isPlaying = false;
					}
					else
					{
						_progressTime = 0f;
					}
				}
				yield return new WaitForEndOfFrame();
			}
			yield return null;
			if (onComplete != null)
			{
				onComplete(true);
			}
			_currState = null;
		}
		else
		{
			animation.Play(clipName);
		}
		yield break;
	}

	// Token: 0x060028C7 RID: 10439 RVA: 0x0001FCD9 File Offset: 0x0001DED9
	public static IEnumerator Reverse(this Animation animation, string clipName, bool useTimeScale, Action<bool> onComplete)
	{
		if (!useTimeScale)
		{
			AnimationState _currState = animation[clipName];
			bool isPlaying = true;
			float _progressTime = 0f;
			float _timeAtLastFrame = 0f;
			animation.Play(clipName);
			_timeAtLastFrame = Time.realtimeSinceStartup;
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
					if (_currState.wrapMode != 2)
					{
						isPlaying = false;
					}
					else
					{
						_progressTime = 0f;
					}
				}
				yield return new WaitForEndOfFrame();
			}
			yield return null;
			if (onComplete != null)
			{
				onComplete(true);
			}
			_currState = null;
		}
		yield break;
	}
}
