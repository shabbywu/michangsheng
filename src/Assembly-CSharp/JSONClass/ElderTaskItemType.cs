using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000837 RID: 2103
	public class ElderTaskItemType : IJSONClass
	{
		// Token: 0x06003EEE RID: 16110 RVA: 0x001AE134 File Offset: 0x001AC334
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ElderTaskItemType.list)
			{
				try
				{
					ElderTaskItemType elderTaskItemType = new ElderTaskItemType();
					elderTaskItemType.type = jsonobject["type"].I;
					elderTaskItemType.Xishu = jsonobject["Xishu"].I;
					elderTaskItemType.quality = jsonobject["quality"].ToList();
					if (ElderTaskItemType.DataDict.ContainsKey(elderTaskItemType.type))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ElderTaskItemType.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", elderTaskItemType.type));
					}
					else
					{
						ElderTaskItemType.DataDict.Add(elderTaskItemType.type, elderTaskItemType);
						ElderTaskItemType.DataList.Add(elderTaskItemType);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ElderTaskItemType.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ElderTaskItemType.OnInitFinishAction != null)
			{
				ElderTaskItemType.OnInitFinishAction();
			}
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003ABD RID: 15037
		public static Dictionary<int, ElderTaskItemType> DataDict = new Dictionary<int, ElderTaskItemType>();

		// Token: 0x04003ABE RID: 15038
		public static List<ElderTaskItemType> DataList = new List<ElderTaskItemType>();

		// Token: 0x04003ABF RID: 15039
		public static Action OnInitFinishAction = new Action(ElderTaskItemType.OnInitFinish);

		// Token: 0x04003AC0 RID: 15040
		public int type;

		// Token: 0x04003AC1 RID: 15041
		public int Xishu;

		// Token: 0x04003AC2 RID: 15042
		public List<int> quality = new List<int>();
	}
}
