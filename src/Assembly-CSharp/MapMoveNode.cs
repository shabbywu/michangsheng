using UnityEngine;

public class MapMoveNode : MonoBehaviour
{
	public int StartNode;

	public int EndNode;

	private void Start()
	{
		string name = ((Object)((Component)this).transform.parent).name;
		int num = name.IndexOf("_");
		string s = name.Substring(0, num);
		string s2 = name.Substring(num + 1, name.Length - num - 1);
		int.TryParse(s, out StartNode);
		int.TryParse(s2, out EndNode);
	}

	private void Update()
	{
	}
}
