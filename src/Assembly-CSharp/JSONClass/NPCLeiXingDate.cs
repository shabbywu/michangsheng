using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A5 RID: 2213
	public class NPCLeiXingDate : IJSONClass
	{
		// Token: 0x060040A7 RID: 16551 RVA: 0x001B9B78 File Offset: 0x001B7D78
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

		// Token: 0x060040A8 RID: 16552 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E7C RID: 15996
		public static Dictionary<int, NPCLeiXingDate> DataDict = new Dictionary<int, NPCLeiXingDate>();

		// Token: 0x04003E7D RID: 15997
		public static List<NPCLeiXingDate> DataList = new List<NPCLeiXingDate>();

		// Token: 0x04003E7E RID: 15998
		public static Action OnInitFinishAction = new Action(NPCLeiXingDate.OnInitFinish);

		// Token: 0x04003E7F RID: 15999
		public int id;

		// Token: 0x04003E80 RID: 16000
		public int Type;

		// Token: 0x04003E81 RID: 16001
		public int LiuPai;

		// Token: 0x04003E82 RID: 16002
		public int MengPai;

		// Token: 0x04003E83 RID: 16003
		public int Level;

		// Token: 0x04003E84 RID: 16004
		public int yuanying;

		// Token: 0x04003E85 RID: 16005
		public int HuaShenLingYu;

		// Token: 0x04003E86 RID: 16006
		public int wudaoType;

		// Token: 0x04003E87 RID: 16007
		public int canjiaPaiMai;

		// Token: 0x04003E88 RID: 16008
		public int AvatarType;

		// Token: 0x04003E89 RID: 16009
		public int XinQuType;

		// Token: 0x04003E8A RID: 16010
		public int AttackType;

		// Token: 0x04003E8B RID: 16011
		public int DefenseType;

		// Token: 0x04003E8C RID: 16012
		public string FirstName;

		// Token: 0x04003E8D RID: 16013
		public List<int> skills = new List<int>();

		// Token: 0x04003E8E RID: 16014
		public List<int> staticSkills = new List<int>();

		// Token: 0x04003E8F RID: 16015
		public List<int> LingGen = new List<int>();

		// Token: 0x04003E90 RID: 16016
		public List<int> NPCTag = new List<int>();

		// Token: 0x04003E91 RID: 16017
		public List<int> paimaifenzu = new List<int>();

		// Token: 0x04003E92 RID: 16018
		public List<int> equipWeapon = new List<int>();

		// Token: 0x04003E93 RID: 16019
		public List<int> equipClothing = new List<int>();

		// Token: 0x04003E94 RID: 16020
		public List<int> equipRing = new List<int>();

		// Token: 0x04003E95 RID: 16021
		public List<int> JinDanType = new List<int>();

		// Token: 0x04003E96 RID: 16022
		public List<int> ShiLi = new List<int>();
	}
}
