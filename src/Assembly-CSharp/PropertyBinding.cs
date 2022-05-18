using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Property Binding")]
public class PropertyBinding : MonoBehaviour
{
	// Token: 0x060007BB RID: 1979 RVA: 0x0000A762 File Offset: 0x00008962
	private void Start()
	{
		this.UpdateTarget();
		if (this.update == PropertyBinding.UpdateCondition.OnStart)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0000A779 File Offset: 0x00008979
	private void Update()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0000A78A File Offset: 0x0000898A
	private void LateUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnLateUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x0000A79B File Offset: 0x0000899B
	private void FixedUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnFixedUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0000A7AC File Offset: 0x000089AC
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

	// Token: 0x060007C0 RID: 1984 RVA: 0x0007FFF4 File Offset: 0x0007E1F4
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

	// Token: 0x0400056A RID: 1386
	public PropertyReference source;

	// Token: 0x0400056B RID: 1387
	public PropertyReference target;

	// Token: 0x0400056C RID: 1388
	public PropertyBinding.Direction direction;

	// Token: 0x0400056D RID: 1389
	public PropertyBinding.UpdateCondition update = PropertyBinding.UpdateCondition.OnUpdate;

	// Token: 0x0400056E RID: 1390
	public bool editMode = true;

	// Token: 0x0400056F RID: 1391
	private object mLastValue;

	// Token: 0x020000C4 RID: 196
	public enum UpdateCondition
	{
		// Token: 0x04000571 RID: 1393
		OnStart,
		// Token: 0x04000572 RID: 1394
		OnUpdate,
		// Token: 0x04000573 RID: 1395
		OnLateUpdate,
		// Token: 0x04000574 RID: 1396
		OnFixedUpdate
	}

	// Token: 0x020000C5 RID: 197
	public enum Direction
	{
		// Token: 0x04000576 RID: 1398
		SourceUpdatesTarget,
		// Token: 0x04000577 RID: 1399
		TargetUpdatesSource,
		// Token: 0x04000578 RID: 1400
		BiDirectional
	}
}
