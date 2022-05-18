using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D3B RID: 3387
	[Serializable]
	public abstract class BaseSkill : ISkill
	{
		// Token: 0x0600506A RID: 20586
		public abstract void SetSkill(int id, int level);

		// Token: 0x0600506B RID: 20587
		public abstract BaseSkill Clone();

		// Token: 0x0600506C RID: 20588 RVA: 0x0000B171 File Offset: 0x00009371
		public virtual Sprite GetIconSprite()
		{
			return null;
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x00039EC9 File Offset: 0x000380C9
		public int GetImgQuality()
		{
			return this.Quality * 2;
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x00219C84 File Offset: 0x00217E84
		public virtual Sprite GetQualitySprite()
		{
			return BagMag.Inst.QualityDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x00219CB0 File Offset: 0x00217EB0
		public virtual Sprite GetQualityUpSprite()
		{
			return BagMag.Inst.QualityUpDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x06005070 RID: 20592
		public abstract string GetDesc1();

		// Token: 0x06005071 RID: 20593 RVA: 0x00219CDC File Offset: 0x00217EDC
		public virtual string GetDesc2()
		{
			foreach (_ItemJsonData itemJsonData in _ItemJsonData.DataList)
			{
				if (itemJsonData.desc.Replace(".0", "") == this.SkillId.ToString())
				{
					return itemJsonData.desc2;
				}
			}
			return "暂无";
		}

		// Token: 0x06005072 RID: 20594
		public abstract string GetTypeName();

		// Token: 0x06005073 RID: 20595 RVA: 0x00039ED3 File Offset: 0x000380D3
		public string GetQualityName()
		{
			return this.GetPinJie() + this.GetPinJieName();
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x00219D60 File Offset: 0x00217F60
		private string GetPinJie()
		{
			switch (this.Quality)
			{
			case 1:
				return "人阶";
			case 2:
				return "地阶";
			case 3:
				return "天阶";
			default:
				return "无";
			}
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x00219DA4 File Offset: 0x00217FA4
		private string GetPinJieName()
		{
			switch (this.PinJie)
			{
			case 1:
				return "下";
			case 2:
				return "中";
			case 3:
				return "上";
			default:
				return "无";
			}
		}

		// Token: 0x06005076 RID: 20598
		public abstract List<int> GetCiZhuiList();

		// Token: 0x06005077 RID: 20599
		public abstract bool SkillTypeIsEqual(int skIllType);

		// Token: 0x040051C2 RID: 20930
		public int Id;

		// Token: 0x040051C3 RID: 20931
		public int SkillId;

		// Token: 0x040051C4 RID: 20932
		public int Level;

		// Token: 0x040051C5 RID: 20933
		public int Quality;

		// Token: 0x040051C6 RID: 20934
		public string Name;

		// Token: 0x040051C7 RID: 20935
		public int PinJie;

		// Token: 0x040051C8 RID: 20936
		public CanSlotType CanPutSlotType;
	}
}
