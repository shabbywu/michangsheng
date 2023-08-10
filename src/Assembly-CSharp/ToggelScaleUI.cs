using UnityEngine;
using UnityEngine.UI;

public class ToggelScaleUI : MonoBehaviour
{
	public float nomel = 1f;

	public float ison = 1f;

	private void Start()
	{
	}

	public void setison()
	{
		if (((Component)this).GetComponent<Toggle>().isOn)
		{
			iTween.ScaleTo(((Component)this).gameObject, iTween.Hash(new object[10] { "x", ison, "y", ison, "z", ison, "time", 0.3f, "islocal", true }));
		}
		else
		{
			iTween.ScaleTo(((Component)this).gameObject, iTween.Hash(new object[10] { "x", nomel, "y", nomel, "z", nomel, "time", 0.3f, "islocal", true }));
		}
	}

	private void Update()
	{
	}
}
