using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200089C RID: 2204
	public class NPCChuShiShuZiDate : IJSONClass
	{
		// Token: 0x06004083 RID: 16515 RVA: 0x001B88C0 File Offset: 0x001B6AC0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCChuShiShuZiDate.list)
			{
				try
				{
					NPCChuShiShuZiDate npcchuShiShuZiDate = new NPCChuShiShuZiDate();
					npcchuShiShuZiDate.id = jsonobject["id"].I;
					npcchuShiShuZiDate.xiuwei = jsonobject["xiuwei"].I;
					npcchuShiShuZiDate.equipWeapon = jsonobject["equipWeapon"].I;
					npcchuShiShuZiDate.equipWeapon2 = jsonobject["equipWeapon2"].I;
					npcchuShiShuZiDate.equipClothing = jsonobject["equipClothing"].I;
					npcchuShiShuZiDate.equipRing = jsonobject["equipRing"].I;
					npcchuShiShuZiDate.bag = jsonobject["bag"].I;
					npcchuShiShuZiDate.age = jsonobject["age"].ToList();
					npcchuShiShuZiDate.shouYuan = jsonobject["shouYuan"].ToList();
					npcchuShiShuZiDate.SexType = jsonobject["SexType"].ToList();
					npcchuShiShuZiDate.HP = jsonobject["HP"].ToList();
					npcchuShiShuZiDate.ziZhi = jsonobject["ziZhi"].ToList();
					npcchuShiShuZiDate.wuXin = jsonobject["wuXin"].ToList();
					npcchuShiShuZiDate.dunSu = jsonobject["dunSu"].ToList();
					npcchuShiShuZiDate.shengShi = jsonobject["shengShi"].ToList();
					npcchuShiShuZiDate.MoneyType = jsonobject["MoneyType"].ToList();
					npcchuShiShuZiDate.ShopType = jsonobject["ShopType"].ToList();
					npcchuShiShuZiDate.quality = jsonobject["quality"].ToList();
					if (NPCChuShiShuZiDate.DataDict.ContainsKey(npcchuShiShuZiDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCChuShiShuZiDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcchuShiShuZiDate.id));
					}
					else
					{
						NPCChuShiShuZiDate.DataDict.Add(npcchuShiShuZiDate.id, npcchuShiShuZiDate);
						NPCChuShiShuZiDate.DataList.Add(npcchuShiShuZiDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCChuShiShuZiDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCChuShiShuZiDate.OnInitFinishAction != null)
			{
				NPCChuShiShuZiDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E14 RID: 15892
		public static Dictionary<int, NPCChuShiShuZiDate> DataDict = new Dictionary<int, NPCChuShiShuZiDate>();

		// Token: 0x04003E15 RID: 15893
		public static List<NPCChuShiShuZiDate> DataList = new List<NPCChuShiShuZiDate>();

		// Token: 0x04003E16 RID: 15894
		public static Action OnInitFinishAction = new Action(NPCChuShiShuZiDate.OnInitFinish);

		// Token: 0x04003E17 RID: 15895
		public int id;

		// Token: 0x04003E18 RID: 15896
		public int xiuwei;

		// Token: 0x04003E19 RID: 15897
		public int equipWeapon;

		// Token: 0x04003E1A RID: 15898
		public int equipWeapon2;

		// Token: 0x04003E1B RID: 15899
		public int equipClothing;

		// Token: 0x04003E1C RID: 15900
		public int equipRing;

		// Token: 0x04003E1D RID: 15901
		public int bag;

		// Token: 0x04003E1E RID: 15902
		public List<int> age = new List<int>();

		// Token: 0x04003E1F RID: 15903
		public List<int> shouYuan = new List<int>();

		// Token: 0x04003E20 RID: 15904
		public List<int> SexType = new List<int>();

		// Token: 0x04003E21 RID: 15905
		public List<int> HP = new List<int>();

		// Token: 0x04003E22 RID: 15906
		public List<int> ziZhi = new List<int>();

		// Token: 0x04003E23 RID: 15907
		public List<int> wuXin = new List<int>();

		// Token: 0x04003E24 RID: 15908
		public List<int> dunSu = new List<int>();

		// Token: 0x04003E25 RID: 15909
		public List<int> shengShi = new List<int>();

		// Token: 0x04003E26 RID: 15910
		public List<int> MoneyType = new List<int>();

		// Token: 0x04003E27 RID: 15911
		public List<int> ShopType = new List<int>();

		// Token: 0x04003E28 RID: 15912
		public List<int> quality = new List<int>();
	}
}
