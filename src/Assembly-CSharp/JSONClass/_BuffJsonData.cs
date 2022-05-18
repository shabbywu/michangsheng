using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AD8 RID: 2776
	public class _BuffJsonData : IJSONClass
	{
		// Token: 0x060046CA RID: 18122 RVA: 0x001E48BC File Offset: 0x001E2ABC
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

		// Token: 0x060046CB RID: 18123 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EE3 RID: 16099
		public static Dictionary<int, _BuffJsonData> DataDict = new Dictionary<int, _BuffJsonData>();

		// Token: 0x04003EE4 RID: 16100
		public static List<_BuffJsonData> DataList = new List<_BuffJsonData>();

		// Token: 0x04003EE5 RID: 16101
		public static Action OnInitFinishAction = new Action(_BuffJsonData.OnInitFinish);

		// Token: 0x04003EE6 RID: 16102
		public int buffid;

		// Token: 0x04003EE7 RID: 16103
		public int BuffIcon;

		// Token: 0x04003EE8 RID: 16104
		public int bufftype;

		// Token: 0x04003EE9 RID: 16105
		public int trigger;

		// Token: 0x04003EEA RID: 16106
		public int removeTrigger;

		// Token: 0x04003EEB RID: 16107
		public int looptime;

		// Token: 0x04003EEC RID: 16108
		public int totaltime;

		// Token: 0x04003EED RID: 16109
		public int BuffType;

		// Token: 0x04003EEE RID: 16110
		public int isHide;

		// Token: 0x04003EEF RID: 16111
		public int ShowOnlyOne;

		// Token: 0x04003EF0 RID: 16112
		public string skillEffect;

		// Token: 0x04003EF1 RID: 16113
		public string name;

		// Token: 0x04003EF2 RID: 16114
		public string descr;

		// Token: 0x04003EF3 RID: 16115
		public string script;

		// Token: 0x04003EF4 RID: 16116
		public List<int> Affix = new List<int>();

		// Token: 0x04003EF5 RID: 16117
		public List<int> seid = new List<int>();
	}
}
