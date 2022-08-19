using System;
using UnityEngine;

// Token: 0x0200046D RID: 1133
public class showLianDanPopup : MonoBehaviour
{
	// Token: 0x0600237B RID: 9083 RVA: 0x000F3111 File Offset: 0x000F1311
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x0600237C RID: 9084 RVA: 0x000F313C File Offset: 0x000F133C
	public int getInputID(string name)
	{
		int num = 0;
		foreach (string b in this.mList.items)
		{
			if (name == b)
			{
				break;
			}
			num++;
		}
		return num;
	}

	// Token: 0x0600237D RID: 9085 RVA: 0x000F31A0 File Offset: 0x000F13A0
	private void OnChange()
	{
		this.lianDanDanFang.showtype = this.getInputID(this.mList.value);
	}

	// Token: 0x0600237E RID: 9086 RVA: 0x00004095 File Offset: 0x00002295
	public void onchenge()
	{
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C76 RID: 7286
	public LianDanDanFang lianDanDanFang;

	// Token: 0x04001C77 RID: 7287
	private UIPopupList mList;
}
