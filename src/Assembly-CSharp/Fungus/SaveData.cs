using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

public class SaveData : MonoBehaviour
{
	protected const string FlowchartDataKey = "FlowchartData";

	protected const string NarrativeLogKey = "NarrativeLogData";

	[Tooltip("A list of Flowchart objects whose variables will be encoded in the save data. Boolean, Integer, Float and String variables are supported.")]
	[SerializeField]
	protected List<Flowchart> flowcharts = new List<Flowchart>();

	public virtual void Encode(List<SaveDataItem> saveDataItems)
	{
		for (int i = 0; i < flowcharts.Count; i++)
		{
			FlowchartData flowchartData = FlowchartData.Encode(flowcharts[i]);
			SaveDataItem item = SaveDataItem.Create("FlowchartData", JsonUtility.ToJson((object)flowchartData));
			saveDataItems.Add(item);
			SaveDataItem item2 = SaveDataItem.Create("NarrativeLogData", FungusManager.Instance.NarrativeLog.GetJsonHistory());
			saveDataItems.Add(item2);
		}
	}

	public virtual void Decode(List<SaveDataItem> saveDataItems)
	{
		for (int i = 0; i < saveDataItems.Count; i++)
		{
			SaveDataItem saveDataItem = saveDataItems[i];
			if (saveDataItem == null)
			{
				continue;
			}
			if (saveDataItem.DataType == "FlowchartData")
			{
				FlowchartData flowchartData = JsonUtility.FromJson<FlowchartData>(saveDataItem.Data);
				if (flowchartData == null)
				{
					Debug.LogError((object)"Failed to decode Flowchart save data item");
					break;
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
