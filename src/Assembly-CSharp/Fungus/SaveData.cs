using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012ED RID: 4845
	public class SaveData : MonoBehaviour
	{
		// Token: 0x060075F8 RID: 30200 RVA: 0x002B1FD0 File Offset: 0x002B01D0
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

		// Token: 0x060075F9 RID: 30201 RVA: 0x002B2040 File Offset: 0x002B0240
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

		// Token: 0x040066EC RID: 26348
		protected const string FlowchartDataKey = "FlowchartData";

		// Token: 0x040066ED RID: 26349
		protected const string NarrativeLogKey = "NarrativeLogData";

		// Token: 0x040066EE RID: 26350
		[Tooltip("A list of Flowchart objects whose variables will be encoded in the save data. Boolean, Integer, Float and String variables are supported.")]
		[SerializeField]
		protected List<Flowchart> flowcharts = new List<Flowchart>();
	}
}
