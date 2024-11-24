using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleStates { START, PLAYERTURN, ENEMYTURN, WIN, LOSE }
public class BattleSystem : MonoBehaviour
{
    public BattleStates state;
    public void Start()
    {
        state = BattleStates.START;
        SetupBattle();
    }
    void SetupBattle()
    {

    }
}
