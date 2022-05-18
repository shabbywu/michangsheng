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
	// Token: 0x02000AC2 RID: 2754
	public class LianDanUIMag : MonoBehaviour
	{
		// Token: 0x06004652 RID: 18002 RVA: 0x001DFB00 File Offset: 0x001DDD00
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

		// Token: 0x06004653 RID: 18003 RVA: 0x001DFBAC File Offset: 0x001DDDAC
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

		// Token: 0x06004654 RID: 18004 RVA: 0x000323CF File Offset: 0x000305CF
		public void Close()
		{
			PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹, 0);
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x001DFC60 File Offset: 0x001DDE60
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

		// Token: 0x06004656 RID: 18006 RVA: 0x000FCCF4 File Offset: 0x000FAEF4
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

		// Token: 0x06004657 RID: 18007 RVA: 0x001DFCE4 File Offset: 0x001DDEE4
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

		// Token: 0x04003E76 RID: 15990
		public static LianDanUIMag Instance;

		// Token: 0x04003E77 RID: 15991
		public DanFangPanel DanFangPanel;

		// Token: 0x04003E78 RID: 15992
		public PutDanLuPanel PutDanLuPanel;

		// Token: 0x04003E79 RID: 15993
		public LianDanPanel LianDanPanel;

		// Token: 0x04003E7A RID: 15994
		public LianDanResult LianDanResult;

		// Token: 0x04003E7B RID: 15995
		public DanLuBag DanLuBag;

		// Token: 0x04003E7C RID: 15996
		public BagItemSelect Select;

		// Token: 0x04003E7D RID: 15997
		public CaoYaoBag CaoYaoBag;

		// Token: 0x04003E7E RID: 15998
		public Transform Vector2;
	}
}
