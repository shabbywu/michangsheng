using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200048F RID: 1167
internal static class AnimationExtensions
{
	// Token: 0x060024DA RID: 9434 RVA: 0x000FFBA2 File Offset: 0x000FDDA2
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

	// Token: 0x060024DB RID: 9435 RVA: 0x000FFBC6 File Offset: 0x000FDDC6
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
