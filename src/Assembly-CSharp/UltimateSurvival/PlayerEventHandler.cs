using System;
using System.Collections.Generic;
using UltimateSurvival.Building;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005CB RID: 1483
	public class PlayerEventHandler : EntityEventHandler
	{
		// Token: 0x04002A35 RID: 10805
		public Value<Vector3> LastSleepPosition = new Value<Vector3>(Vector3.zero);

		// Token: 0x04002A36 RID: 10806
		public Value<float> Stamina = new Value<float>(100f);

		// Token: 0x04002A37 RID: 10807
		public Value<float> Thirst = new Value<float>(100f);

		// Token: 0x04002A38 RID: 10808
		public Value<float> Hunger = new Value<float>(100f);

		// Token: 0x04002A39 RID: 10809
		public Value<int> Defense = new Value<int>(0);

		// Token: 0x04002A3A RID: 10810
		public Value<Vector2> MovementInput = new Value<Vector2>(Vector2.zero);

		// Token: 0x04002A3B RID: 10811
		public Value<Vector2> LookInput = new Value<Vector2>(Vector2.zero);

		// Token: 0x04002A3C RID: 10812
		public Value<Vector3> LookDirection = new Value<Vector3>(Vector3.zero);

		// Token: 0x04002A3D RID: 10813
		public Value<bool> ViewLocked = new Value<bool>(false);

		// Token: 0x04002A3E RID: 10814
		public Value<float> MovementSpeedFactor = new Value<float>(1f);

		// Token: 0x04002A3F RID: 10815
		public Queue<Transform> NearLadders = new Queue<Transform>();

		// Token: 0x04002A40 RID: 10816
		public Value<RaycastData> RaycastData = new Value<RaycastData>(null);

		// Token: 0x04002A41 RID: 10817
		public Attempt InteractOnce = new Attempt();

		// Token: 0x04002A42 RID: 10818
		public Value<bool> InteractContinuously = new Value<bool>(false);

		// Token: 0x04002A43 RID: 10819
		public Value<bool> IsCloseToAnObject = new Value<bool>(false);

		// Token: 0x04002A44 RID: 10820
		public Attempt<SavableItem, bool> ChangeEquippedItem = new Attempt<SavableItem, bool>();

		// Token: 0x04002A45 RID: 10821
		public Value<SavableItem> EquippedItem = new Value<SavableItem>(null);

		// Token: 0x04002A46 RID: 10822
		public Attempt DestroyEquippedItem = new Attempt();

		// Token: 0x04002A47 RID: 10823
		public Attempt<SleepingBag> StartSleeping = new Attempt<SleepingBag>();

		// Token: 0x04002A48 RID: 10824
		public Activity Sleep = new Activity();

		// Token: 0x04002A49 RID: 10825
		public Activity Walk = new Activity();

		// Token: 0x04002A4A RID: 10826
		public Attempt AttackOnce = new Attempt();

		// Token: 0x04002A4B RID: 10827
		public Attempt AttackContinuously = new Attempt();

		// Token: 0x04002A4C RID: 10828
		public Value<bool> CanShowObjectPreview = new Value<bool>(false);

		// Token: 0x04002A4D RID: 10829
		public Attempt PlaceObject = new Attempt();

		// Token: 0x04002A4E RID: 10830
		public Value<float> ScrollValue = new Value<float>(0f);

		// Token: 0x04002A4F RID: 10831
		public Value<BuildingPiece> SelectedBuildable = new Value<BuildingPiece>(null);

		// Token: 0x04002A50 RID: 10832
		public Activity SelectBuildable = new Activity();

		// Token: 0x04002A51 RID: 10833
		public Attempt<float> RotateObject = new Attempt<float>();

		// Token: 0x04002A52 RID: 10834
		public Activity Run = new Activity();

		// Token: 0x04002A53 RID: 10835
		public Activity Crouch = new Activity();

		// Token: 0x04002A54 RID: 10836
		public Activity Jump = new Activity();

		// Token: 0x04002A55 RID: 10837
		public Activity Aim = new Activity();
	}
}
