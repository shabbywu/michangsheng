using System;
using UnityEngine;

// Token: 0x020001E7 RID: 487
public class MapMoveTips : MonoBehaviour
{
	// Token: 0x06001450 RID: 5200 RVA: 0x00082EC5 File Offset: 0x000810C5
	private void Awake()
	{
		MapMoveTips.Inst = this;
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x00082ED0 File Offset: 0x000810D0
	public static void Show()
	{
		if (MapMoveTips.Inst == null)
		{
			ResManager.inst.LoadPrefab("MoveTips").Inst(NewUICanvas.Inst.transform);
			MapMoveTips.Inst.transform.SetAsLastSibling();
		}
		MapMoveTips.Inst.gameObject.SetActive(true);
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x00082F28 File Offset: 0x00081128
	public static void Hide()
	{
		if (MapMoveTips.Inst == null)
		{
			ResManager.inst.LoadPrefab("MoveTips").Inst(NewUICanvas.Inst.transform);
			MapMoveTips.Inst.transform.SetAsLastSibling();
		}
		MapMoveTips.Inst.gameObject.SetActive(false);
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x00082F80 File Offset: 0x00081180
	private void OnDestroy()
	{
		MapMoveTips.Inst = null;
	}

	// Token: 0x04000F1C RID: 3868
	public static MapMoveTips Inst;
}
