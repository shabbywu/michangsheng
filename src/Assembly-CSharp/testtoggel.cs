using UnityEngine;
using UnityEngine.UI;

public class testtoggel : MonoBehaviour
{
	private void Start()
	{
		((MonoBehaviour)this).Invoke("set", 1f);
	}

	public void set()
	{
		((Component)((Component)this).transform.Find("Toggle")).GetComponent<Toggle>().isOn = true;
	}

	private void Update()
	{
	}
}
