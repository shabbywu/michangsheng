using System;
using UnityEngine;

// Token: 0x020004FB RID: 1275
public class TextOutline : MonoBehaviour
{
	// Token: 0x06002949 RID: 10569 RVA: 0x0013B958 File Offset: 0x00139B58
	private void Start()
	{
		this.textMesh = base.GetComponent<TextMesh>();
		this.meshRenderer = base.GetComponent<MeshRenderer>();
		for (int i = 0; i < 8; i++)
		{
			MeshRenderer component = new GameObject("outline", new Type[]
			{
				typeof(TextMesh)
			})
			{
				transform = 
				{
					parent = base.transform,
					localScale = new Vector3(1f, 1f, 1f)
				}
			}.GetComponent<MeshRenderer>();
			component.material = new Material(this.meshRenderer.material);
			component.castShadows = false;
			component.receiveShadows = false;
			component.sortingLayerID = this.meshRenderer.sortingLayerID;
			component.sortingLayerName = this.meshRenderer.sortingLayerName;
		}
	}

	// Token: 0x0600294A RID: 10570 RVA: 0x0013BA28 File Offset: 0x00139C28
	private void LateUpdate()
	{
		Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.position);
		this.outlineColor.a = this.textMesh.color.a * this.textMesh.color.a;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			TextMesh component = base.transform.GetChild(i).GetComponent<TextMesh>();
			component.color = this.outlineColor;
			component.text = this.textMesh.text;
			component.alignment = this.textMesh.alignment;
			component.anchor = this.textMesh.anchor;
			component.characterSize = this.textMesh.characterSize;
			component.font = this.textMesh.font;
			component.fontSize = this.textMesh.fontSize;
			component.fontStyle = this.textMesh.fontStyle;
			component.richText = this.textMesh.richText;
			component.tabSize = this.textMesh.tabSize;
			component.lineSpacing = this.textMesh.lineSpacing;
			component.offsetZ = this.textMesh.offsetZ;
			bool flag = this.resolutionDependant && (Screen.width > this.doubleResolution || Screen.height > this.doubleResolution);
			Vector3 vector2 = this.GetOffset(i) * (flag ? (2f * this.pixelSize) : this.pixelSize);
			Vector3 position = Camera.main.ScreenToWorldPoint(vector + vector2);
			component.transform.position = position;
			MeshRenderer component2 = base.transform.GetChild(i).GetComponent<MeshRenderer>();
			component2.sortingLayerID = this.meshRenderer.sortingLayerID;
			component2.sortingLayerName = this.meshRenderer.sortingLayerName;
		}
	}

	// Token: 0x0600294B RID: 10571 RVA: 0x0013BC0C File Offset: 0x00139E0C
	private Vector3 GetOffset(int i)
	{
		switch (i % 8)
		{
		case 0:
			return new Vector3(0f, 1f, 1f);
		case 1:
			return new Vector3(1f, 1f, 1f);
		case 2:
			return new Vector3(1f, 0f, 1f);
		case 3:
			return new Vector3(1f, -1f, 1f);
		case 4:
			return new Vector3(0f, -1f, 1f);
		case 5:
			return new Vector3(-1f, -1f, 1f);
		case 6:
			return new Vector3(-1f, 0f, 1f);
		case 7:
			return new Vector3(-1f, 1f, 1f);
		default:
			return Vector3.zero;
		}
	}

	// Token: 0x04002577 RID: 9591
	public float pixelSize = 1f;

	// Token: 0x04002578 RID: 9592
	public Color outlineColor = Color.black;

	// Token: 0x04002579 RID: 9593
	public bool resolutionDependant;

	// Token: 0x0400257A RID: 9594
	public int doubleResolution = 1024;

	// Token: 0x0400257B RID: 9595
	private TextMesh textMesh;

	// Token: 0x0400257C RID: 9596
	private MeshRenderer meshRenderer;
}
