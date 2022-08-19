using System;
using script.YarnEditor.Manager;
using UnityEngine;

namespace script.YarnEditor.Component.TriggerData
{
	// Token: 0x020009C9 RID: 2505
	[Serializable]
	public class TriggerConfig
	{
		// Token: 0x060045BC RID: 17852 RVA: 0x001D8BC8 File Offset: 0x001D6DC8
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

		// Token: 0x04004732 RID: 18226
		public bool IsNull = true;

		// Token: 0x04004733 RID: 18227
		public int Type;

		// Token: 0x04004734 RID: 18228
		public bool IsValue;

		// Token: 0x04004735 RID: 18229
		public string NpcId = "";

		// Token: 0x04004736 RID: 18230
		public string SceneName = "";

		// Token: 0x04004737 RID: 18231
		public string Path;

		// Token: 0x04004738 RID: 18232
		public string ModPath;

		// Token: 0x04004739 RID: 18233
		public string ValueId;

		// Token: 0x0400473A RID: 18234
		public string Value;
	}
}
