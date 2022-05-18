using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE8 RID: 2792
	public class AvatarJsonData : IJSONClass
	{
		// Token: 0x0600470A RID: 18186 RVA: 0x001E65DC File Offset: 0x001E47DC
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

		// Token: 0x0600470B RID: 18187 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FA4 RID: 16292
		public static Dictionary<int, AvatarJsonData> DataDict = new Dictionary<int, AvatarJsonData>();

		// Token: 0x04003FA5 RID: 16293
		public static List<AvatarJsonData> DataList = new List<AvatarJsonData>();

		// Token: 0x04003FA6 RID: 16294
		public static Action OnInitFinishAction = new Action(AvatarJsonData.OnInitFinish);

		// Token: 0x04003FA7 RID: 16295
		public int id;

		// Token: 0x04003FA8 RID: 16296
		public int face;

		// Token: 0x04003FA9 RID: 16297
		public int fightFace;

		// Token: 0x04003FAA RID: 16298
		public int SexType;

		// Token: 0x04003FAB RID: 16299
		public int AvatarType;

		// Token: 0x04003FAC RID: 16300
		public int Level;

		// Token: 0x04003FAD RID: 16301
		public int HP;

		// Token: 0x04003FAE RID: 16302
		public int dunSu;

		// Token: 0x04003FAF RID: 16303
		public int ziZhi;

		// Token: 0x04003FB0 RID: 16304
		public int wuXin;

		// Token: 0x04003FB1 RID: 16305
		public int shengShi;

		// Token: 0x04003FB2 RID: 16306
		public int shaQi;

		// Token: 0x04003FB3 RID: 16307
		public int shouYuan;

		// Token: 0x04003FB4 RID: 16308
		public int age;

		// Token: 0x04003FB5 RID: 16309
		public int equipWeapon;

		// Token: 0x04003FB6 RID: 16310
		public int equipClothing;

		// Token: 0x04003FB7 RID: 16311
		public int equipRing;

		// Token: 0x04003FB8 RID: 16312
		public int yuanying;

		// Token: 0x04003FB9 RID: 16313
		public int HuaShenLingYu;

		// Token: 0x04003FBA RID: 16314
		public int MoneyType;

		// Token: 0x04003FBB RID: 16315
		public int IsRefresh;

		// Token: 0x04003FBC RID: 16316
		public int dropType;

		// Token: 0x04003FBD RID: 16317
		public int canjiaPaiMai;

		// Token: 0x04003FBE RID: 16318
		public int wudaoType;

		// Token: 0x04003FBF RID: 16319
		public int XinQuType;

		// Token: 0x04003FC0 RID: 16320
		public int gudingjiage;

		// Token: 0x04003FC1 RID: 16321
		public int sellPercent;

		// Token: 0x04003FC2 RID: 16322
		public string Title;

		// Token: 0x04003FC3 RID: 16323
		public string FirstName;

		// Token: 0x04003FC4 RID: 16324
		public string Name;

		// Token: 0x04003FC5 RID: 16325
		public string menPai;

		// Token: 0x04003FC6 RID: 16326
		public List<int> LingGen = new List<int>();

		// Token: 0x04003FC7 RID: 16327
		public List<int> skills = new List<int>();

		// Token: 0x04003FC8 RID: 16328
		public List<int> staticSkills = new List<int>();

		// Token: 0x04003FC9 RID: 16329
		public List<int> paimaifenzu = new List<int>();
	}
}
