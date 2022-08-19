using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E25 RID: 3621
	[CommandInfo("Flow", "Save Point", "Creates a Save Point and adds it to the Save History. The player can save the Save History to persistent storage and load it again later using the Save Menu.", 0)]
	public class SavePoint : Command
	{
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06006609 RID: 26121 RVA: 0x00284E8C File Offset: 0x0028308C
		// (set) Token: 0x0600660A RID: 26122 RVA: 0x00284E94 File Offset: 0x00283094
		public bool IsStartPoint
		{
			get
			{
				return this.isStartPoint;
			}
			set
			{
				this.isStartPoint = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600660B RID: 26123 RVA: 0x00284EA0 File Offset: 0x002830A0
		public string SavePointKey
		{
			get
			{
				if (this.keyMode == SavePoint.KeyMode.BlockName)
				{
					return this.ParentBlock.BlockName;
				}
				if (this.keyMode == SavePoint.KeyMode.BlockNameAndCustom)
				{
					return this.ParentBlock.BlockName + this.keySeparator + this.customKey;
				}
				return this.customKey;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x0600660C RID: 26124 RVA: 0x00284EF0 File Offset: 0x002830F0
		public string SavePointDescription
		{
			get
			{
				if (this.descriptionMode == SavePoint.DescriptionMode.Timestamp)
				{
					return DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy");
				}
				return this.customDescription;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x0600660D RID: 26125 RVA: 0x00284F1E File Offset: 0x0028311E
		public bool ResumeOnLoad
		{
			get
			{
				return this.resumeOnLoad;
			}
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x00284F26 File Offset: 0x00283126
		public override void OnEnter()
		{
			FungusManager.Instance.SaveManager.AddSavePoint(this.SavePointKey, this.SavePointDescription);
			if (this.fireEvent)
			{
				SavePointLoaded.NotifyEventHandlers(this.SavePointKey);
			}
			this.Continue();
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x00284F5C File Offset: 0x0028315C
		public override string GetSummary()
		{
			if (this.keyMode == SavePoint.KeyMode.BlockName)
			{
				return "key: " + this.ParentBlock.BlockName;
			}
			if (this.keyMode == SavePoint.KeyMode.BlockNameAndCustom)
			{
				return "key: " + this.ParentBlock.BlockName + this.keySeparator + this.customKey;
			}
			return "key: " + this.customKey;
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x00284FC4 File Offset: 0x002831C4
		public override bool IsPropertyVisible(string propertyName)
		{
			return (!(propertyName == "customKey") || this.keyMode == SavePoint.KeyMode.Custom || this.keyMode == SavePoint.KeyMode.BlockNameAndCustom) && (!(propertyName == "keySeparator") || this.keyMode == SavePoint.KeyMode.BlockNameAndCustom) && (!(propertyName == "customDescription") || this.descriptionMode == SavePoint.DescriptionMode.Custom);
		}

		// Token: 0x04005785 RID: 22405
		[Tooltip("Marks this Save Point as the starting point for Flowchart execution in the scene. Each scene in your game should have exactly one Save Point with this enabled.")]
		[SerializeField]
		protected bool isStartPoint;

		// Token: 0x04005786 RID: 22406
		[Tooltip("How the Save Point Key for this Save Point is defined.")]
		[SerializeField]
		protected SavePoint.KeyMode keyMode;

		// Token: 0x04005787 RID: 22407
		[Tooltip("A string key which uniquely identifies this save point.")]
		[SerializeField]
		protected string customKey = "";

		// Token: 0x04005788 RID: 22408
		[Tooltip("A string to seperate the block name and custom key when using KeyMode.Both.")]
		[SerializeField]
		protected string keySeparator = "_";

		// Token: 0x04005789 RID: 22409
		[Tooltip("How the description for this Save Point is defined.")]
		[SerializeField]
		protected SavePoint.DescriptionMode descriptionMode;

		// Token: 0x0400578A RID: 22410
		[Tooltip("A short description of this save point.")]
		[SerializeField]
		protected string customDescription;

		// Token: 0x0400578B RID: 22411
		[Tooltip("Fire a Save Point Loaded event when this command executes.")]
		[SerializeField]
		protected bool fireEvent = true;

		// Token: 0x0400578C RID: 22412
		[Tooltip("Resume execution from this location after loading this Save Point.")]
		[SerializeField]
		protected bool resumeOnLoad = true;

		// Token: 0x020016C3 RID: 5827
		public enum KeyMode
		{
			// Token: 0x04007399 RID: 29593
			BlockName,
			// Token: 0x0400739A RID: 29594
			Custom,
			// Token: 0x0400739B RID: 29595
			BlockNameAndCustom
		}

		// Token: 0x020016C4 RID: 5828
		public enum DescriptionMode
		{
			// Token: 0x0400739D RID: 29597
			Timestamp,
			// Token: 0x0400739E RID: 29598
			Custom
		}
	}
}
