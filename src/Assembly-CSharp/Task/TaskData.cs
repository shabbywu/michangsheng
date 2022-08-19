using System;
using System.Collections.Generic;
using JSONClass;

namespace Task
{
	// Token: 0x020006E8 RID: 1768
	[Serializable]
	public class TaskData
	{
		// Token: 0x060038F7 RID: 14583 RVA: 0x00184FF8 File Offset: 0x001831F8
		public TaskData(int TaskId, int taskType, int CyId, int NpcId, int failStaticId, DateTime SendTime)
		{
			NewTaskMagData newTaskMagData = NewTaskMagData.DataDict[TaskId];
			this.CyId = CyId;
			this.SendNpcId = NpcId;
			this.TaskType = taskType;
			this.TaskId = TaskId;
			this.FailLevel = newTaskMagData.ShiBaiLevel;
			this.FailTypeList = new List<int>(newTaskMagData.ShiBaiType);
			this.FailStaticId = failStaticId;
			if (this.FailTypeList.Contains(3) || this.FailTypeList.Contains(4))
			{
				DateTime dateTime = SendTime.AddMonths(newTaskMagData.continueTime);
				DateTime dateTime2;
				if (DateTime.TryParse(newTaskMagData.EndTime, out dateTime2))
				{
					if (dateTime <= dateTime2)
					{
						this.EndTime = dateTime;
						return;
					}
					this.EndTime = dateTime2;
					return;
				}
				else
				{
					this.EndTime = dateTime;
				}
			}
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x001850B4 File Offset: 0x001832B4
		public bool IsFail(out int failType)
		{
			if (this.FailTypeList.Count < 1)
			{
				failType = 0;
				return false;
			}
			foreach (int num in this.FailTypeList)
			{
				switch (num)
				{
				case 1:
					if (NpcJieSuanManager.inst.IsDeath(this.SendNpcId))
					{
						failType = num;
						return true;
					}
					break;
				case 2:
					if (!NpcJieSuanManager.inst.IsDeath(this.SendNpcId) && NpcJieSuanManager.inst.GetNpcData(this.SendNpcId)["Level"].I >= this.FailLevel)
					{
						failType = num;
						return true;
					}
					break;
				case 3:
				case 4:
					if (NpcJieSuanManager.inst.GetNowTime() > this.EndTime)
					{
						failType = num;
						return true;
					}
					break;
				}
			}
			failType = 0;
			return false;
		}

		// Token: 0x04003101 RID: 12545
		public int TaskId;

		// Token: 0x04003102 RID: 12546
		public int TaskType;

		// Token: 0x04003103 RID: 12547
		public int CyId;

		// Token: 0x04003104 RID: 12548
		public int SendNpcId;

		// Token: 0x04003105 RID: 12549
		public int FailStaticId;

		// Token: 0x04003106 RID: 12550
		public List<int> FailTypeList;

		// Token: 0x04003107 RID: 12551
		public int FailLevel;

		// Token: 0x04003108 RID: 12552
		public DateTime EndTime;
	}
}
