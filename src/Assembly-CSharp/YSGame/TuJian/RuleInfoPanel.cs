using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000DE8 RID: 3560
	public class RuleInfoPanel : InfoPanelBase
	{
		// Token: 0x060055D8 RID: 21976 RVA: 0x0003D6C9 File Offset: 0x0003B8C9
		public void Start()
		{
			this.Init();
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x0003D6D1 File Offset: 0x0003B8D1
		public override void Update()
		{
			base.Update();
			this.RefreshSVHeight();
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x0023C8E0 File Offset: 0x0023AAE0
		public void Init()
		{
			this._NormalSV = base.transform.Find("HyTextSV").gameObject;
			this._DoubleSV = base.transform.Find("DoubleHyTextSV").gameObject;
			this._HyContentTransform = (base.transform.Find("HyTextSV/Viewport/Content") as RectTransform);
			this._DoubleContentTransform = (base.transform.Find("DoubleHyTextSV/Viewport/Content") as RectTransform);
			this._HyText = base.transform.Find("HyTextSV/Viewport/Content/Text").GetComponent<SymbolText>();
			this._HyScrollbar = base.transform.Find("HyTextSV/Scrollbar Vertical").GetComponent<Scrollbar>();
			this._DoubleScrollbar = base.transform.Find("DoubleHyTextSV/Scrollbar Vertical").GetComponent<Scrollbar>();
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x0023C9AC File Offset: 0x0023ABAC
		public void RefreshSVHeight()
		{
			if (this._HyContentTransform != null && this._HyContentTransform.sizeDelta.y != this._HyText.preferredHeight + 34f)
			{
				this._HyContentTransform.sizeDelta = new Vector2(this._HyContentTransform.sizeDelta.x, this._HyText.preferredHeight + 34f);
			}
			if (this._DoubleContentTransform != null)
			{
				float num = 34f;
				foreach (CiZhuiSVItem ciZhuiSVItem in this.DoubleSVItemList)
				{
					num += ciZhuiSVItem.Height;
				}
				if (this._DoubleContentTransform.sizeDelta.y != num)
				{
					this._DoubleContentTransform.sizeDelta = new Vector2(this._DoubleContentTransform.sizeDelta.x, num);
					if (this.needSetPos)
					{
						if (this.setPosCount > 2)
						{
							this.needSetPos = false;
							this.setPosCount = 0;
							return;
						}
						float num2 = 0f;
						int num3 = this.DoubleSVItemList.Count - 1;
						while (num3 >= 0 && this.DoubleSVItemList[num3].ID != this.posID)
						{
							num2 += this.DoubleSVItemList[num3].Height;
							num3--;
						}
						this._DoubleContentTransform.anchoredPosition = new Vector2(this._DoubleContentTransform.anchoredPosition.x, num2 - 34f);
						this.setPosCount++;
					}
				}
			}
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x0003D6DF File Offset: 0x0003B8DF
		public void EnableNormalPanel()
		{
			this._DoubleSV.SetActive(false);
			this._NormalSV.SetActive(true);
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x0003D6F9 File Offset: 0x0003B8F9
		public void EnableDoublePanel()
		{
			this._NormalSV.SetActive(false);
			this._DoubleSV.SetActive(true);
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x0003D713 File Offset: 0x0003B913
		public override void OnHyperlink(int[] args)
		{
			base.OnHyperlink(args);
			if (args[1] / 100 == 1)
			{
				this.needSetPos = true;
				if (args[1] == 101)
				{
					this.posID = args[3];
					return;
				}
				this.posID = args[2];
			}
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x0023CB5C File Offset: 0x0023AD5C
		public string FindSearch()
		{
			string result = "";
			foreach (KeyValuePair<int, List<int>> keyValuePair in TuJianDB.RuleCiZhuiIndexData)
			{
				int key = keyValuePair.Key;
				foreach (int num in keyValuePair.Value)
				{
					DoubleItem doubleItem = TuJianDB.RuleDoubleData[num];
					if (TuJianManager.Inst.Searcher.IsContansSearch(doubleItem.Name))
					{
						result = string.Format("2_101_{0}_{1}", key, num);
						return result;
					}
				}
			}
			foreach (KeyValuePair<int, List<int>> keyValuePair2 in TuJianDB.RuleDoubleIndexData)
			{
				int key2 = keyValuePair2.Key;
				foreach (int num2 in keyValuePair2.Value)
				{
					DoubleItem doubleItem2 = TuJianDB.RuleDoubleData[num2];
					if (TuJianManager.Inst.Searcher.IsContansSearch(doubleItem2.Name))
					{
						result = string.Format("2_{0}_{1}", key2, num2);
						return result;
					}
				}
			}
			return result;
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x0023CD10 File Offset: 0x0023AF10
		public override void RefreshPanelData()
		{
			base.RefreshPanelData();
			int nowSelectID = TuJianRuleTab.Inst.TypeSSV.NowSelectID;
			if (TuJianDB.RuleTuJianTypeDoubleSVData[nowSelectID])
			{
				this.EnableDoublePanel();
				List<int> list;
				if (nowSelectID == 101)
				{
					int nowSelectID2 = TuJianRuleTab.Inst.FilterSSV.NowSelectID;
					list = TuJianDB.RuleCiZhuiIndexData[nowSelectID2];
					if (this.needSetPos)
					{
						TuJianManager.Inst.NowPageHyperlink = string.Format("2_{0}_{1}_{2}", nowSelectID, nowSelectID2, this.posID);
					}
					else
					{
						TuJianManager.Inst.NowPageHyperlink = string.Format("2_{0}_{1}_{2}", nowSelectID, nowSelectID2, list[0]);
					}
				}
				else
				{
					list = TuJianDB.RuleDoubleIndexData[nowSelectID];
					if (this.needSetPos)
					{
						TuJianManager.Inst.NowPageHyperlink = string.Format("2_{0}_{1}", nowSelectID, this.posID);
					}
					else
					{
						TuJianManager.Inst.NowPageHyperlink = string.Format("2_{0}_{1}", nowSelectID, list[0]);
					}
				}
				this.HideAllDoubleItem();
				for (int i = list.Count - 1; i >= 0; i--)
				{
					CiZhuiSVItem doubleItem = this.GetDoubleItem();
					DoubleItem doubleItem2 = TuJianDB.RuleDoubleData[list[i]];
					doubleItem.SetCiZhui(list[i], doubleItem2.Name, doubleItem2.Desc);
					doubleItem.transform.SetSiblingIndex(0);
				}
				this._DoubleScrollbar.value = 1f;
				return;
			}
			this.EnableNormalPanel();
			string text;
			if (TuJianDB.RuleTuJianTypeHasChildData[nowSelectID])
			{
				int num = TuJianRuleTab.Inst.FilterSSV.NowSelectID;
				if (num == -1)
				{
					num = 1;
				}
				TuJianManager.Inst.NowPageHyperlink = string.Format("2_{0}_{1}", nowSelectID, num);
				text = TuJianDB.RuleTuJianTypeChildDescData[nowSelectID][num];
			}
			else
			{
				TuJianManager.Inst.NowPageHyperlink = string.Format("2_{0}_0", nowSelectID);
				text = TuJianDB.RuleTuJianTypeDescData[nowSelectID];
			}
			text = text.Replace("<Title>", "\n\n#w2#s40");
			text = text.Replace("</Title>", "#n#s28#w1\n");
			text = text.Replace("<Image>", "#W<sprite n=");
			text = text.Replace("</Image>", ">#n");
			this._HyText.text = text;
			this._HyScrollbar.value = 1f;
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x0023CF90 File Offset: 0x0023B190
		public void HideAllDoubleItem()
		{
			foreach (CiZhuiSVItem ciZhuiSVItem in this.DoubleSVItemList)
			{
				ciZhuiSVItem.gameObject.SetActive(false);
				this.HideDoubleSVItemList.Add(ciZhuiSVItem);
			}
			this.DoubleSVItemList.Clear();
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x0023D000 File Offset: 0x0023B200
		public CiZhuiSVItem GetDoubleItem()
		{
			CiZhuiSVItem ciZhuiSVItem;
			if (this.HideDoubleSVItemList.Count > 0)
			{
				ciZhuiSVItem = this.HideDoubleSVItemList[0];
				this.HideDoubleSVItemList.RemoveAt(0);
				ciZhuiSVItem.gameObject.SetActive(true);
			}
			else
			{
				ciZhuiSVItem = Object.Instantiate<GameObject>(this.DoubleSVItem, this._DoubleContentTransform).GetComponent<CiZhuiSVItem>();
			}
			this.DoubleSVItemList.Add(ciZhuiSVItem);
			return ciZhuiSVItem;
		}

		// Token: 0x04005588 RID: 21896
		public GameObject DoubleSVItem;

		// Token: 0x04005589 RID: 21897
		private GameObject _NormalSV;

		// Token: 0x0400558A RID: 21898
		private GameObject _DoubleSV;

		// Token: 0x0400558B RID: 21899
		private RectTransform _HyContentTransform;

		// Token: 0x0400558C RID: 21900
		private RectTransform _DoubleContentTransform;

		// Token: 0x0400558D RID: 21901
		private Scrollbar _HyScrollbar;

		// Token: 0x0400558E RID: 21902
		private Scrollbar _DoubleScrollbar;

		// Token: 0x0400558F RID: 21903
		private SymbolText _HyText;

		// Token: 0x04005590 RID: 21904
		public Color HyTextColor = new Color(0.003921569f, 0.4745098f, 0.43529412f);

		// Token: 0x04005591 RID: 21905
		public Color HyTextHoverColor = new Color(0.015686275f, 0.3882353f, 0.35686275f);

		// Token: 0x04005592 RID: 21906
		private List<CiZhuiSVItem> DoubleSVItemList = new List<CiZhuiSVItem>();

		// Token: 0x04005593 RID: 21907
		private List<CiZhuiSVItem> HideDoubleSVItemList = new List<CiZhuiSVItem>();

		// Token: 0x04005594 RID: 21908
		private bool needSetPos;

		// Token: 0x04005595 RID: 21909
		private int setPosCount;

		// Token: 0x04005596 RID: 21910
		private int posID;
	}
}
