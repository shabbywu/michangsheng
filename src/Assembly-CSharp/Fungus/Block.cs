using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[ExecuteInEditMode]
[RequireComponent(typeof(Flowchart))]
[AddComponentMenu("")]
public class Block : Node
{
	[SerializeField]
	protected int itemId = -1;

	[FormerlySerializedAs("sequenceName")]
	[Tooltip("The name of the block node as displayed in the Flowchart window")]
	[SerializeField]
	public string blockName = "New Block";

	[TextArea(2, 5)]
	[Tooltip("Description text to display under the block node")]
	[SerializeField]
	protected string description = "";

	[Tooltip("An optional Event Handler which can execute the block when an event occurs")]
	[SerializeField]
	protected EventHandler eventHandler;

	[SerializeField]
	public List<Command> commandList = new List<Command>();

	protected ExecutionState executionState;

	protected Command activeCommand;

	protected Action lastOnCompleteAction;

	protected int previousActiveCommandIndex = -1;

	protected int jumpToCommandIndex = -1;

	protected int executionCount;

	protected bool executionInfoSet;

	public bool IsSelected { get; set; }

	public bool IsFiltered { get; set; }

	public bool IsControlSelected { get; set; }

	public virtual ExecutionState State => executionState;

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

	public virtual string BlockName
	{
		get
		{
			return blockName;
		}
		set
		{
			blockName = value;
		}
	}

	public virtual string Description => description;

	public virtual EventHandler _EventHandler
	{
		get
		{
			return eventHandler;
		}
		set
		{
			eventHandler = value;
		}
	}

	public virtual Command ActiveCommand => activeCommand;

	public virtual float ExecutingIconTimer { get; set; }

	public virtual List<Command> CommandList => commandList;

	public virtual int JumpToCommandIndex
	{
		set
		{
			jumpToCommandIndex = value;
		}
	}

	protected virtual void Awake()
	{
		SetExecutionInfo();
	}

	protected virtual void SetExecutionInfo()
	{
		int num = 0;
		for (int i = 0; i < commandList.Count; i++)
		{
			Command command = commandList[i];
			if (!((Object)(object)command == (Object)null))
			{
				command.ParentBlock = this;
				command.CommandIndex = num++;
			}
		}
		UpdateIndentLevels();
		executionInfoSet = true;
	}

	public virtual Flowchart GetFlowchart()
	{
		return ((Component)this).GetComponent<Flowchart>();
	}

	public virtual bool IsExecuting()
	{
		return executionState == ExecutionState.Executing;
	}

	public virtual int GetExecutionCount()
	{
		return executionCount;
	}

	public virtual void StartExecution()
	{
		((MonoBehaviour)this).StartCoroutine(Execute());
	}

