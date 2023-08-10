using System;
using System.Collections.Generic;

[Serializable]
public class RandomTask
{
	public int TaskId;

	public int LockActionId;

	public int TaskValue;

	public int TaskType;

	public List<int> StaticId;

	public List<int> StaticValue;

	public int CyId;

	public RandomTask(int cyId, int taskId, int taskType, int taskValue, int lockActionId, List<int> staticId, List<int> staticValue)
	{
		TaskId = taskId;
		TaskValue = taskValue;
		TaskType = taskType;
		CyId = cyId;
		LockActionId = lockActionId;
		StaticId = staticId ?? new List<int>();
		StaticValue = staticValue ?? new List<int>();
	}
}
