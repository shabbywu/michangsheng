using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200038D RID: 909
public class UINPCLeftList : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06001994 RID: 6548 RVA: 0x0001600B File Offset: 0x0001420B
	public static bool HasNPC
	{
		get
		{
			return UINPCJiaoHu.Inst.NPCIDList.Count + UINPCJiaoHu.Inst.TNPCIDList.Count + UINPCJiaoHu.Inst.SeaNPCEventIDList.Count > 0;
		}
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x0001603F File Offset: 0x0001423F
	private void Awake()
	{
		UINPCLeftList.Inst = this;
		this.OpenButton.mouseUpEvent.AddListener(new UnityAction(this.OnOpenButtonClick));
		this.OpenMoreButton.mouseUpEvent.AddListener(new UnityAction(this.OnOpenMoreButtonClick));
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x0001607F File Offset: 0x0001427F
	private void Update()
	{
		if (this.needRefresh)
		{
			this.RefreshNPC();
		}
		if (this.needToLeft)
		{
			this.ToLeft();
		}
		if (this.needToRight)
		{
			this.ToRight();
		}
		this.AutoHide();
	}

	// Token: 0x06001997 RID: 6551 RVA: 0x000E33E0 File Offset: 0x000E15E0
	public void SetNowJiaoHu(UINPCSVItem npcitem)
	{
		UINPCJiaoHu.Inst.NowJiaoHuNPC = npcitem.NPCData;
		UINPCSVItem.NowSelectedUINPCSVItem = npcitem;
		if (UINPCJiaoHu.Inst.JiaoHuPop.gameObject.activeInHierarchy)
		{
			npcitem.Selected.SetActive(true);
			npcitem.BG.sprite = npcitem.SelectedBG;
		}
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x000E3438 File Offset: 0x000E1638
	public void RefreshNPC()
	{
		this.needRefresh = false;
		this.SVy = this.SVTransform.anchoredPosition.y;
		this.CountText.text = (UINPCJiaoHu.Inst.NPCIDList.Count + UINPCJiaoHu.Inst.TNPCIDList.Count + UINPCJiaoHu.Inst.SeaNPCEventIDList.Count).ToString();
		if (this.SVTransform.childCount > 0)
		{
			this.SVTransform.DestoryAllChild();
		}
		this.sNpcList.Clear();
		this.rNpcList.Clear();
		this.npcDict.Clear();
		foreach (int num in UINPCJiaoHu.Inst.TNPCIDList)
		{
			if (!this.npcDict.ContainsKey(num))
			{
				UINPCData uinpcdata = new UINPCData(num, true);
				if (!uinpcdata.IsException)
				{
					UINPCSVItem component = Object.Instantiate<GameObject>(this.SVItemPrefab, this.SVTransform).GetComponent<UINPCSVItem>();
					try
					{
						component.NPCData = uinpcdata;
					}
					catch
					{
						Object.Destroy(component.gameObject);
						continue;
					}
					this.sNpcList.Add(component);
					this.npcDict.Add(num, component);
					if (UINPCJiaoHu.Inst.NowJiaoHuNPC != null && UINPCJiaoHu.Inst.NowJiaoHuNPC.ID == num)
					{
						this.SetNowJiaoHu(component);
					}
				}
			}
		}
		foreach (int num2 in UINPCJiaoHu.Inst.NPCIDList)
		{
			if (!this.npcDict.ContainsKey(num2))
			{
				UINPCData uinpcdata2 = new UINPCData(num2, false);
				if (!uinpcdata2.IsException)
				{
					UINPCSVItem component2 = Object.Instantiate<GameObject>(this.SVItemPrefab, this.SVTransform).GetComponent<UINPCSVItem>();
					try
					{
						component2.NPCData = uinpcdata2;
					}
					catch
					{
						Object.Destroy(component2.gameObject);
						continue;
					}
					if (num2 < 20000)
					{
						this.sNpcList.Add(component2);
					}
					else
					{
						this.rNpcList.Add(component2);
					}
					this.npcDict.Add(num2, component2);
					if (UINPCJiaoHu.Inst.NowJiaoHuNPC != null && UINPCJiaoHu.Inst.NowJiaoHuNPC.ID == num2)
					{
						this.SetNowJiaoHu(component2);
					}
				}
			}
		}
		for (int i = 0; i < UINPCJiaoHu.Inst.SeaNPCIDList.Count; i++)
		{
			int num3 = UINPCJiaoHu.Inst.SeaNPCIDList[i];
			if (!this.npcDict.ContainsKey(num3))
			{
				UINPCData uinpcdata3 = new UINPCData(num3, false);
				if (!uinpcdata3.IsException)
				{
					UINPCSVItem component3 = Object.Instantiate<GameObject>(this.SVItemPrefab, this.SVTransform).GetComponent<UINPCSVItem>();
					try
					{
						component3.NPCData = uinpcdata3;
					}
					catch
					{
						Object.Destroy(component3.gameObject);
						goto IL_38C;
					}
					component3.NPCData.UUID = UINPCJiaoHu.Inst.SeaNPCUUIDList[i];
					component3.NPCData.IsSeaNPC = true;
					component3.NPCData.SeaEventID = UINPCJiaoHu.Inst.SeaNPCEventIDList[i];
					if (UINPCJiaoHu.Inst.SeaNPCIDList[i] < 20000)
					{
						this.sNpcList.Add(component3);
					}
					else
					{
						this.rNpcList.Add(component3);
					}
					this.npcDict.Add(UINPCJiaoHu.Inst.SeaNPCIDList[i], component3);
					if (UINPCJiaoHu.Inst.NowJiaoHuNPC != null && UINPCJiaoHu.Inst.NowJiaoHuNPC.ID == num3)
					{
						this.SetNowJiaoHu(component3);
					}
				}
			}
			IL_38C:;
		}
		if (UINPCLeftList.HasNPC)
		{
			this.sNpcList.Sort();
			this.rNpcList.Sort();
			foreach (UINPCSVItem uinpcsvitem in this.sNpcList)
			{
				uinpcsvitem.transform.SetAsLastSibling();
			}
			foreach (UINPCSVItem uinpcsvitem2 in this.rNpcList)
			{
				uinpcsvitem2.transform.SetAsLastSibling();
			}
			this.ToRight();
			if (this.lastRefreshTime == PlayerEx.Player.worldTimeMag.nowTime)
			{
				base.Invoke("SetSVY", (float)this.SVTransform.childCount * 0.01f);
			}
		}
		if (UINPCJiaoHu.Inst.QingJiao.gameObject.activeInHierarchy)
		{
			UINPCJiaoHu.Inst.QingJiao.RefreshUI();
		}
		this.lastRefreshTime = PlayerEx.Player.worldTimeMag.nowTime;
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x000160B1 File Offset: 0x000142B1
	public void SetSVY()
	{
		this.SVTransform.anchoredPosition = new Vector2(this.SVTransform.anchoredPosition.x, this.SVy);
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x000E3950 File Offset: 0x000E1B50
	public bool CanShow()
	{
		return !UINPCLeftList.ShoudHide && !UINPCJiaoHu.AllShouldHide && UINPCLeftList.HasNPC && (!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject == null)) && (!(PanelMamager.inst != null) || PanelMamager.inst.nowPanel == PanelMamager.PanelType.空) && NpcJieSuanManager.inst.isCanJieSuan;
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x000160D9 File Offset: 0x000142D9
	private void AutoHide()
	{
		if (!this.CanShow())
		{
			base.gameObject.SetActive(false);
			this.IsMouseInUI = false;
		}
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x000160F6 File Offset: 0x000142F6
	public void OnOpenButtonClick()
	{
		if (this.nowLeft)
		{
			this.ToRight();
			return;
		}
		this.ToLeft();
	}

	// Token: 0x0600199D RID: 6557 RVA: 0x000042DD File Offset: 0x000024DD
	public void OnOpenMoreButtonClick()
	{
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x000E39C0 File Offset: 0x000E1BC0
	public void ToLeft()
	{
		if (!this.nowIsAniming)
		{
			this.nowIsAniming = true;
			this.nowLeft = true;
			this.needToLeft = false;
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = ShortcutExtensions.DOMoveX(base.transform, this.LeftPos.position.x, this.animTime, false);
			tweenerCore.onComplete = (TweenCallback)Delegate.Combine(tweenerCore.onComplete, new TweenCallback(delegate()
			{
				this.BGTransform.gameObject.SetActive(false);
				this.OpenMoreButton.gameObject.SetActive(false);
				this.nowIsAniming = false;
			}));
			ShortcutExtensions.DOLocalRotate(this.OpenButton.transform, new Vector3(0f, 0f, 180f), this.animTime, 0);
			return;
		}
		this.needToLeft = true;
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x000E3A64 File Offset: 0x000E1C64
	public void ToRight()
	{
		if (!this.nowIsAniming)
		{
			this.nowIsAniming = true;
			this.nowLeft = false;
			this.needToRight = false;
			this.BGTransform.gameObject.SetActive(true);
			this.OpenMoreButton.gameObject.SetActive(false);
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = ShortcutExtensions.DOMoveX(base.transform, this.RightPos.position.x, this.animTime, false);
			tweenerCore.onComplete = (TweenCallback)Delegate.Combine(tweenerCore.onComplete, new TweenCallback(delegate()
			{
				this.nowIsAniming = false;
			}));
			ShortcutExtensions.DOLocalRotate(this.OpenButton.transform, Vector3.zero, this.animTime, 0);
			return;
		}
		this.needToRight = true;
	}

	// Token: 0x060019A0 RID: 6560 RVA: 0x0001610D File Offset: 0x0001430D
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.IsMouseInUI = true;
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x00016116 File Offset: 0x00014316
	public void OnPointerExit(PointerEventData eventData)
	{
		this.IsMouseInUI = false;
	}

	// Token: 0x040014BC RID: 5308
	public static bool ShoudHide;

	// Token: 0x040014BD RID: 5309
	public static UINPCLeftList Inst;

	// Token: 0x040014BE RID: 5310
	[HideInInspector]
	public bool IsMouseInUI;

	// Token: 0x040014BF RID: 5311
	public Text CountText;

	// Token: 0x040014C0 RID: 5312
	public FpBtn OpenButton;

	// Token: 0x040014C1 RID: 5313
	public FpBtn OpenMoreButton;

	// Token: 0x040014C2 RID: 5314
	public Transform BGTransform;

	// Token: 0x040014C3 RID: 5315
	public RectTransform SVTransform;

	// Token: 0x040014C4 RID: 5316
	public GameObject SVItemPrefab;

	// Token: 0x040014C5 RID: 5317
	[HideInInspector]
	public bool nowLeft;

	// Token: 0x040014C6 RID: 5318
	private float animTime = 0.3f;

	// Token: 0x040014C7 RID: 5319
	private bool nowIsAniming;

	// Token: 0x040014C8 RID: 5320
	public RectTransform LeftPos;

	// Token: 0x040014C9 RID: 5321
	public RectTransform RightPos;

	// Token: 0x040014CA RID: 5322
	public bool needRefresh;

	// Token: 0x040014CB RID: 5323
	private string lastRefreshTime = "";

	// Token: 0x040014CC RID: 5324
	private float SVy;

	// Token: 0x040014CD RID: 5325
	private List<UINPCSVItem> rNpcList = new List<UINPCSVItem>();

	// Token: 0x040014CE RID: 5326
	private List<UINPCSVItem> sNpcList = new List<UINPCSVItem>();

	// Token: 0x040014CF RID: 5327
	private Dictionary<int, UINPCSVItem> npcDict = new Dictionary<int, UINPCSVItem>();

	// Token: 0x040014D0 RID: 5328
	private bool needToLeft;

	// Token: 0x040014D1 RID: 5329
	private bool needToRight;
}
