using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000AA1 RID: 2721
	public class TuJianSearcher : MonoBehaviour
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06004C50 RID: 19536 RVA: 0x00208B02 File Offset: 0x00206D02
		public int SearchCount
		{
			get
			{
				if (this.searchStrs == null)
				{
					return 0;
				}
				return this.searchStrs.Length;
			}
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x00208B16 File Offset: 0x00206D16
		private void Awake()
		{
			TuJianSearcher.Inst = this;
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x00208B1E File Offset: 0x00206D1E
		private void Start()
		{
			this.input.text = "";
			this.input.onValueChanged.RemoveAllListeners();
			this.input.onValueChanged.AddListener(new UnityAction<string>(this.Search));
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x00208B5C File Offset: 0x00206D5C
		public void Search(string str)
		{
			this.searchStrs = str.Split(new char[]
			{
				' '
			});
			TuJianManager.Inst.NeedRefreshDataList = true;
			TuJianManager.TabDict[TuJianManager.Inst.NowTuJianTab].RefreshPanel(false);
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x00208B9A File Offset: 0x00206D9A
		public void ClearSearchStrAndNoSearch()
		{
			this.searchStrs = null;
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x00208BA4 File Offset: 0x00206DA4
		public bool IsContansSearch(string str)
		{
			if (this.searchStrs == null || this.searchStrs.Length == 0)
			{
				return true;
			}
			foreach (string value in this.searchStrs)
			{
				if (str.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004B7D RID: 19325
		public static TuJianSearcher Inst;

		// Token: 0x04004B7E RID: 19326
		public InputField input;

		// Token: 0x04004B7F RID: 19327
		private string[] searchStrs;
	}
}
