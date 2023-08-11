using System;

namespace MoonSharp.Interpreter.Debugging;

public class DebuggerAction
{
	public enum ActionType
	{
		ByteCodeStepIn,
		ByteCodeStepOver,
		ByteCodeStepOut,
		StepIn,
		StepOver,
		StepOut,
		Run,
		ToggleBreakpoint,
		SetBreakpoint,
		ClearBreakpoint,
		ResetBreakpoints,
		Refresh,
		HardRefresh,
		None
	}

	public ActionType Action { get; set; }

	public DateTime TimeStampUTC { get; private set; }

	public int SourceID { get; set; }

	public int SourceLine { get; set; }

	public int SourceCol { get; set; }

	public int[] Lines { get; set; }

	public TimeSpan Age => DateTime.UtcNow - TimeStampUTC;

	public DebuggerAction()
	{
		TimeStampUTC = DateTime.UtcNow;
	}

	public override string ToString()
	{
		if (Action == ActionType.ToggleBreakpoint || Action == ActionType.SetBreakpoint || Action == ActionType.ClearBreakpoint)
		{
			return $"{Action} {SourceID}:({SourceLine},{SourceCol})";
		}
		return Action.ToString();
	}
}
