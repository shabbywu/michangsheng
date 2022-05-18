using System;
using UnityEngine;

// Token: 0x02000520 RID: 1312
public class CloseUIScene : MonoBehaviour
{
	// Token: 0x060021B8 RID: 8632 RVA: 0x00118E1C File Offset: 0x0011701C
	private void Start()
	{
		if (PanelMamager.inst != null)
		{
			if (PanelMamager.inst.UISceneGameObject != null)
			{
				Object.Destroy(PanelMamager.inst.UISceneGameObject);
			}
			if (PanelMamager.inst.UIBlackMaskGameObject != null)
			{
				Object.Destroy(PanelMamager.inst.UIBlackMaskGameObject);
			}
		}
		if (FpUIMag.inst != null)
		{
			Object.Destroy(FpUIMag.inst.gameObject);
		}
	}
}
