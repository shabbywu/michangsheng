using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200128B RID: 4747
	[CommandInfo("Sprite", "Set Sorting Layer", "Sets the Renderer sorting layer of every child of a game object. Applies to all Renderers (including mesh, skinned mesh, and sprite).", 0)]
	[AddComponentMenu("")]
	public class SetSortingLayer : Command
	{
		// Token: 0x06007318 RID: 29464 RVA: 0x002A9DB4 File Offset: 0x002A7FB4
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

		// Token: 0x06007319 RID: 29465 RVA: 0x0004E75F File Offset: 0x0004C95F
		public override void OnEnter()
		{
			if (this.targetObject != null)
			{
				this.ApplySortingLayer(this.targetObject.transform, this.sortingLayer);
			}
			this.Continue();
		}

		// Token: 0x0600731A RID: 29466 RVA: 0x0004E78C File Offset: 0x0004C98C
		public override string GetSummary()
		{
			if (this.targetObject == null)
			{
				return "Error: No game object selected";
			}
			return this.targetObject.name;
		}

		// Token: 0x0600731B RID: 29467 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0400651D RID: 25885
		[Tooltip("Root Object that will have the Sorting Layer set. Any children will also be affected")]
		[SerializeField]
		protected GameObject targetObject;

		// Token: 0x0400651E RID: 25886
		[Tooltip("The New Layer Name to apply")]
		[SerializeField]
		protected string sortingLayer;
	}
}
