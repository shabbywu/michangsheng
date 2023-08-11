using UnityEngine;

public class MainUILoadData : MonoBehaviour
{
	[SerializeField]
	private GameObject dataObj;

	[SerializeField]
	private RectTransform dataList;

	public void Init(int index)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		Tools.ClearObj(dataObj.transform);
		for (int i = 0; i < MainUIMag.inst.smallDataNum; i++)
		{
			Object.Instantiate<GameObject>(dataObj, (Transform)(object)dataList).GetComponent<MainUIDataCell>().Init(index, i);
		}
		dataList.anchoredPosition = new Vector2(dataList.anchoredPosition.x, 0f);
		((Component)this).gameObject.SetActive(true);
	}
}
