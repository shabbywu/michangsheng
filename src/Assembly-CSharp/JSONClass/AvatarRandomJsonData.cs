using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AEA RID: 2794
	public class AvatarRandomJsonData : IJSONClass
	{
		// Token: 0x06004712 RID: 18194 RVA: 0x001E6B40 File Offset: 0x001E4D40
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AvatarRandomJsonData.list)
			{
				try
				{
					AvatarRandomJsonData avatarRandomJsonData = new AvatarRandomJsonData();
					avatarRandomJsonData.Sex = jsonobject["Sex"].I;
					avatarRandomJsonData.feature = jsonobject["feature"].I;
					avatarRandomJsonData.yanying = jsonobject["yanying"].I;
					avatarRandomJsonData.Shawl_hair = jsonobject["Shawl_hair"].I;
					avatarRandomJsonData.back_gown = jsonobject["back_gown"].I;
					avatarRandomJsonData.r_arm = jsonobject["r_arm"].I;
					avatarRandomJsonData.gown = jsonobject["gown"].I;
					avatarRandomJsonData.l_arm = jsonobject["l_arm"].I;
					avatarRandomJsonData.l_big_arm = jsonobject["l_big_arm"].I;
					avatarRandomJsonData.lower_body = jsonobject["lower_body"].I;
					avatarRandomJsonData.r_big_arm = jsonobject["r_big_arm"].I;
					avatarRandomJsonData.blush = jsonobject["blush"].I;
					avatarRandomJsonData.tattoo = jsonobject["tattoo"].I;
					avatarRandomJsonData.shoes = jsonobject["shoes"].I;
					avatarRandomJsonData.upper_body = jsonobject["upper_body"].I;
					avatarRandomJsonData.yanqiu = jsonobject["yanqiu"].I;
					avatarRandomJsonData.hairColorG = jsonobject["hairColorG"].I;
					avatarRandomJsonData.hairColorB = jsonobject["hairColorB"].I;
					avatarRandomJsonData.mouthColor = jsonobject["mouthColor"].I;
					avatarRandomJsonData.tattooColor = jsonobject["tattooColor"].I;
					avatarRandomJsonData.blushColor = jsonobject["blushColor"].I;
					avatarRandomJsonData.HaoGanDu = jsonobject["HaoGanDu"].I;
					avatarRandomJsonData.head = jsonobject["head"].I;
					avatarRandomJsonData.eyes = jsonobject["eyes"].I;
					avatarRandomJsonData.mouth = jsonobject["mouth"].I;
					avatarRandomJsonData.nose = jsonobject["nose"].I;
					avatarRandomJsonData.eyebrow = jsonobject["eyebrow"].I;
					avatarRandomJsonData.hair = jsonobject["hair"].I;
					avatarRandomJsonData.a_hair = jsonobject["a_hair"].I;
					avatarRandomJsonData.b_hair = jsonobject["b_hair"].I;
					avatarRandomJsonData.characteristic = jsonobject["characteristic"].I;
					avatarRandomJsonData.a_suit = jsonobject["a_suit"].I;
					avatarRandomJsonData.hairColorR = jsonobject["hairColorR"].I;
					avatarRandomJsonData.yanzhuColor = jsonobject["yanzhuColor"].I;
					avatarRandomJsonData.tezhengColor = jsonobject["tezhengColor"].I;
					avatarRandomJsonData.eyebrowColor = jsonobject["eyebrowColor"].I;
					avatarRandomJsonData.BirthdayTime = jsonobject["BirthdayTime"].Str;
					avatarRandomJsonData.Name = jsonobject["Name"].Str;
					if (AvatarRandomJsonData.DataDict.ContainsKey(avatarRandomJsonData.Sex))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AvatarRandomJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", avatarRandomJsonData.Sex));
					}
					else
					{
						AvatarRandomJsonData.DataDict.Add(avatarRandomJsonData.Sex, avatarRandomJsonData);
						AvatarRandomJsonData.DataList.Add(avatarRandomJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AvatarRandomJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AvatarRandomJsonData.OnInitFinishAction != null)
			{
				AvatarRandomJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FD1 RID: 16337
		public static Dictionary<int, AvatarRandomJsonData> DataDict = new Dictionary<int, AvatarRandomJsonData>();

		// Token: 0x04003FD2 RID: 16338
		public static List<AvatarRandomJsonData> DataList = new List<AvatarRandomJsonData>();

		// Token: 0x04003FD3 RID: 16339
		public static Action OnInitFinishAction = new Action(AvatarRandomJsonData.OnInitFinish);

		// Token: 0x04003FD4 RID: 16340
		public int Sex;

		// Token: 0x04003FD5 RID: 16341
		public int feature;

		// Token: 0x04003FD6 RID: 16342
		public int yanying;

		// Token: 0x04003FD7 RID: 16343
		public int Shawl_hair;

		// Token: 0x04003FD8 RID: 16344
		public int back_gown;

		// Token: 0x04003FD9 RID: 16345
		public int r_arm;

		// Token: 0x04003FDA RID: 16346
		public int gown;

		// Token: 0x04003FDB RID: 16347
		public int l_arm;

		// Token: 0x04003FDC RID: 16348
		public int l_big_arm;

		// Token: 0x04003FDD RID: 16349
		public int lower_body;

		// Token: 0x04003FDE RID: 16350
		public int r_big_arm;

		// Token: 0x04003FDF RID: 16351
		public int blush;

		// Token: 0x04003FE0 RID: 16352
		public int tattoo;

		// Token: 0x04003FE1 RID: 16353
		public int shoes;

		// Token: 0x04003FE2 RID: 16354
		public int upper_body;

		// Token: 0x04003FE3 RID: 16355
		public int yanqiu;

		// Token: 0x04003FE4 RID: 16356
		public int hairColorG;

		// Token: 0x04003FE5 RID: 16357
		public int hairColorB;

		// Token: 0x04003FE6 RID: 16358
		public int mouthColor;

		// Token: 0x04003FE7 RID: 16359
		public int tattooColor;

		// Token: 0x04003FE8 RID: 16360
		public int blushColor;

		// Token: 0x04003FE9 RID: 16361
		public int HaoGanDu;

		// Token: 0x04003FEA RID: 16362
		public int head;

		// Token: 0x04003FEB RID: 16363
		public int eyes;

		// Token: 0x04003FEC RID: 16364
		public int mouth;

		// Token: 0x04003FED RID: 16365
		public int nose;

		// Token: 0x04003FEE RID: 16366
		public int eyebrow;

		// Token: 0x04003FEF RID: 16367
		public int hair;

		// Token: 0x04003FF0 RID: 16368
		public int a_hair;

		// Token: 0x04003FF1 RID: 16369
		public int b_hair;

		// Token: 0x04003FF2 RID: 16370
		public int characteristic;

		// Token: 0x04003FF3 RID: 16371
		public int a_suit;

		// Token: 0x04003FF4 RID: 16372
		public int hairColorR;

		// Token: 0x04003FF5 RID: 16373
		public int yanzhuColor;

		// Token: 0x04003FF6 RID: 16374
		public int tezhengColor;

		// Token: 0x04003FF7 RID: 16375
		public int eyebrowColor;

		// Token: 0x04003FF8 RID: 16376
		public string BirthdayTime;

		// Token: 0x04003FF9 RID: 16377
		public string Name;
	}
}
