using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000FA9 RID: 4009
	[Serializable]
	public class FungusData
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06006FCF RID: 28623 RVA: 0x002A7ED3 File Offset: 0x002A60D3
		// (set) Token: 0x06006FD0 RID: 28624 RVA: 0x002A7EDB File Offset: 0x002A60DB
		public int TalkType { get; private set; }

		// Token: 0x06006FD1 RID: 28625 RVA: 0x002A7EE4 File Offset: 0x002A60E4
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

		// Token: 0x06006FD2 RID: 28626 RVA: 0x002A807C File Offset: 0x002A627C
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

		// Token: 0x06006FD3 RID: 28627 RVA: 0x002A80E6 File Offset: 0x002A62E6
		public bool IsNewBlock()
		{
			return this.LastBlockName != this.BlockName;
		}

		// Token: 0x04005C3F RID: 23615
		[NonSerialized]
		public Flowchart Flowchart;

		// Token: 0x04005C40 RID: 23616
		[NonSerialized]
		public Block Block;

		// Token: 0x04005C41 RID: 23617
		public Dictionary<string, float> Floats;

		// Token: 0x04005C42 RID: 23618
		public Dictionary<string, int> Ints;

		// Token: 0x04005C43 RID: 23619
		public Dictionary<string, string> Strings;

		// Token: 0x04005C44 RID: 23620
		public Dictionary<string, bool> Bools;

		// Token: 0x04005C45 RID: 23621
		public string RealCommandName;

		// Token: 0x04005C46 RID: 23622
		public string TalkName;

		// Token: 0x04005C47 RID: 23623
		public string BlockName;

		// Token: 0x04005C48 RID: 23624
		public string LastBlockName;

		// Token: 0x04005C49 RID: 23625
		public string CommandName;

		// Token: 0x04005C4A RID: 23626
		public int FirstMenu = -1;

		// Token: 0x04005C4B RID: 23627
		public int CommandIndex;

		// Token: 0x04005C4C RID: 23628
		public bool TalkIsEnd = true;

		// Token: 0x04005C4D RID: 23629
		public bool IsNeedLoad;
	}
}
