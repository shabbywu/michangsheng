using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using script.NewLianDan.DanFang;
using script.NewLianDan.LianDan;
using script.NewLianDan.PutDanLu;
using script.NewLianDan.Result;
using UnityEngine;

namespace script.NewLianDan
{
	// Token: 0x020009F8 RID: 2552
	public class LianDanUIMag : MonoBehaviour
	{
		// Token: 0x060046AC RID: 18092 RVA: 0x001DE02C File Offset: 0x001DC22C
		private void Awake()
		{
			LianDanUIMag.Instance = this;
			LianDanUIMag.Instance.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			LianDanUIMag.Instance.transform.localScale = Vector3.one;
			LianDanUIMag.Instance.transform.localPosition = Vector3.zero;
			LianDanUIMag.Instance.transform.SetAsLastSibling();
			LianDanUIMag.Instance.Init();
			if (UIHeadPanel.Inst != null)
			{
				UIHeadPanel.Inst.transform.SetAsLastSibling();
			}
			if (UIMiniTaskPanel.Inst != null)
			{
				UIMiniTaskPanel.Inst.transform.SetAsLastSibling();
			}
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x001DE0D8 File Offset: 0x001DC2D8
		private void Init()
		{
			this.LianDanResult = new LianDanResult(base.transform.Find("炼丹结果").gameObject);
			this.LianDanPanel = new LianDanPanel(base.transform.Find("炼丹界面").gameObject);
			this.DanFangPanel = new DanFangPanel(base.transform.Find("丹方").gameObject);
			this.DanFangPanel.UpdateFilter(0);
			this.PutDanLuPanel = new PutDanLuPanel(base.transform.Find("放入丹炉界面").gameObject);
			this.DanLuBag.Init(0, true);
			this.CaoYaoBag.Init(0, true);
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x001DE18B File Offset: 0x001DC38B
		public void Close()
		{
			PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹, 0);
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x001DE1A4 File Offset: 0x001DC3A4
		public void LianDanCallBack()
		{
			foreach (BigDanFang bigDanFang in this.DanFangPanel.DanFangList)
			{
				bigDanFang.UpdateState();
			}
			this.DanLuBag.CreateTempList();
			this.DanLuBag.UpdateItem(false);
			this.LianDanPanel.CheckCanMade();
			this.LianDanPanel.DanLuUI();
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x001DE228 File Offset: 0x001DC428
		public bool CheckCanLianZhi(JSONObject child)
		{
			if (child == null)
			{
				return false;
			}
			List<int> list = child["Type"].ToList();
			List<int> list2 = child["Num"].ToList();
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int i = 0; i < list.Count; i++)
			{
				if (!dictionary.ContainsKey(list[i]))
				{
					dictionary.Add(list[i], list2[i]);
				}
				else
				{
					Dictionary<int, int> dictionary2 = dictionary;
					int key = list[i];
					dictionary2[key] += list2[i];
				}
			}
			foreach (int num in dictionary.Keys)
			{
				if (dictionary[num] > 0)
				{
					bool flag = false;
					foreach (ITEM_INFO item_INFO in Tools.instance.getPlayer().itemList.values)
					{
						if (num == item_INFO.itemId && (long)dictionary[num] <= (long)((ulong)item_INFO.itemCount))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x001DE38C File Offset: 0x001DC58C
		private void OnDestroy()
		{
			LianDanUIMag.Instance = null;
			PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹, 1);
			if (UIMiniTaskPanel.Inst != null)
			{
				UIMiniTaskPanel.Inst.transform.SetAsFirstSibling();
			}
			if (UIHeadPanel.Inst != null)
			{
				UIHeadPanel.Inst.transform.SetAsFirstSibling();
			}
		}

		// Token: 0x0400480C RID: 18444
		public static LianDanUIMag Instance;

		// Token: 0x0400480D RID: 18445
		public DanFangPanel DanFangPanel;

		// Token: 0x0400480E RID: 18446
		public PutDanLuPanel PutDanLuPanel;

		// Token: 0x0400480F RID: 18447
		public LianDanPanel LianDanPanel;

		// Token: 0x04004810 RID: 18448
		public LianDanResult LianDanResult;

		// Token: 0x04004811 RID: 18449
		public DanLuBag DanLuBag;

		// Token: 0x04004812 RID: 18450
		public BagItemSelect Select;

		// Token: 0x04004813 RID: 18451
		public CaoYaoBag CaoYaoBag;

		// Token: 0x04004814 RID: 18452
		public Transform Vector2;
	}
}
