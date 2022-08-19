using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A7C RID: 2684
	public class SetAvatarFaceRandomInfo : MonoBehaviour
	{
		// Token: 0x06004B5B RID: 19291 RVA: 0x00200021 File Offset: 0x001FE221
		private void Awake()
		{
			SetAvatarFaceRandomInfo.inst = this;
			this.UpdateNPCDate();
			Object.DontDestroyOnLoad(this);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00200038 File Offset: 0x001FE238
		public List<AvatarFaceInfo> findType(SetAvatarFaceRandomInfo.InfoName type)
		{
			List<AvatarFaceInfo> list = new List<AvatarFaceInfo>();
			foreach (AvatarFaceInfo avatarFaceInfo in this.RandomInfo)
			{
				if (avatarFaceInfo.SkinTypeName == type)
				{
					list.Add(avatarFaceInfo);
				}
			}
			return list;
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x0020009C File Offset: 0x001FE29C
		public int findStatic(int avatarID, SetAvatarFaceRandomInfo.InfoName type)
		{
			foreach (StaticFaceInfo staticFaceInfo in this.StaticRandomInfo)
			{
				if (staticFaceInfo.AvatarScope == avatarID)
				{
					foreach (StaticFaceRandomInfo staticFaceRandomInfo in staticFaceInfo.faceinfoList)
					{
						if (staticFaceRandomInfo.SkinTypeName == type)
						{
							return staticFaceRandomInfo.SkinTypeScope;
						}
					}
				}
			}
			return -100;
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x00200144 File Offset: 0x001FE344
		public void UpdateNPCDate()
		{
			this.StaticRandomInfo = (List<StaticFaceInfo>)ResManager.inst.LoadObject("Data/StaticRandomInfo.bin");
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x00200160 File Offset: 0x001FE360
		public AvatarFaceInfo FindAvatarType(int AvatarID, SetAvatarFaceRandomInfo.InfoName type)
		{
			foreach (AvatarFaceInfo avatarFaceInfo in this.findType(type))
			{
				foreach (Vector2 vector in avatarFaceInfo.AvatarScope)
				{
					if (this.NumIn((int)vector.x, (int)vector.y, AvatarID))
					{
						return avatarFaceInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x00200208 File Offset: 0x001FE408
		public bool NumIn(int min, int max, int num)
		{
			return num > min && num < max;
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x00200218 File Offset: 0x001FE418
		public StaticFaceInfo getStaticFace(int AvatarID)
		{
			foreach (StaticFaceInfo staticFaceInfo in this.StaticRandomInfo)
			{
				if (staticFaceInfo.AvatarScope == AvatarID)
				{
					return staticFaceInfo;
				}
			}
			return null;
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x00200274 File Offset: 0x001FE474
		public int getFace(int AvatarID, SetAvatarFaceRandomInfo.InfoName type)
		{
			AvatarFaceInfo avatarFaceInfo = this.FindAvatarType(AvatarID, type);
			int num = this.findStatic(AvatarID, type);
			List<int> list = new List<int>();
			if (num != -100)
			{
				return num;
			}
			if (avatarFaceInfo == null)
			{
				return -100;
			}
			foreach (Vector2 vector in avatarFaceInfo.SkinTypeScope)
			{
				for (int i = (int)vector.x; i <= (int)vector.y; i++)
				{
					list.Add(i);
				}
			}
			if (list.Count == 0)
			{
				return -100;
			}
			return list[jsonData.instance.QuikeGetRandom() % list.Count];
		}

		// Token: 0x04004A7A RID: 19066
		public List<AvatarFaceInfo> RandomInfo = new List<AvatarFaceInfo>();

		// Token: 0x04004A7B RID: 19067
		public List<StaticFaceInfo> StaticRandomInfo = new List<StaticFaceInfo>();

		// Token: 0x04004A7C RID: 19068
		public static SetAvatarFaceRandomInfo inst;

		// Token: 0x020015A5 RID: 5541
		public enum InfoName
		{
			// Token: 0x04006FF4 RID: 28660
			[Description("头发")]
			hair,
			// Token: 0x04006FF5 RID: 28661
			[Description("嘴巴")]
			mouth,
			// Token: 0x04006FF6 RID: 28662
			[Description("鼻子")]
			nose,
			// Token: 0x04006FF7 RID: 28663
			[Description("眼睛")]
			eyes,
			// Token: 0x04006FF8 RID: 28664
			[Description("眉毛")]
			eyebrow,
			// Token: 0x04006FF9 RID: 28665
			[Description("头部")]
			head,
			// Token: 0x04006FFA RID: 28666
			[Description("服饰")]
			a_suit,
			// Token: 0x04006FFB RID: 28667
			[Description("上胡")]
			a_hair,
			// Token: 0x04006FFC RID: 28668
			[Description("下胡")]
			b_hair,
			// Token: 0x04006FFD RID: 28669
			[Description("面部特征")]
			characteristic,
			// Token: 0x04006FFE RID: 28670
			[Description("发色")]
			hairColorR,
			// Token: 0x04006FFF RID: 28671
			[Description("眼珠颜色")]
			yanzhuColor,
			// Token: 0x04007000 RID: 28672
			[Description("特征颜色")]
			tezhengColor,
			// Token: 0x04007001 RID: 28673
			[Description("眉毛颜色")]
			eyebrowColor,
			// Token: 0x04007002 RID: 28674
			[Description("女修特征")]
			feature,
			// Token: 0x04007003 RID: 28675
			[Description("女修特征")]
			yanying
		}
	}
}
