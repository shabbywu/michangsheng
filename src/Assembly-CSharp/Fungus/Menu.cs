using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Narrative", "Menu", "Displays a button in a multiple choice menu", 0)]
[AddComponentMenu("")]
public class Menu : Command, ILocalizable
{
	[Tooltip("Text to display on the menu button")]
	[TextArea]
	[SerializeField]
	protected string text = "Option Text";

	[Tooltip("(高优先级)用变量显示文本")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable VarText;

	[Tooltip("Notes about the option text for other authors, localization, etc.")]
	[SerializeField]
	protected string description = "";

	[FormerlySerializedAs("targetSequence")]
	[Tooltip("Block to execute when this option is selected")]
	[SerializeField]
	protected Block targetBlock;

	[Tooltip("Hide this option if the target block has been executed previously")]
	[SerializeField]
	protected bool hideIfVisited;

	[Tooltip("If false, the menu option will be displayed but will not be selectable")]
	[SerializeField]
	protected BooleanData interactable = new BooleanData(v: true);

	[Tooltip("A custom Menu Dialog to use to display this menu. All subsequent Menu commands will use this dialog.")]
	[SerializeField]
	protected MenuDialog setMenuDialog;

	[Tooltip("If true, this option will be passed to the Menu Dialogue but marked as hidden, this can be used to hide options while maintaining a Menu Shuffle.")]
	[SerializeField]
	protected BooleanData hideThisOption = new BooleanData(v: false);

	[SerializeField]
	protected List<MenuSetVar> SetVarList = new List<MenuSetVar>();

	public MenuDialog SetMenuDialog
	{
		get
		{
			return setMenuDialog;
		}
		set
		{
			setMenuDialog = value;
		}
	}

	public override void OnEnter()
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		if ((Object)(object)setMenuDialog != (Object)null)
		{
			MenuDialog.ActiveMenuDialog = setMenuDialog;
		}
		bool hideOption = (hideIfVisited && (Object)(object)targetBlock != (Object)null && targetBlock.GetExecutionCount() > 0) || hideThisOption.Value;
		MenuDialog menuDialog = MenuDialog.GetMenuDialog();
		if ((Object)(object)menuDialog != (Object)null)
		{
			menuDialog.SetActive(state: true);
			string text = GetFlowchart().SubstituteVariables(this.text);
			if ((Object)(object)VarText != (Object)null)
			{
				text = VarText.Value;
			}
			menuDialog.AddOption(text, interactable, hideOption, targetBlock);
			((UnityEvent)menuDialog.CachedButtons[menuDialog.NowOption].onClick).AddListener((UnityAction)delegate
			{
				Tools.instance.getPlayer().StreamData.FungusSaveMgr.ClearMenu();
				foreach (MenuSetVar setVar in SetVarList)
				{
					setVar.setValue();
				}
			});
		}
		Continue();
	}

	public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
	{
		if ((Object)(object)targetBlock != (Object)null)
		{
			connectedBlocks.Add(targetBlock);
		}
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetBlock == (Object)null)
		{
			return "Error: No target block selected";
		}
		if (text == "")
		{
			return "Error: No button text selected";
		}
		return text + " : " + targetBlock.BlockName;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)interactable.booleanRef == (Object)(object)variable) && !((Object)(object)hideThisOption.booleanRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	public virtual string GetStandardText()
	{
		return text;
	}

	public virtual void SetStandardText(string standardText)
	{
		text = standardText;
	}

	public virtual string GetDescription()
	{
		return description;
	}

	public virtual string GetStringId()
	{
		return "MENU." + GetFlowchartLocalizationId() + "." + itemId;
	}
}
