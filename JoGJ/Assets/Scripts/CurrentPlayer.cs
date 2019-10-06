using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayer : MonoBehaviour
{
    public GameObject player1, player2, player3; //between one and three players are allowed
    public GameObject currentPlayer {get; private set; }

    public static CurrentPlayer instance {get; private set;}


    void Awake() {
        if(instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    void Start() {
        player1.SetActive(true); //start with player1 active
        if(player2 != null) player2.SetActive(false);
        if(player3 != null) player3.SetActive(false);

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
        if(player2 != null && Input.GetKeyDown(KeyCode.Alpha2) && !player2.activeSelf) {
            activate(player2);
            deactivate(player3);
            deactivate(player1);
        }
        if(player3 != null && Input.GetKeyDown(KeyCode.Alpha3) && !player3.activeSelf) {
            activate(player3);
            deactivate(player1);
            deactivate(player2);
        }
    }

    private void deactivate(GameObject p) {
        if(p != null) p.SetActive(false);
    }

    private void activate(GameObject p) {
        
        if(p != null) {
            p.SetActive(true);
            currentPlayer = p;
        }
    }
}
