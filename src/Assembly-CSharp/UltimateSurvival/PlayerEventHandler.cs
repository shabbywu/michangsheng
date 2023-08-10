using System.Collections.Generic;
using UltimateSurvival.Building;
using UnityEngine;

namespace UltimateSurvival;

public class PlayerEventHandler : EntityEventHandler
{
	public Value<Vector3> LastSleepPosition = new Value<Vector3>(Vector3.zero);

	public Value<float> Stamina = new Value<float>(100f);

	public Value<float> Thirst = new Value<float>(100f);

	public Value<float> Hunger = new Value<float>(100f);

	public Value<int> Defense = new Value<int>(0);

	public Value<Vector2> MovementInput = new Value<Vector2>(Vector2.zero);

	public Value<Vector2> LookInput = new Value<Vector2>(Vector2.zero);

	public Value<Vector3> LookDirection = new Value<Vector3>(Vector3.zero);

	public Value<bool> ViewLocked = new Value<bool>(initialValue: false);

	public Value<float> MovementSpeedFactor = new Value<float>(1f);

	public Queue<Transform> NearLadders = new Queue<Transform>();

	public Value<RaycastData> RaycastData = new Value<RaycastData>(null);

	public Attempt InteractOnce = new Attempt();

	public Value<bool> InteractContinuously = new Value<bool>(initialValue: false);

	public Value<bool> IsCloseToAnObject = new Value<bool>(initialValue: false);

	public Attempt<SavableItem, bool> ChangeEquippedItem = new Attempt<SavableItem, bool>();

	public Value<SavableItem> EquippedItem = new Value<SavableItem>(null);

	public Attempt DestroyEquippedItem = new Attempt();

	public Attempt<SleepingBag> StartSleeping = new Attempt<SleepingBag>();

	public Activity Sleep = new Activity();

	public Activity Walk = new Activity();

	public Attempt AttackOnce = new Attempt();

	public Attempt AttackContinuously = new Attempt();

	public Value<bool> CanShowObjectPreview = new Value<bool>(initialValue: false);

	public Attempt PlaceObject = new Attempt();

	public Value<float> ScrollValue = new Value<float>(0f);

	public Value<BuildingPiece> SelectedBuildable = new Value<BuildingPiece>(null);

	public Activity SelectBuildable = new Activity();

	public Attempt<float> RotateObject = new Attempt<float>();

	public Activity Run = new Activity();

	public Activity Crouch = new Activity();

	public Activity Jump = new Activity();

	public Activity Aim = new Activity();
}
