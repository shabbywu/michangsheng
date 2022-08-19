using System;
using System.Collections.Generic;

// Token: 0x020002A1 RID: 673
[Serializable]
public class RandomTask
{
	// Token: 0x06001809 RID: 6153 RVA: 0x000A7AC4 File Offset: 0x000A5CC4
	public RandomTask(int cyId, int taskId, int taskType, int taskValue, int lockActionId, List<int> staticId, List<int> staticValue)
	{
		this.TaskId = taskId;
		this.TaskValue = taskValue;
		this.TaskType = taskType;
		this.CyId = cyId;
		this.LockActionId = lockActionId;
		this.StaticId = (staticId ?? new List<int>());
		this.StaticValue = (staticValue ?? new List<int>());
	}

	// Token: 0x04001300 RID: 4864
	public int TaskId;

	// Token: 0x04001301 RID: 4865
	public int LockActionId;

	// Token: 0x04001302 RID: 4866
	public int TaskValue;

	// Token: 0x04001303 RID: 4867
	public int TaskType;

	// Token: 0x04001304 RID: 4868
	public List<int> StaticId;

	// Token: 0x04001305 RID: 4869
	public List<int> StaticValue;

	// Token: 0x04001306 RID: 4870
	public int CyId;
}
