using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviourPun
{

    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            cam = Camera.main;

            FollowLocalPlayer();
        }
    }

    void FollowLocalPlayer()
    {
        if(photonView.IsMine)
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
