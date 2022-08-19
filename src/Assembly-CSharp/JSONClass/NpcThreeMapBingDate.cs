using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B2 RID: 2226
	public class NpcThreeMapBingDate : IJSONClass
	{
		// Token: 0x060040DB RID: 16603 RVA: 0x001BBBE8 File Offset: 0x001B9DE8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcThreeMapBingDate.list)
			{
				try
				{
					NpcThreeMapBingDate npcThreeMapBingDate = new NpcThreeMapBingDate();
					npcThreeMapBingDate.id = jsonobject["id"].I;
					npcThreeMapBingDate.LianDan = jsonobject["LianDan"].ToList();
					npcThreeMapBingDate.LianQi = jsonobject["LianQi"].ToList();
					npcThreeMapBingDate.CaiJi = jsonobject["CaiJi"].ToList();
					npcThreeMapBingDate.CaiKuang = jsonobject["CaiKuang"].ToList();
					npcThreeMapBingDate.MiJi = jsonobject["MiJi"].ToList();
					npcThreeMapBingDate.FaBao = jsonobject["FaBao"].ToList();
					npcThreeMapBingDate.GuangChang = jsonobject["GuangChang"].ToList();
					npcThreeMapBingDate.DaDian = jsonobject["DaDian"].ToList();
					npcThreeMapBingDate.DongShiGuFangShi = jsonobject["DongShiGuFangShi"].ToList();
					npcThreeMapBingDate.TianXingChengFangShi = jsonobject["TianXingChengFangShi"].ToList();
					npcThreeMapBingDate.HaiShangFangShi = jsonobject["HaiShangFangShi"].ToList();
					npcThreeMapBingDate.DongShiGuPaiMai = jsonobject["DongShiGuPaiMai"].ToList();
					npcThreeMapBingDate.TianJiGePaiMai = jsonobject["TianJiGePaiMai"].ToList();
					npcThreeMapBingDate.HaiShangPaiMai = jsonobject["HaiShangPaiMai"].ToList();
					npcThreeMapBingDate.NanYaChengPaiMai = jsonobject["NanYaChengPaiMai"].ToList();
					npcThreeMapBingDate.YaoDian = jsonobject["YaoDian"].ToList();
					npcThreeMapBingDate.GangKou = jsonobject["GangKou"].ToList();
					npcThreeMapBingDate.DongFu = jsonobject["DongFu"].ToList();
					if (NpcThreeMapBingDate.DataDict.ContainsKey(npcThreeMapBingDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcThreeMapBingDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcThreeMapBingDate.id));
					}
					else
					{
						NpcThreeMapBingDate.DataDict.Add(npcThreeMapBingDate.id, npcThreeMapBingDate);
						NpcThreeMapBingDate.DataList.Add(npcThreeMapBingDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcThreeMapBingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcThreeMapBingDate.OnInitFinishAction != null)
			{
				NpcThreeMapBingDate.OnInitFinishAction();
			}
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F58 RID: 16216
		public static Dictionary<int, NpcThreeMapBingDate> DataDict = new Dictionary<int, NpcThreeMapBingDate>();

		// Token: 0x04003F59 RID: 16217
		public static List<NpcThreeMapBingDate> DataList = new List<NpcThreeMapBingDate>();

		// Token: 0x04003F5A RID: 16218
		public static Action OnInitFinishAction = new Action(NpcThreeMapBingDate.OnInitFinish);

		// Token: 0x04003F5B RID: 16219
		public int id;

		// Token: 0x04003F5C RID: 16220
		public List<int> LianDan = new List<int>();

		// Token: 0x04003F5D RID: 16221
		public List<int> LianQi = new List<int>();

		// Token: 0x04003F5E RID: 16222
		public List<int> CaiJi = new List<int>();

		// Token: 0x04003F5F RID: 16223
		public List<int> CaiKuang = new List<int>();

		// Token: 0x04003F60 RID: 16224
		public List<int> MiJi = new List<int>();

		// Token: 0x04003F61 RID: 16225
		public List<int> FaBao = new List<int>();

		// Token: 0x04003F62 RID: 16226
		public List<int> GuangChang = new List<int>();

		// Token: 0x04003F63 RID: 16227
		public List<int> DaDian = new List<int>();

		// Token: 0x04003F64 RID: 16228
		public List<int> DongShiGuFangShi = new List<int>();

		// Token: 0x04003F65 RID: 16229
		public List<int> TianXingChengFangShi = new List<int>();

		// Token: 0x04003F66 RID: 16230
		public List<int> HaiShangFangShi = new List<int>();

		// Token: 0x04003F67 RID: 16231
		public List<int> DongShiGuPaiMai = new List<int>();

		// Token: 0x04003F68 RID: 16232
		public List<int> TianJiGePaiMai = new List<int>();

		// Token: 0x04003F69 RID: 16233
		public List<int> HaiShangPaiMai = new List<int>();

		// Token: 0x04003F6A RID: 16234
		public List<int> NanYaChengPaiMai = new List<int>();

		// Token: 0x04003F6B RID: 16235
		public List<int> YaoDian = new List<int>();

		// Token: 0x04003F6C RID: 16236
		public List<int> GangKou = new List<int>();

		// Token: 0x04003F6D RID: 16237
		public List<int> DongFu = new List<int>();
	}
}
