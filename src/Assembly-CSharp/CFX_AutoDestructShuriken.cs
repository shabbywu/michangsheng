using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000221 RID: 545
[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	// Token: 0x060010EF RID: 4335 RVA: 0x00010884 File Offset: 0x0000EA84
	private void OnEnable()
	{
		base.StartCoroutine("CheckIfAlive");
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x00010892 File Offset: 0x0000EA92
	private IEnumerator CheckIfAlive()
	{
		do
		{
			yield return new WaitForSeconds(0.5f);
		}
		while (base.GetComponent<ParticleSystem>().IsAlive(true));
		if (this.OnlyDeactivate)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
		yield break;
	}

	// Token: 0x04000D9F RID: 3487
	public bool OnlyDeactivate;
}
