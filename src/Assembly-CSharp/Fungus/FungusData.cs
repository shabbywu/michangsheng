using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200145F RID: 5215
	[Serializable]
	public class FungusData
	{
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06007DBD RID: 32189 RVA: 0x00054FDA File Offset: 0x000531DA
		// (set) Token: 0x06007DBE RID: 32190 RVA: 0x00054FE2 File Offset: 0x000531E2
		public int TalkType { get; private set; }

		// Token: 0x06007DBF RID: 32191 RVA: 0x002C77B8 File Offset: 0x002C59B8
		public void Save()
		{
			if (this.TalkIsEnd || this.TalkName == "NPCJiaoHuTalk" || this.CommandName == "YSSaveGame")
			{
				this.IsNeedLoad = false;
				return;
			}
			this.FindTalkType();
			this.Floats = new Dictionary<string, float>();
			this.Ints = new Dictionary<string, int>();
			this.Strings = new Dictionary<string, string>();
			this.Bools = new Dictionary<string, bool>();
			foreach (Variable variable in this.Flowchart.Variables)
			{
				if (variable is FloatVariable)
				{
					this.Floats.Add(variable.Key, this.Flowchart.GetFloatVariable(variable.Key));
				}
				else if (variable is IntegerVariable)
				{
					this.Ints.Add(variable.Key, this.Flowchart.GetIntegerVariable(variable.Key));
				}
				else if (variable is StringVariable)
				{
					this.Strings.Add(variable.Key, this.Flowchart.GetStringVariable(variable.Key));
				}
				else if (variable is BooleanVariable)
				{
					this.Bools.Add(variable.Key, this.Flowchart.GetBooleanVariable(variable.Key));
				}
			}
			if (this.FirstMenu != -1)
			{
				this.CommandIndex = this.FirstMenu;
				this.CommandName = "Menu";
			}
			this.IsNeedLoad = true;
		}

		// Token: 0x06007DC0 RID: 32192 RVA: 0x002C7950 File Offset: 0x002C5B50
		public void FindTalkType()
		{
			if (!(ResManager.inst.LoadTalk("TalkPrefab/" + this.TalkName) == null))
			{
				this.TalkType = 0;
				return;
			}
			this.TalkType = 1;
			if (GameObject.Find("AllMap/LevelsWorld0/" + this.TalkName) != null)
			{
				this.TalkType = 1;
				return;
			}
			this.TalkType = 2;
		}

		// Token: 0x06007DC1 RID: 32193 RVA: 0x00054FEB File Offset: 0x000531EB
		public bool IsNewBlock()
		{
			return this.LastBlockName != this.BlockName;
		}

		// Token: 0x04006B33 RID: 27443
		[NonSerialized]
		public Flowchart Flowchart;

		// Token: 0x04006B34 RID: 27444
		[NonSerialized]
		public Block Block;

		// Token: 0x04006B35 RID: 27445
		public Dictionary<string, float> Floats;

		// Token: 0x04006B36 RID: 27446
		public Dictionary<string, int> Ints;

		// Token: 0x04006B37 RID: 27447
		public Dictionary<string, string> Strings;

		// Token: 0x04006B38 RID: 27448
		public Dictionary<string, bool> Bools;

		// Token: 0x04006B39 RID: 27449
		public string RealCommandName;

		// Token: 0x04006B3A RID: 27450
		public string TalkName;

		// Token: 0x04006B3B RID: 27451
		public string BlockName;

		// Token: 0x04006B3C RID: 27452
		public string LastBlockName;

		// Token: 0x04006B3D RID: 27453
		public string CommandName;

		// Token: 0x04006B3E RID: 27454
		public int FirstMenu = -1;

		// Token: 0x04006B3F RID: 27455
		public int CommandIndex;

		// Token: 0x04006B40 RID: 27456
		public bool TalkIsEnd = true;

		// Token: 0x04006B41 RID: 27457
		public bool IsNeedLoad;
	}
}
