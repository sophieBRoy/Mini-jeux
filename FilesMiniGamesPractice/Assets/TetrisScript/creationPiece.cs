using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creationPiece : MonoBehaviour
{
    public List<GameObject> pieces;
    public GameObject piece1;
    public GameObject piece2;
    public GameObject piece3;
    public GameObject piece4;
    public GameObject piece5;
    public GameObject piece6;
    public GameObject piece7;
    private GameObject piece;

    // Start is called before the first frame update
    void Start()
    {
        //on ajoute les pièces à une liste pour permettre une initialisation d'une pièce au hasard par la suite
        pieces = new List<GameObject>();
        pieces.Add(piece1);
        pieces.Add(piece2);
        pieces.Add(piece3);
        pieces.Add(piece4);
        pieces.Add(piece5);
        pieces.Add(piece6);
        pieces.Add(piece7);
        createPiece();
    }

    //fonction permettant l'instantiation d'une pièce
    public void createPiece()
    {
        int index = Random.Range(0, pieces.Count);
        piece = pieces[index];
        Instantiate(piece);
        piece.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }
   

    
}
