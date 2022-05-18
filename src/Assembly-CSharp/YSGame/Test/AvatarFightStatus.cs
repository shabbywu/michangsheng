using System;
using System.Collections.Generic;
using System.Text;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace YSGame.Test
{
	// Token: 0x02000E0E RID: 3598
	[Serializable]
	public class AvatarFightStatus
	{
		// Token: 0x060056EC RID: 22252 RVA: 0x0003E214 File Offset: 0x0003C414
		public AvatarFightStatus(Avatar avatar)
		{
			this.Avatar = avatar;
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x00243060 File Offset: 0x00241260
		public void RefreshData()
		{
			this.Name = this.Avatar.name;
			this.HP = string.Format("{0}/{1}", this.Avatar.HP, this.Avatar.HP_Max);
			this.LingGen = this.GetLingGenDesc();
			this.LingQi = this.GetLingQiDesc();
			this.Buff = this.GetBuffDesc();
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x002430D4 File Offset: 0x002412D4
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

		// Token: 0x060056EF RID: 22255 RVA: 0x002432C0 File Offset: 0x002414C0
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

		// Token: 0x060056F0 RID: 22256 RVA: 0x0024331C File Offset: 0x0024151C
		public string GetBuffDesc()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (List<int> list in this.Avatar.bufflist)
			{
				_BuffJsonData buffJsonData = _BuffJsonData.DataDict[list[2]];
				stringBuilder.AppendLine(string.Format("{0} ID:{1} {2}层 NUM:{3}", new object[]
				{
					buffJsonData.name,
					list[2],
					list[1],
					list[0]
				}));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04005699 RID: 22169
		[NonSerialized]
		public Avatar Avatar;

		// Token: 0x0400569A RID: 22170
		public string Name;

		// Token: 0x0400569B RID: 22171
		public string HP;

		// Token: 0x0400569C RID: 22172
		public string LingGen;

		// Token: 0x0400569D RID: 22173
		public string LingQi;

		// Token: 0x0400569E RID: 22174
		[Multiline]
		public string Buff;
	}
}
