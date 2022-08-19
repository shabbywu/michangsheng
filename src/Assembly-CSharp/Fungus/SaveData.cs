using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E80 RID: 3712
	public class SaveData : MonoBehaviour
	{
		// Token: 0x06006909 RID: 26889 RVA: 0x0028FA28 File Offset: 0x0028DC28
		public virtual void Encode(List<SaveDataItem> saveDataItems)
		{
			for (int i = 0; i < this.flowcharts.Count; i++)
			{
				FlowchartData flowchartData = FlowchartData.Encode(this.flowcharts[i]);
				SaveDataItem item = SaveDataItem.Create("FlowchartData", JsonUtility.ToJson(flowchartData));
				saveDataItems.Add(item);
				SaveDataItem item2 = SaveDataItem.Create("NarrativeLogData", FungusManager.Instance.NarrativeLog.GetJsonHistory());
				saveDataItems.Add(item2);
			}
		}

		// Token: 0x0600690A RID: 26890 RVA: 0x0028FA98 File Offset: 0x0028DC98
		public virtual void Decode(List<SaveDataItem> saveDataItems)
		{
			for (int i = 0; i < saveDataItems.Count; i++)
			{
				SaveDataItem saveDataItem = saveDataItems[i];
				if (saveDataItem != null)
				{
					if (saveDataItem.DataType == "FlowchartData")
					{
						FlowchartData flowchartData = JsonUtility.FromJson<FlowchartData>(saveDataItem.Data);
						if (flowchartData == null)
						{
							Debug.LogError("Failed to decode Flowchart save data item");
							return;
						}
						FlowchartData.Decode(flowchartData);
					}
					if (saveDataItem.DataType == "NarrativeLogData")
					{
						FungusManager.Instance.NarrativeLog.LoadHistory(saveDataItem.Data);
					}
				}
			}
		}

		// Token: 0x0400591E RID: 22814
		protected const string FlowchartDataKey = "FlowchartData";

		// Token: 0x0400591F RID: 22815
		protected const string NarrativeLogKey = "NarrativeLogData";

		// Token: 0x04005920 RID: 22816
		[Tooltip("A list of Flowchart objects whose variables will be encoded in the save data. Boolean, Integer, Float and String variables are supported.")]
		[SerializeField]
		protected List<Flowchart> flowcharts = new List<Flowchart>();
	}
}
