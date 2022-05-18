using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C4A RID: 3146
	public class NvZuJianBiao : IJSONClass
	{
		// Token: 0x06004C91 RID: 19601 RVA: 0x00205D68 File Offset: 0x00203F68
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NvZuJianBiao.list)
			{
				try
				{
					NvZuJianBiao nvZuJianBiao = new NvZuJianBiao();
					nvZuJianBiao.id = jsonobject["id"].I;
					nvZuJianBiao.StrID = jsonobject["StrID"].Str;
					if (NvZuJianBiao.DataDict.ContainsKey(nvZuJianBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NvZuJianBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", nvZuJianBiao.id));
					}
					else
					{
						NvZuJianBiao.DataDict.Add(nvZuJianBiao.id, nvZuJianBiao);
						NvZuJianBiao.DataList.Add(nvZuJianBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NvZuJianBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NvZuJianBiao.OnInitFinishAction != null)
			{
				NvZuJianBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004B2F RID: 19247
		public static Dictionary<int, NvZuJianBiao> DataDict = new Dictionary<int, NvZuJianBiao>();

		// Token: 0x04004B30 RID: 19248
		public static List<NvZuJianBiao> DataList = new List<NvZuJianBiao>();

		// Token: 0x04004B31 RID: 19249
		public static Action OnInitFinishAction = new Action(NvZuJianBiao.OnInitFinish);

		// Token: 0x04004B32 RID: 19250
		public int id;

		// Token: 0x04004B33 RID: 19251
		public string StrID;
	}
}
