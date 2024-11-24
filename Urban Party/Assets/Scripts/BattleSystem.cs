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

    public TextMeshProUGUI flavorText;
    Unit enemyUnit;
    public void Start()
    {
        state = BattleStates.START;
        StartCoroutine(SetupBattle());
    }
    IEnumerator SetupBattle()
    {

        GameObject enemy = CreateEnemy();
        enemyUnit = enemy.GetComponent<Unit>();
        //Connecting enemy to healthbar
        enemyUnit.hud = Instantiate(enemyInfoPrefab, enemyInfoBackground.transform).GetComponent<BattleHUD>();
        enemyBattleHUD.SetHUD(enemyPrefab.GetComponent<Unit>());

        flavorText.text = "A battle begins...";
        yield return new WaitForSeconds(2);

        KhyeTurn();
    }
    void KhyeTurn()
    {
        state = BattleStates.KHYETURN;
        flavorText.text = $"What will Khye do?";
    }
    public void OnAttackButton()
    {
        Debug.Log($"This Code runs, {state}");
        switch (state)
        {
            case BattleStates.KHYETURN:
                StartCoroutine(PlayerAttack(Khye));
                state = BattleStates.TSAINETURN;
                break;
            
            case BattleStates.TSAINETURN:
                StartCoroutine(PlayerAttack(Tsaine));
                state = BattleStates.LILOIDDTURN;
                break;
            case BattleStates.LILOIDDTURN:
                StartCoroutine(PlayerAttack(Li_Loid));
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
        }
    }
    IEnumerator EnemyAttack(Unit unit)
    {
        int attacked = Random.Range(1, 3);

        bool isDead = 
        yield return new WaitForSeconds(2);

    }


    public GameObject CreateEnemy()
    {
        return Instantiate(enemyPrefab, enemyBattleStation);
    }
}
