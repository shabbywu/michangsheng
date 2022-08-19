using System;
using System.Collections.Generic;
using KBEngine;

namespace GUIPackage
{
	// Token: 0x02000A53 RID: 2643
	public class WuDaoStaticSkill : StaticSkill
	{
		// Token: 0x060049BB RID: 18875 RVA: 0x001EC7E6 File Offset: 0x001EA9E6
		public WuDaoStaticSkill(int id, int level, int max)
		{
			this.skill_ID = id;
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x001F4610 File Offset: 0x001F2810
		public override Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
		{
			return attaker.wuDaoMag.WuDaoSkillSeidFlag;
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x001F461D File Offset: 0x001F281D
		public override JSONObject getSeidJson(int seid)
		{
			return jsonData.instance.WuDaoSeidJsonData[seid][string.Concat(this.skill_ID)];
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x001F4640 File Offset: 0x001F2840
		public static void resetWuDaoSeid(Avatar attaker)
		{
			attaker.wuDaoMag.WuDaoSkillSeidFlag.Clear();
			foreach (SkillItem skillItem in attaker.wuDaoMag.GetAllWuDaoSkills())
			{
				new WuDaoStaticSkill(skillItem.itemId, 0, 5).Puting(attaker, attaker, 2);
			}
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x001F46B4 File Offset: 0x001F28B4
		public override JSONObject getJsonData()
		{
			return jsonData.instance.WuDaoJson;
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x001F46C0 File Offset: 0x001F28C0
		public void StudtRealizeSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.setHP(attaker.HP + (int)this.getSeidJson(seid)["value1"].n);
		}
	}
}
