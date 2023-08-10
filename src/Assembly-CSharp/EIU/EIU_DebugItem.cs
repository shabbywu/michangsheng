using UnityEngine;
using UnityEngine.UI;

namespace EIU;

public class EIU_DebugItem : MonoBehaviour
{
	public Text axisName;

	public Text keyName;

	public void Init(string axisName, string keyName)
	{
		this.axisName.text = axisName;
		this.keyName.text = keyName;
	}
}
