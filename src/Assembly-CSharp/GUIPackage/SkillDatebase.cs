using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A6C RID: 2668
	public class SkillDatebase : MonoBehaviour
	{
		// Token: 0x06004AF9 RID: 19193 RVA: 0x001FE789 File Offset: 0x001FC989
		public void Awake()
		{
			SkillDatebase.instence = this;
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x001FE791 File Offset: 0x001FC991
		private void OnDestroy()
		{
			SkillDatebase.instence = null;
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x001FE799 File Offset: 0x001FC999
		public void Preload(int taskID)
		{
			Loom.RunAsync(delegate
			{
				this.LoadAsync(taskID);
			});
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x001FE7C0 File Offset: 0x001FC9C0
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
				PreloadManager.Inst.TaskDone(taskID);
			}
		}

		// Token: 0x04004A22 RID: 18978
		public static SkillDatebase instence;

		// Token: 0x04004A23 RID: 18979
		public List<Skill> skills = new List<Skill>();

		// Token: 0x04004A24 RID: 18980
		public Dictionary<int, Skill> dicSkills = new Dictionary<int, Skill>();

		// Token: 0x04004A25 RID: 18981
		public Dictionary<int, Dictionary<int, Skill>> Dict = new Dictionary<int, Dictionary<int, Skill>>();
	}
}
