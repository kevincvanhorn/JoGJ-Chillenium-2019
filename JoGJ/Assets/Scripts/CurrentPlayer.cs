using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayer : MonoBehaviour
{
    public GameObject player1, player2, player3;
    private GameObject currentPlayer;

    void Start() {
        player1.SetActive(true); //start with player1 active
        player2.SetActive(false);
        player3.SetActive(false);

        currentPlayer = player1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && !player1.activeSelf) {
            activate(player1);
            deactivate(player2);
            deactivate(player3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && !player2.activeSelf) {
            activate(player2);
            deactivate(player3);
            deactivate(player1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && !player3.activeSelf) {
            activate(player3);
            deactivate(player1);
            deactivate(player2);
        }
    }

    private void deactivate(GameObject p) {
        p.SetActive(false);
    }

    private void activate(GameObject p) {
        p.SetActive(true);
        currentPlayer = p;
    }
}
