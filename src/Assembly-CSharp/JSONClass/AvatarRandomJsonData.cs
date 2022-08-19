using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000752 RID: 1874
	public class AvatarRandomJsonData : IJSONClass
	{
		// Token: 0x06003B5C RID: 15196 RVA: 0x00198AA4 File Offset: 0x00196CA4
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

		// Token: 0x06003B5D RID: 15197 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003438 RID: 13368
		public static Dictionary<int, AvatarRandomJsonData> DataDict = new Dictionary<int, AvatarRandomJsonData>();

		// Token: 0x04003439 RID: 13369
		public static List<AvatarRandomJsonData> DataList = new List<AvatarRandomJsonData>();

		// Token: 0x0400343A RID: 13370
		public static Action OnInitFinishAction = new Action(AvatarRandomJsonData.OnInitFinish);

		// Token: 0x0400343B RID: 13371
		public int Sex;

		// Token: 0x0400343C RID: 13372
		public int feature;

		// Token: 0x0400343D RID: 13373
		public int yanying;

		// Token: 0x0400343E RID: 13374
		public int Shawl_hair;

		// Token: 0x0400343F RID: 13375
		public int back_gown;

		// Token: 0x04003440 RID: 13376
		public int r_arm;

		// Token: 0x04003441 RID: 13377
		public int gown;

		// Token: 0x04003442 RID: 13378
		public int l_arm;

		// Token: 0x04003443 RID: 13379
		public int l_big_arm;

		// Token: 0x04003444 RID: 13380
		public int lower_body;

		// Token: 0x04003445 RID: 13381
		public int r_big_arm;

		// Token: 0x04003446 RID: 13382
		public int blush;

		// Token: 0x04003447 RID: 13383
		public int tattoo;

		// Token: 0x04003448 RID: 13384
		public int shoes;

		// Token: 0x04003449 RID: 13385
		public int upper_body;

		// Token: 0x0400344A RID: 13386
		public int yanqiu;

		// Token: 0x0400344B RID: 13387
		public int hairColorG;

		// Token: 0x0400344C RID: 13388
		public int hairColorB;

		// Token: 0x0400344D RID: 13389
		public int mouthColor;

		// Token: 0x0400344E RID: 13390
		public int tattooColor;

		// Token: 0x0400344F RID: 13391
		public int blushColor;

		// Token: 0x04003450 RID: 13392
		public int HaoGanDu;

		// Token: 0x04003451 RID: 13393
		public int head;

		// Token: 0x04003452 RID: 13394
		public int eyes;

		// Token: 0x04003453 RID: 13395
		public int mouth;

		// Token: 0x04003454 RID: 13396
		public int nose;

		// Token: 0x04003455 RID: 13397
		public int eyebrow;

		// Token: 0x04003456 RID: 13398
		public int hair;

		// Token: 0x04003457 RID: 13399
		public int a_hair;

		// Token: 0x04003458 RID: 13400
		public int b_hair;

		// Token: 0x04003459 RID: 13401
		public int characteristic;

		// Token: 0x0400345A RID: 13402
		public int a_suit;

		// Token: 0x0400345B RID: 13403
		public int hairColorR;

		// Token: 0x0400345C RID: 13404
		public int yanzhuColor;

		// Token: 0x0400345D RID: 13405
		public int tezhengColor;

		// Token: 0x0400345E RID: 13406
		public int eyebrowColor;

		// Token: 0x0400345F RID: 13407
		public string BirthdayTime;

		// Token: 0x04003460 RID: 13408
		public string Name;
	}
}
