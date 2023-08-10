using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame.TuJian;

namespace CaiJi;

public class CaiJiUIMag : MonoBehaviour, IESCClose
{
	public enum CaiJiType
	{
		采药 = 1,
		挖矿
	}

	public enum CaiJiState
	{
		开始 = 1,
		采集中,
		采集结束
	}

	public List<CaiJiImpSlot> ImportSlotList;

	public Dictionary<int, CaiJiCell> TotalItemDict;

	public Dictionary<int, UIIconShow> MayGetItemDict;

	public List<int> RandomQualit = new List<int> { 12, 10, 8, 6, 4, 2 };

	[SerializeField]
	private GameObject CaiJiItemCell;

	[SerializeField]
	private Transform TotalItemList;

	[SerializeField]
	private GameObject MayGetItemCell;

	[SerializeField]
	private Transform MayGetItemList;

	[SerializeField]
	private Transform GetItemList;

	public CaiJiTimeSelect TimeSlider;

	[SerializeField]
	private Text Desc;

	public int MaxNum;

	public int CurNum;

	public bool IsMax;

	public int CostTime;

	public int MayCanGetItemCount;

	public CaiJiType Type;

	public CaiJiState State;

	[SerializeField]
	private GameObject BtnPanel;

	[SerializeField]
	private FpBtn CloseBtn;

	[SerializeField]
	private FpBtn CaiJiBtn;

	[SerializeField]
	private FpBtn ContinueBtn;

	[SerializeField]
	private CaiJiZhong CaiJingZhongPanel;

	[SerializeField]
	private GameObject CaiJiMask;

	[SerializeField]
	private GameObject CaiJiMask2;

	[SerializeField]
	private GameObject WaKuangMask;

	[SerializeField]
	private GameObject CompleteBtnPanel;

	public static CaiJiUIMag inst;

	private Avatar player;

	private void Awake()
	{
		inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		player = Tools.instance.getPlayer();
		MessageMag.Instance.Register("CaiJi_Item_Select", SelectItem);
		MessageMag.Instance.Register("CaiJi_Item_Cancel", CancalSelectItem);
		MayGetItemDict = new Dictionary<int, UIIconShow>();
	}

	public void OpenCaiJi(int id)
	{
		Init();
		JSONObject jSONObject = jsonData.instance.CaiYaoDiaoLuo[id.ToString()];
		TuJianManager.Inst.UnlockMap(jSONObject["FuBen"].str);
		int i = jSONObject["type"].I;
		TotalItemDict = new Dictionary<int, CaiJiCell>();
		for (int j = 1; j <= 8; j++)
		{
			int i2 = jSONObject["value" + j].I;
			if (i2 > 0)
			{
				TotalItemDict.Add(i2, CaiJiItemCell.Inst(TotalItemList).GetComponent<CaiJiCell>().Init(i2));
				UIIconShow component = MayGetItemCell.Inst(MayGetItemList).GetComponent<UIIconShow>();
				component.SetItem(i2);
				component.CanDrag = false;
				((Component)component).gameObject.SetActive(false);
				MayGetItemDict.Add(i2, component);
			}
		}
		switch (i)
		{
		case 1:
			Type = CaiJiType.采药;
			break;
		case 2:
			Type = CaiJiType.挖矿;
			break;
		}
		State = CaiJiState.开始;
		TimeSlider.Init();
	}

