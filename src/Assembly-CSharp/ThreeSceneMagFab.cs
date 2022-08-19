using System;
using GUIPackage;
using UnityEngine;

// Token: 0x0200039A RID: 922
public class ThreeSceneMagFab : MonoBehaviour
{
	// Token: 0x06001E3F RID: 7743 RVA: 0x000D55B0 File Offset: 0x000D37B0
	private void Start()
	{
		ThreeSceneMagFab.inst = this;
		if (UI_Manager.inst != null)
		{
			base.transform.localPosition = new Vector3(0f, 0f, 0f);
			base.transform.localScale = Vector3.one;
			ThreeSceneMag component = base.gameObject.GetComponent<ThreeSceneMag>();
			component.CraftingList = UI_Manager.inst.CraftingList;
			component.xiala = UI_Manager.inst.xialian;
			component.exchangeUI = UI_Manager.inst.exchangeUI;
			component.init();
		}
	}

	// Token: 0x040018D3 RID: 6355
	public static ThreeSceneMagFab inst;
}
