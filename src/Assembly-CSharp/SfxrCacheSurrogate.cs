using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("")]
public class SfxrCacheSurrogate : MonoBehaviour
{
	public void CacheSound(SfxrSynth __synth, Action __callback)
	{
		((MonoBehaviour)this).StartCoroutine(CacheSoundAsynchronously(__synth, __callback));
	}

	private IEnumerator CacheSoundAsynchronously(SfxrSynth __synth, Action __callback)
	{
		yield return null;
		__synth.CacheSound(null, __isFromCoroutine: true);
		__callback();
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void CacheMutations(SfxrSynth __synth, uint __mutationsNum, float __mutationAmount, Action __callback)
	{
		((MonoBehaviour)this).StartCoroutine(CacheMutationsAsynchronously(__synth, __mutationsNum, __mutationAmount, __callback));
	}

	private IEnumerator CacheMutationsAsynchronously(SfxrSynth __synth, uint __mutationsNum, float __mutationAmount, Action __callback)
	{
		yield return null;
		__synth.CacheMutations(__mutationsNum, __mutationAmount, null, __isFromCoroutine: true);
		__callback();
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
