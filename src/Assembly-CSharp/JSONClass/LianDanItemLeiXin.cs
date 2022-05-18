using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C01 RID: 3073
	public class LianDanItemLeiXin : IJSONClass
	{
		// Token: 0x06004B6D RID: 19309 RVA: 0x001FD96C File Offset: 0x001FBB6C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianDanItemLeiXin.list)
			{
				try
				{
					LianDanItemLeiXin lianDanItemLeiXin = new LianDanItemLeiXin();
					lianDanItemLeiXin.id = jsonobject["id"].I;
					lianDanItemLeiXin.name = jsonobject["name"].Str;
					lianDanItemLeiXin.desc = jsonobject["desc"].Str;
					if (LianDanItemLeiXin.DataDict.ContainsKey(lianDanItemLeiXin.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianDanItemLeiXin.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianDanItemLeiXin.id));
					}
					else
					{
						LianDanItemLeiXin.DataDict.Add(lianDanItemLeiXin.id, lianDanItemLeiXin);
						LianDanItemLeiXin.DataList.Add(lianDanItemLeiXin);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianDanItemLeiXin.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianDanItemLeiXin.OnInitFinishAction != null)
			{
				LianDanItemLeiXin.OnInitFinishAction();
			}
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040047EA RID: 18410
		public static Dictionary<int, LianDanItemLeiXin> DataDict = new Dictionary<int, LianDanItemLeiXin>();

		// Token: 0x040047EB RID: 18411
		public static List<LianDanItemLeiXin> DataList = new List<LianDanItemLeiXin>();

		// Token: 0x040047EC RID: 18412
		public static Action OnInitFinishAction = new Action(LianDanItemLeiXin.OnInitFinish);

		// Token: 0x040047ED RID: 18413
		public int id;

		// Token: 0x040047EE RID: 18414
		public string name;

		// Token: 0x040047EF RID: 18415
		public string desc;
	}
}
