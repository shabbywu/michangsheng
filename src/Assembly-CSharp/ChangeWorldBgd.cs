using UnityEngine;

public class ChangeWorldBgd : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void ChangeBgd()
	{
		((Component)((Component)Camera.main).transform.Find("Background")).GetComponent<Renderer>().material = GameObject.Find("TempBgd").GetComponent<Renderer>().material;
	}
}
