using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private List<Texture> menuImgs = new List<Texture>();

    [SerializeField] private RawImage bg;
    [SerializeField] private float _x, _y;

    private float timer = 20.0f;
    private float transitionTime = 20.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bg.uvRect = new Rect(bg.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, bg.uvRect.size);

        if(Time.time > timer)
        {
            int newIdx = menuImgs.IndexOf(bg.texture) + 1;

            if (newIdx >= menuImgs.Count)
                newIdx = 0;

            bg.texture = menuImgs[newIdx];
            
            timer = Time.time + transitionTime;
        }
    }
}
