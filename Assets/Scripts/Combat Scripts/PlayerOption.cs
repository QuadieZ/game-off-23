using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerOption : Button
{
    public CombatAction option;
    public TMP_Text mytext;

    public void PlaceOption(CombatAction newOption)
    {
        option = newOption;
        mytext = GetComponentInChildren<TMP_Text>();
        mytext.text = option.actionName;
        //we check whether the player is in a combo and if the current action is allowed
        switch ((CombatManager.Instance.playerChar.comboing, option.speed))
        {
            case (true, CombatAction.ActionSpeed.starter):
                interactable = false;
                break;
            case (false, CombatAction.ActionSpeed.starter):
                interactable = true;
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (true, CombatAction.ActionSpeed.combo):
                interactable = true;
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (false, CombatAction.ActionSpeed.combo):
                interactable = false;
                break;
            case (true, CombatAction.ActionSpeed.finisher):
                interactable = true;
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (false, CombatAction.ActionSpeed.finisher):
                interactable = false;
                break;
            case (true, CombatAction.ActionSpeed.standalone):
                interactable = false;
                break;
            case (false, CombatAction.ActionSpeed.standalone):
                interactable = true;
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (true, CombatAction.ActionSpeed.flexible):
                interactable = true;
                onClick.AddListener(delegate { OptionSelected(); });
                break;
            case (false, CombatAction.ActionSpeed.flexible):
                interactable = true;
                onClick.AddListener(delegate { OptionSelected(); });
                break;
        }


    }

    public void RemoveOption()
    {
        onClick.RemoveListener (delegate { OptionSelected(); });
        mytext = null;

    }

    public void OptionSelected()
    {
        CombatManager.Instance.playerChar.GetAction(option);
    }


}
