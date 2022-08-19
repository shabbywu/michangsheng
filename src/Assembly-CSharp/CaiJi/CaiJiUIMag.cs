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
	// Token: 0x02000736 RID: 1846
	public class CaiJiUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x06003AD7 RID: 15063 RVA: 0x00194788 File Offset: 0x00192988
		private void Awake()
		{
			CaiJiUIMag.inst = this;
			ESCCloseManager.Inst.RegisterClose(this);
			this.player = Tools.instance.getPlayer();
			MessageMag.Instance.Register("CaiJi_Item_Select", new Action<MessageData>(this.SelectItem));
			MessageMag.Instance.Register("CaiJi_Item_Cancel", new Action<MessageData>(this.CancalSelectItem));
			this.MayGetItemDict = new Dictionary<int, UIIconShow>();
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x001947F8 File Offset: 0x001929F8
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

		// Token: 0x06003AD9 RID: 15065 RVA: 0x00194920 File Offset: 0x00192B20
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

		// Token: 0x06003ADA RID: 15066 RVA: 0x00194985 File Offset: 0x00192B85
		private void SelectItem(MessageData data)
		{
			this.CurNum++;
			this.GetNullSlot().PutItem(data.valueInt);
			if (this.CurNum == this.MaxNum)
			{
				this.IsMax = true;
			}
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x001949BB File Offset: 0x00192BBB
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

		// Token: 0x06003ADC RID: 15068 RVA: 0x001949F8 File Offset: 0x00192BF8
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

		// Token: 0x06003ADD RID: 15069 RVA: 0x00194A54 File Offset: 0x00192C54
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

		// Token: 0x06003ADE RID: 15070 RVA: 0x00194B38 File Offset: 0x00192D38
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

		// Token: 0x06003ADF RID: 15071 RVA: 0x00194D9C File Offset: 0x00192F9C
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

		// Token: 0x06003AE0 RID: 15072 RVA: 0x00194E8C File Offset: 0x0019308C
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

		// Token: 0x06003AE1 RID: 15073 RVA: 0x00194F4C File Offset: 0x0019314C
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

		// Token: 0x06003AE2 RID: 15074 RVA: 0x00195064 File Offset: 0x00193264
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

		// Token: 0x06003AE3 RID: 15075 RVA: 0x00195134 File Offset: 0x00193334
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

		// Token: 0x06003AE4 RID: 15076 RVA: 0x001951F9 File Offset: 0x001933F9
		public void Close()
		{
			CaiJiUIMag.inst = null;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x0019520C File Offset: 0x0019340C
		private void OnDestroy()
		{
			MessageMag.Instance.Remove("CaiJi_Item_Select", new Action<MessageData>(this.SelectItem));
			MessageMag.Instance.Remove("CaiJi_Item_Cancel", new Action<MessageData>(this.CancalSelectItem));
			Tools.canClickFlag = true;
			PanelMamager.CanOpenOrClose = true;
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x00195266 File Offset: 0x00193466
		public bool TryEscClose()
		{
			if (this.State == CaiJiUIMag.CaiJiState.采集中)
			{
				return false;
			}
			this.Close();
			return true;
		}

		// Token: 0x040032F6 RID: 13046
		public List<CaiJiImpSlot> ImportSlotList;

		// Token: 0x040032F7 RID: 13047
		public Dictionary<int, CaiJiCell> TotalItemDict;

		// Token: 0x040032F8 RID: 13048
		public Dictionary<int, UIIconShow> MayGetItemDict;

		// Token: 0x040032F9 RID: 13049
		public List<int> RandomQualit = new List<int>
		{
			12,
			10,
			8,
			6,
			4,
			2
		};

		// Token: 0x040032FA RID: 13050
		[SerializeField]
		private GameObject CaiJiItemCell;

		// Token: 0x040032FB RID: 13051
		[SerializeField]
		private Transform TotalItemList;

		// Token: 0x040032FC RID: 13052
		[SerializeField]
		private GameObject MayGetItemCell;

		// Token: 0x040032FD RID: 13053
		[SerializeField]
		private Transform MayGetItemList;

		// Token: 0x040032FE RID: 13054
		[SerializeField]
		private Transform GetItemList;

		// Token: 0x040032FF RID: 13055
		public CaiJiTimeSelect TimeSlider;

		// Token: 0x04003300 RID: 13056
		[SerializeField]
		private Text Desc;

		// Token: 0x04003301 RID: 13057
		public int MaxNum;

		// Token: 0x04003302 RID: 13058
		public int CurNum;

		// Token: 0x04003303 RID: 13059
		public bool IsMax;

		// Token: 0x04003304 RID: 13060
		public int CostTime;

		// Token: 0x04003305 RID: 13061
		public int MayCanGetItemCount;

		// Token: 0x04003306 RID: 13062
		public CaiJiUIMag.CaiJiType Type;

		// Token: 0x04003307 RID: 13063
		public CaiJiUIMag.CaiJiState State;

		// Token: 0x04003308 RID: 13064
		[SerializeField]
		private GameObject BtnPanel;

		// Token: 0x04003309 RID: 13065
		[SerializeField]
		private FpBtn CloseBtn;

		// Token: 0x0400330A RID: 13066
		[SerializeField]
		private FpBtn CaiJiBtn;

		// Token: 0x0400330B RID: 13067
		[SerializeField]
		private FpBtn ContinueBtn;

		// Token: 0x0400330C RID: 13068
		[SerializeField]
		private CaiJiZhong CaiJingZhongPanel;

		// Token: 0x0400330D RID: 13069
		[SerializeField]
		private GameObject CaiJiMask;

		// Token: 0x0400330E RID: 13070
		[SerializeField]
		private GameObject CaiJiMask2;

		// Token: 0x0400330F RID: 13071
		[SerializeField]
		private GameObject WaKuangMask;

		// Token: 0x04003310 RID: 13072
		[SerializeField]
		private GameObject CompleteBtnPanel;

		// Token: 0x04003311 RID: 13073
		public static CaiJiUIMag inst;

		// Token: 0x04003312 RID: 13074
		private Avatar player;

		// Token: 0x02001544 RID: 5444
		public enum CaiJiType
		{
			// Token: 0x04006EED RID: 28397
			采药 = 1,
			// Token: 0x04006EEE RID: 28398
			挖矿
		}

		// Token: 0x02001545 RID: 5445
		public enum CaiJiState
		{
			// Token: 0x04006EF0 RID: 28400
			开始 = 1,
			// Token: 0x04006EF1 RID: 28401
			采集中,
			// Token: 0x04006EF2 RID: 28402
			采集结束
		}
	}
}