	public virtual IEnumerator Execute(int commandIndex = 0, Action onComplete = null)
	{
		new List<Command>();
		if (executionState != 0)
		{
			Debug.LogWarning((object)(BlockName + " cannot be executed, it is already running."));
			yield break;
		}
		lastOnCompleteAction = onComplete;
		if (!executionInfoSet)
		{
			SetExecutionInfo();
		}
		executionCount++;
		int executionCountAtStart = executionCount;
		Flowchart flowchart = GetFlowchart();
		executionState = ExecutionState.Executing;
		BlockSignals.DoBlockStart(this);
		jumpToCommandIndex = commandIndex;
		int i = 0;
		while (true)
		{
			if (jumpToCommandIndex > -1)
			{
				i = jumpToCommandIndex;
				jumpToCommandIndex = -1;
			}
			try
			{
				while (i < commandList.Count && (!((Behaviour)commandList[i]).enabled || ((object)commandList[i]).GetType() == typeof(Comment) || ((object)commandList[i]).GetType() == typeof(Label)))
				{
					i = commandList[i].CommandIndex + 1;
					_ = i + 1;
					_ = commandList.Count;
				}
			}
			catch (Exception ex)
			{
				Debug.Log((object)ex);
			}
			if (i >= commandList.Count)
			{
				break;
			}
			if ((Object)(object)activeCommand == (Object)null)
			{
				previousActiveCommandIndex = -1;
			}
			else
			{
				previousActiveCommandIndex = activeCommand.CommandIndex;
			}
			Command command = commandList[i];
			if ((Object)(object)command == (Object)null)
			{
				string text = $"当前block含有{commandList.Count}个command，当前处于{i}";
				if ((Object)(object)((Component)flowchart).gameObject.transform.parent != (Object)null)
				{
					Debug.LogError((object)("fungus遇到空command，已断开循环，当前场景" + SceneEx.NowSceneName + "，Flowchart:" + ((Object)((Component)flowchart).gameObject.transform.parent).name + "，Block:" + BlockName + "\n" + text));
				}
				else
				{
					Debug.LogError((object)("fungus遇到空command，已断开循环，当前场景" + SceneEx.NowSceneName + "，Flowchart:" + flowchart.GetName() + "，Block:" + BlockName + "\n" + text));
				}
				break;
			}
			activeCommand = command;
			if (flowchart.IsActive() && ((flowchart.SelectedCommands.Count == 0 && i == 0) || (flowchart.SelectedCommands.Count == 1 && flowchart.SelectedCommands[0].CommandIndex == previousActiveCommandIndex)))
			{
				flowchart.ClearSelectedCommands();
				flowchart.AddSelectedCommand(commandList[i]);
			}
			command.IsExecuting = true;
			command.ExecutingIconTimer = Time.realtimeSinceStartup + 0.5f;
			BlockSignals.DoCommandExecute(this, command, i, commandList.Count);
			command.Execute();
			while (jumpToCommandIndex == -1)
			{
				yield return null;
			}
			command.IsExecuting = false;
		}
		if (State == ExecutionState.Executing && executionCountAtStart == executionCount)
		{
			ReturnToIdle();
		}
	}

	private void ReturnToIdle()
	{
		executionState = ExecutionState.Idle;
		activeCommand = null;
		BlockSignals.DoBlockEnd(this);
		if (lastOnCompleteAction != null)
		{
			lastOnCompleteAction();
		}
		lastOnCompleteAction = null;
	}

	public virtual void Stop()
	{
		if ((Object)(object)activeCommand != (Object)null)
		{
			activeCommand.IsExecuting = false;
			activeCommand.OnStopExecuting();
		}
		jumpToCommandIndex = int.MaxValue;
		ReturnToIdle();
	}

	public virtual List<Block> GetConnectedBlocks()
	{
		List<Block> connectedBlocks = new List<Block>();
		for (int i = 0; i < commandList.Count; i++)
		{
			Command command = commandList[i];
			if ((Object)(object)command != (Object)null)
			{
				command.GetConnectedBlocks(ref connectedBlocks);
			}
		}
		return connectedBlocks;
	}

	public virtual Type GetPreviousActiveCommandType()
	{
		if (previousActiveCommandIndex >= 0 && previousActiveCommandIndex < commandList.Count)
		{
			return ((object)commandList[previousActiveCommandIndex]).GetType();
		}
		return null;
	}

	public virtual int GetPreviousActiveCommandIndent()
	{
		if (previousActiveCommandIndex >= 0 && previousActiveCommandIndex < commandList.Count)
		{
			return commandList[previousActiveCommandIndex].IndentLevel;
		}
		return -1;
	}

	public virtual void UpdateIndentLevels()
	{
		int num = 0;
		for (int i = 0; i < commandList.Count; i++)
		{
			Command command = commandList[i];
			if (!((Object)(object)command == (Object)null))
			{
				if (command.CloseBlock())
				{
					num--;
				}
				num = (command.IndentLevel = Math.Max(num, 0));
				if (command.OpenBlock())
				{
					num++;
				}
			}
		}
	}

	public virtual int GetLabelIndex(string labelKey)
	{
		if (labelKey.Length == 0)
		{
			return -1;
		}
		for (int i = 0; i < commandList.Count; i++)
		{
			Label label = commandList[i] as Label;
			if ((Object)(object)label != (Object)null && string.Compare(label.Key, labelKey, ignoreCase: true) == 0)
			{
				return i;
			}
		}
		return -1;
	}
}
