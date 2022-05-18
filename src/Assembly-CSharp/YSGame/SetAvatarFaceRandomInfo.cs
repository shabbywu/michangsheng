using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DAB RID: 3499
	public class SetAvatarFaceRandomInfo : MonoBehaviour
	{
		// Token: 0x0600546F RID: 21615 RVA: 0x0003C71A File Offset: 0x0003A91A
		private void Awake()
		{
			SetAvatarFaceRandomInfo.inst = this;
			this.UpdateNPCDate();
			Object.DontDestroyOnLoad(this);
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x00231A40 File Offset: 0x0022FC40
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

		// Token: 0x06005471 RID: 21617 RVA: 0x00231AA4 File Offset: 0x0022FCA4
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

		// Token: 0x06005472 RID: 21618 RVA: 0x0003C72E File Offset: 0x0003A92E
		public void UpdateNPCDate()
		{
			this.StaticRandomInfo = (List<StaticFaceInfo>)ResManager.inst.LoadObject("Data/StaticRandomInfo.bin");
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x00231B4C File Offset: 0x0022FD4C
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

		// Token: 0x06005474 RID: 21620 RVA: 0x0003C74A File Offset: 0x0003A94A
		public bool NumIn(int min, int max, int num)
		{
			return num > min && num < max;
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x00231BF4 File Offset: 0x0022FDF4
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

		// Token: 0x06005476 RID: 21622 RVA: 0x00231C50 File Offset: 0x0022FE50
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

		// Token: 0x0400541F RID: 21535
		public List<AvatarFaceInfo> RandomInfo = new List<AvatarFaceInfo>();

		// Token: 0x04005420 RID: 21536
		public List<StaticFaceInfo> StaticRandomInfo = new List<StaticFaceInfo>();

		// Token: 0x04005421 RID: 21537
		public static SetAvatarFaceRandomInfo inst;

		// Token: 0x02000DAC RID: 3500
		public enum InfoName
		{
			// Token: 0x04005423 RID: 21539
			[Description("头发")]
			hair,
			// Token: 0x04005424 RID: 21540
			[Description("嘴巴")]
			mouth,
			// Token: 0x04005425 RID: 21541
			[Description("鼻子")]
			nose,
			// Token: 0x04005426 RID: 21542
			[Description("眼睛")]
			eyes,
			// Token: 0x04005427 RID: 21543
			[Description("眉毛")]
			eyebrow,
			// Token: 0x04005428 RID: 21544
			[Description("头部")]
			head,
			// Token: 0x04005429 RID: 21545
			[Description("服饰")]
			a_suit,
			// Token: 0x0400542A RID: 21546
			[Description("上胡")]
			a_hair,
			// Token: 0x0400542B RID: 21547
			[Description("下胡")]
			b_hair,
			// Token: 0x0400542C RID: 21548
			[Description("面部特征")]
			characteristic,
			// Token: 0x0400542D RID: 21549
			[Description("发色")]
			hairColorR,
			// Token: 0x0400542E RID: 21550
			[Description("眼珠颜色")]
			yanzhuColor,
			// Token: 0x0400542F RID: 21551
			[Description("特征颜色")]
			tezhengColor,
			// Token: 0x04005430 RID: 21552
			[Description("眉毛颜色")]
			eyebrowColor,
			// Token: 0x04005431 RID: 21553
			[Description("女修特征")]
			feature,
			// Token: 0x04005432 RID: 21554
			[Description("女修特征")]
			yanying
		}
	}
}
