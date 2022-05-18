using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BDA RID: 3034
	public class HuaShenData : IJSONClass
	{
		// Token: 0x06004AD0 RID: 19152 RVA: 0x001FA6D8 File Offset: 0x001F88D8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.HuaShenData.list)
			{
				try
				{
					HuaShenData huaShenData = new HuaShenData();
					huaShenData.id = jsonobject["id"].I;
					huaShenData.Buff = jsonobject["Buff"].I;
					huaShenData.Skill = jsonobject["Skill"].I;
					huaShenData.Name = jsonobject["Name"].Str;
					if (HuaShenData.DataDict.ContainsKey(huaShenData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典HuaShenData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", huaShenData.id));
					}
					else
					{
						HuaShenData.DataDict.Add(huaShenData.id, huaShenData);
						HuaShenData.DataList.Add(huaShenData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典HuaShenData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (HuaShenData.OnInitFinishAction != null)
			{
				HuaShenData.OnInitFinishAction();
			}
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046D3 RID: 18131
		public static Dictionary<int, HuaShenData> DataDict = new Dictionary<int, HuaShenData>();

		// Token: 0x040046D4 RID: 18132
		public static List<HuaShenData> DataList = new List<HuaShenData>();

		// Token: 0x040046D5 RID: 18133
		public static Action OnInitFinishAction = new Action(HuaShenData.OnInitFinish);

		// Token: 0x040046D6 RID: 18134
		public int id;

		// Token: 0x040046D7 RID: 18135
		public int Buff;

		// Token: 0x040046D8 RID: 18136
		public int Skill;

		// Token: 0x040046D9 RID: 18137
		public string Name;
	}
}
