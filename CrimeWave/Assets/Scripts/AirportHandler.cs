using Photon.Pun;
using UnityEngine;

public class AirportHandler : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if not null and is player
        if (collision != null && collision.CompareTag("Player"))
        {
            // if has more than 1m
            if(collision.GetComponent<CurrencyHandler>().money >= 1000000)
            {
                collision.GetComponent<PhotonView>().RPC("SetWinner", RpcTarget.All, true);
                PhotonNetwork.LoadLevel("EndScene"); // load 
            }
        }
    }
}
