using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang.Filter;

public class QualityFilter : UIBase
{
	public int Value;

	public UnityAction<int> Action;

	public QualityFilter(GameObject go, string name, int value, float x, float y)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		_go = go;
		((Object)_go).name = name;
		Value = value;
		_go.GetComponent<FpBtn>().mouseUpEvent.AddListener((UnityAction)delegate
		{
			Action?.Invoke(Value);
		});
		Get<Text>("Value").SetText(name);
		_go.transform.localPosition = Vector2.op_Implicit(new Vector2(x, y));
		_go.SetActive(true);
	}
}
