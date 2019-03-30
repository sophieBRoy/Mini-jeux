using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public Text points;
    private int pointsValue;
    // Start is called before the first frame update
    void Start()
    {
        pointsValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        points.text = ("Score: " + pointsValue);
    }
    public void updatePoints(int numberOfLines)
    {

        switch (numberOfLines)
        {
            case 1:
                pointsValue += 25;
                break;
            case 2:
                pointsValue += 100;
                break;
            case 3:
                pointsValue += 200;
                break;
            default:
                pointsValue += 400;
                break;
        }
    }
}

