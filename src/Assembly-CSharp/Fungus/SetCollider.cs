using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001287 RID: 4743
	[CommandInfo("Sprite", "Set Collider", "Sets all collider (2d or 3d) components on the target objects to be active / inactive", 0)]
	[AddComponentMenu("")]
	public class SetCollider : Command
	{
		// Token: 0x060072FE RID: 29438 RVA: 0x002A9ACC File Offset: 0x002A7CCC
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

		// Token: 0x060072FF RID: 29439 RVA: 0x002A9B34 File Offset: 0x002A7D34
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

		// Token: 0x06007300 RID: 29440 RVA: 0x002A9BB4 File Offset: 0x002A7DB4
		public override string GetSummary()
		{
			int count = this.targetObjects.Count;
			if (this.activeState.Value)
			{
				return "Enable " + count + " collider objects.";
			}
			return "Disable " + count + " collider objects.";
		}

		// Token: 0x06007301 RID: 29441 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x0004E5AF File Offset: 0x0004C7AF
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetObjects";
		}

		// Token: 0x06007303 RID: 29443 RVA: 0x0004E5BC File Offset: 0x0004C7BC
		public override bool HasReference(Variable variable)
		{
			return this.activeState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006513 RID: 25875
		[Tooltip("A list of gameobjects containing collider components to be set active / inactive")]
		[SerializeField]
		protected List<GameObject> targetObjects = new List<GameObject>();

		// Token: 0x04006514 RID: 25876
		[Tooltip("All objects with this tag will have their collider set active / inactive")]
		[SerializeField]
		protected string targetTag = "";

		// Token: 0x04006515 RID: 25877
		[Tooltip("Set to true to enable the collider components")]
		[SerializeField]
		protected BooleanData activeState;
	}
}
