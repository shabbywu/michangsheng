using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
	// Token: 0x06000494 RID: 1172 RVA: 0x00019491 File Offset: 0x00017691
	private void OnClick()
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, this.state);
		}
	}

	// Token: 0x040002C4 RID: 708
	public GameObject target;

	// Token: 0x040002C5 RID: 709
	public bool state = true;
}
