using GUIPackage;
using UnityEngine;

public class XiuLianGongFaCell : MonoBehaviour
{
	public UILabel Name;

	public UILabel desc;

	public UILabel Time;

	public InitLinWu linWu;

	private KeyCell keyCell;

	private void Start()
	{
		keyCell = ((Component)this).GetComponent<KeyCell>();
	}

	public void init1()
	{
	}

	public void useGongFa()
	{
	}

	private void Update()
	{
	}
}
