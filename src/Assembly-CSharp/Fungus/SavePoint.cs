using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001274 RID: 4724
	[CommandInfo("Flow", "Save Point", "Creates a Save Point and adds it to the Save History. The player can save the Save History to persistent storage and load it again later using the Save Menu.", 0)]
	public class SavePoint : Command
	{
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06007297 RID: 29335 RVA: 0x0004E0D6 File Offset: 0x0004C2D6
		// (set) Token: 0x06007298 RID: 29336 RVA: 0x0004E0DE File Offset: 0x0004C2DE
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

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06007299 RID: 29337 RVA: 0x002A88CC File Offset: 0x002A6ACC
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

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600729A RID: 29338 RVA: 0x002A891C File Offset: 0x002A6B1C
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

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600729B RID: 29339 RVA: 0x0004E0E7 File Offset: 0x0004C2E7
		public bool ResumeOnLoad
		{
			get
			{
				return this.resumeOnLoad;
			}
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x0004E0EF File Offset: 0x0004C2EF
		public override void OnEnter()
		{
			FungusManager.Instance.SaveManager.AddSavePoint(this.SavePointKey, this.SavePointDescription);
			if (this.fireEvent)
			{
				SavePointLoaded.NotifyEventHandlers(this.SavePointKey);
			}
			this.Continue();
		}

		// Token: 0x0600729D RID: 29341 RVA: 0x002A894C File Offset: 0x002A6B4C
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

		// Token: 0x0600729E RID: 29342 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x002A89B4 File Offset: 0x002A6BB4
		public override bool IsPropertyVisible(string propertyName)
		{
			return (!(propertyName == "customKey") || this.keyMode == SavePoint.KeyMode.Custom || this.keyMode == SavePoint.KeyMode.BlockNameAndCustom) && (!(propertyName == "keySeparator") || this.keyMode == SavePoint.KeyMode.BlockNameAndCustom) && (!(propertyName == "customDescription") || this.descriptionMode == SavePoint.DescriptionMode.Custom);
		}

		// Token: 0x040064C2 RID: 25794
		[Tooltip("Marks this Save Point as the starting point for Flowchart execution in the scene. Each scene in your game should have exactly one Save Point with this enabled.")]
		[SerializeField]
		protected bool isStartPoint;

		// Token: 0x040064C3 RID: 25795
		[Tooltip("How the Save Point Key for this Save Point is defined.")]
		[SerializeField]
		protected SavePoint.KeyMode keyMode;

		// Token: 0x040064C4 RID: 25796
		[Tooltip("A string key which uniquely identifies this save point.")]
		[SerializeField]
		protected string customKey = "";

		// Token: 0x040064C5 RID: 25797
		[Tooltip("A string to seperate the block name and custom key when using KeyMode.Both.")]
		[SerializeField]
		protected string keySeparator = "_";

		// Token: 0x040064C6 RID: 25798
		[Tooltip("How the description for this Save Point is defined.")]
		[SerializeField]
		protected SavePoint.DescriptionMode descriptionMode;

		// Token: 0x040064C7 RID: 25799
		[Tooltip("A short description of this save point.")]
		[SerializeField]
		protected string customDescription;

		// Token: 0x040064C8 RID: 25800
		[Tooltip("Fire a Save Point Loaded event when this command executes.")]
		[SerializeField]
		protected bool fireEvent = true;

		// Token: 0x040064C9 RID: 25801
		[Tooltip("Resume execution from this location after loading this Save Point.")]
		[SerializeField]
		protected bool resumeOnLoad = true;

		// Token: 0x02001275 RID: 4725
		public enum KeyMode
		{
			// Token: 0x040064CB RID: 25803
			BlockName,
			// Token: 0x040064CC RID: 25804
			Custom,
			// Token: 0x040064CD RID: 25805
			BlockNameAndCustom
		}

		// Token: 0x02001276 RID: 4726
		public enum DescriptionMode
		{
			// Token: 0x040064CF RID: 25807
			Timestamp,
			// Token: 0x040064D0 RID: 25808
			Custom
		}
	}
}
