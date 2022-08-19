using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200014C RID: 332
[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	// Token: 0x06000ECF RID: 3791 RVA: 0x0005A4BB File Offset: 0x000586BB
	private void OnEnable()
	{
		base.StartCoroutine("CheckIfAlive");
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x0005A4C9 File Offset: 0x000586C9
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

	// Token: 0x04000B04 RID: 2820
	public bool OnlyDeactivate;
}
