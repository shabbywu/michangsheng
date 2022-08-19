using System;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class TabPanelBase : MonoBehaviour
{
	// Token: 0x06001D06 RID: 7430 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public virtual void OnPanelShow()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x000B5E62 File Offset: 0x000B4062
	public virtual void OnPanelHide()
	{
		base.gameObject.SetActive(false);
	}
}
