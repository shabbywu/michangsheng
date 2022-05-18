using System;
using Fungus;
using KBEngine;
using UnityEngine;
using Yarn.Unity;

namespace script.YarnEditor.Component.SayLine
{
	// Token: 0x02000AB1 RID: 2737
	public class SayView : DialogueViewBase
	{
		// Token: 0x0600461A RID: 17946 RVA: 0x001DEB38 File Offset: 0x001DCD38
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

		// Token: 0x0600461B RID: 17947 RVA: 0x0003225B File Offset: 0x0003045B
		public void Continue()
		{
			if (this.currentLine == null)
			{
				return;
			}
			base.ReadyForNextLine();
		}

		// Token: 0x04003E4C RID: 15948
		private SayDialog sayDialog;

		// Token: 0x04003E4D RID: 15949
		private LocalizedLine currentLine;
	}
}
