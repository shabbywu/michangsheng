using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class Tutorial5 : MonoBehaviour
{
	// Token: 0x060004BE RID: 1214 RVA: 0x0006F9F0 File Offset: 0x0006DBF0
	public void SetDurationToCurrentProgress()
	{
		UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].duration = Mathf.Lerp(2f, 0.5f, UIProgressBar.current.value);
		}
	}
}
