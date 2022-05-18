using System;
using script.YarnEditor.Manager;
using UnityEngine;

namespace script.YarnEditor.Component.TriggerData
{
	// Token: 0x02000AB0 RID: 2736
	[Serializable]
	public class TriggerConfig
	{
		// Token: 0x06004618 RID: 17944 RVA: 0x001DE9DC File Offset: 0x001DCBDC
		public bool CanTrigger(string value)
		{
			if (this.IsNull)
			{
				return false;
			}
			if (this.Type == 0)
			{
				if (this.NpcId == "" || this.NpcId.Length < 1)
				{
					return false;
				}
				try
				{
					if (this.IsValue)
					{
						this.NpcId = StoryManager.Inst.GetGoalValue(this.NpcId);
					}
					else
					{
						this.NpcId = NPCEx.NPCIDToNew(int.Parse(this.NpcId)).ToString();
					}
				}
				catch (Exception ex)
				{
					Debug.Log(ex);
					StoryManager.Inst.LogError(ex.Message);
					return false;
				}
				if (value != this.NpcId)
				{
					return false;
				}
			}
			else if (this.Type == 1)
			{
				if (this.SceneName == "" || this.SceneName.Length < 1)
				{
					return false;
				}
				if (this.IsValue)
				{
					this.SceneName = StoryManager.Inst.GetGoalValue(this.SceneName);
				}
				if (this.SceneName != value)
				{
					return false;
				}
			}
			return (!(this.ValueId != "") && this.ValueId.Length <= 0) || !(StoryManager.Inst.GetGoalValue(this.ValueId) != value);
		}

		// Token: 0x04003E43 RID: 15939
		public bool IsNull = true;

		// Token: 0x04003E44 RID: 15940
		public int Type;

		// Token: 0x04003E45 RID: 15941
		public bool IsValue;

		// Token: 0x04003E46 RID: 15942
		public string NpcId = "";

		// Token: 0x04003E47 RID: 15943
		public string SceneName = "";

		// Token: 0x04003E48 RID: 15944
		public string Path;

		// Token: 0x04003E49 RID: 15945
		public string ModPath;

		// Token: 0x04003E4A RID: 15946
		public string ValueId;

		// Token: 0x04003E4B RID: 15947
		public string Value;
	}
}
