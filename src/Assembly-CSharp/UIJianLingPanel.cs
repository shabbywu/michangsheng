using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002D6 RID: 726
public class UIJianLingPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001943 RID: 6467 RVA: 0x000B5200 File Offset: 0x000B3400
	private void Awake()
	{
		UIJianLingPanel.Inst = this;
		this.JiaoTanBtn.mouseUpEvent.AddListener(new UnityAction(this.OnJiaoTanBtnClick));
		this.XianSuoBtn.mouseUpEvent.AddListener(new UnityAction(this.OnXianSuoBtnClick));
		this.QingJiaoBtn.mouseUpEvent.AddListener(new UnityAction(this.OnQingJiaoBtnClick));
		this.LiKaiBtn.mouseUpEvent.AddListener(new UnityAction(this.OnLiKaiBtnClick));
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001944 RID: 6468 RVA: 0x000B5290 File Offset: 0x000B3490
	private void Update()
	{
		if (UIHeadPanel.Inst != null && this.QingJiaoRedPoint.activeSelf != UIHeadPanel.Inst.TieJianRedPoint.activeSelf)
		{
			this.QingJiaoRedPoint.SetActive(UIHeadPanel.Inst.TieJianRedPoint.activeSelf);
		}
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x000B52E0 File Offset: 0x000B34E0
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

	// Token: 0x06001946 RID: 6470 RVA: 0x000B5338 File Offset: 0x000B3538
	bool IESCClose.TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x06001947 RID: 6471 RVA: 0x000B5341 File Offset: 0x000B3541
	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001948 RID: 6472 RVA: 0x000B5359 File Offset: 0x000B3559
	public void OnJiaoTanBtnClick()
	{
		NPCEx.OpenTalk(518);
		this.CenterObj.SetActive(false);
	}

	// Token: 0x06001949 RID: 6473 RVA: 0x000B5371 File Offset: 0x000B3571
	public void OnJiaoTanEnd()
	{
		this.CenterObj.SetActive(true);
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x000B537F File Offset: 0x000B357F
	public void OnXianSuoBtnClick()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingXianSuoPanel"), NewUICanvas.Inst.Canvas.transform).transform.SetAsLastSibling();
		this.Close();
	}

	// Token: 0x0600194B RID: 6475 RVA: 0x000B53AF File Offset: 0x000B35AF
	public void OnQingJiaoBtnClick()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingQingJiaoPanel"), NewUICanvas.Inst.Canvas.transform).transform.SetAsLastSibling();
		this.Close();
	}

	// Token: 0x0600194C RID: 6476 RVA: 0x000B53DF File Offset: 0x000B35DF
	public void OnLiKaiBtnClick()
	{
		this.Close();
	}

	// Token: 0x04001475 RID: 5237
	public static UIJianLingPanel Inst;

	// Token: 0x04001476 RID: 5238
	public FpBtn JiaoTanBtn;

	// Token: 0x04001477 RID: 5239
	public FpBtn XianSuoBtn;

	// Token: 0x04001478 RID: 5240
	public FpBtn QingJiaoBtn;

	// Token: 0x04001479 RID: 5241
	public FpBtn LiKaiBtn;

	// Token: 0x0400147A RID: 5242
	public GameObject CenterObj;

	// Token: 0x0400147B RID: 5243
	public GameObject QingJiaoRedPoint;
}
