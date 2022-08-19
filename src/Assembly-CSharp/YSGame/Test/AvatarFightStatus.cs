using System;
using System.Collections.Generic;
using System.Text;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace YSGame.Test
{
	// Token: 0x02000ACF RID: 2767
	[Serializable]
	public class AvatarFightStatus
	{
		// Token: 0x06004D9B RID: 19867 RVA: 0x00212F80 File Offset: 0x00211180
		public AvatarFightStatus(Avatar avatar)
		{
			this.Avatar = avatar;
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x00212F90 File Offset: 0x00211190
		public void RefreshData()
		{
			this.Name = this.Avatar.name;
			this.HP = string.Format("{0}/{1}", this.Avatar.HP, this.Avatar.HP_Max);
			this.LingGen = this.GetLingGenDesc();
			this.LingQi = this.GetLingQiDesc();
			this.Buff = this.GetBuffDesc();
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x00213004 File Offset: 0x00211204
		public string GetLingGenDesc()
		{
			string text = "基础灵根";
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int i = 0; i < 6; i++)
			{
				dictionary.Add(i, 0);
			}
			int num = 0;
			foreach (int num2 in this.Avatar.GetLingGeng)
			{
				if (num > 5)
				{
					break;
				}
				Dictionary<int, int> dictionary2 = dictionary;
				int key = num;
				dictionary2[key] += num2;
				text += string.Format("{0}{1} ", num.ToLingQiName(), num2);
				num++;
			}
			foreach (KeyValuePair<int, int> keyValuePair in this.Avatar.DrawWeight)
			{
				Dictionary<int, int> dictionary2 = dictionary;
				int key = keyValuePair.Key;
				dictionary2[key] += keyValuePair.Value;
			}
			if (this.Avatar.SkillSeidFlag.ContainsKey(13))
			{
				foreach (KeyValuePair<int, int> keyValuePair2 in this.Avatar.SkillSeidFlag[13])
				{
					Dictionary<int, int> dictionary2 = dictionary;
					int key = keyValuePair2.Key;
					dictionary2[key] += keyValuePair2.Value;
				}
			}
			text += "战时灵根";
			for (int j = 0; j < dictionary.Count; j++)
			{
				text += string.Format("{0}{1} ", j.ToLingQiName(), dictionary[j]);
			}
			return text;
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x002131F0 File Offset: 0x002113F0
		public string GetLingQiDesc()
		{
			string text = "";
			List<int> list = this.Avatar.cardMag.ToListInt32();
			for (int i = 0; i < list.Count; i++)
			{
				text += string.Format("{0}{1} ", i.ToLingQiName(), list[i]);
			}
			return text;
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x0021324C File Offset: 0x0021144C
		public string GetBuffDesc()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (List<int> list in this.Avatar.bufflist)
			{
				_BuffJsonData buffJsonData = _BuffJsonData.DataDict[list[2]];
				stringBuilder.AppendLine(string.Format("{0} ID:{1} ROUND:{2} NUM:{3}", new object[]
				{
					buffJsonData.name,
					list[2],
					list[1],
					list[0]
				}));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04004CBE RID: 19646
		[NonSerialized]
		public Avatar Avatar;

		// Token: 0x04004CBF RID: 19647
		public string Name;

		// Token: 0x04004CC0 RID: 19648
		public string HP;

		// Token: 0x04004CC1 RID: 19649
		public string LingGen;

		// Token: 0x04004CC2 RID: 19650
		public string LingQi;

		// Token: 0x04004CC3 RID: 19651
		[Multiline]
		public string Buff;
	}
}
