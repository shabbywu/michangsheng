using System;
using Fungus;
using KBEngine;
using UnityEngine;
using Yarn.Unity;

namespace script.YarnEditor.Component.SayLine;

public class SayView : DialogueViewBase
{
	private SayDialog sayDialog;

	private LocalizedLine currentLine;

	public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)sayDialog == (Object)null)
		{
			sayDialog = SayDialog.GetSayDialog();
		}
		currentLine = dialogueLine;
		int num = int.Parse(dialogueLine.CharacterName);
		string text = dialogueLine.TextWithoutCharacterName.Text;
		Avatar player = Tools.instance.getPlayer();
		int npcid = NPCEx.NPCIDToNew(num);
		if (PlayerEx.IsDaoLv(npcid))
		{
			string daoLvNickName = PlayerEx.GetDaoLvNickName(npcid);
			text = text.Replace("{LastName}", player.lastName).Replace("{FirstName}", "").Replace("{gongzi}", daoLvNickName)
				.Replace("{xiongdi}", daoLvNickName)
				.Replace("{shidi}", daoLvNickName)
				.Replace("{shixiong}", daoLvNickName)
				.Replace("{xiaozi}", (player.Sex == 1) ? "小子" : "丫头")
				.Replace("{ta}", (player.Sex == 1) ? "他" : "她")
				.Replace("{menpai}", Tools.getStr("menpai" + player.menPai));
		}
		else
		{
			text = text.Replace("{LastName}", player.lastName).Replace("{FirstName}", player.firstName).Replace("{gongzi}", (player.Sex == 1) ? "公子" : "姑娘")
				.Replace("{xiongdi}", (player.Sex == 1) ? "兄弟" : "姑娘")
				.Replace("{shidi}", (player.Sex == 1) ? "师弟" : "师妹")
				.Replace("{shixiong}", (player.Sex == 1) ? "师兄" : "师姐")
				.Replace("{xiaozi}", (player.Sex == 1) ? "小子" : "丫头")
				.Replace("{ta}", (player.Sex == 1) ? "他" : "她")
				.Replace("{menpai}", Tools.getStr("menpai" + player.menPai));
		}
		((Component)sayDialog).gameObject.SetActive(true);
		sayDialog.SetCharacter(null, num);
		sayDialog.SetCharacterImage(null, num);
		onDialogueLineFinished();
		Debug.Log((object)text);
		sayDialog.Say(text, clearPrevious: true, waitForInput: true, fadeWhenDone: true, stopVoiceover: true, waitForVO: false, null, Continue);
	}

	public void Continue()
	{
		if (currentLine != null)
		{
			((DialogueViewBase)this).ReadyForNextLine();
		}
	}
}
