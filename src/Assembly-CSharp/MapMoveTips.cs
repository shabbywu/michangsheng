using System;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class MapMoveTips : MonoBehaviour
{
	// Token: 0x060016F4 RID: 5876 RVA: 0x0001453C File Offset: 0x0001273C
	private void Awake()
	{
		MapMoveTips.Inst = this;
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x000CBEC0 File Offset: 0x000CA0C0
	public static void Show()
	{
		if (MapMoveTips.Inst == null)
		{
			ResManager.inst.LoadPrefab("MoveTips").Inst(NewUICanvas.Inst.transform);
			MapMoveTips.Inst.transform.SetAsLastSibling();
		}
		MapMoveTips.Inst.gameObject.SetActive(true);
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x000CBF18 File Offset: 0x000CA118
	public static void Hide()
	{
		if (MapMoveTips.Inst == null)
		{
			ResManager.inst.LoadPrefab("MoveTips").Inst(NewUICanvas.Inst.transform);
			MapMoveTips.Inst.transform.SetAsLastSibling();
		}
		MapMoveTips.Inst.gameObject.SetActive(false);
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x00014544 File Offset: 0x00012744
	private void OnDestroy()
	{
		MapMoveTips.Inst = null;
	}

	// Token: 0x0400125A RID: 4698
	public static MapMoveTips Inst;
}
