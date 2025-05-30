using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerListUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerListPrefab;
    [SerializeField] private GameObject playerListContainer;

    private List<GameObject> playersListObjects = new List<GameObject>();

    private bool tabHeld = false;
    private float updateInterval = 1.5f;

    void Start()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            GameObject newPlayer = Instantiate(playerListPrefab, transform.position, Quaternion.identity, playerListContainer.transform);
            playersListObjects.Add(newPlayer);
        }

        if (SceneManager.GetActiveScene().name == "Game")
            playerListContainer.SetActive(false);
        else if (SceneManager.GetActiveScene().name == "EndScene")
        {
            playerListContainer.SetActive(true);
            UpdateList();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                playerListContainer.SetActive(true);
                tabHeld = true;
                StartCoroutine(UpdatePlayerList());
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                playerListContainer.SetActive(false);
                tabHeld = false;
                StopCoroutine(UpdatePlayerList());
            }
        }
    }

    IEnumerator UpdatePlayerList()
    {
        while (tabHeld)
        {
            UpdateList();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    void UpdateList()
    {
        for (int i = 0; i < playersListObjects.Count; i++)
        {
            GameObject player = (GameObject)PhotonNetwork.PlayerList[i].TagObject;

            if (player == null) // avoid crashing game when pressing tab during player load
                continue;

            GameObject playerList = playersListObjects[i]; // player listing

            // set sprite and color
            playerList.transform.GetChild(0).GetComponent<Image>().sprite = player.GetComponent<SpriteRenderer>().sprite;
            playerList.transform.GetChild(0).GetComponent<Image>().color = player.GetComponent<SpriteRenderer>().color;

            // set name
            playerList.transform.GetChild(1).GetComponent<TMP_Text>().text = player.GetComponent<PhotonView>().Owner.NickName;

            // set money have
            playerList.transform.GetChild(2).GetComponent<TMP_Text>().text = "$" + player.GetComponent<CurrencyHandler>().money.ToString();

            Debug.Log(player.GetComponent<PhotonView>().Owner.NickName + ": " + player.GetComponent<CurrencyHandler>().money);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject ply = Instantiate(playerListPrefab, transform.position, Quaternion.identity, playerListContainer.transform);
        playersListObjects.Add(ply);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Destroy(playersListObjects[playersListObjects.Count - 1]);
        playersListObjects.RemoveAt(playersListObjects.Count - 1);
    }
}
