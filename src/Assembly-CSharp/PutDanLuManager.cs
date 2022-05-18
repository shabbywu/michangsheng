using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000440 RID: 1088
public class PutDanLuManager : MonoBehaviour
{
	// Token: 0x06001CF6 RID: 7414 RVA: 0x00018288 File Offset: 0x00016488
	private void Awake()
	{
		this.putDanLuBtn.onClick.AddListener(new UnityAction(this.putDanLu));
	}

	// Token: 0x06001CF7 RID: 7415 RVA: 0x000182A6 File Offset: 0x000164A6
	public void putDanLu()
	{
		LianDanSystemManager.inst.lianDanPageManager.CanClick = false;
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.setCurSelectIndex(30);
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.openCaiLiaoPackge(9);
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x000182DA File Offset: 0x000164DA
	public void backPutDanLuPanel()
	{
		base.gameObject.SetActive(true);
		LianDanSystemManager.inst.lianDanPageManager.gameObject.SetActive(false);
	}

	// Token: 0x040018F6 RID: 6390
	[SerializeField]
	private Button putDanLuBtn;
}
