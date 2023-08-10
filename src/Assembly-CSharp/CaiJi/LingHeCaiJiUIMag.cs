using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace CaiJi;

public class LingHeCaiJiUIMag : MonoBehaviour, IESCClose
{
	public enum CaiJiState
	{
		开始 = 1,
		采集中,
		采集结束
	}

	public Dictionary<int, UIIconShow> MayGetItemDict;

	[SerializeField]
	private Transform MayGetItemList;

	[SerializeField]
	private Transform GetItemList;

	public CaiJiTimeSelect TimeSlider;

	[SerializeField]
	private Text Desc;

	[SerializeField]
	private Text BenDiLingQi;

	[SerializeField]
	private Text LingHeCount;

	[SerializeField]
	private Text TanChaNanDu;

	[HideInInspector]
	public int CostTime;

	[HideInInspector]
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

	public static LingHeCaiJiUIMag inst;

	private Avatar player;

	[HideInInspector]
	public int nowMapIndex;

	private LingHeCaiJiResult caiJiResult;

	private void Awake()
	{
		inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		player = Tools.instance.getPlayer();
		MayGetItemDict = new Dictionary<int, UIIconShow>();
	}

	public void OpenCaiJi(int mapIndex)
	{
		Init();
		nowMapIndex = mapIndex;
		LingHeCaiJi lingHeCaiJi = LingHeCaiJi.DataDict[mapIndex];
		BenDiLingQi.text = "本地灵气：<size=34>" + LingMaiPinJie.DataDict[lingHeCaiJi.ShouYiLv].ShouYiDesc + "</size>";
		int key = 1;
		for (int i = 1; i <= 5; i++)
		{
			if (lingHeCaiJi.LingHe >= LingMaiPinJie.DataDict[i].LingHeLv)
			{
				key = i;
			}
		}
		LingHeCount.text = "灵核数量：<size=34>" + LingMaiPinJie.DataDict[key].LingHeDesc + "</size>";
		int key2 = 1;
		for (int j = 1; j <= 5; j++)
		{
			if (lingHeCaiJi.ShengShiLimit >= LingMaiPinJie.DataDict[j].ShengShiLv)
			{
				key2 = j;
			}
		}
		TanChaNanDu.text = $"探查难度：<size=34>{LingMaiPinJie.DataDict[key2].ShengShiDesc}</size>（神识需求{lingHeCaiJi.ShengShiLimit}）";
		UIIconShow component = UIIconShow.Prefab.Inst(MayGetItemList).GetComponent<UIIconShow>();
		component.SetItem(10035);
		component.CanDrag = false;
		((Component)component).gameObject.SetActive(false);
		MayGetItemDict.Add(10035, component);
		UIIconShow component2 = UIIconShow.Prefab.Inst(MayGetItemList).GetComponent<UIIconShow>();
		component2.SetItem(10005);
		component2.CanDrag = false;
		((Component)component2).gameObject.SetActive(false);
		MayGetItemDict.Add(10005, component2);
		State = CaiJiState.开始;
		TimeSlider.Init(isLingHeCaiJi: true);
	}

	public void UpdateItemShow()
	{
		foreach (int key in MayGetItemDict.Keys)
		{
			((Component)MayGetItemDict[key]).gameObject.SetActive(true);
		}
	}

	private void Init()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		Tools.canClickFlag = false;
		PanelMamager.CanOpenOrClose = false;
	}

	public void StartCaiJi()
	{
		State = CaiJiState.采集中;
		Tools.ClearChild(GetItemList);
		caiJiResult = LingHeCaiJiManager.DoCaiJi(nowMapIndex, CostTime);
		((Component)TimeSlider).gameObject.SetActive(false);
		BtnPanel.SetActive(false);
		player.AddTime(0, caiJiResult.RealCostTime);
		CaiJingZhongPanel.ShowSlider();
		PlayeCaiJiAn();
		((Component)GetItemList).gameObject.SetActive(true);
	}

	private void PlayeCaiJiAn()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		GameObject animation = null;
		Animator ctr = null;
		animation = WaKuangMask;
		ctr = animation.GetComponent<Animator>();
		animation.SetActive(true);
		ctr.Play("Stop");
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
							CanvasGroup canvasGroup = animation.GetComponent<CanvasGroup>();
							DOTween.To((DOGetter<float>)(() => canvasGroup.alpha), (DOSetter<float>)delegate(float x)
							{
								canvasGroup.alpha = x;
							}, 1f, 0.2f);
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
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Expected O, but got Unknown
		if (caiJiResult.LingShiCount > 0)
		{
			UIIconShow component = UIIconShow.Prefab.Inst(GetItemList).GetComponent<UIIconShow>();
			component.SetItem(10035);
			component.Count = caiJiResult.LingShiCount;
			component.CanDrag = false;
			player.AddMoney(caiJiResult.LingShiCount);
		}
		if (caiJiResult.LingHeCount > 0)
		{
			UIIconShow component2 = UIIconShow.Prefab.Inst(GetItemList).GetComponent<UIIconShow>();
			component2.SetItem(10005);
			component2.Count = caiJiResult.LingHeCount;
			component2.CanDrag = false;
			player.addItem(10005, caiJiResult.LingHeCount, Tools.CreateItemSeid(10005));
		}
		int num = GlobalValue.Get(173, "LingHeCaiJiUIMag.CaiJiComplete 获取采矿时间");
		GlobalValue.Set(173, num + caiJiResult.RealCostTime, "LingHeCaiJiUIMag.CaiJiComplete 记录采矿时间");
		GameObject val = null;
		((Component)MayGetItemList).gameObject.SetActive(false);
		val = WaKuangMask;
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
		if (caiJiResult.HasTiaoZhan)
		{
			Debug.Log((object)"采集中遇到挑战，跳转到fungus");
			LingHeCaiJiManager.LingHeTiaoZhanArg = new LingHeTiaoZhanArg();
			LingHeCaiJiManager.LingHeTiaoZhanArg.LingMaiLv = LingHeCaiJi.DataDict[nowMapIndex].ShouYiLv;
			Close();
			LingHeCaiJiManager.IsOnTiaoZhan = true;
		}
	}

	public void Restart()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		GameObject animation = null;
		animation = WaKuangMask;
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
					animation.SetActive(false);
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
