using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A1 RID: 2209
	public class NPCImportantDate : IJSONClass
	{
		// Token: 0x06004097 RID: 16535 RVA: 0x001B9370 File Offset: 0x001B7570
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCImportantDate.list)
			{
				try
				{
					NPCImportantDate npcimportantDate = new NPCImportantDate();
					npcimportantDate.id = jsonobject["id"].I;
					npcimportantDate.LiuPai = jsonobject["LiuPai"].I;
					npcimportantDate.level = jsonobject["level"].I;
					npcimportantDate.sex = jsonobject["sex"].I;
					npcimportantDate.zizhi = jsonobject["zizhi"].I;
					npcimportantDate.wuxing = jsonobject["wuxing"].I;
					npcimportantDate.nianling = jsonobject["nianling"].I;
					npcimportantDate.XingGe = jsonobject["XingGe"].I;
					npcimportantDate.ChengHao = jsonobject["ChengHao"].I;
					npcimportantDate.NPCTag = jsonobject["NPCTag"].I;
					npcimportantDate.DaShiXiong = jsonobject["DaShiXiong"].I;
					npcimportantDate.ZhangMeng = jsonobject["ZhangMeng"].I;
					npcimportantDate.ZhangLao = jsonobject["ZhangLao"].I;
					npcimportantDate.ZhuJiTime = jsonobject["ZhuJiTime"].Str;
					npcimportantDate.JinDanTime = jsonobject["JinDanTime"].Str;
					npcimportantDate.YuanYingTime = jsonobject["YuanYingTime"].Str;
					npcimportantDate.HuaShengTime = jsonobject["HuaShengTime"].Str;
					if (NPCImportantDate.DataDict.ContainsKey(npcimportantDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCImportantDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcimportantDate.id));
					}
					else
					{
						NPCImportantDate.DataDict.Add(npcimportantDate.id, npcimportantDate);
						NPCImportantDate.DataList.Add(npcimportantDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCImportantDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCImportantDate.OnInitFinishAction != null)
			{
				NPCImportantDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E4E RID: 15950
		public static Dictionary<int, NPCImportantDate> DataDict = new Dictionary<int, NPCImportantDate>();

		// Token: 0x04003E4F RID: 15951
		public static List<NPCImportantDate> DataList = new List<NPCImportantDate>();

		// Token: 0x04003E50 RID: 15952
		public static Action OnInitFinishAction = new Action(NPCImportantDate.OnInitFinish);

		// Token: 0x04003E51 RID: 15953
		public int id;

		// Token: 0x04003E52 RID: 15954
		public int LiuPai;

		// Token: 0x04003E53 RID: 15955
		public int level;

		// Token: 0x04003E54 RID: 15956
		public int sex;

		// Token: 0x04003E55 RID: 15957
		public int zizhi;

		// Token: 0x04003E56 RID: 15958
		public int wuxing;

		// Token: 0x04003E57 RID: 15959
		public int nianling;

		// Token: 0x04003E58 RID: 15960
		public int XingGe;

		// Token: 0x04003E59 RID: 15961
		public int ChengHao;

		// Token: 0x04003E5A RID: 15962
		public int NPCTag;

		// Token: 0x04003E5B RID: 15963
		public int DaShiXiong;

		// Token: 0x04003E5C RID: 15964
		public int ZhangMeng;

		// Token: 0x04003E5D RID: 15965
		public int ZhangLao;

		// Token: 0x04003E5E RID: 15966
		public string ZhuJiTime;

		// Token: 0x04003E5F RID: 15967
		public string JinDanTime;

		// Token: 0x04003E60 RID: 15968
		public string YuanYingTime;

		// Token: 0x04003E61 RID: 15969
		public string HuaShengTime;
	}
}