	private void Init()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		IsMax = false;
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		Tools.canClickFlag = false;
		PanelMamager.CanOpenOrClose = false;
	}

	private void SelectItem(MessageData data)
	{
		CurNum++;
		GetNullSlot().PutItem(data.valueInt);
		if (CurNum == MaxNum)
		{
			IsMax = true;
		}
	}

	private void CancalSelectItem(MessageData data)
	{
		TotalItemDict[data.valueInt].Show();
		CurNum--;
		IsMax = false;
		if (CurNum < 0)
		{
			CurNum = 0;
		}
	}

	private CaiJiImpSlot GetNullSlot()
	{
		foreach (CaiJiImpSlot importSlot in ImportSlotList)
		{
			if (importSlot.IsNull)
			{
				return importSlot;
			}
		}
		return null;
	}

	public void UpdateMayGetItem()
	{
		int num = player.GetTianFuAddCaoYaoCaiJi(CaiYaoShoYi.DataDict[player.getLevelType()].QuanZhon[1]) * CostTime;
		MayCanGetItemCount = 0;
		foreach (int key in MayGetItemDict.Keys)
		{
			if (MayGetItemDict[key].tmpItem.itemPrice > num)
			{
				((Component)MayGetItemDict[key]).gameObject.SetActive(false);
				continue;
			}
			MayCanGetItemCount++;
			((Component)MayGetItemDict[key]).gameObject.SetActive(true);
		}
	}

	public void StartCaiJi()
	{
		if (MayCanGetItemCount == 0)
		{
			UIPopTip.Inst.Pop("没有可以采集到的物品");
			return;
		}
		State = CaiJiState.采集中;
		new List<int>();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (int key in MayGetItemDict.Keys)
		{
			if (((Component)MayGetItemDict[key]).gameObject.activeSelf)
			{
				int num = 0;
				num = ((!TotalItemDict[key].IsSelected) ? RandomQualit[TotalItemDict[key].Item.tmpItem.quality - 1] : (10 * RandomQualit[TotalItemDict[key].Item.tmpItem.quality - 1]));
				dictionary.Add(key, num);
			}
		}
		CaiYaoShoYi caiYaoShoYi = CaiYaoShoYi.DataDict[player.getLevelType()];
		int totalMoney = Tools.instance.GetRandomInt(player.GetTianFuAddCaoYaoCaiJi(caiYaoShoYi.QuanZhon[0]), player.GetTianFuAddCaoYaoCaiJi(caiYaoShoYi.QuanZhon[1])) * CostTime;
		Dictionary<int, int> caiJiItemList = GetCaiJiItemList(dictionary, totalMoney);
		Tools.ClearChild(GetItemList);
		foreach (int key2 in caiJiItemList.Keys)
		{
			UIIconShow component = MayGetItemCell.Inst(GetItemList).GetComponent<UIIconShow>();
			component.SetItem(key2);
			component.Count = caiJiItemList[key2];
			component.CanDrag = false;
			player.addItem(key2, caiJiItemList[key2], Tools.CreateItemSeid(key2));
		}
		((Component)TimeSlider).gameObject.SetActive(false);
		BtnPanel.SetActive(false);
		player.AddTime(0, CostTime);
		CaiJingZhongPanel.ShowSlider();
		PlayeCaiJiAn();
		((Component)GetItemList).gameObject.SetActive(true);
	}

	private Dictionary<int, int> GetCaiJiItemList(Dictionary<int, int> randomDict, int totalMoney)
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		while (randomDict.Count > 0)
		{
			num = GetRandomByQuanZhong(randomDict);
			num2 = TotalItemDict[num].Item.tmpItem.itemPrice;
			if (num2 <= totalMoney)
			{
				totalMoney -= num2;
				if (dictionary.ContainsKey(num))
				{
					dictionary[num]++;
				}
				else
				{
					dictionary.Add(num, 1);
				}
				continue;
			}
			randomDict.Remove(num);
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			foreach (int key in randomDict.Keys)
			{
				num3 = randomDict[key];
				dictionary2.Add(key, num3);
			}
			randomDict = dictionary2;
		}
		return dictionary;
	}

	private int GetRandomByQuanZhong(Dictionary<int, int> randomDict)
	{
		int num = 0;
		foreach (int key in randomDict.Keys)
		{
			num += randomDict[key];
		}
		int randomInt = Tools.instance.GetRandomInt(0, num);
		int num2 = 0;
		foreach (int key2 in randomDict.Keys)
		{
			num2 += randomDict[key2];
			if (num2 >= randomInt)
			{
				return key2;
			}
		}
		return 0;
	}

	private void PlayeCaiJiAn()
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		GameObject animation = null;
		Animator ctr = null;
		if (Type == CaiJiType.采药)
		{
			animation = CaiJiMask;
		}
		else if (Type == CaiJiType.挖矿)
		{
			animation = WaKuangMask;
		}
		ctr = animation.GetComponent<Animator>();
		ctr.Play("Stop");
		animation.SetActive(true);
		Transform child = ((Component)animation.transform.GetChild(0)).transform;
		TweenCallback val = default(TweenCallback);
		TweenCallback val2 = default(TweenCallback);
		TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(child, 14f, 0.34f, false), (TweenCallback)delegate
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			//IL_0034: Expected O, but got Unknown
			TweenerCore<Vector3, Vector3, VectorOptions> obj3 = ShortcutExtensions.DOLocalMoveY(child, 38f, 0.08f, false);
			TweenCallback obj4 = val;
			if (obj4 == null)
			{
				TweenCallback val6 = delegate
				{
					//IL_0028: Unknown result type (might be due to invalid IL or missing references)
					//IL_002d: Unknown result type (might be due to invalid IL or missing references)
					//IL_002f: Expected O, but got Unknown
					//IL_0034: Expected O, but got Unknown
					TweenerCore<Vector3, Vector3, VectorOptions> obj5 = ShortcutExtensions.DOLocalMoveY(child, 14f, 0.05f, false);
					TweenCallback obj6 = val2;
					if (obj6 == null)
					{
						TweenCallback val8 = delegate
						{
							//IL_004d: Unknown result type (might be due to invalid IL or missing references)
							//IL_0052: Unknown result type (might be due to invalid IL or missing references)
							if (Type == CaiJiType.采药)
							{
								animation.GetComponent<CanvasGroup>().alpha = 1f;
								DOTweenModuleUI.DOColor(((Component)animation.transform.GetChild(1)).GetComponent<Image>(), Color32.op_Implicit(new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue)), 0.2f);
							}
							else if (Type == CaiJiType.挖矿)
							{
								CanvasGroup canvasGroup = animation.GetComponent<CanvasGroup>();
								DOTween.To((DOGetter<float>)(() => canvasGroup.alpha), (DOSetter<float>)delegate(float x)
								{
									canvasGroup.alpha = x;
								}, 1f, 0.2f);
							}
							ctr.Play("Start");
						};
						TweenCallback val9 = val8;
						val2 = val8;
						obj6 = val9;
					}
					TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(obj5, obj6);
				};
				TweenCallback val7 = val6;
				val = val6;
				obj4 = val7;
			}
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(obj3, obj4);
		});
		CaiJiMask2.SetActive(true);
		Transform child2 = ((Component)CaiJiMask2.transform.GetChild(0)).transform;
		TweenCallback val3 = default(TweenCallback);
		TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(child2, 2f, 0.34f, false), (TweenCallback)delegate
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			//IL_0034: Expected O, but got Unknown
			TweenerCore<Vector3, Vector3, VectorOptions> obj = ShortcutExtensions.DOLocalMoveY(child2, 20f, 0.08f, false);
			TweenCallback obj2 = val3;
			if (obj2 == null)
			{
				TweenCallback val4 = delegate
				{
					ShortcutExtensions.DOLocalMoveY(child2, 2f, 0.05f, false);
				};
				TweenCallback val5 = val4;
				val3 = val4;
				obj2 = val5;
			}
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(obj, obj2);
		});
	}

	public void CaiJiComplete()
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		GameObject val = null;
		((Component)MayGetItemList).gameObject.SetActive(false);
		if (Type == CaiJiType.采药)
		{
			val = CaiJiMask;
		}
		else if (Type == CaiJiType.挖矿)
		{
			val = WaKuangMask;
		}
		CanvasGroup canvasGroup = val.GetComponent<CanvasGroup>();
		DOTween.To((DOGetter<float>)(() => canvasGroup.alpha), (DOSetter<float>)delegate(float x)
		{
			canvasGroup.alpha = x;
		}, 0f, 0.2f);
		TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(((Component)CaiJiMask2.transform.GetChild(0)).transform, 420f, 0.15f, false), (TweenCallback)delegate
		{
			CaiJiMask2.SetActive(false);
			Desc.text = "本次采集完成";
		});
		State = CaiJiState.采集结束;
		CompleteBtnPanel.SetActive(true);
	}

	public void Restart()
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		GameObject animation = null;
		if (Type == CaiJiType.采药)
		{
			animation = CaiJiMask;
		}
		else if (Type == CaiJiType.挖矿)
		{
			animation = WaKuangMask;
		}
		Transform child = ((Component)animation.transform.GetChild(0)).transform;
		CaiJiMask2.SetActive(true);
		Transform child2 = ((Component)CaiJiMask2.transform.GetChild(0)).transform;
		TweenCallback val = default(TweenCallback);
		TweenCallback val2 = default(TweenCallback);
		TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(child2, 2f, 0.2f, false), (TweenCallback)delegate
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			//IL_0060: Expected O, but got Unknown
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Expected O, but got Unknown
			//IL_009b: Expected O, but got Unknown
			((Component)MayGetItemList).gameObject.SetActive(true);
			((Component)GetItemList).gameObject.SetActive(false);
			TweenerCore<Vector3, Vector3, VectorOptions> obj = ShortcutExtensions.DOLocalMoveY(child2, 420f, 0.3f, false);
			TweenCallback obj2 = val;
			if (obj2 == null)
			{
				TweenCallback val3 = delegate
				{
					CaiJiMask2.SetActive(false);
				};
				TweenCallback val4 = val3;
				val = val3;
				obj2 = val4;
			}
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(obj, obj2);
			TweenerCore<Vector3, Vector3, VectorOptions> obj3 = ShortcutExtensions.DOLocalMoveY(child, 800f, 0.3f, false);
			TweenCallback obj4 = val2;
			if (obj4 == null)
			{
				TweenCallback val5 = delegate
				{
					//IL_0040: Unknown result type (might be due to invalid IL or missing references)
					//IL_0045: Unknown result type (might be due to invalid IL or missing references)
					animation.SetActive(false);
					if (Type == CaiJiType.采药)
					{
						((Graphic)((Component)animation.transform.GetChild(1)).GetComponent<Image>()).color = Color32.op_Implicit(new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)0));
					}
					BtnPanel.SetActive(true);
					State = CaiJiState.开始;
					((Component)TimeSlider).gameObject.SetActive(true);
					Desc.text = "本次可能采到的材料";
				};
				TweenCallback val4 = val5;
				val2 = val5;
				obj4 = val4;
			}
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(obj3, obj4);
		});
		CompleteBtnPanel.SetActive(false);
	}

	public void Close()
	{
		inst = null;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void OnDestroy()
	{
		MessageMag.Instance.Remove("CaiJi_Item_Select", SelectItem);
		MessageMag.Instance.Remove("CaiJi_Item_Cancel", CancalSelectItem);
		Tools.canClickFlag = true;
		PanelMamager.CanOpenOrClose = true;
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		if (State == CaiJiState.采集中)
		{
			return false;
		}
		Close();
		return true;
	}
}
