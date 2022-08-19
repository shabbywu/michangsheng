using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A6E RID: 2670
	public class SkillStaticDatebase : MonoBehaviour
	{
		// Token: 0x06004B0B RID: 19211 RVA: 0x001FED3F File Offset: 0x001FCF3F
		public void Awake()
		{
			SkillStaticDatebase.instence = this;
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x001FED47 File Offset: 0x001FCF47
		private void OnDestroy()
		{
			SkillStaticDatebase.instence = null;
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x001FED4F File Offset: 0x001FCF4F
		public void Preload(int taskID)
		{
			Loom.RunAsync(delegate
			{
				this.LoadAsync(taskID);
			});
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x001FED78 File Offset: 0x001FCF78
		public void LoadAsync(int taskID)
		{
			try
			{
				foreach (JSONObject jsonobject in jsonData.instance.StaticSkillJsonData.list)
				{
					Skill skill = new Skill();
					int i = jsonobject["id"].I;
					skill.initStaticSkill(i, 0, 5);
					this.skills.Add(skill);
					this.dicSkills.Add(i, skill);
					if (!this.Dict.ContainsKey(skill.SkillID))
					{
						this.Dict.Add(skill.SkillID, new Dictionary<int, Skill>());
					}
					if (!this.Dict[skill.SkillID].ContainsKey(skill.Skill_Lv))
					{
						this.Dict[skill.SkillID].TryAdd(skill.Skill_Lv, skill, "");
					}
				}
				PreloadManager.Inst.TaskDone(taskID);
			}
			catch (Exception arg)
			{
				PreloadManager.IsException = true;
				PreloadManager.ExceptionData += string.Format("{0}\n", arg);
				PreloadManager.Inst.TaskDone(taskID);
			}
		}

		// Token: 0x04004A35 RID: 18997
		public static SkillStaticDatebase instence;

		// Token: 0x04004A36 RID: 18998
		public List<Skill> skills = new List<Skill>();

		// Token: 0x04004A37 RID: 18999
		public Dictionary<int, Skill> dicSkills = new Dictionary<int, Skill>();

		// Token: 0x04004A38 RID: 19000
		public Dictionary<int, Dictionary<int, Skill>> Dict = new Dictionary<int, Dictionary<int, Skill>>();
	}
}
