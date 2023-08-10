using UnityEngine;
using UnityEngine.UI;

public class CyChildSelect : MonoBehaviour
{
	[SerializeField]
	private Text Content;

	public FpBtn Btn;

	public GameObject Line;

	public void Init(string msg)
	{
		Content.text = msg;
		((Component)this).gameObject.SetActive(true);
	}
}
