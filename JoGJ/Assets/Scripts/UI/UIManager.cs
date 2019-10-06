using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text DisplayText;

    public Image EnergyBar_01, EnergyBar_02, EnergyBar_03;
    private PFirstPersonController Player_01, Player_02, Player_03;

    private CurrentPlayer currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DisplayText.text = "";

        currentPlayer = FindObjectOfType<CurrentPlayer>();
        if (currentPlayer)
        {
            Player_01 = currentPlayer.player1.GetComponent<PFirstPersonController>();
            Player_02 = currentPlayer.player2.GetComponent<PFirstPersonController>();
            Player_03 = currentPlayer.player3.GetComponent<PFirstPersonController>();
        }
    }

    public void drainBattery(PFirstPersonController Player, float batteryPercentage)
    {
        if (!Player) return;
        if (Player && batteryPercentage >= 0.0f && batteryPercentage <= 1.0f)
        {
            if (Player == Player_01 && EnergyBar_01)
                EnergyBar_01.gameObject.transform.localScale = new Vector3(batteryPercentage,1,1);
            else if (Player == Player_02 && EnergyBar_02)
                EnergyBar_02.gameObject.transform.localScale = new Vector3(batteryPercentage, 1, 1);
            else if (Player == Player_03 && EnergyBar_03)
                EnergyBar_03.gameObject.transform.localScale = new Vector3(batteryPercentage, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
