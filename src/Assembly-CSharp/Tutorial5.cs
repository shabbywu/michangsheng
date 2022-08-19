using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class Tutorial5 : MonoBehaviour
{
	// Token: 0x06000470 RID: 1136 RVA: 0x000186F8 File Offset: 0x000168F8
	public void SetDurationToCurrentProgress()
	{
		UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].duration = Mathf.Lerp(2f, 0.5f, UIProgressBar.current.value);
		}
	}
}
