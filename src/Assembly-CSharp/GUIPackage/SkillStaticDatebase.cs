using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D9B RID: 3483
	public class SkillStaticDatebase : MonoBehaviour
	{
		// Token: 0x06005417 RID: 21527 RVA: 0x0003C240 File Offset: 0x0003A440
		public void Awake()
		{
			SkillStaticDatebase.instence = this;
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x0003C248 File Offset: 0x0003A448
		private void OnDestroy()
		{
			SkillStaticDatebase.instence = null;
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x0003C250 File Offset: 0x0003A450
		public void Preload(int taskID)
		{
			Loom.RunAsync(delegate
			{
				this.LoadAsync(taskID);
			});
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x00230B6C File Offset: 0x0022ED6C
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
			}
		}

		// Token: 0x040053D4 RID: 21460
		public static SkillStaticDatebase instence;

		// Token: 0x040053D5 RID: 21461
		public List<Skill> skills = new List<Skill>();

		// Token: 0x040053D6 RID: 21462
		public Dictionary<int, Skill> dicSkills = new Dictionary<int, Skill>();

		// Token: 0x040053D7 RID: 21463
		public Dictionary<int, Dictionary<int, Skill>> Dict = new Dictionary<int, Dictionary<int, Skill>>();
	}
}
