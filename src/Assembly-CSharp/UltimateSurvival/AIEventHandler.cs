namespace UltimateSurvival;

public class AIEventHandler : EntityEventHandler
{
	public Value<bool> IsHungry = new Value<bool>(initialValue: false);

	public Value<float> LastFedTime = new Value<float>(0f);

	public Activity Patrol = new Activity();

	public Activity Chase = new Activity();

	public Activity Attack = new Activity();

	public Activity RunAway = new Activity();
}
