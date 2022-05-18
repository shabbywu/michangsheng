using System;
using System.Collections.Generic;
using KBEngine;

namespace GUIPackage
{
	// Token: 0x02000D6B RID: 3435
	public class WuDaoStaticSkill : StaticSkill
	{
		// Token: 0x06005293 RID: 21139 RVA: 0x0003AB5C File Offset: 0x00038D5C
		public WuDaoStaticSkill(int id, int level, int max)
		{
			this.skill_ID = id;
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x0003B2B5 File Offset: 0x000394B5
		public override Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
		{
			return attaker.wuDaoMag.WuDaoSkillSeidFlag;
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x0003B2C2 File Offset: 0x000394C2
		public override JSONObject getSeidJson(int seid)
		{
			return jsonData.instance.WuDaoSeidJsonData[seid][string.Concat(this.skill_ID)];
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x0022706C File Offset: 0x0022526C
		public static void resetWuDaoSeid(Avatar attaker)
		{
			attaker.wuDaoMag.WuDaoSkillSeidFlag.Clear();
			foreach (SkillItem skillItem in attaker.wuDaoMag.GetAllWuDaoSkills())
			{
				new WuDaoStaticSkill(skillItem.itemId, 0, 5).Puting(attaker, attaker, 2);
			}
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x0003B2E5 File Offset: 0x000394E5
		public override JSONObject getJsonData()
		{
			return jsonData.instance.WuDaoJson;
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0003B2F1 File Offset: 0x000394F1
		public void StudtRealizeSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.setHP(attaker.HP + (int)this.getSeidJson(seid)["value1"].n);
		}
	}
}
