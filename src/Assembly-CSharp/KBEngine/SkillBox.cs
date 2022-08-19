using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C7C RID: 3196
	public class SkillBox
	{
		// Token: 0x06005866 RID: 22630 RVA: 0x0024B478 File Offset: 0x00249678
		public SkillBox()
		{
			SkillBox.inst = this;
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x00004095 File Offset: 0x00002295
		public void initSkillDisplay()
		{
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x0024B49C File Offset: 0x0024969C
		public void pull()
		{
			this.clear();
			Entity entity = KBEngineApp.app.player();
			if (entity != null)
			{
				entity.cellCall("requestPull", Array.Empty<object>());
			}
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x0024B4CD File Offset: 0x002496CD
		public void clear()
		{
			this.skills.Clear();
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x0024B4DC File Offset: 0x002496DC
		public void add(Skill skill)
		{
			for (int i = 0; i < this.skills.Count; i++)
			{
				if (this.skills[i].id == skill.id)
				{
					Dbg.DEBUG_MSG("SkillBox::add: " + skill.id + " is exist!");
					return;
				}
			}
			this.skills.Add(skill);
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x0024B544 File Offset: 0x00249744
		public void remove(int id)
		{
			for (int i = 0; i < this.skills.Count; i++)
			{
				if (this.skills[i].id == id)
				{
					this.skills.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x0024B588 File Offset: 0x00249788
		public int findBoxId(int skillid)
		{
			for (int i = 0; i < this.skills.Count; i++)
			{
				if (this.skills[i].id == skillid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x0024B5C4 File Offset: 0x002497C4
		public void showUpdateCD(int skillid)
		{
			int num = this.findBoxId(skillid);
			if (num != -1)
			{
				UI_MainUI.inst.btnAll[num].gameObject.GetComponent<updateCD>().OnBtnClickSkill();
			}
		}

		// Token: 0x0600586E RID: 22638 RVA: 0x0024B5FC File Offset: 0x002497FC
		public Skill get(int id)
		{
			for (int i = 0; i < this.skills.Count; i++)
			{
				if (this.skills[i].id == id)
				{
					return this.skills[i];
				}
			}
			Skill skill = new Skill();
			skill.id = id;
			skill.name = id + " ";
			skill.displayType = (Skill_DisplayType)jsonData.instance.skillJsonData[string.Concat(id)]["Skill_DisplayType"].n;
			skill.canUseDistMax = jsonData.instance.skillJsonData[string.Concat(id)]["canUseDistMax"].n;
			skill.skillEffect = jsonData.instance.skillJsonData[string.Concat(id)]["skillEffect"].str;
			skill.name = jsonData.instance.skillJsonData[string.Concat(id)]["name"].str;
			skill.coolTime = jsonData.instance.skillJsonData[string.Concat(id)]["CD"].n;
			SkillBox.inst.add(skill);
			return skill;
		}

		// Token: 0x040051FF RID: 20991
		public static SkillBox inst;

		// Token: 0x04005200 RID: 20992
		public List<Skill> skills = new List<Skill>();

		// Token: 0x04005201 RID: 20993
		public Dictionary<string, GameObject> dictSkillDisplay = new Dictionary<string, GameObject>();
	}
}
