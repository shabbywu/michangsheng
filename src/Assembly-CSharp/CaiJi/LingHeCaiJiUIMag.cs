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
	// Token: 0x02000AA3 RID: 2723
	public class LingHeCaiJiUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x060045C3 RID: 17859 RVA: 0x00031E61 File Offset: 0x00030061
		private void Awake()
		{
			LingHeCaiJiUIMag.inst = this;
			ESCCloseManager.Inst.RegisterClose(this);
			this.player = Tools.instance.getPlayer();
			this.MayGetItemDict = new Dictionary<int, UIIconShow>();
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x001DD1D8 File Offset: 0x001DB3D8
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

		// Token: 0x060045C5 RID: 17861 RVA: 0x001DD388 File Offset: 0x001DB588
		public void UpdateItemShow()
		{
			foreach (int key in this.MayGetItemDict.Keys)
			{
				this.MayGetItemDict[key].gameObject.SetActive(true);
			}
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x001DD3F0 File Offset: 0x001DB5F0
		private void Init()
		{
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = Vector3.one;
			base.transform.SetAsLastSibling();
			Tools.canClickFlag = false;
			PanelMamager.CanOpenOrClose = false;
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x001DD450 File Offset: 0x001DB650
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

		// Token: 0x060045C8 RID: 17864 RVA: 0x001DD4E0 File Offset: 0x001DB6E0
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

		// Token: 0x060045C9 RID: 17865 RVA: 0x001DD5D0 File Offset: 0x001DB7D0
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

		// Token: 0x060045CA RID: 17866 RVA: 0x001DD7C4 File Offset: 0x001DB9C4
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

		// Token: 0x060045CB RID: 17867 RVA: 0x00031E8F File Offset: 0x0003008F
		public void Close()
		{
			LingHeCaiJiUIMag.inst = null;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x00031EA2 File Offset: 0x000300A2
		private void OnDestroy()
		{
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x00031EBB File Offset: 0x000300BB
		public bool TryEscClose()
		{
			if (this.State == LingHeCaiJiUIMag.CaiJiState.采集中)
			{
				return false;
			}
			this.Close();
			return true;
		}

		// Token: 0x04003DEF RID: 15855
		public Dictionary<int, UIIconShow> MayGetItemDict;

		// Token: 0x04003DF0 RID: 15856
		[SerializeField]
		private Transform MayGetItemList;

		// Token: 0x04003DF1 RID: 15857
		[SerializeField]
		private Transform GetItemList;

		// Token: 0x04003DF2 RID: 15858
		public CaiJiTimeSelect TimeSlider;

		// Token: 0x04003DF3 RID: 15859
		[SerializeField]
		private Text Desc;

		// Token: 0x04003DF4 RID: 15860
		[SerializeField]
		private Text BenDiLingQi;

		// Token: 0x04003DF5 RID: 15861
		[SerializeField]
		private Text LingHeCount;

		// Token: 0x04003DF6 RID: 15862
		[SerializeField]
		private Text TanChaNanDu;

		// Token: 0x04003DF7 RID: 15863
		[HideInInspector]
		public int CostTime;

		// Token: 0x04003DF8 RID: 15864
		[HideInInspector]
		public LingHeCaiJiUIMag.CaiJiState State;

		// Token: 0x04003DF9 RID: 15865
		[SerializeField]
		private GameObject BtnPanel;

		// Token: 0x04003DFA RID: 15866
		[SerializeField]
		private FpBtn CloseBtn;

		// Token: 0x04003DFB RID: 15867
		[SerializeField]
		private FpBtn CaiJiBtn;

		// Token: 0x04003DFC RID: 15868
		[SerializeField]
		private FpBtn ContinueBtn;

		// Token: 0x04003DFD RID: 15869
		[SerializeField]
		private CaiJiZhong CaiJingZhongPanel;

		// Token: 0x04003DFE RID: 15870
		[SerializeField]
		private GameObject CaiJiMask;

		// Token: 0x04003DFF RID: 15871
		[SerializeField]
		private GameObject CaiJiMask2;

		// Token: 0x04003E00 RID: 15872
		[SerializeField]
		private GameObject WaKuangMask;

		// Token: 0x04003E01 RID: 15873
		[SerializeField]
		private GameObject CompleteBtnPanel;

		// Token: 0x04003E02 RID: 15874
		public static LingHeCaiJiUIMag inst;

		// Token: 0x04003E03 RID: 15875
		private Avatar player;

		// Token: 0x04003E04 RID: 15876
		[HideInInspector]
		public int nowMapIndex;

		// Token: 0x04003E05 RID: 15877
		private LingHeCaiJiResult caiJiResult;

		// Token: 0x02000AA4 RID: 2724
		public enum CaiJiState
		{
			// Token: 0x04003E07 RID: 15879
			开始 = 1,
			// Token: 0x04003E08 RID: 15880
			采集中,
			// Token: 0x04003E09 RID: 15881
			采集结束
		}
	}
}
