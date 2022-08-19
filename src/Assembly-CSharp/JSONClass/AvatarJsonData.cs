using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000750 RID: 1872
	public class AvatarJsonData : IJSONClass
	{
		// Token: 0x06003B54 RID: 15188 RVA: 0x001984BC File Offset: 0x001966BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AvatarJsonData.list)
			{
				try
				{
					AvatarJsonData avatarJsonData = new AvatarJsonData();
					avatarJsonData.id = jsonobject["id"].I;
					avatarJsonData.face = jsonobject["face"].I;
					avatarJsonData.fightFace = jsonobject["fightFace"].I;
					avatarJsonData.SexType = jsonobject["SexType"].I;
					avatarJsonData.AvatarType = jsonobject["AvatarType"].I;
					avatarJsonData.Level = jsonobject["Level"].I;
					avatarJsonData.HP = jsonobject["HP"].I;
					avatarJsonData.dunSu = jsonobject["dunSu"].I;
					avatarJsonData.ziZhi = jsonobject["ziZhi"].I;
					avatarJsonData.wuXin = jsonobject["wuXin"].I;
					avatarJsonData.shengShi = jsonobject["shengShi"].I;
					avatarJsonData.shaQi = jsonobject["shaQi"].I;
					avatarJsonData.shouYuan = jsonobject["shouYuan"].I;
					avatarJsonData.age = jsonobject["age"].I;
					avatarJsonData.equipWeapon = jsonobject["equipWeapon"].I;
					avatarJsonData.equipClothing = jsonobject["equipClothing"].I;
					avatarJsonData.equipRing = jsonobject["equipRing"].I;
					avatarJsonData.yuanying = jsonobject["yuanying"].I;
					avatarJsonData.HuaShenLingYu = jsonobject["HuaShenLingYu"].I;
					avatarJsonData.MoneyType = jsonobject["MoneyType"].I;
					avatarJsonData.IsRefresh = jsonobject["IsRefresh"].I;
					avatarJsonData.dropType = jsonobject["dropType"].I;
					avatarJsonData.canjiaPaiMai = jsonobject["canjiaPaiMai"].I;
					avatarJsonData.wudaoType = jsonobject["wudaoType"].I;
					avatarJsonData.XinQuType = jsonobject["XinQuType"].I;
					avatarJsonData.gudingjiage = jsonobject["gudingjiage"].I;
					avatarJsonData.sellPercent = jsonobject["sellPercent"].I;
					avatarJsonData.Title = jsonobject["Title"].Str;
					avatarJsonData.FirstName = jsonobject["FirstName"].Str;
					avatarJsonData.Name = jsonobject["Name"].Str;
					avatarJsonData.menPai = jsonobject["menPai"].Str;
					avatarJsonData.LingGen = jsonobject["LingGen"].ToList();
					avatarJsonData.skills = jsonobject["skills"].ToList();
					avatarJsonData.staticSkills = jsonobject["staticSkills"].ToList();
					avatarJsonData.paimaifenzu = jsonobject["paimaifenzu"].ToList();
					if (AvatarJsonData.DataDict.ContainsKey(avatarJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AvatarJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", avatarJsonData.id));
					}
					else
					{
						AvatarJsonData.DataDict.Add(avatarJsonData.id, avatarJsonData);
						AvatarJsonData.DataList.Add(avatarJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AvatarJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AvatarJsonData.OnInitFinishAction != null)
			{
				AvatarJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400340B RID: 13323
		public static Dictionary<int, AvatarJsonData> DataDict = new Dictionary<int, AvatarJsonData>();

		// Token: 0x0400340C RID: 13324
		public static List<AvatarJsonData> DataList = new List<AvatarJsonData>();

		// Token: 0x0400340D RID: 13325
		public static Action OnInitFinishAction = new Action(AvatarJsonData.OnInitFinish);

		// Token: 0x0400340E RID: 13326
		public int id;

		// Token: 0x0400340F RID: 13327
		public int face;

		// Token: 0x04003410 RID: 13328
		public int fightFace;

		// Token: 0x04003411 RID: 13329
		public int SexType;

		// Token: 0x04003412 RID: 13330
		public int AvatarType;

		// Token: 0x04003413 RID: 13331
		public int Level;

		// Token: 0x04003414 RID: 13332
		public int HP;

		// Token: 0x04003415 RID: 13333
		public int dunSu;

		// Token: 0x04003416 RID: 13334
		public int ziZhi;

		// Token: 0x04003417 RID: 13335
		public int wuXin;

		// Token: 0x04003418 RID: 13336
		public int shengShi;

		// Token: 0x04003419 RID: 13337
		public int shaQi;

		// Token: 0x0400341A RID: 13338
		public int shouYuan;

		// Token: 0x0400341B RID: 13339
		public int age;

		// Token: 0x0400341C RID: 13340
		public int equipWeapon;

		// Token: 0x0400341D RID: 13341
		public int equipClothing;

		// Token: 0x0400341E RID: 13342
		public int equipRing;

		// Token: 0x0400341F RID: 13343
		public int yuanying;

		// Token: 0x04003420 RID: 13344
		public int HuaShenLingYu;

		// Token: 0x04003421 RID: 13345
		public int MoneyType;

		// Token: 0x04003422 RID: 13346
		public int IsRefresh;

		// Token: 0x04003423 RID: 13347
		public int dropType;

		// Token: 0x04003424 RID: 13348
		public int canjiaPaiMai;

		// Token: 0x04003425 RID: 13349
		public int wudaoType;

		// Token: 0x04003426 RID: 13350
		public int XinQuType;

		// Token: 0x04003427 RID: 13351
		public int gudingjiage;

		// Token: 0x04003428 RID: 13352
		public int sellPercent;

		// Token: 0x04003429 RID: 13353
		public string Title;

		// Token: 0x0400342A RID: 13354
		public string FirstName;

		// Token: 0x0400342B RID: 13355
		public string Name;

		// Token: 0x0400342C RID: 13356
		public string menPai;

		// Token: 0x0400342D RID: 13357
		public List<int> LingGen = new List<int>();

		// Token: 0x0400342E RID: 13358
		public List<int> skills = new List<int>();

		// Token: 0x0400342F RID: 13359
		public List<int> staticSkills = new List<int>();

		// Token: 0x04003430 RID: 13360
		public List<int> paimaifenzu = new List<int>();
	}
}
