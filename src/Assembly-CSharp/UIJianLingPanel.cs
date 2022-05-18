using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000422 RID: 1058
public class UIJianLingPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001C4B RID: 7243 RVA: 0x000FB0DC File Offset: 0x000F92DC
	private void Awake()
	{
		UIJianLingPanel.Inst = this;
		this.JiaoTanBtn.mouseUpEvent.AddListener(new UnityAction(this.OnJiaoTanBtnClick));
		this.XianSuoBtn.mouseUpEvent.AddListener(new UnityAction(this.OnXianSuoBtnClick));
		this.QingJiaoBtn.mouseUpEvent.AddListener(new UnityAction(this.OnQingJiaoBtnClick));
		this.LiKaiBtn.mouseUpEvent.AddListener(new UnityAction(this.OnLiKaiBtnClick));
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001C4C RID: 7244 RVA: 0x000FB16C File Offset: 0x000F936C
	public static void OpenPanel()
	{
		int num = GlobalValue.Get(692, "打开剑灵界面");
		if (num == 3 || num == 4)
		{
			NPCEx.OpenTalk(518);
			return;
		}
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingPanel"), NewUICanvas.Inst.Canvas.transform).transform.SetAsLastSibling();
	}

	// Token: 0x06001C4D RID: 7245 RVA: 0x00017A63 File Offset: 0x00015C63
	bool IESCClose.TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x06001C4E RID: 7246 RVA: 0x00017A6C File Offset: 0x00015C6C
	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x00017A84 File Offset: 0x00015C84
	public void OnJiaoTanBtnClick()
	{
		NPCEx.OpenTalk(518);
		this.CenterObj.SetActive(false);
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x00017A9C File Offset: 0x00015C9C
	public void OnJiaoTanEnd()
	{
		this.CenterObj.SetActive(true);
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x00017AAA File Offset: 0x00015CAA
	public void OnXianSuoBtnClick()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingXianSuoPanel"), NewUICanvas.Inst.Canvas.transform).transform.SetAsLastSibling();
		this.Close();
	}

	// Token: 0x06001C52 RID: 7250 RVA: 0x00017ADA File Offset: 0x00015CDA
	public void OnQingJiaoBtnClick()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingQingJiaoPanel"), NewUICanvas.Inst.Canvas.transform).transform.SetAsLastSibling();
		this.Close();
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x00017B0A File Offset: 0x00015D0A
	public void OnLiKaiBtnClick()
	{
		this.Close();
	}

	// Token: 0x04001843 RID: 6211
	public static UIJianLingPanel Inst;

	// Token: 0x04001844 RID: 6212
	public FpBtn JiaoTanBtn;

	// Token: 0x04001845 RID: 6213
	public FpBtn XianSuoBtn;

	// Token: 0x04001846 RID: 6214
	public FpBtn QingJiaoBtn;

	// Token: 0x04001847 RID: 6215
	public FpBtn LiKaiBtn;

	// Token: 0x04001848 RID: 6216
	public GameObject CenterObj;
}
