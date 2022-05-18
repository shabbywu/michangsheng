using System;
using System.Collections.Generic;
using JSONClass;

namespace Task
{
	// Token: 0x02000A27 RID: 2599
	[Serializable]
	public class TaskData
	{
		// Token: 0x0600435E RID: 17246 RVA: 0x001CCACC File Offset: 0x001CACCC
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

		// Token: 0x0600435F RID: 17247 RVA: 0x001CCB88 File Offset: 0x001CAD88
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
					if (!NpcJieSuanManager.inst.IsDeath(this.SendNpcId) && NpcJieSuanManager.inst.getNpcData(this.SendNpcId)["Level"].I >= this.FailLevel)
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

		// Token: 0x04003B60 RID: 15200
		public int TaskId;

		// Token: 0x04003B61 RID: 15201
		public int TaskType;

		// Token: 0x04003B62 RID: 15202
		public int CyId;

		// Token: 0x04003B63 RID: 15203
		public int SendNpcId;

		// Token: 0x04003B64 RID: 15204
		public int FailStaticId;

		// Token: 0x04003B65 RID: 15205
		public List<int> FailTypeList;

		// Token: 0x04003B66 RID: 15206
		public int FailLevel;

		// Token: 0x04003B67 RID: 15207
		public DateTime EndTime;
	}
}
