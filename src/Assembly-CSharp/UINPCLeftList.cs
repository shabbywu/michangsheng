using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000274 RID: 628
public class UINPCLeftList : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x1700023C RID: 572
	// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0009BBB5 File Offset: 0x00099DB5
	public static bool HasNPC
	{
		get
		{
			return UINPCJiaoHu.Inst.NPCIDList.Count + UINPCJiaoHu.Inst.TNPCIDList.Count + UINPCJiaoHu.Inst.SeaNPCEventIDList.Count > 0;
		}
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x0009BBE9 File Offset: 0x00099DE9
	private void Awake()
	{
		UINPCLeftList.Inst = this;
		this.OpenButton.mouseUpEvent.AddListener(new UnityAction(this.OnOpenButtonClick));
		this.OpenMoreButton.mouseUpEvent.AddListener(new UnityAction(this.OnOpenMoreButtonClick));
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x0009BC29 File Offset: 0x00099E29
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

	// Token: 0x060016E5 RID: 5861 RVA: 0x0009BC5C File Offset: 0x00099E5C
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

	// Token: 0x060016E6 RID: 5862 RVA: 0x0009BCB4 File Offset: 0x00099EB4
	public void RefreshNPC()
	{
		this.needRefresh = false;
		this.SVy = this.SVTransform.anchoredPosition.y;
		if (this.SVTransform.childCount > 0)
		{
			this.SVTransform.DestoryAllChild();
		}
		this.sNpcList.Clear();
		this.rNpcList.Clear();
		this.npcDict.Clear();
		this.CountText.text = (UINPCJiaoHu.Inst.NPCIDList.Count + UINPCJiaoHu.Inst.TNPCIDList.Count + UINPCJiaoHu.Inst.SeaNPCEventIDList.Count).ToString();
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

	// Token: 0x060016E7 RID: 5863 RVA: 0x0009C1CC File Offset: 0x0009A3CC
	public void SetSVY()
	{
		this.SVTransform.anchoredPosition = new Vector2(this.SVTransform.anchoredPosition.x, this.SVy);
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x0009C1F4 File Offset: 0x0009A3F4
	public bool CanShow()
	{
		return !UINPCLeftList.ShoudHide && !UINPCJiaoHu.AllShouldHide && UINPCLeftList.HasNPC && (!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject == null)) && (!(PanelMamager.inst != null) || PanelMamager.inst.nowPanel == PanelMamager.PanelType.空) && NpcJieSuanManager.inst.isCanJieSuan;
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x0009C264 File Offset: 0x0009A464
	private void AutoHide()
	{
		if (!this.CanShow())
		{
			base.gameObject.SetActive(false);
			this.IsMouseInUI = false;
		}
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x0009C281 File Offset: 0x0009A481
	public void OnOpenButtonClick()
	{
		if (this.nowLeft)
		{
			this.ToRight();
			return;
		}
		this.ToLeft();
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x00004095 File Offset: 0x00002295
	public void OnOpenMoreButtonClick()
	{
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x0009C298 File Offset: 0x0009A498
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

	// Token: 0x060016ED RID: 5869 RVA: 0x0009C33C File Offset: 0x0009A53C
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

	// Token: 0x060016EE RID: 5870 RVA: 0x0009C3F3 File Offset: 0x0009A5F3
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.IsMouseInUI = true;
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x0009C3FC File Offset: 0x0009A5FC
	public void OnPointerExit(PointerEventData eventData)
	{
		this.IsMouseInUI = false;
	}

	// Token: 0x0400116B RID: 4459
	public static bool ShoudHide;

	// Token: 0x0400116C RID: 4460
	public static UINPCLeftList Inst;

	// Token: 0x0400116D RID: 4461
	[HideInInspector]
	public bool IsMouseInUI;

	// Token: 0x0400116E RID: 4462
	public Text CountText;

	// Token: 0x0400116F RID: 4463
	public FpBtn OpenButton;

	// Token: 0x04001170 RID: 4464
	public FpBtn OpenMoreButton;

	// Token: 0x04001171 RID: 4465
	public Transform BGTransform;

	// Token: 0x04001172 RID: 4466
	public RectTransform SVTransform;

	// Token: 0x04001173 RID: 4467
	public GameObject SVItemPrefab;

	// Token: 0x04001174 RID: 4468
	[HideInInspector]
	public bool nowLeft;

	// Token: 0x04001175 RID: 4469
	private float animTime = 0.3f;

	// Token: 0x04001176 RID: 4470
	private bool nowIsAniming;

	// Token: 0x04001177 RID: 4471
	public RectTransform LeftPos;

	// Token: 0x04001178 RID: 4472
	public RectTransform RightPos;

	// Token: 0x04001179 RID: 4473
	public bool needRefresh;

	// Token: 0x0400117A RID: 4474
	private string lastRefreshTime = "";

	// Token: 0x0400117B RID: 4475
	private float SVy;

	// Token: 0x0400117C RID: 4476
	private List<UINPCSVItem> rNpcList = new List<UINPCSVItem>();

	// Token: 0x0400117D RID: 4477
	private List<UINPCSVItem> sNpcList = new List<UINPCSVItem>();

	// Token: 0x0400117E RID: 4478
	private Dictionary<int, UINPCSVItem> npcDict = new Dictionary<int, UINPCSVItem>();

	// Token: 0x0400117F RID: 4479
	private bool needToLeft;

	// Token: 0x04001180 RID: 4480
	private bool needToRight;
}
