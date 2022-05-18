using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002D RID: 45
[AddComponentMenu("")]
public class SfxrCacheSurrogate : MonoBehaviour
{
	// Token: 0x06000305 RID: 773 RVA: 0x00006DA9 File Offset: 0x00004FA9
	public void CacheSound(SfxrSynth __synth, Action __callback)
	{
		base.StartCoroutine(this.CacheSoundAsynchronously(__synth, __callback));
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00006DBA File Offset: 0x00004FBA
	private IEnumerator CacheSoundAsynchronously(SfxrSynth __synth, Action __callback)
	{
		yield return null;
		__synth.CacheSound(null, true);
		__callback();
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00006DD7 File Offset: 0x00004FD7
	public void CacheMutations(SfxrSynth __synth, uint __mutationsNum, float __mutationAmount, Action __callback)
	{
		base.StartCoroutine(this.CacheMutationsAsynchronously(__synth, __mutationsNum, __mutationAmount, __callback));
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00006DEB File Offset: 0x00004FEB
	private IEnumerator CacheMutationsAsynchronously(SfxrSynth __synth, uint __mutationsNum, float __mutationAmount, Action __callback)
	{
		yield return null;
		__synth.CacheMutations(__mutationsNum, __mutationAmount, null, true);
		__callback();
		Object.Destroy(base.gameObject);
		yield break;
	}
}
