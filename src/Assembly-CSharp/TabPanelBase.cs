using System;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
public class TabPanelBase : MonoBehaviour
{
	// Token: 0x06002070 RID: 8304 RVA: 0x00011B82 File Offset: 0x0000FD82
	public virtual void OnPanelShow()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x00017C2D File Offset: 0x00015E2D
	public virtual void OnPanelHide()
	{
		base.gameObject.SetActive(false);
	}
}
