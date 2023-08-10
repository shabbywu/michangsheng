using UnityEngine;
using UnityEngine.UI;

public class XiaoGuoTips : MonoBehaviour
{
	[SerializeField]
	private Text content;

	public void show()
	{
		((Component)this).gameObject.SetActive(true);
	}

	public void setPosition(float y)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		float x = ((Component)this).gameObject.transform.position.x;
		float z = ((Component)this).gameObject.transform.position.z;
		((Component)this).gameObject.transform.position = new Vector3(x, y, z);
	}

	public void close()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void setContent(string str)
	{
		content.text = Tools.Code64(str);
	}
}
