using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

public abstract class Command : MonoBehaviour
{
	[FormerlySerializedAs("commandId")]
	[HideInInspector]
	[SerializeField]
	protected int itemId = -1;

	[HideInInspector]
	[SerializeField]
	protected int indentLevel;

	protected string errorMessage = "";

	public virtual int ItemId
	{
		get
		{
			return itemId;
		}
		set
		{
			itemId = value;
		}
	}

	public virtual string ErrorMessage => errorMessage;

	public virtual int IndentLevel
	{
		get
		{
			return indentLevel;
		}
		set
		{
			indentLevel = value;
		}
	}

	public virtual int CommandIndex { get; set; }

	public virtual bool IsExecuting { get; set; }

	public virtual float ExecutingIconTimer { get; set; }

	public virtual Block ParentBlock { get; set; }

	public virtual Flowchart GetFlowchart()
	{
		Flowchart component = ((Component)this).GetComponent<Flowchart>();
		if ((Object)(object)component == (Object)null && (Object)(object)((Component)this).transform.parent != (Object)null)
		{
			component = ((Component)((Component)this).transform.parent).GetComponent<Flowchart>();
		}
		return component;
	}

	public virtual void Execute()
	{
		try
		{
			Tools.instance.getPlayer().StreamData.FungusSaveMgr.SetCommand(this);
			OnEnter();
		}
		catch (Exception ex)
		{
			string parentName = GetFlowchart().GetParentName();
			Debug.LogError((object)$"Fungus出现异常，所在Flowchart {parentName}，所在Block {ParentBlock.BlockName}，CommandIndex {CommandIndex}，异常信息:\n{ex.Message}\n{ex.StackTrace}");
			Continue();
		}
	}

	public virtual void Continue()
	{
		if (IsExecuting)
		{
			Continue(CommandIndex + 1);
		}
	}

	public virtual void Continue(int nextCommandIndex)
	{
		OnExit();
		if ((Object)(object)ParentBlock != (Object)null)
		{
			ParentBlock.JumpToCommandIndex = nextCommandIndex;
		}
		Tools.instance.getPlayer().StreamData.FungusSaveMgr.ClearCommand();
	}

	public virtual void StopParentBlock()
	{
		OnExit();
		if ((Object)(object)ParentBlock != (Object)null)
		{
			ParentBlock.Stop();
		}
	}

	public virtual void OnStopExecuting()
	{
	}

	public virtual void OnCommandAdded(Block parentBlock)
	{
	}

	public virtual void OnCommandRemoved(Block parentBlock)
	{
	}

	public virtual void OnEnter()
	{
	}

	public virtual void OnExit()
	{
	}

	public virtual void OnReset()
	{
	}

	public virtual void GetConnectedBlocks(ref List<Block> connectedBlocks)
	{
	}

	public virtual bool HasReference(Variable variable)
	{
		return false;
	}

	public virtual void OnValidate()
	{
	}

	public virtual string GetSummary()
	{
		return "";
	}

	public virtual string GetHelpText()
	{
		return "";
	}

	public virtual bool OpenBlock()
	{
		return false;
	}

	public virtual bool CloseBlock()
	{
		return false;
	}

	public virtual Color GetButtonColor()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.white;
	}

	public virtual bool IsPropertyVisible(string propertyName)
	{
		return true;
	}

	public virtual bool IsReorderableArray(string propertyName)
	{
		return false;
	}

	public virtual string GetFlowchartLocalizationId()
	{
		Flowchart flowchart = GetFlowchart();
		if ((Object)(object)flowchart == (Object)null)
		{
			return "";
		}
		string text = GetFlowchart().LocalizationId;
		if (text.Length == 0)
		{
			text = flowchart.GetName();
		}
		return text;
	}

	public string GetCommandSourceDesc()
	{
		try
		{
			return ((object)this).GetType().Name + "指令, flowchart:" + GetFlowchart().GetParentName() + ", block:" + ParentBlock.BlockName;
		}
		catch
		{
			return ((object)this).GetType().Name + "指令, unknow";
		}
	}
}
