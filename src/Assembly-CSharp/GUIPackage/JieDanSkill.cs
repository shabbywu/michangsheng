using System;
using System.Collections.Generic;
using KBEngine;

namespace GUIPackage
{
	// Token: 0x02000A4F RID: 2639
	public class JieDanSkill : StaticSkill
	{
		// Token: 0x060048CA RID: 18634 RVA: 0x001EC7E6 File Offset: 0x001EA9E6
		public JieDanSkill(int id, int level, int max)
		{
			this.skill_ID = id;
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x001EC7F5 File Offset: 0x001EA9F5
		public override Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
		{
			return attaker.JieDanSkillSeidFlag;
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x001EC7FD File Offset: 0x001EA9FD
		public override JSONObject getSeidJson(int seid)
		{
			return jsonData.instance.JieDanSeidJsonData[seid][string.Concat(this.skill_ID)];
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x001EC820 File Offset: 0x001EAA20
		public static void resetJieDanSeid(Avatar attaker)
		{
			attaker.JieDanSkillSeidFlag.Clear();
			foreach (SkillItem skillItem in attaker.hasJieDanSkillList)
			{
				new JieDanSkill(skillItem.itemId, 0, 5).Puting(attaker, attaker, 2);
			}
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x001EC88C File Offset: 0x001EAA8C
		public override JSONObject getJsonData()
		{
			return jsonData.instance.JieDanBiao;
		}
	}
}
