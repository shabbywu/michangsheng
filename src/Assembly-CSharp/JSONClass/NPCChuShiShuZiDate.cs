using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C2A RID: 3114
	public class NPCChuShiShuZiDate : IJSONClass
	{
		// Token: 0x06004C11 RID: 19473 RVA: 0x00201AFC File Offset: 0x001FFCFC
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

		// Token: 0x06004C12 RID: 19474 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400496D RID: 18797
		public static Dictionary<int, NPCChuShiShuZiDate> DataDict = new Dictionary<int, NPCChuShiShuZiDate>();

		// Token: 0x0400496E RID: 18798
		public static List<NPCChuShiShuZiDate> DataList = new List<NPCChuShiShuZiDate>();

		// Token: 0x0400496F RID: 18799
		public static Action OnInitFinishAction = new Action(NPCChuShiShuZiDate.OnInitFinish);

		// Token: 0x04004970 RID: 18800
		public int id;

		// Token: 0x04004971 RID: 18801
		public int xiuwei;

		// Token: 0x04004972 RID: 18802
		public int equipWeapon;

		// Token: 0x04004973 RID: 18803
		public int equipWeapon2;

		// Token: 0x04004974 RID: 18804
		public int equipClothing;

		// Token: 0x04004975 RID: 18805
		public int equipRing;

		// Token: 0x04004976 RID: 18806
		public int bag;

		// Token: 0x04004977 RID: 18807
		public List<int> age = new List<int>();

		// Token: 0x04004978 RID: 18808
		public List<int> shouYuan = new List<int>();

		// Token: 0x04004979 RID: 18809
		public List<int> SexType = new List<int>();

		// Token: 0x0400497A RID: 18810
		public List<int> HP = new List<int>();

		// Token: 0x0400497B RID: 18811
		public List<int> ziZhi = new List<int>();

		// Token: 0x0400497C RID: 18812
		public List<int> wuXin = new List<int>();

		// Token: 0x0400497D RID: 18813
		public List<int> dunSu = new List<int>();

		// Token: 0x0400497E RID: 18814
		public List<int> shengShi = new List<int>();

		// Token: 0x0400497F RID: 18815
		public List<int> MoneyType = new List<int>();

		// Token: 0x04004980 RID: 18816
		public List<int> ShopType = new List<int>();

		// Token: 0x04004981 RID: 18817
		public List<int> quality = new List<int>();
	}
}
