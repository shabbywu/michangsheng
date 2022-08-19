using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E36 RID: 3638
	[CommandInfo("Sprite", "Set Collider", "Sets all collider (2d or 3d) components on the target objects to be active / inactive", 0)]
	[AddComponentMenu("")]
	public class SetCollider : Command
	{
		// Token: 0x06006670 RID: 26224 RVA: 0x0028656C File Offset: 0x0028476C
		protected virtual void SetColliderActive(GameObject go)
		{
			if (go != null)
			{
				Collider[] componentsInChildren = go.GetComponentsInChildren<Collider>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = this.activeState.Value;
				}
				Collider2D[] componentsInChildren2 = go.GetComponentsInChildren<Collider2D>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					componentsInChildren2[j].enabled = this.activeState.Value;
				}
			}
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x002865D4 File Offset: 0x002847D4
		public override void OnEnter()
		{
			for (int i = 0; i < this.targetObjects.Count; i++)
			{
				GameObject colliderActive = this.targetObjects[i];
				this.SetColliderActive(colliderActive);
			}
			GameObject[] array = null;
			try
			{
				array = GameObject.FindGameObjectsWithTag(this.targetTag);
			}
			catch
			{
			}
			if (array != null)
			{
				foreach (GameObject colliderActive2 in array)
				{
					this.SetColliderActive(colliderActive2);
				}
			}
			this.Continue();
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x00286654 File Offset: 0x00284854
		public override string GetSummary()
		{
			int count = this.targetObjects.Count;
			if (this.activeState.Value)
			{
				return "Enable " + count + " collider objects.";
			}
			return "Disable " + count + " collider objects.";
		}

		// Token: 0x06006673 RID: 26227 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006674 RID: 26228 RVA: 0x002866A5 File Offset: 0x002848A5
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetObjects";
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x002866B2 File Offset: 0x002848B2
		public override bool HasReference(Variable variable)
		{
			return this.activeState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x040057CF RID: 22479
		[Tooltip("A list of gameobjects containing collider components to be set active / inactive")]
		[SerializeField]
		protected List<GameObject> targetObjects = new List<GameObject>();

		// Token: 0x040057D0 RID: 22480
		[Tooltip("All objects with this tag will have their collider set active / inactive")]
		[SerializeField]
		protected string targetTag = "";

		// Token: 0x040057D1 RID: 22481
		[Tooltip("Set to true to enable the collider components")]
		[SerializeField]
		protected BooleanData activeState;
	}
}
