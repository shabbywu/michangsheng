using System;
using UnityEngine;

// Token: 0x02000088 RID: 136
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Property Binding")]
public class PropertyBinding : MonoBehaviour
{
	// Token: 0x06000738 RID: 1848 RVA: 0x0002AF85 File Offset: 0x00029185
	private void Start()
	{
		this.UpdateTarget();
		if (this.update == PropertyBinding.UpdateCondition.OnStart)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0002AF9C File Offset: 0x0002919C
	private void Update()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0002AFAD File Offset: 0x000291AD
	private void LateUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnLateUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0002AFBE File Offset: 0x000291BE
	private void FixedUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnFixedUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0002AFCF File Offset: 0x000291CF
	private void OnValidate()
	{
		if (this.source != null)
		{
			this.source.Reset();
		}
		if (this.target != null)
		{
			this.target.Reset();
		}
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0002AFF8 File Offset: 0x000291F8
	[ContextMenu("Update Now")]
	public void UpdateTarget()
	{
		if (this.source != null && this.target != null && this.source.isValid && this.target.isValid)
		{
			if (this.direction == PropertyBinding.Direction.SourceUpdatesTarget)
			{
				this.target.Set(this.source.Get());
				return;
			}
			if (this.direction == PropertyBinding.Direction.TargetUpdatesSource)
			{
				this.source.Set(this.target.Get());
				return;
			}
			if (this.source.GetPropertyType() == this.target.GetPropertyType())
			{
				object obj = this.source.Get();
				if (this.mLastValue == null || !this.mLastValue.Equals(obj))
				{
					this.mLastValue = obj;
					this.target.Set(obj);
					return;
				}
				obj = this.target.Get();
				if (!this.mLastValue.Equals(obj))
				{
					this.mLastValue = obj;
					this.source.Set(obj);
				}
			}
		}
	}

	// Token: 0x0400047F RID: 1151
	public PropertyReference source;

	// Token: 0x04000480 RID: 1152
	public PropertyReference target;

	// Token: 0x04000481 RID: 1153
	public PropertyBinding.Direction direction;

	// Token: 0x04000482 RID: 1154
	public PropertyBinding.UpdateCondition update = PropertyBinding.UpdateCondition.OnUpdate;

	// Token: 0x04000483 RID: 1155
	public bool editMode = true;

	// Token: 0x04000484 RID: 1156
	private object mLastValue;

	// Token: 0x020011FF RID: 4607
	public enum UpdateCondition
	{
		// Token: 0x0400643B RID: 25659
		OnStart,
		// Token: 0x0400643C RID: 25660
		OnUpdate,
		// Token: 0x0400643D RID: 25661
		OnLateUpdate,
		// Token: 0x0400643E RID: 25662
		OnFixedUpdate
	}

	// Token: 0x02001200 RID: 4608
	public enum Direction
	{
		// Token: 0x04006440 RID: 25664
		SourceUpdatesTarget,
		// Token: 0x04006441 RID: 25665
		TargetUpdatesSource,
		// Token: 0x04006442 RID: 25666
		BiDirectional
	}
}
