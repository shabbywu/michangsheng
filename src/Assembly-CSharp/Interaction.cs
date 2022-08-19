using System;
using UnityEngine;

// Token: 0x02000508 RID: 1288
public class Interaction : MonoBehaviour
{
	// Token: 0x06002972 RID: 10610 RVA: 0x0013D050 File Offset: 0x0013B250
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

	// Token: 0x06002973 RID: 10611 RVA: 0x0013D216 File Offset: 0x0013B416
	private void ActiveCardButton(bool canReject)
	{
		this.play.SetActive(true);
		this.disard.SetActive(true);
		this.disard.GetComponent<UIButton>().isEnabled = canReject;
	}

	// Token: 0x06002974 RID: 10612 RVA: 0x0013D241 File Offset: 0x0013B441
	public void DealCallBack()
	{
		this.controller.DealCards();
		this.grab.SetActive(true);
		this.disgrab.SetActive(true);
		this.deal.SetActive(false);
	}

	// Token: 0x06002975 RID: 10613 RVA: 0x0013D272 File Offset: 0x0013B472
	private void PlayCallBack()
	{
		if (GameObject.Find("Player").GetComponent<PlayCard>().CheckSelectCards())
		{
			this.play.SetActive(false);
			this.disard.SetActive(false);
		}
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x0013D2A2 File Offset: 0x0013B4A2
	private void DiscardCallBack()
	{
		OrderController.Instance.Turn();
		this.play.SetActive(false);
		this.disard.SetActive(false);
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x0013D2C6 File Offset: 0x0013B4C6
	private void GrabLordCallBack()
	{
		this.controller.CardsOnTable(CharacterType.Player);
		OrderController.Instance.Init(CharacterType.Player);
		this.grab.SetActive(false);
		this.disgrab.SetActive(false);
	}

	// Token: 0x06002978 RID: 10616 RVA: 0x0013D2F8 File Offset: 0x0013B4F8
	private void DisgrabLordCallBack()
	{
		int type = Random.Range(2, 4);
		this.controller.CardsOnTable((CharacterType)type);
		OrderController.Instance.Init((CharacterType)type);
		this.grab.SetActive(false);
		this.disgrab.SetActive(false);
	}

	// Token: 0x040025D9 RID: 9689
	private GameObject deal;

	// Token: 0x040025DA RID: 9690
	private GameObject play;

	// Token: 0x040025DB RID: 9691
	private GameObject disard;

	// Token: 0x040025DC RID: 9692
	private GameObject grab;

	// Token: 0x040025DD RID: 9693
	private GameObject disgrab;

	// Token: 0x040025DE RID: 9694
	private GameController controller;
}
