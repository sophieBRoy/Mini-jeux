using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    public string game;
    private ChangeScene myAccess;
    // Start is called before the first frame update
    void Start()
    {
        myAccess = GameObject.Find("Main Camera").GetComponent<ChangeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        myAccess.ChangeMenuScene(game);
    }
}
