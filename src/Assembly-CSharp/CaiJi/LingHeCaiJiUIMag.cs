using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace CaiJi
{
	// Token: 0x0200073B RID: 1851
	public class LingHeCaiJiUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x06003AF4 RID: 15092 RVA: 0x00195696 File Offset: 0x00193896
		private void Awake()
		{
			LingHeCaiJiUIMag.inst = this;
			ESCCloseManager.Inst.RegisterClose(this);
			this.player = Tools.instance.getPlayer();
			this.MayGetItemDict = new Dictionary<int, UIIconShow>();
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x001956C4 File Offset: 0x001938C4
		public void OpenCaiJi(int mapIndex)
		{
			this.Init();
			this.nowMapIndex = mapIndex;
			LingHeCaiJi lingHeCaiJi = LingHeCaiJi.DataDict[mapIndex];
			this.BenDiLingQi.text = "本地灵气：<size=34>" + LingMaiPinJie.DataDict[lingHeCaiJi.ShouYiLv].ShouYiDesc + "</size>";
			int key = 1;
			for (int i = 1; i <= 5; i++)
			{
				if (lingHeCaiJi.LingHe >= LingMaiPinJie.DataDict[i].LingHeLv)
				{
					key = i;
				}
			}
			this.LingHeCount.text = "灵核数量：<size=34>" + LingMaiPinJie.DataDict[key].LingHeDesc + "</size>";
			int key2 = 1;
			for (int j = 1; j <= 5; j++)
			{
				if (lingHeCaiJi.ShengShiLimit >= LingMaiPinJie.DataDict[j].ShengShiLv)
				{
					key2 = j;
				}
			}
			this.TanChaNanDu.text = string.Format("探查难度：<size=34>{0}</size>（神识需求{1}）", LingMaiPinJie.DataDict[key2].ShengShiDesc, lingHeCaiJi.ShengShiLimit);
			UIIconShow component = UIIconShow.Prefab.Inst(this.MayGetItemList).GetComponent<UIIconShow>();
			component.SetItem(10035);
			component.CanDrag = false;
			component.gameObject.SetActive(false);
			this.MayGetItemDict.Add(10035, component);
			UIIconShow component2 = UIIconShow.Prefab.Inst(this.MayGetItemList).GetComponent<UIIconShow>();
			component2.SetItem(10005);
			component2.CanDrag = false;
			component2.gameObject.SetActive(false);
			this.MayGetItemDict.Add(10005, component2);
			this.State = LingHeCaiJiUIMag.CaiJiState.开始;
			this.TimeSlider.Init(true);
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x00195874 File Offset: 0x00193A74
		public void UpdateItemShow()
		{
			foreach (int key in this.MayGetItemDict.Keys)
			{
				this.MayGetItemDict[key].gameObject.SetActive(true);
			}
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x001958DC File Offset: 0x00193ADC
		private void Init()
		{
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = Vector3.one;
			base.transform.SetAsLastSibling();
			Tools.canClickFlag = false;
			PanelMamager.CanOpenOrClose = false;
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x0019593C File Offset: 0x00193B3C
		public void StartCaiJi()
		{
			this.State = LingHeCaiJiUIMag.CaiJiState.采集中;
			Tools.ClearChild(this.GetItemList);
			this.caiJiResult = LingHeCaiJiManager.DoCaiJi(this.nowMapIndex, this.CostTime);
			this.TimeSlider.gameObject.SetActive(false);
			this.BtnPanel.SetActive(false);
			this.player.AddTime(0, this.caiJiResult.RealCostTime, 0);
			this.CaiJingZhongPanel.ShowSlider();
			this.PlayeCaiJiAn();
			this.GetItemList.gameObject.SetActive(true);
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x001959CC File Offset: 0x00193BCC
		private void PlayeCaiJiAn()
		{
			GameObject animation = null;
			Animator ctr = null;
			animation = this.WaKuangMask;
			ctr = animation.GetComponent<Animator>();
			animation.SetActive(true);
			ctr.Play("Stop");
			Transform child = animation.transform.GetChild(0).transform;
			TweenCallback <>9__3;
			TweenCallback <>9__2;
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(child, 14f, 0.34f, false), delegate()
			{
				TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = ShortcutExtensions.DOLocalMoveY(child, 38f, 0.08f, false);
				TweenCallback tweenCallback;
				if ((tweenCallback = <>9__2) == null)
				{
					tweenCallback = (<>9__2 = delegate()
					{
						TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore2 = ShortcutExtensions.DOLocalMoveY(child, 14f, 0.05f, false);
						TweenCallback tweenCallback2;
						if ((tweenCallback2 = <>9__3) == null)
						{
							tweenCallback2 = (<>9__3 = delegate()
							{
								CanvasGroup canvasGroup = animation.GetComponent<CanvasGroup>();
								DOTween.To(() => canvasGroup.alpha, delegate(float x)
								{
									canvasGroup.alpha = x;
								}, 1f, 0.2f);
								ctr.Play("Start");
							});
						}
						TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(tweenerCore2, tweenCallback2);
					});
				}
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(tweenerCore, tweenCallback);
			});
			this.CaiJiMask2.SetActive(true);
			Transform child3 = this.CaiJiMask2.transform.GetChild(0).transform;
			TweenCallback <>9__6;
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(child3, 2f, 0.34f, false), delegate()
			{
				TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = ShortcutExtensions.DOLocalMoveY(child3, 20f, 0.08f, false);
				TweenCallback tweenCallback;
				if ((tweenCallback = <>9__6) == null)
				{
					tweenCallback = (<>9__6 = delegate()
					{
						ShortcutExtensions.DOLocalMoveY(child3, 2f, 0.05f, false);
					});
				}
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(tweenerCore, tweenCallback);
			});
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x00195ABC File Offset: 0x00193CBC
		public void CaiJiComplete()
		{
			if (this.caiJiResult.LingShiCount > 0)
			{
				UIIconShow component = UIIconShow.Prefab.Inst(this.GetItemList).GetComponent<UIIconShow>();
				component.SetItem(10035);
				component.Count = this.caiJiResult.LingShiCount;
				component.CanDrag = false;
				this.player.AddMoney(this.caiJiResult.LingShiCount);
			}
			if (this.caiJiResult.LingHeCount > 0)
			{
				UIIconShow component2 = UIIconShow.Prefab.Inst(this.GetItemList).GetComponent<UIIconShow>();
				component2.SetItem(10005);
				component2.Count = this.caiJiResult.LingHeCount;
				component2.CanDrag = false;
				this.player.addItem(10005, this.caiJiResult.LingHeCount, Tools.CreateItemSeid(10005), false);
			}
			int num = GlobalValue.Get(173, "LingHeCaiJiUIMag.CaiJiComplete 获取采矿时间");
			GlobalValue.Set(173, num + this.caiJiResult.RealCostTime, "LingHeCaiJiUIMag.CaiJiComplete 记录采矿时间");
			this.MayGetItemList.gameObject.SetActive(false);
			GameObject waKuangMask = this.WaKuangMask;
			CanvasGroup canvasGroup = waKuangMask.GetComponent<CanvasGroup>();
			DOTween.To(() => canvasGroup.alpha, delegate(float x)
			{
				canvasGroup.alpha = x;
			}, 0f, 0.2f);
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(this.CaiJiMask2.transform.GetChild(0).transform, 420f, 0.15f, false), delegate()
			{
				this.CaiJiMask2.SetActive(false);
				this.Desc.text = "本次采集完成";
			});
			this.State = LingHeCaiJiUIMag.CaiJiState.采集结束;
			this.CompleteBtnPanel.SetActive(true);
			if (this.caiJiResult.HasTiaoZhan)
			{
				Debug.Log("采集中遇到挑战，跳转到fungus");
				LingHeCaiJiManager.LingHeTiaoZhanArg = new LingHeTiaoZhanArg();
				LingHeCaiJiManager.LingHeTiaoZhanArg.LingMaiLv = LingHeCaiJi.DataDict[this.nowMapIndex].ShouYiLv;
				this.Close();
				LingHeCaiJiManager.IsOnTiaoZhan = true;
			}
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x00195CB0 File Offset: 0x00193EB0
		public void Restart()
		{
			GameObject animation = null;
			animation = this.WaKuangMask;
			Transform child = animation.transform.GetChild(0).transform;
			this.CaiJiMask2.SetActive(true);
			Transform child3 = this.CaiJiMask2.transform.GetChild(0).transform;
			TweenCallback <>9__1;
			TweenCallback <>9__2;
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(child3, 2f, 0.2f, false), delegate()
			{
				this.MayGetItemList.gameObject.SetActive(true);
				this.GetItemList.gameObject.SetActive(false);
				TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = ShortcutExtensions.DOLocalMoveY(child3, 420f, 0.3f, false);
				TweenCallback tweenCallback;
				if ((tweenCallback = <>9__1) == null)
				{
					tweenCallback = (<>9__1 = delegate()
					{
						this.CaiJiMask2.SetActive(false);
					});
				}
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(tweenerCore, tweenCallback);
				TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore2 = ShortcutExtensions.DOLocalMoveY(child, 800f, 0.3f, false);
				TweenCallback tweenCallback2;
				if ((tweenCallback2 = <>9__2) == null)
				{
					tweenCallback2 = (<>9__2 = delegate()
					{
						animation.SetActive(false);
						this.BtnPanel.SetActive(true);
						this.State = LingHeCaiJiUIMag.CaiJiState.开始;
						this.TimeSlider.gameObject.SetActive(true);
						this.Desc.text = "本次可能采到的材料";
					});
				}
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(tweenerCore2, tweenCallback2);
			});
			this.CompleteBtnPanel.SetActive(false);
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x00195D55 File Offset: 0x00193F55
		public void Close()
		{
			LingHeCaiJiUIMag.inst = null;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x00195D68 File Offset: 0x00193F68
		private void OnDestroy()
		{
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00195D81 File Offset: 0x00193F81
		public bool TryEscClose()
		{
			if (this.State == LingHeCaiJiUIMag.CaiJiState.采集中)
			{
				return false;
			}
			this.Close();
			return true;
		}

		// Token: 0x0400331E RID: 13086
		public Dictionary<int, UIIconShow> MayGetItemDict;

		// Token: 0x0400331F RID: 13087
		[SerializeField]
		private Transform MayGetItemList;

		// Token: 0x04003320 RID: 13088
		[SerializeField]
		private Transform GetItemList;

		// Token: 0x04003321 RID: 13089
		public CaiJiTimeSelect TimeSlider;

		// Token: 0x04003322 RID: 13090
		[SerializeField]
		private Text Desc;

		// Token: 0x04003323 RID: 13091
		[SerializeField]
		private Text BenDiLingQi;

		// Token: 0x04003324 RID: 13092
		[SerializeField]
		private Text LingHeCount;

		// Token: 0x04003325 RID: 13093
		[SerializeField]
		private Text TanChaNanDu;

		// Token: 0x04003326 RID: 13094
		[HideInInspector]
		public int CostTime;

		// Token: 0x04003327 RID: 13095
		[HideInInspector]
		public LingHeCaiJiUIMag.CaiJiState State;

		// Token: 0x04003328 RID: 13096
		[SerializeField]
		private GameObject BtnPanel;

		// Token: 0x04003329 RID: 13097
		[SerializeField]
		private FpBtn CloseBtn;

		// Token: 0x0400332A RID: 13098
		[SerializeField]
		private FpBtn CaiJiBtn;

		// Token: 0x0400332B RID: 13099
		[SerializeField]
		private FpBtn ContinueBtn;

		// Token: 0x0400332C RID: 13100
		[SerializeField]
		private CaiJiZhong CaiJingZhongPanel;

		// Token: 0x0400332D RID: 13101
		[SerializeField]
		private GameObject CaiJiMask;

		// Token: 0x0400332E RID: 13102
		[SerializeField]
		private GameObject CaiJiMask2;

		// Token: 0x0400332F RID: 13103
		[SerializeField]
		private GameObject WaKuangMask;

		// Token: 0x04003330 RID: 13104
		[SerializeField]
		private GameObject CompleteBtnPanel;

		// Token: 0x04003331 RID: 13105
		public static LingHeCaiJiUIMag inst;

		// Token: 0x04003332 RID: 13106
		private Avatar player;

		// Token: 0x04003333 RID: 13107
		[HideInInspector]
		public int nowMapIndex;

		// Token: 0x04003334 RID: 13108
		private LingHeCaiJiResult caiJiResult;

		// Token: 0x0200154A RID: 5450
		public enum CaiJiState
		{
			// Token: 0x04006F05 RID: 28421
			开始 = 1,
			// Token: 0x04006F06 RID: 28422
			采集中,
			// Token: 0x04006F07 RID: 28423
			采集结束
		}
	}
}
