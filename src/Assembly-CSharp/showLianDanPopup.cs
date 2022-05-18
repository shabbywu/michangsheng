using System;
using UnityEngine;

// Token: 0x0200062A RID: 1578
public class showLianDanPopup : MonoBehaviour
{
	// Token: 0x06002734 RID: 10036 RVA: 0x0001F1CE File Offset: 0x0001D3CE
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x0013329C File Offset: 0x0013149C
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

	// Token: 0x06002736 RID: 10038 RVA: 0x0001F1F9 File Offset: 0x0001D3F9
	private void OnChange()
	{
		this.lianDanDanFang.showtype = this.getInputID(this.mList.value);
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x000042DD File Offset: 0x000024DD
	public void onchenge()
	{
	}

	// Token: 0x06002738 RID: 10040 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0400214E RID: 8526
	public LianDanDanFang lianDanDanFang;

	// Token: 0x0400214F RID: 8527
	private UIPopupList mList;
}
