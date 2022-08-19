using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002E6 RID: 742
public class PutDanLuManager : MonoBehaviour
{
	// Token: 0x060019D4 RID: 6612 RVA: 0x000B8E38 File Offset: 0x000B7038
	private void Awake()
	{
		this.putDanLuBtn.onClick.AddListener(new UnityAction(this.putDanLu));
	}

	// Token: 0x060019D5 RID: 6613 RVA: 0x000B8E56 File Offset: 0x000B7056
	public void putDanLu()
	{
		LianDanSystemManager.inst.lianDanPageManager.CanClick = false;
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.setCurSelectIndex(30);
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.openCaiLiaoPackge(9);
	}

	// Token: 0x060019D6 RID: 6614 RVA: 0x000B8E8A File Offset: 0x000B708A
	public void backPutDanLuPanel()
	{
		base.gameObject.SetActive(true);
		LianDanSystemManager.inst.lianDanPageManager.gameObject.SetActive(false);
	}

	// Token: 0x040014F2 RID: 5362
	[SerializeField]
	private Button putDanLuBtn;
}
