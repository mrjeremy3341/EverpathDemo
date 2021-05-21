using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public BattleManager battleManager;

    public TMP_Text turnText;
    public GameObject[] activeUI;
    public Button attackButton;
    public Button abiliityButton;
    public Button moveButton;

    public GameObject endScreen;
    public TMP_Text victoryText;
    public TMP_Text defeatText;

    public DamageCounter damageCounter;

    private void Update()
    {
        turnText.text = battleManager.turnManager.currentTurn.unitStats.unitName;

        if (battleManager.turnManager.currentTurn.actionMode != BattleUnit.ActionMode.Idle || !battleManager.turnManager.currentTurn.isAlly)
        {
            foreach (GameObject i in activeUI)
            {
                i.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject i in activeUI)
            {
                i.gameObject.SetActive(true);
            }
        }

        if (battleManager.turnManager.currentTurn.actionUsed)
        {
            attackButton.interactable = false;
            abiliityButton.interactable = false;
        }
        else
        {
            attackButton.interactable = true;
            if (battleManager.turnManager.currentTurn.unitStats.currentAP == battleManager.turnManager.currentTurn.unitStats.maxAP)
            {
                abiliityButton.interactable = true;
            }
            else
            {
                abiliityButton.interactable = false;
            }
        }

        if (battleManager.turnManager.currentTurn.moveUsed)
        {
            moveButton.interactable = false;
        }
        else
        {
            moveButton.interactable = true;
        }
    }

    public void NextTurn()
    {
        battleManager.turnManager.currentTurn.battleTurn.EndTurn();
    }

    public void ShowMoveRange()
    {
        BattleUnit currentTurn = battleManager.turnManager.currentTurn;
        currentTurn.waitingForInput = true;
        currentTurn.actionMode = BattleUnit.ActionMode.Move;

        List<GridCell> cellsInRange = AStar.FindMovementRange(currentTurn.currentCell, currentTurn.unitStats.currentMP);
        foreach(GridCell cell in cellsInRange)
        {
            if(cell.currentUnit == null)
            {
                cell.selectable = true;
                cell.spriteRenderer.color = Color.red;
            }
        }
    }

    public void ShowAttackRange()
    {
        BattleUnit currentTurn = battleManager.turnManager.currentTurn;
        currentTurn.waitingForInput = true;
        currentTurn.actionMode = BattleUnit.ActionMode.Attack;

        currentTurn.battleActions.basicAttack.ShowRange();
    }

    public void ShowAbilityRange()
    {
        BattleUnit currentTurn = battleManager.turnManager.currentTurn;
        currentTurn.waitingForInput = true;
        currentTurn.actionMode = BattleUnit.ActionMode.Ability;

        currentTurn.battleActions.unitAbilities.ShowRange();
    }

    public void SpawnDamageCounter(BattleUnit target, int damage)
    {
        Canvas unitCanvas = target.GetComponentInChildren<Canvas>();
        Vector3 pos = new Vector3(0, -85, 0);
        DamageCounter counter = Instantiate<DamageCounter>(damageCounter, unitCanvas.transform, false);
        //counter.transform.position = pos;
        counter.SetDamage(damage);
    }

    public void EndScreen(bool allyVictory)
    {
        endScreen.gameObject.SetActive(true);

        if (allyVictory)
        {
            victoryText.gameObject.SetActive(true);
        }
        else
        {
            defeatText.gameObject.SetActive(true);
        }
    }

    public void RestartBattle()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
