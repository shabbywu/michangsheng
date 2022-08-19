using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E3A RID: 3642
	[CommandInfo("Sprite", "Set Sorting Layer", "Sets the Renderer sorting layer of every child of a game object. Applies to all Renderers (including mesh, skinned mesh, and sprite).", 0)]
	[AddComponentMenu("")]
	public class SetSortingLayer : Command
	{
		// Token: 0x0600668A RID: 26250 RVA: 0x00286A04 File Offset: 0x00284C04
		protected void ApplySortingLayer(Transform target, string layerName)
		{
			Renderer component = target.gameObject.GetComponent<Renderer>();
			if (component)
			{
				component.sortingLayerName = layerName;
				Debug.Log(target.name);
			}
			foreach (object obj in target.transform)
			{
				Transform target2 = (Transform)obj;
				this.ApplySortingLayer(target2, layerName);
			}
		}

		// Token: 0x0600668B RID: 26251 RVA: 0x00286A84 File Offset: 0x00284C84
		public override void OnEnter()
		{
			if (this.targetObject != null)
			{
				this.ApplySortingLayer(this.targetObject.transform, this.sortingLayer);
			}
			this.Continue();
		}

		// Token: 0x0600668C RID: 26252 RVA: 0x00286AB1 File Offset: 0x00284CB1
		public override string GetSummary()
		{
			if (this.targetObject == null)
			{
				return "Error: No game object selected";
			}
			return this.targetObject.name;
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040057D9 RID: 22489
		[Tooltip("Root Object that will have the Sorting Layer set. Any children will also be affected")]
		[SerializeField]
		protected GameObject targetObject;

		// Token: 0x040057DA RID: 22490
		[Tooltip("The New Layer Name to apply")]
		[SerializeField]
		protected string sortingLayer;
	}
}
