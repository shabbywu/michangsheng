using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
	// Token: 0x060004E2 RID: 1250 RVA: 0x00008485 File Offset: 0x00006685
	private void OnClick()
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, this.state);
		}
	}

	// Token: 0x04000337 RID: 823
	public GameObject target;

	// Token: 0x04000338 RID: 824
	public bool state = true;
}
