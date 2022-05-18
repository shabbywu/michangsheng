using System;
using System.Collections.Generic;

// Token: 0x020003DA RID: 986
[Serializable]
public class RandomTask
{
	// Token: 0x06001AFB RID: 6907 RVA: 0x000EED00 File Offset: 0x000ECF00
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

	// Token: 0x0400169C RID: 5788
	public int TaskId;

	// Token: 0x0400169D RID: 5789
	public int LockActionId;

	// Token: 0x0400169E RID: 5790
	public int TaskValue;

	// Token: 0x0400169F RID: 5791
	public int TaskType;

	// Token: 0x040016A0 RID: 5792
	public List<int> StaticId;

	// Token: 0x040016A1 RID: 5793
	public List<int> StaticValue;

	// Token: 0x040016A2 RID: 5794
	public int CyId;
}
