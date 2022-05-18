using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C33 RID: 3123
	public class NPCLeiXingDate : IJSONClass
	{
		// Token: 0x06004C35 RID: 19509 RVA: 0x00202BD4 File Offset: 0x00200DD4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCLeiXingDate.list)
			{
				try
				{
					NPCLeiXingDate npcleiXingDate = new NPCLeiXingDate();
					npcleiXingDate.id = jsonobject["id"].I;
					npcleiXingDate.Type = jsonobject["Type"].I;
					npcleiXingDate.LiuPai = jsonobject["LiuPai"].I;
					npcleiXingDate.MengPai = jsonobject["MengPai"].I;
					npcleiXingDate.Level = jsonobject["Level"].I;
					npcleiXingDate.yuanying = jsonobject["yuanying"].I;
					npcleiXingDate.HuaShenLingYu = jsonobject["HuaShenLingYu"].I;
					npcleiXingDate.wudaoType = jsonobject["wudaoType"].I;
					npcleiXingDate.canjiaPaiMai = jsonobject["canjiaPaiMai"].I;
					npcleiXingDate.AvatarType = jsonobject["AvatarType"].I;
					npcleiXingDate.XinQuType = jsonobject["XinQuType"].I;
					npcleiXingDate.AttackType = jsonobject["AttackType"].I;
					npcleiXingDate.DefenseType = jsonobject["DefenseType"].I;
					npcleiXingDate.FirstName = jsonobject["FirstName"].Str;
					npcleiXingDate.skills = jsonobject["skills"].ToList();
					npcleiXingDate.staticSkills = jsonobject["staticSkills"].ToList();
					npcleiXingDate.LingGen = jsonobject["LingGen"].ToList();
					npcleiXingDate.NPCTag = jsonobject["NPCTag"].ToList();
					npcleiXingDate.paimaifenzu = jsonobject["paimaifenzu"].ToList();
					npcleiXingDate.equipWeapon = jsonobject["equipWeapon"].ToList();
					npcleiXingDate.equipClothing = jsonobject["equipClothing"].ToList();
					npcleiXingDate.equipRing = jsonobject["equipRing"].ToList();
					npcleiXingDate.JinDanType = jsonobject["JinDanType"].ToList();
					npcleiXingDate.ShiLi = jsonobject["ShiLi"].ToList();
					if (NPCLeiXingDate.DataDict.ContainsKey(npcleiXingDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCLeiXingDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcleiXingDate.id));
					}
					else
					{
						NPCLeiXingDate.DataDict.Add(npcleiXingDate.id, npcleiXingDate);
						NPCLeiXingDate.DataList.Add(npcleiXingDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCLeiXingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCLeiXingDate.OnInitFinishAction != null)
			{
				NPCLeiXingDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040049D5 RID: 18901
		public static Dictionary<int, NPCLeiXingDate> DataDict = new Dictionary<int, NPCLeiXingDate>();

		// Token: 0x040049D6 RID: 18902
		public static List<NPCLeiXingDate> DataList = new List<NPCLeiXingDate>();

		// Token: 0x040049D7 RID: 18903
		public static Action OnInitFinishAction = new Action(NPCLeiXingDate.OnInitFinish);

		// Token: 0x040049D8 RID: 18904
		public int id;

		// Token: 0x040049D9 RID: 18905
		public int Type;

		// Token: 0x040049DA RID: 18906
		public int LiuPai;

		// Token: 0x040049DB RID: 18907
		public int MengPai;

		// Token: 0x040049DC RID: 18908
		public int Level;

		// Token: 0x040049DD RID: 18909
		public int yuanying;

		// Token: 0x040049DE RID: 18910
		public int HuaShenLingYu;

		// Token: 0x040049DF RID: 18911
		public int wudaoType;

		// Token: 0x040049E0 RID: 18912
		public int canjiaPaiMai;

		// Token: 0x040049E1 RID: 18913
		public int AvatarType;

		// Token: 0x040049E2 RID: 18914
		public int XinQuType;

		// Token: 0x040049E3 RID: 18915
		public int AttackType;

		// Token: 0x040049E4 RID: 18916
		public int DefenseType;

		// Token: 0x040049E5 RID: 18917
		public string FirstName;

		// Token: 0x040049E6 RID: 18918
		public List<int> skills = new List<int>();

		// Token: 0x040049E7 RID: 18919
		public List<int> staticSkills = new List<int>();

		// Token: 0x040049E8 RID: 18920
		public List<int> LingGen = new List<int>();

		// Token: 0x040049E9 RID: 18921
		public List<int> NPCTag = new List<int>();

		// Token: 0x040049EA RID: 18922
		public List<int> paimaifenzu = new List<int>();

		// Token: 0x040049EB RID: 18923
		public List<int> equipWeapon = new List<int>();

		// Token: 0x040049EC RID: 18924
		public List<int> equipClothing = new List<int>();

		// Token: 0x040049ED RID: 18925
		public List<int> equipRing = new List<int>();

		// Token: 0x040049EE RID: 18926
		public List<int> JinDanType = new List<int>();

		// Token: 0x040049EF RID: 18927
		public List<int> ShiLi = new List<int>();
	}
}
