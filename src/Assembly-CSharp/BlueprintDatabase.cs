using System.Collections.Generic;
using UnityEngine;

public class BlueprintDatabase : ScriptableObject
{
	[SerializeField]
	public List<Blueprint> blueprints = new List<Blueprint>();
}
