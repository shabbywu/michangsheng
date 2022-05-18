using System;
using System.Collections.Generic;
using UltimateSurvival.Building;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200088D RID: 2189
	public class PlayerEventHandler : EntityEventHandler
	{
		// Token: 0x040032CB RID: 13003
		public Value<Vector3> LastSleepPosition = new Value<Vector3>(Vector3.zero);

		// Token: 0x040032CC RID: 13004
		public Value<float> Stamina = new Value<float>(100f);

		// Token: 0x040032CD RID: 13005
		public Value<float> Thirst = new Value<float>(100f);

		// Token: 0x040032CE RID: 13006
		public Value<float> Hunger = new Value<float>(100f);

		// Token: 0x040032CF RID: 13007
		public Value<int> Defense = new Value<int>(0);

		// Token: 0x040032D0 RID: 13008
		public Value<Vector2> MovementInput = new Value<Vector2>(Vector2.zero);

		// Token: 0x040032D1 RID: 13009
		public Value<Vector2> LookInput = new Value<Vector2>(Vector2.zero);

		// Token: 0x040032D2 RID: 13010
		public Value<Vector3> LookDirection = new Value<Vector3>(Vector3.zero);

		// Token: 0x040032D3 RID: 13011
		public Value<bool> ViewLocked = new Value<bool>(false);

		// Token: 0x040032D4 RID: 13012
		public Value<float> MovementSpeedFactor = new Value<float>(1f);

		// Token: 0x040032D5 RID: 13013
		public Queue<Transform> NearLadders = new Queue<Transform>();

		// Token: 0x040032D6 RID: 13014
		public Value<RaycastData> RaycastData = new Value<RaycastData>(null);

		// Token: 0x040032D7 RID: 13015
		public Attempt InteractOnce = new Attempt();

		// Token: 0x040032D8 RID: 13016
		public Value<bool> InteractContinuously = new Value<bool>(false);

		// Token: 0x040032D9 RID: 13017
		public Value<bool> IsCloseToAnObject = new Value<bool>(false);

		// Token: 0x040032DA RID: 13018
		public Attempt<SavableItem, bool> ChangeEquippedItem = new Attempt<SavableItem, bool>();

		// Token: 0x040032DB RID: 13019
		public Value<SavableItem> EquippedItem = new Value<SavableItem>(null);

		// Token: 0x040032DC RID: 13020
		public Attempt DestroyEquippedItem = new Attempt();

		// Token: 0x040032DD RID: 13021
		public Attempt<SleepingBag> StartSleeping = new Attempt<SleepingBag>();

		// Token: 0x040032DE RID: 13022
		public Activity Sleep = new Activity();

		// Token: 0x040032DF RID: 13023
		public Activity Walk = new Activity();

		// Token: 0x040032E0 RID: 13024
		public Attempt AttackOnce = new Attempt();

		// Token: 0x040032E1 RID: 13025
		public Attempt AttackContinuously = new Attempt();

		// Token: 0x040032E2 RID: 13026
		public Value<bool> CanShowObjectPreview = new Value<bool>(false);

		// Token: 0x040032E3 RID: 13027
		public Attempt PlaceObject = new Attempt();

		// Token: 0x040032E4 RID: 13028
		public Value<float> ScrollValue = new Value<float>(0f);

		// Token: 0x040032E5 RID: 13029
		public Value<BuildingPiece> SelectedBuildable = new Value<BuildingPiece>(null);

		// Token: 0x040032E6 RID: 13030
		public Activity SelectBuildable = new Activity();

		// Token: 0x040032E7 RID: 13031
		public Attempt<float> RotateObject = new Attempt<float>();

		// Token: 0x040032E8 RID: 13032
		public Activity Run = new Activity();

		// Token: 0x040032E9 RID: 13033
		public Activity Crouch = new Activity();

		// Token: 0x040032EA RID: 13034
		public Activity Jump = new Activity();

		// Token: 0x040032EB RID: 13035
		public Activity Aim = new Activity();
	}
}
