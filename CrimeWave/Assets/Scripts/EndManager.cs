using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text finishText;
    [SerializeField] private GameObject restartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // disable all players
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerObj = (GameObject)player.TagObject;

            playerObj.GetComponent<SpriteRenderer>().enabled = false;
            playerObj.GetComponent<PlayerController>().enabled = false;
            playerObj.GetComponent<Rigidbody2D>().simulated = false;
            playerObj.GetComponent<PlayerGun>().enabled = false;
            playerObj.GetComponent<PerkTimer>().enabled = false;

            if (playerObj.GetComponent<PlayerManager>().winner)
            {
                finishText.text = player.NickName + " won the game.";
            }
        }

        Cursor.visible = true;
        PlayerManager.localPlayerInstance = null;

        if (!PhotonNetwork.IsMasterClient)
            restartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerObj = (GameObject)player.TagObject;
            PhotonNetwork.Destroy(playerObj);
            player.TagObject = null;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master client loading level for all players"); // ADDED: Debug info
            PhotonNetwork.LoadLevel("Game");
        }
    }

    public void LeaveGame()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerObj = (GameObject)player.TagObject;
            PhotonNetwork.Destroy(playerObj);
            player.TagObject = null;
        }

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left room, reconnecting to master server"); // ADDED: Debug info
        SceneManager.LoadScene("Lobby");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // everybody leaves when host leaves
        LeaveGame();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // host leaves when anybody leaves
        LeaveGame();
    }
}
