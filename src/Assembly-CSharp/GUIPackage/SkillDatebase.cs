using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D98 RID: 3480
	public class SkillDatebase : MonoBehaviour
	{
		// Token: 0x06005403 RID: 21507 RVA: 0x0003C0C6 File Offset: 0x0003A2C6
		public void Awake()
		{
			SkillDatebase.instence = this;
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x0003C0CE File Offset: 0x0003A2CE
		private void OnDestroy()
		{
			SkillDatebase.instence = null;
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x0003C0D6 File Offset: 0x0003A2D6
		public void Preload(int taskID)
		{
			Loom.RunAsync(delegate
			{
				this.LoadAsync(taskID);
			});
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x0023072C File Offset: 0x0022E92C
		public void LoadAsync(int taskID)
		{
			try
			{
				foreach (JSONObject jsonobject in jsonData.instance._skillJsonData.list)
				{
					Skill skill = new Skill(jsonobject["id"].I, 0, 10);
					this.skills.Add(skill);
					this.dicSkills.Add(jsonobject["id"].I, skill);
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

		// Token: 0x040053BF RID: 21439
		public static SkillDatebase instence;

		// Token: 0x040053C0 RID: 21440
		public List<Skill> skills = new List<Skill>();

		// Token: 0x040053C1 RID: 21441
		public Dictionary<int, Skill> dicSkills = new Dictionary<int, Skill>();

		// Token: 0x040053C2 RID: 21442
		public Dictionary<int, Dictionary<int, Skill>> Dict = new Dictionary<int, Dictionary<int, Skill>>();
	}
}
