using System;
using UnityEngine;

// Token: 0x02000397 RID: 919
public class CloseUIScene : MonoBehaviour
{
	// Token: 0x06001E37 RID: 7735 RVA: 0x000D53FC File Offset: 0x000D35FC
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
