using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E68 RID: 3688
	public abstract class Command : MonoBehaviour
	{
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060067BD RID: 26557 RVA: 0x0028B23E File Offset: 0x0028943E
		// (set) Token: 0x060067BE RID: 26558 RVA: 0x0028B246 File Offset: 0x00289446
		public virtual int ItemId
		{
			get
			{
				return this.itemId;
			}
			set
			{
				this.itemId = value;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060067BF RID: 26559 RVA: 0x0028B24F File Offset: 0x0028944F
		public virtual string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060067C0 RID: 26560 RVA: 0x0028B257 File Offset: 0x00289457
		// (set) Token: 0x060067C1 RID: 26561 RVA: 0x0028B25F File Offset: 0x0028945F
		public virtual int IndentLevel
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = value;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060067C2 RID: 26562 RVA: 0x0028B268 File Offset: 0x00289468
		// (set) Token: 0x060067C3 RID: 26563 RVA: 0x0028B270 File Offset: 0x00289470
		public virtual int CommandIndex { get; set; }

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060067C4 RID: 26564 RVA: 0x0028B279 File Offset: 0x00289479
		// (set) Token: 0x060067C5 RID: 26565 RVA: 0x0028B281 File Offset: 0x00289481
		public virtual bool IsExecuting { get; set; }

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060067C6 RID: 26566 RVA: 0x0028B28A File Offset: 0x0028948A
		// (set) Token: 0x060067C7 RID: 26567 RVA: 0x0028B292 File Offset: 0x00289492
		public virtual float ExecutingIconTimer { get; set; }

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060067C8 RID: 26568 RVA: 0x0028B29B File Offset: 0x0028949B
		// (set) Token: 0x060067C9 RID: 26569 RVA: 0x0028B2A3 File Offset: 0x002894A3
		public virtual Block ParentBlock { get; set; }

		// Token: 0x060067CA RID: 26570 RVA: 0x0028B2AC File Offset: 0x002894AC
		public virtual Flowchart GetFlowchart()
		{
			Flowchart component = base.GetComponent<Flowchart>();
			if (component == null && base.transform.parent != null)
			{
				component = base.transform.parent.GetComponent<Flowchart>();
			}
			return component;
		}

		// Token: 0x060067CB RID: 26571 RVA: 0x0028B2F0 File Offset: 0x002894F0
		public virtual void Execute()
		{
			try
			{
				Tools.instance.getPlayer().StreamData.FungusSaveMgr.SetCommand(this);
				this.OnEnter();
			}
			catch (Exception ex)
			{
				string parentName = this.GetFlowchart().GetParentName();
				Debug.LogError(string.Format("Fungus出现异常，所在Flowchart {0}，所在Block {1}，CommandIndex {2}，异常信息:\n{3}\n{4}", new object[]
				{
					parentName,
					this.ParentBlock.BlockName,
					this.CommandIndex,
					ex.Message,
					ex.StackTrace
				}));
				this.Continue();
			}
		}

		// Token: 0x060067CC RID: 26572 RVA: 0x0028B38C File Offset: 0x0028958C
		public virtual void Continue()
		{
			if (this.IsExecuting)
			{
				this.Continue(this.CommandIndex + 1);
			}
		}

		// Token: 0x060067CD RID: 26573 RVA: 0x0028B3A4 File Offset: 0x002895A4
		public virtual void Continue(int nextCommandIndex)
		{
			this.OnExit();
			if (this.ParentBlock != null)
			{
				this.ParentBlock.JumpToCommandIndex = nextCommandIndex;
			}
			Tools.instance.getPlayer().StreamData.FungusSaveMgr.ClearCommand();
		}

		// Token: 0x060067CE RID: 26574 RVA: 0x0028B3DF File Offset: 0x002895DF
		public virtual void StopParentBlock()
		{
			this.OnExit();
			if (this.ParentBlock != null)
			{
				this.ParentBlock.Stop();
			}
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnStopExecuting()
		{
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnCommandAdded(Block parentBlock)
		{
		}

		// Token: 0x060067D1 RID: 26577 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnCommandRemoved(Block parentBlock)
		{
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnEnter()
		{
		}

		// Token: 0x060067D3 RID: 26579 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnExit()
		{
		}

		// Token: 0x060067D4 RID: 26580 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnReset()
		{
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void GetConnectedBlocks(ref List<Block> connectedBlocks)
		{
		}

		// Token: 0x060067D6 RID: 26582 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual bool HasReference(Variable variable)
		{
			return false;
		}

		// Token: 0x060067D7 RID: 26583 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnValidate()
		{
		}

		// Token: 0x060067D8 RID: 26584 RVA: 0x001D84A0 File Offset: 0x001D66A0
		public virtual string GetSummary()
		{
			return "";
		}

		// Token: 0x060067D9 RID: 26585 RVA: 0x001D84A0 File Offset: 0x001D66A0
		public virtual string GetHelpText()
		{
			return "";
		}

		// Token: 0x060067DA RID: 26586 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual bool OpenBlock()
		{
			return false;
		}

		// Token: 0x060067DB RID: 26587 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual bool CloseBlock()
		{
			return false;
		}

		// Token: 0x060067DC RID: 26588 RVA: 0x0028B400 File Offset: 0x00289600
		public virtual Color GetButtonColor()
		{
			return Color.white;
		}

		// Token: 0x060067DD RID: 26589 RVA: 0x00024C5F File Offset: 0x00022E5F
		public virtual bool IsPropertyVisible(string propertyName)
		{
			return true;
		}

		// Token: 0x060067DE RID: 26590 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual bool IsReorderableArray(string propertyName)
		{
			return false;
		}

		// Token: 0x060067DF RID: 26591 RVA: 0x0028B408 File Offset: 0x00289608
		public virtual string GetFlowchartLocalizationId()
		{
			Flowchart flowchart = this.GetFlowchart();
			if (flowchart == null)
			{
				return "";
			}
			string text = this.GetFlowchart().LocalizationId;
			if (text.Length == 0)
			{
				text = flowchart.GetName();
			}
			return text;
		}

		// Token: 0x060067E0 RID: 26592 RVA: 0x0028B448 File Offset: 0x00289648
		public string GetCommandSourceDesc()
		{
			string result;
			try
			{
				result = string.Concat(new string[]
				{
					base.GetType().Name,
					"指令, flowchart:",
					this.GetFlowchart().GetParentName(),
					", block:",
					this.ParentBlock.BlockName
				});
			}
			catch
			{
				result = base.GetType().Name + "指令, unknow";
			}
			return result;
		}

		// Token: 0x0400589B RID: 22683
		[FormerlySerializedAs("commandId")]
		[HideInInspector]
		[SerializeField]
		protected int itemId = -1;

		// Token: 0x0400589C RID: 22684
		[HideInInspector]
		[SerializeField]
		protected int indentLevel;

		// Token: 0x0400589D RID: 22685
		protected string errorMessage = "";
	}
}
