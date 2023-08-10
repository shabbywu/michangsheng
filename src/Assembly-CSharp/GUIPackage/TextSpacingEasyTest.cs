using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPackage;

[AddComponentMenu("UI/Effects/TextSpacingEasyTest")]
public class TextSpacingEasyTest : BaseMeshEffect
{
	[SerializeField]
	private Text text;

	[SerializeField]
	private float textWidth;

	[SerializeField]
	private bool AutoSpace;

	private int fontSize = 14;

	private RectTransform rect;

	public float spacing;

	private void Start()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		text = ((Component)this).GetComponent<Text>();
		rect = ((Component)text).GetComponent<RectTransform>();
		Rect val = rect.rect;
		textWidth = ((Rect)(ref val)).height;
		fontSize = text.fontSize;
	}

	public override void ModifyMesh(VertexHelper vh)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		if (text.text.Length >= 6)
		{
			return;
		}
		float num = textWidth;
		Rect val = rect.rect;
		if (num != ((Rect)(ref val)).height)
		{
			val = rect.rect;
			textWidth = ((Rect)(ref val)).height;
		}
		List<UIVertex> list = new List<UIVertex>();
		vh.GetUIVertexStream(list);
		int count = list.Count;
		int num2 = count / 6 - 1;
		if (AutoSpace)
		{
			spacing = (textWidth - (float)((num2 + 1) * fontSize)) / (float)num2;
		}
		for (int i = 6; i < count; i++)
		{
			UIVertex val2 = list[i];
			ref Vector3 position = ref val2.position;
			position -= new Vector3(0f, spacing * (float)(i / 6), 0f);
			list[i] = val2;
			if (i % 6 <= 2)
			{
				vh.SetUIVertex(val2, i / 6 * 4 + i % 6);
			}
			if (i % 6 == 4)
			{
				vh.SetUIVertex(val2, i / 6 * 4 + i % 6 - 1);
			}
		}
	}
}
