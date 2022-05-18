using System;
using System.Collections.Generic;
using KBEngine;

namespace GUIPackage
{
	// Token: 0x02000D5B RID: 3419
	public class JieDanSkill : StaticSkill
	{
		// Token: 0x06005192 RID: 20882 RVA: 0x0003AB5C File Offset: 0x00038D5C
		public JieDanSkill(int id, int level, int max)
		{
			this.skill_ID = id;
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0003AB6B File Offset: 0x00038D6B
		public override Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
		{
			return attaker.JieDanSkillSeidFlag;
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x0003AB73 File Offset: 0x00038D73
		public override JSONObject getSeidJson(int seid)
		{
			return jsonData.instance.JieDanSeidJsonData[seid][string.Concat(this.skill_ID)];
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x002200C8 File Offset: 0x0021E2C8
		public static void resetJieDanSeid(Avatar attaker)
		{
			attaker.JieDanSkillSeidFlag.Clear();
			foreach (SkillItem skillItem in attaker.hasJieDanSkillList)
			{
				new JieDanSkill(skillItem.itemId, 0, 5).Puting(attaker, attaker, 2);
			}
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x0003AB96 File Offset: 0x00038D96
		public override JSONObject getJsonData()
		{
			return jsonData.instance.JieDanBiao;
		}
	}
}
