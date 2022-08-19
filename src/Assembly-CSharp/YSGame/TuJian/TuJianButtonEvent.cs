using System;
using GUIPackage;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000AAD RID: 2733
	public class TuJianButtonEvent : MonoBehaviour
	{
		// Token: 0x06004C9A RID: 19610 RVA: 0x0020C6B8 File Offset: 0x0020A8B8
		public void CloseTuJian()
		{
			TuJianManager.Inst.CloseTuJian();
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x0020C6C4 File Offset: 0x0020A8C4
		public void ReturnLastPage()
		{
			TuJianManager.Inst.ReturnHyperlink();
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x0020C6D0 File Offset: 0x0020A8D0
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

		// Token: 0x06004C9D RID: 19613 RVA: 0x0020C7FB File Offset: 0x0020A9FB
		public void FightLink()
		{
			TuJianManager.Inst.OnHyperlink("2_502_0");
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x0020C80C File Offset: 0x0020AA0C
		public void JiaoYiLink()
		{
			TuJianManager.Inst.OnHyperlink("2_505_4");
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x0020C81D File Offset: 0x0020AA1D
		public void ZhuJiLink()
		{
			TuJianManager.Inst.OnHyperlink("2_507_1");
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x0020C82E File Offset: 0x0020AA2E
		public void JieDanLink()
		{
			TuJianManager.Inst.OnHyperlink("2_507_2");
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x0020C83F File Offset: 0x0020AA3F
		public void JieYingLink()
		{
			TuJianManager.Inst.OnHyperlink("2_507_3");
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x0020C850 File Offset: 0x0020AA50
		public void HuaShenLink()
		{
			TuJianManager.Inst.OnHyperlink("2_507_4");
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x0020C861 File Offset: 0x0020AA61
		public void NPCInfoLink()
		{
			TuJianManager.Inst.OnHyperlink("2_109_0");
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x0020C872 File Offset: 0x0020AA72
		public void QingJiaoLink()
		{
			TuJianManager.Inst.OnHyperlink("2_505_6");
		}
	}
}
