using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    // Prototype.
    // 1. Passive Item이 들어오면 -> Sprite를 저장한다.
    // 2. Status Data를 상황에 맞게 저장한다.
    // 3. 스탯마다 덧셈과 곱셈을 따로 저장해놓은 뒤, Player에게 적용한다.

    public static EquipManager Instance;

    List<Sprite> itemSprites;

    private void Awake()
    {
        Instance = this;
        itemSprites = new List<Sprite>();
    }

    public void GetPassiveItem(ItemBlueprint itemBlueprint)
    {
        InteractableItemBluePrint passive = itemBlueprint as InteractableItemBluePrint;
        itemSprites.Add(passive.itemSprite);

        foreach(StatusContainer data in passive.valueList)
        {
            // TODO
        }
    }

    private void InputData((float, float) statusType )
    {
        // TODO
    }

}
