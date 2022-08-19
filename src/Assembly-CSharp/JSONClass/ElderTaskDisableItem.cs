using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000835 RID: 2101
	public class ElderTaskDisableItem : IJSONClass
	{
		// Token: 0x06003EE6 RID: 16102 RVA: 0x001ADEB0 File Offset: 0x001AC0B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ElderTaskDisableItem.list)
			{
				try
				{
					ElderTaskDisableItem elderTaskDisableItem = new ElderTaskDisableItem();
					elderTaskDisableItem.id = jsonobject["id"].I;
					if (ElderTaskDisableItem.DataDict.ContainsKey(elderTaskDisableItem.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ElderTaskDisableItem.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", elderTaskDisableItem.id));
					}
					else
					{
						ElderTaskDisableItem.DataDict.Add(elderTaskDisableItem.id, elderTaskDisableItem);
						ElderTaskDisableItem.DataList.Add(elderTaskDisableItem);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ElderTaskDisableItem.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ElderTaskDisableItem.OnInitFinishAction != null)
			{
				ElderTaskDisableItem.OnInitFinishAction();
			}
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AB4 RID: 15028
		public static Dictionary<int, ElderTaskDisableItem> DataDict = new Dictionary<int, ElderTaskDisableItem>();

		// Token: 0x04003AB5 RID: 15029
		public static List<ElderTaskDisableItem> DataList = new List<ElderTaskDisableItem>();

		// Token: 0x04003AB6 RID: 15030
		public static Action OnInitFinishAction = new Action(ElderTaskDisableItem.OnInitFinish);

		// Token: 0x04003AB7 RID: 15031
		public int id;
	}
}
