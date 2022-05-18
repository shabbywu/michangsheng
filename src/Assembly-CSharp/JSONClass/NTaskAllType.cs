using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C47 RID: 3143
	public class NTaskAllType : IJSONClass
	{
		// Token: 0x06004C85 RID: 19589 RVA: 0x00205708 File Offset: 0x00203908
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NTaskAllType.list)
			{
				try
				{
					NTaskAllType ntaskAllType = new NTaskAllType();
					ntaskAllType.Id = jsonobject["Id"].I;
					ntaskAllType.CD = jsonobject["CD"].I;
					ntaskAllType.shili = jsonobject["shili"].I;
					ntaskAllType.GeRen = jsonobject["GeRen"].I;
					ntaskAllType.menpaihuobi = jsonobject["menpaihuobi"].I;
					ntaskAllType.Type = jsonobject["Type"].I;
					ntaskAllType.name = jsonobject["name"].Str;
					ntaskAllType.ZongMiaoShu = jsonobject["ZongMiaoShu"].Str;
					ntaskAllType.jiaofurenwu = jsonobject["jiaofurenwu"].Str;
					ntaskAllType.jiaofudidian = jsonobject["jiaofudidian"].Str;
					ntaskAllType.XiangXiID = jsonobject["XiangXiID"].ToList();
					ntaskAllType.seid = jsonobject["seid"].ToList();
					if (NTaskAllType.DataDict.ContainsKey(ntaskAllType.Id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NTaskAllType.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", ntaskAllType.Id));
					}
					else
					{
						NTaskAllType.DataDict.Add(ntaskAllType.Id, ntaskAllType);
						NTaskAllType.DataList.Add(ntaskAllType);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NTaskAllType.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NTaskAllType.OnInitFinishAction != null)
			{
				NTaskAllType.OnInitFinishAction();
			}
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B01 RID: 19201
		public static Dictionary<int, NTaskAllType> DataDict = new Dictionary<int, NTaskAllType>();

		// Token: 0x04004B02 RID: 19202
		public static List<NTaskAllType> DataList = new List<NTaskAllType>();

		// Token: 0x04004B03 RID: 19203
		public static Action OnInitFinishAction = new Action(NTaskAllType.OnInitFinish);

		// Token: 0x04004B04 RID: 19204
		public int Id;

		// Token: 0x04004B05 RID: 19205
		public int CD;

		// Token: 0x04004B06 RID: 19206
		public int shili;

		// Token: 0x04004B07 RID: 19207
		public int GeRen;

		// Token: 0x04004B08 RID: 19208
		public int menpaihuobi;

		// Token: 0x04004B09 RID: 19209
		public int Type;

		// Token: 0x04004B0A RID: 19210
		public string name;

		// Token: 0x04004B0B RID: 19211
		public string ZongMiaoShu;

		// Token: 0x04004B0C RID: 19212
		public string jiaofurenwu;

		// Token: 0x04004B0D RID: 19213
		public string jiaofudidian;

		// Token: 0x04004B0E RID: 19214
		public List<int> XiangXiID = new List<int>();

		// Token: 0x04004B0F RID: 19215
		public List<int> seid = new List<int>();
	}
}
