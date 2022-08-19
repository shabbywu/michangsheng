using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E03 RID: 3587
	[CommandInfo("Narrative", "Menu", "Displays a button in a multiple choice menu", 0)]
	[AddComponentMenu("")]
	public class Menu : Command, ILocalizable
	{
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06006551 RID: 25937 RVA: 0x00282B83 File Offset: 0x00280D83
		// (set) Token: 0x06006552 RID: 25938 RVA: 0x00282B8B File Offset: 0x00280D8B
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

		// Token: 0x06006553 RID: 25939 RVA: 0x00282B94 File Offset: 0x00280D94
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

		// Token: 0x06006554 RID: 25940 RVA: 0x00282C72 File Offset: 0x00280E72
		public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
		{
			if (this.targetBlock != null)
			{
				connectedBlocks.Add(this.targetBlock);
			}
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x00282C90 File Offset: 0x00280E90
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

		// Token: 0x06006556 RID: 25942 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x00282CE4 File Offset: 0x00280EE4
		public override bool HasReference(Variable variable)
		{
			return this.interactable.booleanRef == variable || this.hideThisOption.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x00282D15 File Offset: 0x00280F15
		public virtual string GetStandardText()
		{
			return this.text;
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x00282D1D File Offset: 0x00280F1D
		public virtual void SetStandardText(string standardText)
		{
			this.text = standardText;
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x00282D26 File Offset: 0x00280F26
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x00282D2E File Offset: 0x00280F2E
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

		// Token: 0x04005711 RID: 22289
		[Tooltip("Text to display on the menu button")]
		[TextArea]
		[SerializeField]
		protected string text = "Option Text";

		// Token: 0x04005712 RID: 22290
		[Tooltip("(高优先级)用变量显示文本")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable VarText;

		// Token: 0x04005713 RID: 22291
		[Tooltip("Notes about the option text for other authors, localization, etc.")]
		[SerializeField]
		protected string description = "";

		// Token: 0x04005714 RID: 22292
		[FormerlySerializedAs("targetSequence")]
		[Tooltip("Block to execute when this option is selected")]
		[SerializeField]
		protected Block targetBlock;

		// Token: 0x04005715 RID: 22293
		[Tooltip("Hide this option if the target block has been executed previously")]
		[SerializeField]
		protected bool hideIfVisited;

		// Token: 0x04005716 RID: 22294
		[Tooltip("If false, the menu option will be displayed but will not be selectable")]
		[SerializeField]
		protected BooleanData interactable = new BooleanData(true);

		// Token: 0x04005717 RID: 22295
		[Tooltip("A custom Menu Dialog to use to display this menu. All subsequent Menu commands will use this dialog.")]
		[SerializeField]
		protected MenuDialog setMenuDialog;

		// Token: 0x04005718 RID: 22296
		[Tooltip("If true, this option will be passed to the Menu Dialogue but marked as hidden, this can be used to hide options while maintaining a Menu Shuffle.")]
		[SerializeField]
		protected BooleanData hideThisOption = new BooleanData(false);

		// Token: 0x04005719 RID: 22297
		[SerializeField]
		protected List<MenuSetVar> SetVarList = new List<MenuSetVar>();
	}
}
