using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("Narrative", "Say", "Writes text in a dialog box.", 0)]
[AddComponentMenu("")]
public class Say : Command, ILocalizable
{
	[TextArea(5, 10)]
	[SerializeField]
	protected string storyText = "";

	[Tooltip("Notes about this story text for other authors, localization, etc.")]
	[SerializeField]
	protected string description = "";

	[Tooltip("设置对话ID的方式")]
	[SerializeField]
	protected StartFight.MonstarType AvatarIDSetType = StartFight.MonstarType.FungusVariable;

	[Tooltip("说话的英雄ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable AvatarID;

	[Tooltip("说话的英雄ID")]
	[SerializeField]
	protected int AvatarIntID;

	[Tooltip("Character that is speaking")]
	[SerializeField]
	protected Character character;

	[Tooltip("Portrait that represents speaking character")]
	[SerializeField]
	protected Sprite portrait;

	[Tooltip("Voiceover audio to play when writing the text")]
	[SerializeField]
	protected AudioClip voiceOverClip;

	[Tooltip("Always show this Say text when the command is executed multiple times")]
	[SerializeField]
	protected bool showAlways = true;

	[Tooltip("Number of times to show this Say text when the command is executed multiple times")]
	[SerializeField]
	protected int showCount = 1;

	[Tooltip("Type this text in the previous dialog box.")]
	[SerializeField]
	protected bool extendPrevious;

	[Tooltip("Fade out the dialog box when writing has finished and not waiting for input.")]
	[SerializeField]
	protected bool fadeWhenDone = true;

	[Tooltip("Wait for player to click before continuing.")]
	[SerializeField]
	protected bool waitForClick = true;

	[Tooltip("Stop playing voiceover when text finishes writing.")]
	[SerializeField]
	protected bool stopVoiceover = true;

	[Tooltip("Wait for the Voice Over to complete before continuing")]
	[SerializeField]
	protected bool waitForVO;

	[Tooltip("Sets the active Say dialog with a reference to a Say Dialog object in the scene. All story text will now display using this Say Dialog.")]
	[SerializeField]
	protected SayDialog setSayDialog;

	protected int executionCount;

	public StartFight.MonstarType pubAvatarIDSetType
	{
		set
		{
			AvatarIDSetType = value;
		}
	}

	public int pubAvatarIntID
	{
		set
		{
			AvatarIntID = value;
		}
	}

	public bool SetfadeWhenDone
	{
		set
		{
			fadeWhenDone = value;
		}
	}

	public bool SetwaitForClick
	{
		set
		{
			waitForClick = value;
		}
	}

	public virtual Character _Character => character;

	public virtual StartFight.MonstarType _AvatarIDSetType => AvatarIDSetType;

	public virtual Sprite Portrait
	{
		get
		{
			return portrait;
		}
		set
		{
			portrait = value;
		}
	}

	public virtual bool ExtendPrevious => extendPrevious;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().StreamData.FungusSaveMgr.SetCommand(this);
		if (!showAlways && executionCount >= showCount)
		{
			Continue();
			return;
		}
		executionCount++;
		if ((Object)(object)character != (Object)null && (Object)(object)character.SetSayDialog != (Object)null)
		{
			SayDialog.ActiveSayDialog = character.SetSayDialog;
		}
		if ((Object)(object)setSayDialog != (Object)null)
		{
			SayDialog.ActiveSayDialog = setSayDialog;
		}
		SayDialog sayDialog = SayDialog.GetSayDialog();
		if ((Object)(object)sayDialog == (Object)null)
		{
			Continue();
			return;
		}
		Flowchart flowchart = GetFlowchart();
		sayDialog.SetActive(state: true);
		int num = 0;
		if (AvatarIDSetType == StartFight.MonstarType.Normal)
		{
			num = AvatarIntID;
		}
		else if ((Object)(object)AvatarID != (Object)null)
		{
			num = AvatarID.Value;
		}
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(num))
		{
			num = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[num];
		}
		sayDialog.SetCharacter(character, num);
		sayDialog.SetCharacterImage(portrait, num);
		Avatar player = Tools.instance.getPlayer();
		int npcid = NPCEx.NPCIDToNew(num);
		if (PlayerEx.IsDaoLv(npcid))
		{
			string daoLvNickName = PlayerEx.GetDaoLvNickName(npcid);
			storyText = storyText.Replace("{LastName}", player.lastName).Replace("{FirstName}", "").Replace("{gongzi}", daoLvNickName)
				.Replace("{xiongdi}", daoLvNickName)
				.Replace("{shidi}", daoLvNickName)
				.Replace("{shixiong}", daoLvNickName)
				.Replace("{xiaozi}", (player.Sex == 1) ? "小子" : "丫头")
				.Replace("{ta}", (player.Sex == 1) ? "他" : "她")
				.Replace("{menpai}", Tools.getStr("menpai" + player.menPai));
		}
		else
		{
			storyText = storyText.Replace("{LastName}", player.lastName).Replace("{FirstName}", player.firstName).Replace("{gongzi}", (player.Sex == 1) ? "公子" : "姑娘")
				.Replace("{xiongdi}", (player.Sex == 1) ? "兄弟" : "姑娘")
				.Replace("{shidi}", (player.Sex == 1) ? "师弟" : "师妹")
				.Replace("{shixiong}", (player.Sex == 1) ? "师兄" : "师姐")
				.Replace("{xiaozi}", (player.Sex == 1) ? "小子" : "丫头")
				.Replace("{ta}", (player.Sex == 1) ? "他" : "她")
				.Replace("{menpai}", Tools.getStr("menpai" + player.menPai));
		}
		string text = storyText;
		List<CustomTag> activeCustomTags = CustomTag.activeCustomTags;
		for (int i = 0; i < activeCustomTags.Count; i++)
		{
			CustomTag customTag = activeCustomTags[i];
			text = text.Replace(customTag.TagStartSymbol, customTag.ReplaceTagStartWith);
			if (customTag.TagEndSymbol != "" && customTag.ReplaceTagEndWith != "")
			{
				text = text.Replace(customTag.TagEndSymbol, customTag.ReplaceTagEndWith);
			}
		}
		string text2 = flowchart.SubstituteVariables(text);
		sayDialog.Say(text2, !extendPrevious, waitForClick, fadeWhenDone, stopVoiceover, waitForVO, voiceOverClip, delegate
		{
			Continue();
		});
	}

	public override string GetSummary()
	{
		string text = "";
		if ((Object)(object)character != (Object)null)
		{
			text = character.NameText + ": ";
		}
		if (extendPrevious)
		{
			text = "EXTEND: ";
		}
		return text + "\"" + storyText + "\"";
	}

	public Say Clone()
	{
		return MemberwiseClone() as Say;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
		executionCount = 0;
	}

	public override void OnStopExecuting()
	{
		SayDialog sayDialog = SayDialog.GetSayDialog();
		if (!((Object)(object)sayDialog == (Object)null))
		{
			sayDialog.Stop();
		}
	}

	public virtual string GetStandardText()
	{
		return storyText;
	}

	public virtual void SetStandardText(string standardText)
	{
		storyText = standardText;
	}

	public virtual string GetDescription()
	{
		return description;
	}

	public virtual string GetStringId()
	{
		string text = "SAY." + GetFlowchartLocalizationId() + "." + itemId + ".";
		if ((Object)(object)character != (Object)null)
		{
			text += character.NameText;
		}
		return text;
	}
}
