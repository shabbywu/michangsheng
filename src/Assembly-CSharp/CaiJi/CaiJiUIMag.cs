using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame.TuJian;

namespace CaiJi
{
	// Token: 0x02000A98 RID: 2712
	public class CaiJiUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x06004595 RID: 17813 RVA: 0x001DC154 File Offset: 0x001DA354
		private void Awake()
		{
			CaiJiUIMag.inst = this;
			ESCCloseManager.Inst.RegisterClose(this);
			this.player = Tools.instance.getPlayer();
			MessageMag.Instance.Register("CaiJi_Item_Select", new Action<MessageData>(this.SelectItem));
			MessageMag.Instance.Register("CaiJi_Item_Cancel", new Action<MessageData>(this.CancalSelectItem));
			this.MayGetItemDict = new Dictionary<int, UIIconShow>();
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x001DC1C4 File Offset: 0x001DA3C4
		public void OpenCaiJi(int id)
		{
			this.Init();
			JSONObject jsonobject = jsonData.instance.CaiYaoDiaoLuo[id.ToString()];
			TuJianManager.Inst.UnlockMap(jsonobject["FuBen"].str);
			int i = jsonobject["type"].I;
			this.TotalItemDict = new Dictionary<int, CaiJiCell>();
			for (int j = 1; j <= 8; j++)
			{
				int i2 = jsonobject["value" + j].I;
				if (i2 > 0)
				{
					this.TotalItemDict.Add(i2, this.CaiJiItemCell.Inst(this.TotalItemList).GetComponent<CaiJiCell>().Init(i2));
					UIIconShow component = this.MayGetItemCell.Inst(this.MayGetItemList).GetComponent<UIIconShow>();
					component.SetItem(i2);
					component.CanDrag = false;
					component.gameObject.SetActive(false);
					this.MayGetItemDict.Add(i2, component);
				}
			}
			if (i == 1)
			{
				this.Type = CaiJiUIMag.CaiJiType.采药;
			}
			else if (i == 2)
			{
				this.Type = CaiJiUIMag.CaiJiType.挖矿;
			}
			this.State = CaiJiUIMag.CaiJiState.开始;
			this.TimeSlider.Init(false);
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x001DC2EC File Offset: 0x001DA4EC
		private void Init()
		{
			this.IsMax = false;
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = Vector3.one;
			base.transform.SetAsLastSibling();
			Tools.canClickFlag = false;
			PanelMamager.CanOpenOrClose = false;
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x00031CA2 File Offset: 0x0002FEA2
		private void SelectItem(MessageData data)
		{
			this.CurNum++;
			this.GetNullSlot().PutItem(data.valueInt);
			if (this.CurNum == this.MaxNum)
			{
				this.IsMax = true;
			}
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x00031CD8 File Offset: 0x0002FED8
		private void CancalSelectItem(MessageData data)
		{
			this.TotalItemDict[data.valueInt].Show();
			this.CurNum--;
			this.IsMax = false;
			if (this.CurNum < 0)
			{
				this.CurNum = 0;
			}
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x001DC354 File Offset: 0x001DA554
		private CaiJiImpSlot GetNullSlot()
		{
			foreach (CaiJiImpSlot caiJiImpSlot in this.ImportSlotList)
			{
				if (caiJiImpSlot.IsNull)
				{
					return caiJiImpSlot;
				}
			}
			return null;
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x001DC3B0 File Offset: 0x001DA5B0
		public void UpdateMayGetItem()
		{
			int num = this.player.GetTianFuAddCaoYaoCaiJi(CaiYaoShoYi.DataDict[this.player.getLevelType()].QuanZhon[1]) * this.CostTime;
			this.MayCanGetItemCount = 0;
			foreach (int key in this.MayGetItemDict.Keys)
			{
				if (this.MayGetItemDict[key].tmpItem.itemPrice > num)
				{
					this.MayGetItemDict[key].gameObject.SetActive(false);
				}
				else
				{
					this.MayCanGetItemCount++;
					this.MayGetItemDict[key].gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x001DC494 File Offset: 0x001DA694
		public void StartCaiJi()
		{
			if (this.MayCanGetItemCount == 0)
			{
				UIPopTip.Inst.Pop("没有可以采集到的物品", PopTipIconType.叹号);
				return;
			}
			this.State = CaiJiUIMag.CaiJiState.采集中;
			new List<int>();
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (int key in this.MayGetItemDict.Keys)
			{
				if (this.MayGetItemDict[key].gameObject.activeSelf)
				{
					int value;
					if (this.TotalItemDict[key].IsSelected)
					{
						value = 10 * this.RandomQualit[this.TotalItemDict[key].Item.tmpItem.quality - 1];
					}
					else
					{
						value = this.RandomQualit[this.TotalItemDict[key].Item.tmpItem.quality - 1];
					}
					dictionary.Add(key, value);
				}
			}
			CaiYaoShoYi caiYaoShoYi = CaiYaoShoYi.DataDict[this.player.getLevelType()];
			int totalMoney = Tools.instance.GetRandomInt(this.player.GetTianFuAddCaoYaoCaiJi(caiYaoShoYi.QuanZhon[0]), this.player.GetTianFuAddCaoYaoCaiJi(caiYaoShoYi.QuanZhon[1])) * this.CostTime;
			Dictionary<int, int> caiJiItemList = this.GetCaiJiItemList(dictionary, totalMoney);
			Tools.ClearChild(this.GetItemList);
			foreach (int num in caiJiItemList.Keys)
			{
				UIIconShow component = this.MayGetItemCell.Inst(this.GetItemList).GetComponent<UIIconShow>();
				component.SetItem(num);
				component.Count = caiJiItemList[num];
				component.CanDrag = false;
				this.player.addItem(num, caiJiItemList[num], Tools.CreateItemSeid(num), false);
			}
			this.TimeSlider.gameObject.SetActive(false);
			this.BtnPanel.SetActive(false);
			this.player.AddTime(0, this.CostTime, 0);
			this.CaiJingZhongPanel.ShowSlider();
			this.PlayeCaiJiAn();
			this.GetItemList.gameObject.SetActive(true);
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x001DC6F8 File Offset: 0x001DA8F8
		private Dictionary<int, int> GetCaiJiItemList(Dictionary<int, int> randomDict, int totalMoney)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			while (randomDict.Count > 0)
			{
				int randomByQuanZhong = this.GetRandomByQuanZhong(randomDict);
				int itemPrice = this.TotalItemDict[randomByQuanZhong].Item.tmpItem.itemPrice;
				if (itemPrice <= totalMoney)
				{
					totalMoney -= itemPrice;
					if (dictionary.ContainsKey(randomByQuanZhong))
					{
						Dictionary<int, int> dictionary2 = dictionary;
						int key = randomByQuanZhong;
						dictionary2[key]++;
					}
					else
					{
						dictionary.Add(randomByQuanZhong, 1);
					}
				}
				else
				{
					randomDict.Remove(randomByQuanZhong);
					Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
					foreach (int key2 in randomDict.Keys)
					{
						int value = randomDict[key2];
						dictionary3.Add(key2, value);
					}
					randomDict = dictionary3;
				}
			}
			return dictionary;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x001DC7E8 File Offset: 0x001DA9E8
		private int GetRandomByQuanZhong(Dictionary<int, int> randomDict)
		{
			int num = 0;
			foreach (int key in randomDict.Keys)
			{
				num += randomDict[key];
			}
			int randomInt = Tools.instance.GetRandomInt(0, num);
			int num2 = 0;
			foreach (int num3 in randomDict.Keys)
			{
				num2 += randomDict[num3];
				if (num2 >= randomInt)
				{
					return num3;
				}
			}
			return 0;
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x001DC8A8 File Offset: 0x001DAAA8
		private void PlayeCaiJiAn()
		{
			GameObject animation = null;
			Animator ctr = null;
			if (this.Type == CaiJiUIMag.CaiJiType.采药)
			{
				animation = this.CaiJiMask;
			}
			else if (this.Type == CaiJiUIMag.CaiJiType.挖矿)
			{
				animation = this.WaKuangMask;
			}
			ctr = animation.GetComponent<Animator>();
			ctr.Play("Stop");
			animation.SetActive(true);
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
								if (this.Type == CaiJiUIMag.CaiJiType.采药)
								{
									animation.GetComponent<CanvasGroup>().alpha = 1f;
									DOTweenModuleUI.DOColor(animation.transform.GetChild(1).GetComponent<Image>(), new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), 0.2f);
								}
								else if (this.Type == CaiJiUIMag.CaiJiType.挖矿)
								{
									CanvasGroup canvasGroup = animation.GetComponent<CanvasGroup>();
									DOTween.To(() => canvasGroup.alpha, delegate(float x)
									{
										canvasGroup.alpha = x;
									}, 1f, 0.2f);
								}
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

		// Token: 0x060045A0 RID: 17824 RVA: 0x001DC9C0 File Offset: 0x001DABC0
		public void CaiJiComplete()
		{
			GameObject gameObject = null;
			this.MayGetItemList.gameObject.SetActive(false);
			if (this.Type == CaiJiUIMag.CaiJiType.采药)
			{
				gameObject = this.CaiJiMask;
			}
			else if (this.Type == CaiJiUIMag.CaiJiType.挖矿)
			{
				gameObject = this.WaKuangMask;
			}
			CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
			DOTween.To(() => canvasGroup.alpha, delegate(float x)
			{
				canvasGroup.alpha = x;
			}, 0f, 0.2f);
			TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(this.CaiJiMask2.transform.GetChild(0).transform, 420f, 0.15f, false), delegate()
			{
				this.CaiJiMask2.SetActive(false);
				this.Desc.text = "本次采集完成";
			});
			this.State = CaiJiUIMag.CaiJiState.采集结束;
			this.CompleteBtnPanel.SetActive(true);
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x001DCA90 File Offset: 0x001DAC90
		public void Restart()
		{
			GameObject animation = null;
			if (this.Type == CaiJiUIMag.CaiJiType.采药)
			{
				animation = this.CaiJiMask;
			}
			else if (this.Type == CaiJiUIMag.CaiJiType.挖矿)
			{
				animation = this.WaKuangMask;
			}
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
						if (this.Type == CaiJiUIMag.CaiJiType.采药)
						{
							animation.transform.GetChild(1).GetComponent<Image>().color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
						}
						this.BtnPanel.SetActive(true);
						this.State = CaiJiUIMag.CaiJiState.开始;
						this.TimeSlider.gameObject.SetActive(true);
						this.Desc.text = "本次可能采到的材料";
					});
				}
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(tweenerCore2, tweenCallback2);
			});
			this.CompleteBtnPanel.SetActive(false);
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x00031D15 File Offset: 0x0002FF15
		public void Close()
		{
			CaiJiUIMag.inst = null;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x001DCB58 File Offset: 0x001DAD58
		private void OnDestroy()
		{
			MessageMag.Instance.Remove("CaiJi_Item_Select", new Action<MessageData>(this.SelectItem));
			MessageMag.Instance.Remove("CaiJi_Item_Cancel", new Action<MessageData>(this.CancalSelectItem));
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x00031D28 File Offset: 0x0002FF28
		public bool TryEscClose()
		{
			if (this.State == CaiJiUIMag.CaiJiState.采集中)
			{
				return false;
			}
			this.Close();
			return true;
		}

		// Token: 0x04003DAF RID: 15791
		public List<CaiJiImpSlot> ImportSlotList;

		// Token: 0x04003DB0 RID: 15792
		public Dictionary<int, CaiJiCell> TotalItemDict;

		// Token: 0x04003DB1 RID: 15793
		public Dictionary<int, UIIconShow> MayGetItemDict;

		// Token: 0x04003DB2 RID: 15794
		public List<int> RandomQualit = new List<int>
		{
			12,
			10,
			8,
			6,
			4,
			2
		};

		// Token: 0x04003DB3 RID: 15795
		[SerializeField]
		private GameObject CaiJiItemCell;

		// Token: 0x04003DB4 RID: 15796
		[SerializeField]
		private Transform TotalItemList;

		// Token: 0x04003DB5 RID: 15797
		[SerializeField]
		private GameObject MayGetItemCell;

		// Token: 0x04003DB6 RID: 15798
		[SerializeField]
		private Transform MayGetItemList;

		// Token: 0x04003DB7 RID: 15799
		[SerializeField]
		private Transform GetItemList;

		// Token: 0x04003DB8 RID: 15800
		public CaiJiTimeSelect TimeSlider;

		// Token: 0x04003DB9 RID: 15801
		[SerializeField]
		private Text Desc;

		// Token: 0x04003DBA RID: 15802
		public int MaxNum;

		// Token: 0x04003DBB RID: 15803
		public int CurNum;

		// Token: 0x04003DBC RID: 15804
		public bool IsMax;

		// Token: 0x04003DBD RID: 15805
		public int CostTime;

		// Token: 0x04003DBE RID: 15806
		public int MayCanGetItemCount;

		// Token: 0x04003DBF RID: 15807
		public CaiJiUIMag.CaiJiType Type;

		// Token: 0x04003DC0 RID: 15808
		public CaiJiUIMag.CaiJiState State;

		// Token: 0x04003DC1 RID: 15809
		[SerializeField]
		private GameObject BtnPanel;

		// Token: 0x04003DC2 RID: 15810
		[SerializeField]
		private FpBtn CloseBtn;

		// Token: 0x04003DC3 RID: 15811
		[SerializeField]
		private FpBtn CaiJiBtn;

		// Token: 0x04003DC4 RID: 15812
		[SerializeField]
		private FpBtn ContinueBtn;

		// Token: 0x04003DC5 RID: 15813
		[SerializeField]
		private CaiJiZhong CaiJingZhongPanel;

		// Token: 0x04003DC6 RID: 15814
		[SerializeField]
		private GameObject CaiJiMask;

		// Token: 0x04003DC7 RID: 15815
		[SerializeField]
		private GameObject CaiJiMask2;

		// Token: 0x04003DC8 RID: 15816
		[SerializeField]
		private GameObject WaKuangMask;

		// Token: 0x04003DC9 RID: 15817
		[SerializeField]
		private GameObject CompleteBtnPanel;

		// Token: 0x04003DCA RID: 15818
		public static CaiJiUIMag inst;

		// Token: 0x04003DCB RID: 15819
		private Avatar player;

		// Token: 0x02000A99 RID: 2713
		public enum CaiJiType
		{
			// Token: 0x04003DCD RID: 15821
			采药 = 1,
			// Token: 0x04003DCE RID: 15822
			挖矿
		}

		// Token: 0x02000A9A RID: 2714
		public enum CaiJiState
		{
			// Token: 0x04003DD0 RID: 15824
			开始 = 1,
			// Token: 0x04003DD1 RID: 15825
			采集中,
			// Token: 0x04003DD2 RID: 15826
			采集结束
		}
	}
}
