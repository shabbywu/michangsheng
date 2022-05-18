using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200124F RID: 4687
	[CommandInfo("Narrative", "Menu", "Displays a button in a multiple choice menu", 0)]
	[AddComponentMenu("")]
	public class Menu : Command, ILocalizable
	{
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060071DF RID: 29151 RVA: 0x0004D6D2 File Offset: 0x0004B8D2
		// (set) Token: 0x060071E0 RID: 29152 RVA: 0x0004D6DA File Offset: 0x0004B8DA
		public MenuDialog SetMenuDialog
		{
			get
			{
				return this.setMenuDialog;
			}
			set
			{
				this.setMenuDialog = value;
			}
		}

		// Token: 0x060071E1 RID: 29153 RVA: 0x002A6FCC File Offset: 0x002A51CC
		public override void OnEnter()
		{
			if (this.setMenuDialog != null)
			{
				MenuDialog.ActiveMenuDialog = this.setMenuDialog;
			}
			bool hideOption = (this.hideIfVisited && this.targetBlock != null && this.targetBlock.GetExecutionCount() > 0) || this.hideThisOption.Value;
			MenuDialog menuDialog = MenuDialog.GetMenuDialog();
			if (menuDialog != null)
			{
				menuDialog.SetActive(true);
				string text = this.GetFlowchart().SubstituteVariables(this.text);
				if (this.VarText != null)
				{
					text = this.VarText.Value;
				}
				menuDialog.AddOption(text, this.interactable, hideOption, this.targetBlock);
				menuDialog.CachedButtons[menuDialog.NowOption].onClick.AddListener(delegate()
				{
					Tools.instance.getPlayer().StreamData.FungusSaveMgr.ClearMenu();
					foreach (MenuSetVar menuSetVar in this.SetVarList)
					{
						menuSetVar.setValue();
					}
				});
			}
			this.Continue();
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x0004D6E3 File Offset: 0x0004B8E3
		public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
		{
			if (this.targetBlock != null)
			{
				connectedBlocks.Add(this.targetBlock);
			}
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x002A70AC File Offset: 0x002A52AC
		public override string GetSummary()
		{
			if (this.targetBlock == null)
			{
				return "Error: No target block selected";
			}
			if (this.text == "")
			{
				return "Error: No button text selected";
			}
			return this.text + " : " + this.targetBlock.BlockName;
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x060071E5 RID: 29157 RVA: 0x0004D700 File Offset: 0x0004B900
		public override bool HasReference(Variable variable)
		{
			return this.interactable.booleanRef == variable || this.hideThisOption.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x060071E6 RID: 29158 RVA: 0x0004D731 File Offset: 0x0004B931
		public virtual string GetStandardText()
		{
			return this.text;
		}

		// Token: 0x060071E7 RID: 29159 RVA: 0x0004D739 File Offset: 0x0004B939
		public virtual void SetStandardText(string standardText)
		{
			this.text = standardText;
		}

		// Token: 0x060071E8 RID: 29160 RVA: 0x0004D742 File Offset: 0x0004B942
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x060071E9 RID: 29161 RVA: 0x0004D74A File Offset: 0x0004B94A
		public virtual string GetStringId()
		{
			return string.Concat(new object[]
			{
				"MENU.",
				this.GetFlowchartLocalizationId(),
				".",
				this.itemId
			});
		}

		// Token: 0x04006443 RID: 25667
		[Tooltip("Text to display on the menu button")]
		[TextArea]
		[SerializeField]
		protected string text = "Option Text";

		// Token: 0x04006444 RID: 25668
		[Tooltip("(高优先级)用变量显示文本")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable VarText;

		// Token: 0x04006445 RID: 25669
		[Tooltip("Notes about the option text for other authors, localization, etc.")]
		[SerializeField]
		protected string description = "";

		// Token: 0x04006446 RID: 25670
		[FormerlySerializedAs("targetSequence")]
		[Tooltip("Block to execute when this option is selected")]
		[SerializeField]
		protected Block targetBlock;

		// Token: 0x04006447 RID: 25671
		[Tooltip("Hide this option if the target block has been executed previously")]
		[SerializeField]
		protected bool hideIfVisited;

		// Token: 0x04006448 RID: 25672
		[Tooltip("If false, the menu option will be displayed but will not be selectable")]
		[SerializeField]
		protected BooleanData interactable = new BooleanData(true);

		// Token: 0x04006449 RID: 25673
		[Tooltip("A custom Menu Dialog to use to display this menu. All subsequent Menu commands will use this dialog.")]
		[SerializeField]
		protected MenuDialog setMenuDialog;

		// Token: 0x0400644A RID: 25674
		[Tooltip("If true, this option will be passed to the Menu Dialogue but marked as hidden, this can be used to hide options while maintaining a Menu Shuffle.")]
		[SerializeField]
		protected BooleanData hideThisOption = new BooleanData(false);

		// Token: 0x0400644B RID: 25675
		[SerializeField]
		protected List<MenuSetVar> SetVarList = new List<MenuSetVar>();
	}
}
