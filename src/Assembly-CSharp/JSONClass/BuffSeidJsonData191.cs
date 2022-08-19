using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200079D RID: 1949
	public class BuffSeidJsonData191 : IJSONClass
	{
		// Token: 0x06003C86 RID: 15494 RVA: 0x0019F8F8 File Offset: 0x0019DAF8
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

		// Token: 0x06003C87 RID: 15495 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003651 RID: 13905
		public static int SEIDID = 191;

		// Token: 0x04003652 RID: 13906
		public static Dictionary<int, BuffSeidJsonData191> DataDict = new Dictionary<int, BuffSeidJsonData191>();

		// Token: 0x04003653 RID: 13907
		public static List<BuffSeidJsonData191> DataList = new List<BuffSeidJsonData191>();

		// Token: 0x04003654 RID: 13908
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData191.OnInitFinish);

		// Token: 0x04003655 RID: 13909
		public int id;

		// Token: 0x04003656 RID: 13910
		public int target;
	}
}
