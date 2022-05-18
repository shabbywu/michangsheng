using System;
using UnityEngine;

// Token: 0x02000799 RID: 1945
public class Interaction : MonoBehaviour
{
	// Token: 0x06003177 RID: 12663 RVA: 0x0018A4B0 File Offset: 0x001886B0
	private void Start()
	{
		this.deal = base.gameObject.transform.Find("DealBtn").gameObject;
		this.play = base.gameObject.transform.Find("PlayBtn").gameObject;
		this.disard = base.gameObject.transform.Find("DiscardBtn").gameObject;
		this.grab = base.gameObject.transform.Find("GrabBtn").gameObject;
		this.disgrab = base.gameObject.transform.Find("DisgrabBtn").gameObject;
		this.controller = GameObject.Find("GameController").GetComponent<GameController>();
		this.deal.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.DealCallBack)));
		this.play.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.PlayCallBack)));
		this.disard.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.DiscardCallBack)));
		this.grab.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.GrabLordCallBack)));
		this.disgrab.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.DisgrabLordCallBack)));
		OrderController.Instance.activeButton += this.ActiveCardButton;
		this.play.SetActive(false);
		this.disard.SetActive(false);
		this.grab.SetActive(false);
		this.disgrab.SetActive(false);
	}

	// Token: 0x06003178 RID: 12664 RVA: 0x000242CA File Offset: 0x000224CA
	private void ActiveCardButton(bool canReject)
	{
		this.play.SetActive(true);
		this.disard.SetActive(true);
		this.disard.GetComponent<UIButton>().isEnabled = canReject;
	}

	// Token: 0x06003179 RID: 12665 RVA: 0x000242F5 File Offset: 0x000224F5
	public void DealCallBack()
	{
		this.controller.DealCards();
		this.grab.SetActive(true);
		this.disgrab.SetActive(true);
		this.deal.SetActive(false);
	}

	// Token: 0x0600317A RID: 12666 RVA: 0x00024326 File Offset: 0x00022526
	private void PlayCallBack()
	{
		if (GameObject.Find("Player").GetComponent<PlayCard>().CheckSelectCards())
		{
			this.play.SetActive(false);
			this.disard.SetActive(false);
		}
	}

	// Token: 0x0600317B RID: 12667 RVA: 0x00024356 File Offset: 0x00022556
	private void DiscardCallBack()
	{
		OrderController.Instance.Turn();
		this.play.SetActive(false);
		this.disard.SetActive(false);
	}

	// Token: 0x0600317C RID: 12668 RVA: 0x0002437A File Offset: 0x0002257A
	private void GrabLordCallBack()
	{
		this.controller.CardsOnTable(CharacterType.Player);
		OrderController.Instance.Init(CharacterType.Player);
		this.grab.SetActive(false);
		this.disgrab.SetActive(false);
	}

	// Token: 0x0600317D RID: 12669 RVA: 0x0018A678 File Offset: 0x00188878
	private void DisgrabLordCallBack()
	{
		int type = Random.Range(2, 4);
		this.controller.CardsOnTable((CharacterType)type);
		OrderController.Instance.Init((CharacterType)type);
		this.grab.SetActive(false);
		this.disgrab.SetActive(false);
	}

	// Token: 0x04002DC1 RID: 11713
	private GameObject deal;

	// Token: 0x04002DC2 RID: 11714
	private GameObject play;

	// Token: 0x04002DC3 RID: 11715
	private GameObject disard;

	// Token: 0x04002DC4 RID: 11716
	private GameObject grab;

	// Token: 0x04002DC5 RID: 11717
	private GameObject disgrab;

	// Token: 0x04002DC6 RID: 11718
	private GameController controller;
}
