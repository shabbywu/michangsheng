using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000025 RID: 37
[AddComponentMenu("")]
public class SfxrCacheSurrogate : MonoBehaviour
{
	// Token: 0x060002F3 RID: 755 RVA: 0x0000F261 File Offset: 0x0000D461
	public void CacheSound(SfxrSynth __synth, Action __callback)
	{
		base.StartCoroutine(this.CacheSoundAsynchronously(__synth, __callback));
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0000F272 File Offset: 0x0000D472
	private IEnumerator CacheSoundAsynchronously(SfxrSynth __synth, Action __callback)
	{
		yield return null;
		__synth.CacheSound(null, true);
		__callback();
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0000F28F File Offset: 0x0000D48F
	public void CacheMutations(SfxrSynth __synth, uint __mutationsNum, float __mutationAmount, Action __callback)
	{
		base.StartCoroutine(this.CacheMutationsAsynchronously(__synth, __mutationsNum, __mutationAmount, __callback));
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0000F2A3 File Offset: 0x0000D4A3
	private IEnumerator CacheMutationsAsynchronously(SfxrSynth __synth, uint __mutationsNum, float __mutationAmount, Action __callback)
	{
		yield return null;
		__synth.CacheMutations(__mutationsNum, __mutationAmount, null, true);
		__callback();
		Object.Destroy(base.gameObject);
		yield break;
	}
}
