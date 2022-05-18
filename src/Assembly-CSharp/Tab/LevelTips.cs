using System;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000A2E RID: 2606
	public class LevelTips : ITabTips
	{
		// Token: 0x0600437A RID: 17274 RVA: 0x001CD210 File Offset: 0x001CB410
		public LevelTips(GameObject go)
		{
			this._go = go;
			this._rect = base.Get<RectTransform>("Bg");
			this._sizeFitter = base.Get<ContentSizeFitter>("Bg");
			this._childSizeFitter = base.Get<ContentSizeFitter>("Bg/Content");
			this._levelTipsTitle = base.Get<Text>("Bg/Title/Text");
			this._levelTipsMessage = base.Get<Text>("Bg/Content");
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x001CD280 File Offset: 0x001CB480
		public void Show()
		{
			Avatar player = PlayerEx.Player;
			int jinDanID = this.getJinDanID();
			if (jinDanID != -1)
			{
				JieDanBiao jieDanBiao = JieDanBiao.DataDict[jinDanID];
				this._levelTipsTitle.text = jieDanBiao.JinDanQuality.ToCNNumber() + "品" + jieDanBiao.name;
				string text = string.Format("<color=#c7c479>气血</color><color=#dbffa2>+ {0}</color>  ", player.getJieDanSkillAddHP());
				text = text + "<color=#c7c479>修炼速度</color><color=#dbffa2>+" + string.Format("{0}%</color>", (int)(Math.Ceiling((double)(player.getJieDanSkillAddExp() * 100f)) - 100.0));
				for (int i = 0; i < jieDanBiao.LinGengType.Count; i++)
				{
					text = string.Concat(new string[]
					{
						text,
						"\n<color=#c7c479>",
						Tools.getStr("xibieFight" + jieDanBiao.LinGengType[i]),
						"灵根权重</color>",
						string.Format("<color=#dbffa2>+{0}</color>", jieDanBiao.LinGengZongShu[i])
					});
				}
				if (jieDanBiao.desc != "")
				{
					text = text + "\n<color=#c7c479>" + jieDanBiao.desc + "</color>";
				}
				if (this.IsYuanYing())
				{
					this._levelTipsTitle.text = "<color=#ce49ff>元婴</color>";
					foreach (SkillItem skillItem in player.equipStaticSkillList)
					{
						int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(skillItem.itemId);
						if (skillItem.itemIndex == 6)
						{
							text += "\n<color=#dbffa2>第二元神：元婴能够单独修炼一门功法，并根据功法属性解锁独有特性</color>";
							text = text + "\n<color=#dbffa2>元婴九变：" + player.getYuanYingStaticDesc(skillItem, staticSkillKeyByID) + "</color>";
							break;
						}
					}
				}
				if (this.IsHuaShen())
				{
					this._levelTipsTitle.text = "<size=24><color=#ff7864>化神</color></size>";
					GUIPackage.Skill skill = SkillDatebase.instence.Dict[player.HuaShenLingYuSkill.I][1];
					text = string.Concat(new string[]
					{
						text,
						"\n<color=#ff7864>",
						skill.skill_Name,
						"</color>\n<color=#dbffa2>",
						skill.skill_Desc,
						"</color>"
					});
				}
				this._levelTipsMessage.text = text;
				this._go.SetActive(true);
				base.UpdateSize();
			}
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		protected override string Replace(string msg)
		{
			return msg;
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x001CD500 File Offset: 0x001CB700
		private int getJinDanID()
		{
			int result = -1;
			foreach (SkillItem skillItem in Tools.instance.getPlayer().hasJieDanSkillList)
			{
				result = skillItem.itemId;
			}
			return result;
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x0003039D File Offset: 0x0002E59D
		private bool IsYuanYing()
		{
			return Tools.instance.getPlayer().level >= 10;
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x000303B5 File Offset: 0x0002E5B5
		private bool IsHuaShen()
		{
			return Tools.instance.getPlayer().level >= 13;
		}

		// Token: 0x04003B7D RID: 15229
		private Text _levelTipsTitle;

		// Token: 0x04003B7E RID: 15230
		private Text _levelTipsMessage;
	}
}
