using System;
using GUIPackage;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000DEA RID: 3562
	public class TuJianButtonEvent : MonoBehaviour
	{
		// Token: 0x060055E7 RID: 21991 RVA: 0x0003D746 File Offset: 0x0003B946
		public void CloseTuJian()
		{
			TuJianManager.Inst.CloseTuJian();
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x0003D752 File Offset: 0x0003B952
		public void ReturnLastPage()
		{
			TuJianManager.Inst.ReturnHyperlink();
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x0023D614 File Offset: 0x0023B814
		public void BeiBaoToTuJian()
		{
			Transform parent = base.transform.parent;
			if (parent.Find("Panel/属性").gameObject.activeInHierarchy)
			{
				TuJianManager.Inst.OnHyperlink("2_503_1");
			}
			if (parent.Find("Panel/悟道").gameObject.activeInHierarchy)
			{
				TuJianManager.Inst.OnHyperlink("2_503_2");
			}
			if (parent.Find("Panel/功法").gameObject.activeInHierarchy)
			{
				TuJianManager.Inst.OnHyperlink("2_503_3");
			}
			if (parent.Find("Panel/神通").gameObject.activeInHierarchy)
			{
				TuJianManager.Inst.OnHyperlink("2_503_4");
			}
			if (parent.Find("Panel/物品").gameObject.activeInHierarchy)
			{
				TuJianManager.Inst.OnHyperlink("2_503_5");
			}
			if (parent.Find("Panel/声望").gameObject.activeInHierarchy)
			{
				TuJianManager.Inst.OnHyperlink("2_503_6");
			}
			if (parent.Find("Panel/系统").gameObject.activeInHierarchy)
			{
				TuJianManager.Inst.OnHyperlink("2_104_0");
			}
			Singleton.ints.ClickTab();
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x0003D75E File Offset: 0x0003B95E
		public void FightLink()
		{
			TuJianManager.Inst.OnHyperlink("2_502_0");
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x0003D76F File Offset: 0x0003B96F
		public void JiaoYiLink()
		{
			TuJianManager.Inst.OnHyperlink("2_505_4");
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x0003D780 File Offset: 0x0003B980
		public void ZhuJiLink()
		{
			TuJianManager.Inst.OnHyperlink("2_507_1");
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x0003D791 File Offset: 0x0003B991
		public void JieDanLink()
		{
			TuJianManager.Inst.OnHyperlink("2_507_2");
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x0003D7A2 File Offset: 0x0003B9A2
		public void JieYingLink()
		{
			TuJianManager.Inst.OnHyperlink("2_507_3");
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x0003D7B3 File Offset: 0x0003B9B3
		public void HuaShenLink()
		{
			TuJianManager.Inst.OnHyperlink("2_507_4");
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
		public void NPCInfoLink()
		{
			TuJianManager.Inst.OnHyperlink("2_109_0");
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x0003D7D5 File Offset: 0x0003B9D5
		public void QingJiaoLink()
		{
			TuJianManager.Inst.OnHyperlink("2_505_6");
		}
	}
}
