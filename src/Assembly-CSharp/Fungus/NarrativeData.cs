using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[Serializable]
public class NarrativeData
{
	[SerializeField]
	public List<Line> lines;

	public NarrativeData()
	{
		lines = new List<Line>();
	}
}
