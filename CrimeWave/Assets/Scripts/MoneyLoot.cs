using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class MoneyLoot : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    private float moneyAmount;
    [SerializeField] private List<Sprite> moneySprites = new List<Sprite>();

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = photonView.InstantiationData;
        if (instantiationData != null && instantiationData.Length > 0)
        {
            GetComponent<SpriteRenderer>().sprite = moneySprites[Random.Range(0, moneySprites.Count)];
            moneyAmount = (float)instantiationData[0];
            Debug.Log("MoneyLoot initialized with amount: " + moneyAmount);
        }
        else
        {
            Debug.LogWarning("MoneyLoot instantiated without data!");
        }
    }

    [PunRPC]
    void RequestPickup(int playerViewID, PhotonMessageInfo info)
    {
        if (!PhotonNetwork.IsMasterClient) return; // Only Master handles pickup

        PhotonView playerPhotonView = PhotonView.Find(playerViewID);
        Debug.Log("RequestPickup called by player with ID: " + playerViewID);
        if (playerPhotonView != null)
        {
            Debug.Log("MasterClient found player with ID: " + playerViewID);

            // Instruct player to add money via RPC (this ensures photonView.IsMine is true)
            playerPhotonView.RPC("AddMoney", RpcTarget.All, moneyAmount);
        }
        else
        {
            Debug.LogWarning("Player photonView not found for ID: " + playerViewID);
        }

        // MasterClient destroys the loot
        PhotonNetwork.Destroy(gameObject);
    }
}
