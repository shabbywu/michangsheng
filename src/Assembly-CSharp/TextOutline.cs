using System;
using UnityEngine;

public class TextOutline : MonoBehaviour
{
	public float pixelSize = 1f;

	public Color outlineColor = Color.black;

	public bool resolutionDependant;

	public int doubleResolution = 1024;

	private TextMesh textMesh;

	private MeshRenderer meshRenderer;

	private void Start()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		textMesh = ((Component)this).GetComponent<TextMesh>();
		meshRenderer = ((Component)this).GetComponent<MeshRenderer>();
		for (int i = 0; i < 8; i++)
		{
			GameObject val = new GameObject("outline", new Type[1] { typeof(TextMesh) });
			val.transform.parent = ((Component)this).transform;
			val.transform.localScale = new Vector3(1f, 1f, 1f);
			MeshRenderer component = val.GetComponent<MeshRenderer>();
			((Renderer)component).material = new Material(((Renderer)meshRenderer).material);
			((Renderer)component).castShadows = false;
			((Renderer)component).receiveShadows = false;
			((Renderer)component).sortingLayerID = ((Renderer)meshRenderer).sortingLayerID;
			((Renderer)component).sortingLayerName = ((Renderer)meshRenderer).sortingLayerName;
		}
	}

	private void LateUpdate()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = Camera.main.WorldToScreenPoint(((Component)this).transform.position);
		outlineColor.a = textMesh.color.a * textMesh.color.a;
		for (int i = 0; i < ((Component)this).transform.childCount; i++)
		{
			TextMesh component = ((Component)((Component)this).transform.GetChild(i)).GetComponent<TextMesh>();
			component.color = outlineColor;
			component.text = textMesh.text;
			component.alignment = textMesh.alignment;
			component.anchor = textMesh.anchor;
			component.characterSize = textMesh.characterSize;
			component.font = textMesh.font;
			component.fontSize = textMesh.fontSize;
			component.fontStyle = textMesh.fontStyle;
			component.richText = textMesh.richText;
			component.tabSize = textMesh.tabSize;
			component.lineSpacing = textMesh.lineSpacing;
			component.offsetZ = textMesh.offsetZ;
			bool flag = resolutionDependant && (Screen.width > doubleResolution || Screen.height > doubleResolution);
			Vector3 val2 = GetOffset(i) * (flag ? (2f * pixelSize) : pixelSize);
			Vector3 position = Camera.main.ScreenToWorldPoint(val + val2);
			((Component)component).transform.position = position;
			MeshRenderer component2 = ((Component)((Component)this).transform.GetChild(i)).GetComponent<MeshRenderer>();
			((Renderer)component2).sortingLayerID = ((Renderer)meshRenderer).sortingLayerID;
			((Renderer)component2).sortingLayerName = ((Renderer)meshRenderer).sortingLayerName;
		}
	}

	private Vector3 GetOffset(int i)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		return (Vector3)((i % 8) switch
		{
			0 => new Vector3(0f, 1f, 1f), 
			1 => new Vector3(1f, 1f, 1f), 
			2 => new Vector3(1f, 0f, 1f), 
			3 => new Vector3(1f, -1f, 1f), 
			4 => new Vector3(0f, -1f, 1f), 
			5 => new Vector3(-1f, -1f, 1f), 
			6 => new Vector3(-1f, 0f, 1f), 
			7 => new Vector3(-1f, 1f, 1f), 
			_ => Vector3.zero, 
		});
	}
}
