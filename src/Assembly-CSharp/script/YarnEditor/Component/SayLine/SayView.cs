using System;
using Fungus;
using KBEngine;
using UnityEngine;
using Yarn.Unity;

namespace script.YarnEditor.Component.SayLine
{
	// Token: 0x020009CA RID: 2506
	public class SayView : DialogueViewBase
	{
		// Token: 0x060045BE RID: 17854 RVA: 0x001D8D4C File Offset: 0x001D6F4C
		public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
		{
			if (this.sayDialog == null)
			{
				this.sayDialog = SayDialog.GetSayDialog();
			}
			this.currentLine = dialogueLine;
			int num = int.Parse(dialogueLine.CharacterName);
			string text = dialogueLine.TextWithoutCharacterName.Text;
			Avatar player = Tools.instance.getPlayer();
			int npcid = NPCEx.NPCIDToNew(num);
			if (PlayerEx.IsDaoLv(npcid))
			{
				string daoLvNickName = PlayerEx.GetDaoLvNickName(npcid);
				text = text.Replace("{LastName}", player.lastName).Replace("{FirstName}", "").Replace("{gongzi}", daoLvNickName).Replace("{xiongdi}", daoLvNickName).Replace("{shidi}", daoLvNickName).Replace("{shixiong}", daoLvNickName).Replace("{xiaozi}", (player.Sex == 1) ? "小子" : "丫头").Replace("{ta}", (player.Sex == 1) ? "他" : "她").Replace("{menpai}", Tools.getStr("menpai" + player.menPai));
			}
			else
			{
				text = text.Replace("{LastName}", player.lastName).Replace("{FirstName}", player.firstName).Replace("{gongzi}", (player.Sex == 1) ? "公子" : "姑娘").Replace("{xiongdi}", (player.Sex == 1) ? "兄弟" : "姑娘").Replace("{shidi}", (player.Sex == 1) ? "师弟" : "师妹").Replace("{shixiong}", (player.Sex == 1) ? "师兄" : "师姐").Replace("{xiaozi}", (player.Sex == 1) ? "小子" : "丫头").Replace("{ta}", (player.Sex == 1) ? "他" : "她").Replace("{menpai}", Tools.getStr("menpai" + player.menPai));
			}
			this.sayDialog.gameObject.SetActive(true);
			this.sayDialog.SetCharacter(null, num);
			this.sayDialog.SetCharacterImage(null, num);
			onDialogueLineFinished();
			Debug.Log(text);
			this.sayDialog.Say(text, true, true, true, true, false, null, new Action(this.Continue));
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x001D8FC3 File Offset: 0x001D71C3
		public void Continue()
		{
			if (this.currentLine == null)
			{
				return;
			}
			base.ReadyForNextLine();
		}

		// Token: 0x0400473B RID: 18235
		private SayDialog sayDialog;

		// Token: 0x0400473C RID: 18236
		private LocalizedLine currentLine;
	}
}
