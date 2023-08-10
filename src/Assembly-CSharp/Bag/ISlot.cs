namespace Bag;

public interface ISlot
{
	void SetSlotData(object data);

	void SetAccptType(CanSlotType slotType);

	void SetNull();

	void InitUI();

	void UpdateUI();
}
