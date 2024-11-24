using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum BattleStates { START, KHYETURN, LILOIDDTURN, TSAINETURN,ENEMYTURN, WIN, LOSE }
public class BattleSystem : MonoBehaviour
{
    public BattleStates state;

    public GameObject enemyPrefab;
    public Transform enemyBattleStation;
    public GameObject enemyInfoBackground;
    public GameObject enemyInfoPrefab;

    public BattleHUD enemyBattleHUD;

    public Unit Khye;
    public Unit Li_Loid;
    public Unit Tsaine;

    public BattleHUD khyeHUD;
    public BattleHUD Li_LoidHUD;
    public BattleHUD TsaineHUD;

    public TextMeshProUGUI turnIndicator;

    public TextMeshProUGUI flavorText;
    Unit enemyUnit;

    int partyMembersLeft = 3;
    public void Start()
    {
        Khye.hud = khyeHUD;
        Li_Loid.hud = Li_LoidHUD;
        Tsaine.hud = TsaineHUD;

        khyeHUD.SetHUD(Khye);
        TsaineHUD.SetHUD(Tsaine);
        Li_LoidHUD.SetHUD(Li_Loid);

        state = BattleStates.START;
        StartCoroutine(SetupBattle());
    }
    IEnumerator SetupBattle()
    {
        GameObject enemy = CreateEnemy();
        enemyUnit = enemy.GetComponent<Unit>();
        //Connecting enemy to healthbar
        enemyUnit.hud = Instantiate(enemyInfoPrefab, enemyInfoBackground.transform).GetComponent<BattleHUD>();
        enemyBattleHUD.SetHUD(enemyUnit);

        flavorText.text = "A battle begins...";
        yield return new WaitForSeconds(2);

        KhyeTurn();
    }
    void KhyeTurn()
    {
        state = BattleStates.KHYETURN;
        flavorText.text = $"What will Khye do?";
    }
    void TsaineTurn()
    {
        flavorText.text = $"What will Tsaine do?";
    }
    void LiLoidd()
    {
        flavorText.text = $"What will Li-Loidd do";
    }
    public void OnAttackButton()
    {
        Debug.Log($"This Code runs, {state}");
        switch (state)
        {
            case BattleStates.KHYETURN:
                if (Khye.currentHP != 0)
                {
                    StartCoroutine(PlayerAttack(Khye));
                }
                state = BattleStates.TSAINETURN;
                
                break;
            
            case BattleStates.TSAINETURN:

                if (Tsaine.currentHP != 0)
                {
                    StartCoroutine(PlayerAttack(Tsaine));
                }
                
                state = BattleStates.LILOIDDTURN;
                break;
            case BattleStates.LILOIDDTURN:
                if (Li_Loid.currentHP != 0)
                {
                    StartCoroutine(PlayerAttack(Li_Loid));
                }
                StartCoroutine(EnemyAttack(enemyUnit));
                state = BattleStates.ENEMYTURN;
                break;
            default:
                return;
        }

    }
    IEnumerator PlayerAttack(Unit unit)
    {
        bool isDead = enemyUnit.TakeDamage(unit.damage);
        Debug.Log("This code runs");
        yield return new WaitForSeconds(2);
        if (isDead)
        {
            Destroy(enemyUnit.gameObject);
            state = BattleStates.WIN;
        }
    }
    IEnumerator EnemyAttack(Unit unit)
    {
        for (int i = 0; i < 3; i++)
        {
            int attacked = Random.Range(1, 3);
            Unit attackedUnit = null;
            switch (attacked)
            {
                case 1:
                    attackedUnit = Khye;
                    flavorText.text = $"{unit.name} attacks Khye for {unit.damage} damage!";
                    break;
                case 2:
                    attackedUnit = Tsaine;
                    flavorText.text = $"{unit.name} attacks Khye for {unit.damage} damage!";
                    break;
                case 3:
                    attackedUnit = Li_Loid;
                    flavorText.text = $"{unit.name} attacks Khye for {unit.damage} damage!";
                    break;
            }
            Debug.Log("working");
            bool isDead = attackedUnit.TakeDamage(unit.damage);
            yield return new WaitForSeconds(2);
            if (isDead)
            {
                partyMembersLeft--;
                if (partyMembersLeft == 0)
                {
                    state = BattleStates.LOSE;
                }
            }
        }
        state = BattleStates.KHYETURN;

    }
    IEnumerator Win()
    {
        flavorText.text = $"You win!!!";
        yield return new WaitForSeconds(2);
    }
    IEnumerator Lose()
    {
        flavorText.text = $"You lose...";
        yield return new WaitForSeconds(2);
    }

    public GameObject CreateEnemy()
    {
        return Instantiate(enemyPrefab, enemyBattleStation);
    }
}
