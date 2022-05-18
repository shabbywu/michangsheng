using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000523 RID: 1315
public class ThreeSceneMagFab : MonoBehaviour
{
	// Token: 0x060021C0 RID: 8640 RVA: 0x00118F94 File Offset: 0x00117194
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

	// Token: 0x04001D3C RID: 7484
	public static ThreeSceneMagFab inst;
}
