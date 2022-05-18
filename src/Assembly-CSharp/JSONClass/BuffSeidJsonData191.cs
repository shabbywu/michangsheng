using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B35 RID: 2869
	public class BuffSeidJsonData191 : IJSONClass
	{
		// Token: 0x0600483C RID: 18492 RVA: 0x001EC9D8 File Offset: 0x001EABD8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[191].list)
			{
				try
				{
					BuffSeidJsonData191 buffSeidJsonData = new BuffSeidJsonData191();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					if (BuffSeidJsonData191.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData191.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData191.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData191.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData191.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData191.OnInitFinishAction != null)
			{
				BuffSeidJsonData191.OnInitFinishAction();
			}
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041EA RID: 16874
		public static int SEIDID = 191;

		// Token: 0x040041EB RID: 16875
		public static Dictionary<int, BuffSeidJsonData191> DataDict = new Dictionary<int, BuffSeidJsonData191>();

		// Token: 0x040041EC RID: 16876
		public static List<BuffSeidJsonData191> DataList = new List<BuffSeidJsonData191>();

		// Token: 0x040041ED RID: 16877
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData191.OnInitFinish);

		// Token: 0x040041EE RID: 16878
		public int id;

		// Token: 0x040041EF RID: 16879
		public int target;
	}
}
