using UnityEngine;

public class qipaixianshi : MonoBehaviour
{
	public UILabel num;

	private void Start()
	{
	}

	private void Update()
	{
		if (((Component)this).GetComponent<UILabel>().text.Contains("弃置"))
		{
			((Component)num).gameObject.SetActive(true);
		}
		else
		{
			((Component)num).gameObject.SetActive(false);
		}
	}
}
