using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C2F RID: 3119
	public class NPCImportantDate : IJSONClass
	{
		// Token: 0x06004C25 RID: 19493 RVA: 0x002024BC File Offset: 0x002006BC
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

		// Token: 0x06004C26 RID: 19494 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040049A7 RID: 18855
		public static Dictionary<int, NPCImportantDate> DataDict = new Dictionary<int, NPCImportantDate>();

		// Token: 0x040049A8 RID: 18856
		public static List<NPCImportantDate> DataList = new List<NPCImportantDate>();

		// Token: 0x040049A9 RID: 18857
		public static Action OnInitFinishAction = new Action(NPCImportantDate.OnInitFinish);

		// Token: 0x040049AA RID: 18858
		public int id;

		// Token: 0x040049AB RID: 18859
		public int LiuPai;

		// Token: 0x040049AC RID: 18860
		public int level;

		// Token: 0x040049AD RID: 18861
		public int sex;

		// Token: 0x040049AE RID: 18862
		public int zizhi;

		// Token: 0x040049AF RID: 18863
		public int wuxing;

		// Token: 0x040049B0 RID: 18864
		public int nianling;

		// Token: 0x040049B1 RID: 18865
		public int XingGe;

		// Token: 0x040049B2 RID: 18866
		public int ChengHao;

		// Token: 0x040049B3 RID: 18867
		public int NPCTag;

		// Token: 0x040049B4 RID: 18868
		public int DaShiXiong;

		// Token: 0x040049B5 RID: 18869
		public int ZhangMeng;

		// Token: 0x040049B6 RID: 18870
		public int ZhangLao;

		// Token: 0x040049B7 RID: 18871
		public string ZhuJiTime;

		// Token: 0x040049B8 RID: 18872
		public string JinDanTime;

		// Token: 0x040049B9 RID: 18873
		public string YuanYingTime;

		// Token: 0x040049BA RID: 18874
		public string HuaShengTime;
	}
}
