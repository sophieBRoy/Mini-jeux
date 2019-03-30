using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballMotion : MonoBehaviour
{
    public float speed = 5;
    private int score1;
    private int score2;
    public Text scoreL;
    public Text scoreR;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        score1 = 0;
        score2 = 0;
        scoreL.text = "Score: " + score1.ToString();
        scoreR.text = "Score: " + score2.ToString();
    }

    private void Update()
    {
        if(score1 >= 10 || score2 >= 10)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "RacketL")
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 dir = new Vector2(1, y).normalized;
            speed += 1;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
        if (col.gameObject.name == "RacketR")
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 dir = new Vector2(-1, y).normalized;
            if(speed < 20)
            { 
            speed += 1;
            }
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
        if(col.gameObject.name == "WallLeft")
        {
            float y = 0;
            setScore(scoreR);
            Vector2 dir = new Vector2(-1, y).normalized;
            transform.position = new Vector3(0, 0, 0);
            speed = 5;
            GetComponent<Rigidbody2D>().velocity = dir * speed;

        }
        if(col.gameObject.name == "WallRight")
        {
            float y = 0;
            setScore(scoreL);
            Vector2 dir = new Vector2(1, y).normalized;
            transform.position = new Vector3(0, 0, 0);
            speed = 5;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
        void setScore(Text player)
        {
            if (player == scoreL)
            {
                score1 += 1;
                scoreL.text = "Score: " + score1.ToString();
            }
            if(player == scoreR)
            {
                score2 += 1;
                scoreR.text = scoreR.text = "Score: " + score2.ToString();
            }
        }
    }
}
