using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x020009B3 RID: 2483
	[Serializable]
	public abstract class BaseSkill : ISkill
	{
		// Token: 0x06004507 RID: 17671
		public abstract void SetSkill(int id, int level);

		// Token: 0x06004508 RID: 17672
		public abstract BaseSkill Clone();

		// Token: 0x06004509 RID: 17673 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public virtual Sprite GetIconSprite()
		{
			return null;
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x001D5734 File Offset: 0x001D3934
		public int GetImgQuality()
		{
			return this.Quality * 2;
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x001D5740 File Offset: 0x001D3940
		public virtual Sprite GetQualitySprite()
		{
			return BagMag.Inst.QualityDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x001D576C File Offset: 0x001D396C
		public virtual Sprite GetQualityUpSprite()
		{
			return BagMag.Inst.QualityUpDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x0600450D RID: 17677
		public abstract string GetDesc1();

		// Token: 0x0600450E RID: 17678 RVA: 0x001D5798 File Offset: 0x001D3998
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

		// Token: 0x0600450F RID: 17679
		public abstract string GetTypeName();

		// Token: 0x06004510 RID: 17680 RVA: 0x001D581C File Offset: 0x001D3A1C
		public string GetQualityName()
		{
			return this.GetPinJie() + this.GetPinJieName();
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x001D5830 File Offset: 0x001D3A30
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

		// Token: 0x06004512 RID: 17682 RVA: 0x001D5874 File Offset: 0x001D3A74
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

		// Token: 0x06004513 RID: 17683
		public abstract List<int> GetCiZhuiList();

		// Token: 0x06004514 RID: 17684
		public abstract bool SkillTypeIsEqual(int skIllType);

		// Token: 0x040046C0 RID: 18112
		public int Id;

		// Token: 0x040046C1 RID: 18113
		public int SkillId;

		// Token: 0x040046C2 RID: 18114
		public int Level;

		// Token: 0x040046C3 RID: 18115
		public int Quality;

		// Token: 0x040046C4 RID: 18116
		public string Name;

		// Token: 0x040046C5 RID: 18117
		public int PinJie;

		// Token: 0x040046C6 RID: 18118
		public CanSlotType CanPutSlotType;
	}
}
