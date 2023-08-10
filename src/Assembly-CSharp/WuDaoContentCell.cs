using UnityEngine;
using UnityEngine.UI;

public class WuDaoContentCell : MonoBehaviour
{
	[SerializeField]
	private Text Name;

	[SerializeField]
	private Text Descr;

	public void setContent(string name, string descr)
	{
		Name.text = Tools.Code64(name);
		Descr.text = Tools.Code64(descr);
	}
}
