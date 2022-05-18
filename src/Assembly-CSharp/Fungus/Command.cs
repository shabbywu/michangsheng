using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x020012C5 RID: 4805
	public abstract class Command : MonoBehaviour
	{
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x0600746F RID: 29807 RVA: 0x0004F7F0 File Offset: 0x0004D9F0
		// (set) Token: 0x06007470 RID: 29808 RVA: 0x0004F7F8 File Offset: 0x0004D9F8
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

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06007471 RID: 29809 RVA: 0x0004F801 File Offset: 0x0004DA01
		public virtual string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06007472 RID: 29810 RVA: 0x0004F809 File Offset: 0x0004DA09
		// (set) Token: 0x06007473 RID: 29811 RVA: 0x0004F811 File Offset: 0x0004DA11
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

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06007474 RID: 29812 RVA: 0x0004F81A File Offset: 0x0004DA1A
		// (set) Token: 0x06007475 RID: 29813 RVA: 0x0004F822 File Offset: 0x0004DA22
		public virtual int CommandIndex { get; set; }

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06007476 RID: 29814 RVA: 0x0004F82B File Offset: 0x0004DA2B
		// (set) Token: 0x06007477 RID: 29815 RVA: 0x0004F833 File Offset: 0x0004DA33
		public virtual bool IsExecuting { get; set; }

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06007478 RID: 29816 RVA: 0x0004F83C File Offset: 0x0004DA3C
		// (set) Token: 0x06007479 RID: 29817 RVA: 0x0004F844 File Offset: 0x0004DA44
		public virtual float ExecutingIconTimer { get; set; }

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600747A RID: 29818 RVA: 0x0004F84D File Offset: 0x0004DA4D
		// (set) Token: 0x0600747B RID: 29819 RVA: 0x0004F855 File Offset: 0x0004DA55
		public virtual Block ParentBlock { get; set; }

		// Token: 0x0600747C RID: 29820 RVA: 0x002ADF60 File Offset: 0x002AC160
		public virtual Flowchart GetFlowchart()
		{
			Flowchart component = base.GetComponent<Flowchart>();
			if (component == null && base.transform.parent != null)
			{
				component = base.transform.parent.GetComponent<Flowchart>();
			}
			return component;
		}

		// Token: 0x0600747D RID: 29821 RVA: 0x002ADFA4 File Offset: 0x002AC1A4
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

		// Token: 0x0600747E RID: 29822 RVA: 0x0004F85E File Offset: 0x0004DA5E
		public virtual void Continue()
		{
			if (this.IsExecuting)
			{
				this.Continue(this.CommandIndex + 1);
			}
		}

		// Token: 0x0600747F RID: 29823 RVA: 0x0004F876 File Offset: 0x0004DA76
		public virtual void Continue(int nextCommandIndex)
		{
			this.OnExit();
			if (this.ParentBlock != null)
			{
				this.ParentBlock.JumpToCommandIndex = nextCommandIndex;
			}
			Tools.instance.getPlayer().StreamData.FungusSaveMgr.ClearCommand();
		}

		// Token: 0x06007480 RID: 29824 RVA: 0x0004F8B1 File Offset: 0x0004DAB1
		public virtual void StopParentBlock()
		{
			this.OnExit();
			if (this.ParentBlock != null)
			{
				this.ParentBlock.Stop();
			}
		}

		// Token: 0x06007481 RID: 29825 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnStopExecuting()
		{
		}

		// Token: 0x06007482 RID: 29826 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnCommandAdded(Block parentBlock)
		{
		}

		// Token: 0x06007483 RID: 29827 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnCommandRemoved(Block parentBlock)
		{
		}

		// Token: 0x06007484 RID: 29828 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnEnter()
		{
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnExit()
		{
		}

		// Token: 0x06007486 RID: 29830 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnReset()
		{
		}

		// Token: 0x06007487 RID: 29831 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void GetConnectedBlocks(ref List<Block> connectedBlocks)
		{
		}

		// Token: 0x06007488 RID: 29832 RVA: 0x00004050 File Offset: 0x00002250
		public virtual bool HasReference(Variable variable)
		{
			return false;
		}

		// Token: 0x06007489 RID: 29833 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnValidate()
		{
		}

		// Token: 0x0600748A RID: 29834 RVA: 0x00032110 File Offset: 0x00030310
		public virtual string GetSummary()
		{
			return "";
		}

		// Token: 0x0600748B RID: 29835 RVA: 0x00032110 File Offset: 0x00030310
		public virtual string GetHelpText()
		{
			return "";
		}

		// Token: 0x0600748C RID: 29836 RVA: 0x00004050 File Offset: 0x00002250
		public virtual bool OpenBlock()
		{
			return false;
		}

		// Token: 0x0600748D RID: 29837 RVA: 0x00004050 File Offset: 0x00002250
		public virtual bool CloseBlock()
		{
			return false;
		}

		// Token: 0x0600748E RID: 29838 RVA: 0x0004F8D2 File Offset: 0x0004DAD2
		public virtual Color GetButtonColor()
		{
			return Color.white;
		}

		// Token: 0x0600748F RID: 29839 RVA: 0x0000A093 File Offset: 0x00008293
		public virtual bool IsPropertyVisible(string propertyName)
		{
			return true;
		}

		// Token: 0x06007490 RID: 29840 RVA: 0x00004050 File Offset: 0x00002250
		public virtual bool IsReorderableArray(string propertyName)
		{
			return false;
		}

		// Token: 0x06007491 RID: 29841 RVA: 0x002AE040 File Offset: 0x002AC240
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

		// Token: 0x06007492 RID: 29842 RVA: 0x002AE080 File Offset: 0x002AC280
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

		// Token: 0x04006633 RID: 26163
		[FormerlySerializedAs("commandId")]
		[HideInInspector]
		[SerializeField]
		protected int itemId = -1;

		// Token: 0x04006634 RID: 26164
		[HideInInspector]
		[SerializeField]
		protected int indentLevel;

		// Token: 0x04006635 RID: 26165
		protected string errorMessage = "";
	}
}
