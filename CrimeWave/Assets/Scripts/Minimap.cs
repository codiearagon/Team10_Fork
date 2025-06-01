using UnityEngine;
using UnityEngine.SceneManagement;

public class Minimap : MonoBehaviour
{
    [SerializeField] private GameObject minimap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minimap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                minimap.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.V))
            {
                minimap.SetActive(false);
            }
        }
    }
}
