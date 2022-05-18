using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000DDE RID: 3550
	public class TuJianSearcher : MonoBehaviour
	{
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600559D RID: 21917 RVA: 0x0003D3CE File Offset: 0x0003B5CE
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

		// Token: 0x0600559E RID: 21918 RVA: 0x0003D3E2 File Offset: 0x0003B5E2
		private void Awake()
		{
			TuJianSearcher.Inst = this;
		}

		// Token: 0x0600559F RID: 21919 RVA: 0x0003D3EA File Offset: 0x0003B5EA
		private void Start()
		{
			this.input.text = "";
			this.input.onValueChanged.RemoveAllListeners();
			this.input.onValueChanged.AddListener(new UnityAction<string>(this.Search));
		}

		// Token: 0x060055A0 RID: 21920 RVA: 0x0003D428 File Offset: 0x0003B628
		public void Search(string str)
		{
			this.searchStrs = str.Split(new char[]
			{
				' '
			});
			TuJianManager.Inst.NeedRefreshDataList = true;
			TuJianManager.TabDict[TuJianManager.Inst.NowTuJianTab].RefreshPanel(false);
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x0003D466 File Offset: 0x0003B666
		public void ClearSearchStrAndNoSearch()
		{
			this.searchStrs = null;
		}

		// Token: 0x060055A2 RID: 21922 RVA: 0x00239DD4 File Offset: 0x00237FD4
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

		// Token: 0x0400555B RID: 21851
		public static TuJianSearcher Inst;

		// Token: 0x0400555C RID: 21852
		public InputField input;

		// Token: 0x0400555D RID: 21853
		private string[] searchStrs;
	}
}
