using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public class SaveDataItem
{
	[SerializeField]
	protected string dataType = "";

	[SerializeField]
	protected string data = "";

	public virtual string DataType => dataType;

	public virtual string Data => data;

	public static SaveDataItem Create(string dataType, string data)
	{
		return new SaveDataItem
		{
			dataType = dataType,
			data = data
		};
	}
}
