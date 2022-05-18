using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C40 RID: 3136
	public class NpcThreeMapBingDate : IJSONClass
	{
		// Token: 0x06004C69 RID: 19561 RVA: 0x00204928 File Offset: 0x00202B28
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

		// Token: 0x06004C6A RID: 19562 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004AAC RID: 19116
		public static Dictionary<int, NpcThreeMapBingDate> DataDict = new Dictionary<int, NpcThreeMapBingDate>();

		// Token: 0x04004AAD RID: 19117
		public static List<NpcThreeMapBingDate> DataList = new List<NpcThreeMapBingDate>();

		// Token: 0x04004AAE RID: 19118
		public static Action OnInitFinishAction = new Action(NpcThreeMapBingDate.OnInitFinish);

		// Token: 0x04004AAF RID: 19119
		public int id;

		// Token: 0x04004AB0 RID: 19120
		public List<int> LianDan = new List<int>();

		// Token: 0x04004AB1 RID: 19121
		public List<int> LianQi = new List<int>();

		// Token: 0x04004AB2 RID: 19122
		public List<int> CaiJi = new List<int>();

		// Token: 0x04004AB3 RID: 19123
		public List<int> CaiKuang = new List<int>();

		// Token: 0x04004AB4 RID: 19124
		public List<int> MiJi = new List<int>();

		// Token: 0x04004AB5 RID: 19125
		public List<int> FaBao = new List<int>();

		// Token: 0x04004AB6 RID: 19126
		public List<int> GuangChang = new List<int>();

		// Token: 0x04004AB7 RID: 19127
		public List<int> DaDian = new List<int>();

		// Token: 0x04004AB8 RID: 19128
		public List<int> DongShiGuFangShi = new List<int>();

		// Token: 0x04004AB9 RID: 19129
		public List<int> TianXingChengFangShi = new List<int>();

		// Token: 0x04004ABA RID: 19130
		public List<int> HaiShangFangShi = new List<int>();

		// Token: 0x04004ABB RID: 19131
		public List<int> DongShiGuPaiMai = new List<int>();

		// Token: 0x04004ABC RID: 19132
		public List<int> TianJiGePaiMai = new List<int>();

		// Token: 0x04004ABD RID: 19133
		public List<int> HaiShangPaiMai = new List<int>();

		// Token: 0x04004ABE RID: 19134
		public List<int> NanYaChengPaiMai = new List<int>();

		// Token: 0x04004ABF RID: 19135
		public List<int> YaoDian = new List<int>();

		// Token: 0x04004AC0 RID: 19136
		public List<int> GangKou = new List<int>();

		// Token: 0x04004AC1 RID: 19137
		public List<int> DongFu = new List<int>();
	}
}
