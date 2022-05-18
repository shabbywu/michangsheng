using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02001029 RID: 4137
	public class SkillBox
	{
		// Token: 0x060062FA RID: 25338 RVA: 0x0004460C File Offset: 0x0004280C
		public SkillBox()
		{
			SkillBox.inst = this;
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x000042DD File Offset: 0x000024DD
		public void initSkillDisplay()
		{
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x002771E0 File Offset: 0x002753E0
		public void pull()
		{
			this.clear();
			Entity entity = KBEngineApp.app.player();
			if (entity != null)
			{
				entity.cellCall("requestPull", Array.Empty<object>());
			}
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x00044630 File Offset: 0x00042830
		public void clear()
		{
			this.skills.Clear();
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x00277214 File Offset: 0x00275414
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

		// Token: 0x060062FF RID: 25343 RVA: 0x0027727C File Offset: 0x0027547C
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

		// Token: 0x06006300 RID: 25344 RVA: 0x002772C0 File Offset: 0x002754C0
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

		// Token: 0x06006301 RID: 25345 RVA: 0x002772FC File Offset: 0x002754FC
		public void showUpdateCD(int skillid)
		{
			int num = this.findBoxId(skillid);
			if (num != -1)
			{
				UI_MainUI.inst.btnAll[num].gameObject.GetComponent<updateCD>().OnBtnClickSkill();
			}
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x00277334 File Offset: 0x00275534
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

		// Token: 0x04005CFF RID: 23807
		public static SkillBox inst;

		// Token: 0x04005D00 RID: 23808
		public List<Skill> skills = new List<Skill>();

		// Token: 0x04005D01 RID: 23809
		public Dictionary<string, GameObject> dictSkillDisplay = new Dictionary<string, GameObject>();
	}
}
