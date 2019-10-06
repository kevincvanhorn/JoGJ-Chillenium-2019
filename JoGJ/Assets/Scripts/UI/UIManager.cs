using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text DisplayText;
    private Color textColor;
    private Color invisibleTextColor;

    public Image EnergyBar_01, EnergyBar_02, EnergyBar_03;
    private PFirstPersonController Player_01, Player_02, Player_03;

    private CurrentPlayer currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DisplayText.text = "";
        textColor = DisplayText.color;
        invisibleTextColor = textColor;
        invisibleTextColor.a = 0f;

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

    public void DisplayMessage(string message) {
        DisplayText.text = message;
        DisplayText.color = textColor;
        //lerp the alpha to 0
        float alphaTime = message.Length / 5f + 3f;
        StartCoroutine(lerpText(alphaTime, message));
    }

    IEnumerator lerpText(float totalTime, string message) {
        yield return new WaitForSeconds(totalTime); //wait this long before fading out

        float timePassed = 0;
        while(DisplayText.color.a > 0f) {
            if(message != DisplayText.text) break; //stop lerping if a new message pops up
            DisplayText.color = Color.Lerp(textColor, invisibleTextColor, timePassed/2f);
            yield return null; //wait one frame
            timePassed += Time.deltaTime;
        }
    }
}
