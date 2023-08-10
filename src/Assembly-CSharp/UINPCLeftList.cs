using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UINPCLeftList : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public static bool ShoudHide;

	public static UINPCLeftList Inst;

	[HideInInspector]
	public bool IsMouseInUI;

	public Text CountText;

	public FpBtn OpenButton;

	public FpBtn OpenMoreButton;

	public Transform BGTransform;

	public RectTransform SVTransform;

	public GameObject SVItemPrefab;

	[HideInInspector]
	public bool nowLeft;

	private float animTime = 0.3f;

	private bool nowIsAniming;

	public RectTransform LeftPos;

	public RectTransform RightPos;

	public bool needRefresh;

	private string lastRefreshTime = "";

	private float SVy;

	private List<UINPCSVItem> rNpcList = new List<UINPCSVItem>();

	private List<UINPCSVItem> sNpcList = new List<UINPCSVItem>();

	private Dictionary<int, UINPCSVItem> npcDict = new Dictionary<int, UINPCSVItem>();

	private bool needToLeft;

	private bool needToRight;

	public static bool HasNPC => UINPCJiaoHu.Inst.NPCIDList.Count + UINPCJiaoHu.Inst.TNPCIDList.Count + UINPCJiaoHu.Inst.SeaNPCEventIDList.Count > 0;

	private void Awake()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		Inst = this;
		OpenButton.mouseUpEvent.AddListener(new UnityAction(OnOpenButtonClick));
		OpenMoreButton.mouseUpEvent.AddListener(new UnityAction(OnOpenMoreButtonClick));
	}

	private void Update()
	{
		if (needRefresh)
		{
			RefreshNPC();
		}
		if (needToLeft)
		{
			ToLeft();
		}
		if (needToRight)
		{
			ToRight();
		}
		AutoHide();
	}

	public void SetNowJiaoHu(UINPCSVItem npcitem)
	{
		UINPCJiaoHu.Inst.NowJiaoHuNPC = npcitem.NPCData;
		UINPCSVItem.NowSelectedUINPCSVItem = npcitem;
		if (((Component)UINPCJiaoHu.Inst.JiaoHuPop).gameObject.activeInHierarchy)
		{
			npcitem.Selected.SetActive(true);
			npcitem.BG.sprite = npcitem.SelectedBG;
		}
	}

	public void RefreshNPC()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		needRefresh = false;
		SVy = SVTransform.anchoredPosition.y;
		if (((Transform)SVTransform).childCount > 0)
		{
			((Transform)(object)SVTransform).DestoryAllChild();
		}
		sNpcList.Clear();
		rNpcList.Clear();
		npcDict.Clear();
		CountText.text = (UINPCJiaoHu.Inst.NPCIDList.Count + UINPCJiaoHu.Inst.TNPCIDList.Count + UINPCJiaoHu.Inst.SeaNPCEventIDList.Count).ToString();
		foreach (int tNPCID in UINPCJiaoHu.Inst.TNPCIDList)
		{
			if (npcDict.ContainsKey(tNPCID))
			{
				continue;
			}
			UINPCData uINPCData = new UINPCData(tNPCID, isThreeSceneNPC: true);
			if (!uINPCData.IsException)
			{
				UINPCSVItem component = Object.Instantiate<GameObject>(SVItemPrefab, (Transform)(object)SVTransform).GetComponent<UINPCSVItem>();
				try
				{
					component.NPCData = uINPCData;
				}
				catch
				{
					Object.Destroy((Object)(object)((Component)component).gameObject);
					continue;
				}
				sNpcList.Add(component);
				npcDict.Add(tNPCID, component);
				if (UINPCJiaoHu.Inst.NowJiaoHuNPC != null && UINPCJiaoHu.Inst.NowJiaoHuNPC.ID == tNPCID)
				{
					SetNowJiaoHu(component);
				}
			}
		}
		foreach (int nPCID in UINPCJiaoHu.Inst.NPCIDList)
		{
			if (npcDict.ContainsKey(nPCID))
			{
				continue;
			}
			UINPCData uINPCData2 = new UINPCData(nPCID);
			if (!uINPCData2.IsException)
			{
				UINPCSVItem component2 = Object.Instantiate<GameObject>(SVItemPrefab, (Transform)(object)SVTransform).GetComponent<UINPCSVItem>();
				try
				{
					component2.NPCData = uINPCData2;
				}
				catch
				{
					Object.Destroy((Object)(object)((Component)component2).gameObject);
					continue;
				}
				if (nPCID < 20000)
				{
					sNpcList.Add(component2);
				}
				else
				{
					rNpcList.Add(component2);
				}
				npcDict.Add(nPCID, component2);
				if (UINPCJiaoHu.Inst.NowJiaoHuNPC != null && UINPCJiaoHu.Inst.NowJiaoHuNPC.ID == nPCID)
				{
					SetNowJiaoHu(component2);
				}
			}
		}
		for (int i = 0; i < UINPCJiaoHu.Inst.SeaNPCIDList.Count; i++)
		{
			int num = UINPCJiaoHu.Inst.SeaNPCIDList[i];
			if (npcDict.ContainsKey(num))
			{
				continue;
			}
			UINPCData uINPCData3 = new UINPCData(num);
			if (!uINPCData3.IsException)
			{
				UINPCSVItem component3 = Object.Instantiate<GameObject>(SVItemPrefab, (Transform)(object)SVTransform).GetComponent<UINPCSVItem>();
				try
				{
					component3.NPCData = uINPCData3;
				}
				catch
				{
					Object.Destroy((Object)(object)((Component)component3).gameObject);
					continue;
				}
				component3.NPCData.UUID = UINPCJiaoHu.Inst.SeaNPCUUIDList[i];
				component3.NPCData.IsSeaNPC = true;
				component3.NPCData.SeaEventID = UINPCJiaoHu.Inst.SeaNPCEventIDList[i];
				if (UINPCJiaoHu.Inst.SeaNPCIDList[i] < 20000)
				{
					sNpcList.Add(component3);
				}
				else
				{
					rNpcList.Add(component3);
				}
				npcDict.Add(UINPCJiaoHu.Inst.SeaNPCIDList[i], component3);
				if (UINPCJiaoHu.Inst.NowJiaoHuNPC != null && UINPCJiaoHu.Inst.NowJiaoHuNPC.ID == num)
				{
					SetNowJiaoHu(component3);
				}
			}
		}
		if (HasNPC)
		{
			sNpcList.Sort();
			rNpcList.Sort();
			foreach (UINPCSVItem sNpc in sNpcList)
			{
				((Component)sNpc).transform.SetAsLastSibling();
			}
			foreach (UINPCSVItem rNpc in rNpcList)
			{
				((Component)rNpc).transform.SetAsLastSibling();
			}
			ToRight();
			if (lastRefreshTime == PlayerEx.Player.worldTimeMag.nowTime)
			{
				((MonoBehaviour)this).Invoke("SetSVY", (float)((Transform)SVTransform).childCount * 0.01f);
			}
		}
		if (((Component)UINPCJiaoHu.Inst.QingJiao).gameObject.activeInHierarchy)
		{
			UINPCJiaoHu.Inst.QingJiao.RefreshUI();
		}
		lastRefreshTime = PlayerEx.Player.worldTimeMag.nowTime;
	}

	public void SetSVY()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		SVTransform.anchoredPosition = new Vector2(SVTransform.anchoredPosition.x, SVy);
	}

	public bool CanShow()
	{
		if (ShoudHide || UINPCJiaoHu.AllShouldHide || !HasNPC)
		{
			return false;
		}
		if ((Object)(object)PanelMamager.inst != (Object)null && (Object)(object)PanelMamager.inst.UISceneGameObject == (Object)null)
		{
			return false;
		}
		if ((Object)(object)PanelMamager.inst != (Object)null && PanelMamager.inst.nowPanel != PanelMamager.PanelType.空)
		{
			return false;
		}
		if (!NpcJieSuanManager.inst.isCanJieSuan)
		{
			return false;
		}
		return true;
	}

	private void AutoHide()
	{
		if (!CanShow())
		{
			((Component)this).gameObject.SetActive(false);
			IsMouseInUI = false;
		}
	}

	public void OnOpenButtonClick()
	{
		if (nowLeft)
		{
			ToRight();
		}
		else
		{
			ToLeft();
		}
	}

	public void OnOpenMoreButtonClick()
	{
	}

	public void ToLeft()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (!nowIsAniming)
		{
			nowIsAniming = true;
			nowLeft = true;
			needToLeft = false;
			TweenerCore<Vector3, Vector3, VectorOptions> obj = ShortcutExtensions.DOMoveX(((Component)this).transform, ((Transform)LeftPos).position.x, animTime, false);
			((Tween)obj).onComplete = (TweenCallback)Delegate.Combine((Delegate?)(object)((Tween)obj).onComplete, (Delegate?)(TweenCallback)delegate
			{
				((Component)BGTransform).gameObject.SetActive(false);
				((Component)OpenMoreButton).gameObject.SetActive(false);
				nowIsAniming = false;
			});
			ShortcutExtensions.DOLocalRotate(((Component)OpenButton).transform, new Vector3(0f, 0f, 180f), animTime, (RotateMode)0);
		}
		else
		{
			needToLeft = true;
		}
	}

	public void ToRight()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		if (!nowIsAniming)
		{
			nowIsAniming = true;
			nowLeft = false;
			needToRight = false;
			((Component)BGTransform).gameObject.SetActive(true);
			((Component)OpenMoreButton).gameObject.SetActive(false);
			TweenerCore<Vector3, Vector3, VectorOptions> obj = ShortcutExtensions.DOMoveX(((Component)this).transform, ((Transform)RightPos).position.x, animTime, false);
			((Tween)obj).onComplete = (TweenCallback)Delegate.Combine((Delegate?)(object)((Tween)obj).onComplete, (Delegate?)(TweenCallback)delegate
			{
				nowIsAniming = false;
			});
			ShortcutExtensions.DOLocalRotate(((Component)OpenButton).transform, Vector3.zero, animTime, (RotateMode)0);
		}
		else
		{
			needToRight = true;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		IsMouseInUI = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		IsMouseInUI = false;
	}
}
