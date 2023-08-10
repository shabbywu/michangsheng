using KBEngine;

public interface IFightEventProcessor
{
	void SetAvatar(Avatar player, Avatar monstar);

	void OnStartFight();

	void OnUpdateBuff();

	void OnUpdateHP();

	void OnUpdateLingQi();

	void OnUpdateRound();
}
