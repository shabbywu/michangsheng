using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000740 RID: 1856
	public class _BuffJsonData : IJSONClass
	{
		// Token: 0x06003B14 RID: 15124 RVA: 0x001963BC File Offset: 0x001945BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._BuffJsonData.list)
			{
				try
				{
					_BuffJsonData buffJsonData = new _BuffJsonData();
					buffJsonData.buffid = jsonobject["buffid"].I;
					buffJsonData.BuffIcon = jsonobject["BuffIcon"].I;
					buffJsonData.bufftype = jsonobject["bufftype"].I;
					buffJsonData.trigger = jsonobject["trigger"].I;
					buffJsonData.removeTrigger = jsonobject["removeTrigger"].I;
					buffJsonData.looptime = jsonobject["looptime"].I;
					buffJsonData.totaltime = jsonobject["totaltime"].I;
					buffJsonData.BuffType = jsonobject["BuffType"].I;
					buffJsonData.isHide = jsonobject["isHide"].I;
					buffJsonData.ShowOnlyOne = jsonobject["ShowOnlyOne"].I;
					buffJsonData.skillEffect = jsonobject["skillEffect"].Str;
					buffJsonData.name = jsonobject["name"].Str;
					buffJsonData.descr = jsonobject["descr"].Str;
					buffJsonData.script = jsonobject["script"].Str;
					buffJsonData.Affix = jsonobject["Affix"].ToList();
					buffJsonData.seid = jsonobject["seid"].ToList();
					if (_BuffJsonData.DataDict.ContainsKey(buffJsonData.buffid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_BuffJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffJsonData.buffid));
					}
					else
					{
						_BuffJsonData.DataDict.Add(buffJsonData.buffid, buffJsonData);
						_BuffJsonData.DataList.Add(buffJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_BuffJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_BuffJsonData.OnInitFinishAction != null)
			{
				_BuffJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003347 RID: 13127
		public static Dictionary<int, _BuffJsonData> DataDict = new Dictionary<int, _BuffJsonData>();

		// Token: 0x04003348 RID: 13128
		public static List<_BuffJsonData> DataList = new List<_BuffJsonData>();

		// Token: 0x04003349 RID: 13129
		public static Action OnInitFinishAction = new Action(_BuffJsonData.OnInitFinish);

		// Token: 0x0400334A RID: 13130
		public int buffid;

		// Token: 0x0400334B RID: 13131
		public int BuffIcon;

		// Token: 0x0400334C RID: 13132
		public int bufftype;

		// Token: 0x0400334D RID: 13133
		public int trigger;

		// Token: 0x0400334E RID: 13134
		public int removeTrigger;

		// Token: 0x0400334F RID: 13135
		public int looptime;

		// Token: 0x04003350 RID: 13136
		public int totaltime;

		// Token: 0x04003351 RID: 13137
		public int BuffType;

		// Token: 0x04003352 RID: 13138
		public int isHide;

		// Token: 0x04003353 RID: 13139
		public int ShowOnlyOne;

		// Token: 0x04003354 RID: 13140
		public string skillEffect;

		// Token: 0x04003355 RID: 13141
		public string name;

		// Token: 0x04003356 RID: 13142
		public string descr;

		// Token: 0x04003357 RID: 13143
		public string script;

		// Token: 0x04003358 RID: 13144
		public List<int> Affix = new List<int>();

		// Token: 0x04003359 RID: 13145
		public List<int> seid = new List<int>();
	}
}
