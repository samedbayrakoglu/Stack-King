using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InGameScreen inGameScreen;

    public TapToPlayScreen tapToPlayScreen;

    public LevelEndScreen levelEndScreen;

    [HideInInspector] public Screen currentScreen;




    public void UpdateLevelNo(int no)
    {
        tapToPlayScreen.UpdateLevelNo(no);
        inGameScreen.UpdateLevelNo(no);
    }

    public void UpdateCoinAmount(int amount)
    {
        tapToPlayScreen.UpdateCoinAmount(amount);
        inGameScreen.UpdateCoinAmount(amount);
        levelEndScreen.UpdateCoinAmount(amount);
    }

}
