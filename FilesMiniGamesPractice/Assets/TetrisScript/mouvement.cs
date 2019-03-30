using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement : MonoBehaviour
{
    public KeyCode left;
    public KeyCode right;
    public KeyCode down;
    public KeyCode up;
    private Transform bloc1;
    private Transform bloc2;
    private Transform bloc3;
    private Transform bloc4;
    private int speed = 1;
    private float counter = 0;
    private GameObject pieceMaker;
    public int widthPos = 5;
    public int widthNeg = -4;
    public int height = -8;
    GameObject cameraObject;
    Tiles grid;
    Points pointsSystem;
    private double posIni;

    static private List<List<GameObject>> placedCubes = new List<List<GameObject>>()
    {
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>(),
        new List<GameObject>()
    };

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = GameObject.Find("Main Camera");
        grid = GameObject.Find("grid").GetComponent<Tiles>();
        pointsSystem = GameObject.Find("Points").GetComponent<Points>();
    }
    //fonction appellée juste avant l'initialisation
    private void Awake()
    {
        posIni = transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        //déplacement à gauche de la pièce
        if (Input.GetKeyDown(left))
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            //si le déplacement est en dehors de la grille ou chevauche une autre pièce, on rammène la pièce à sa position initiale
            if(boundCheck() || checkStates("left"))
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            }
        }
        //déplacement à droite de la pièce
        if (Input.GetKeyDown(right))
        {
            transform.position += new Vector3(1, 0, 0);
            //si le déplacement est en dehors de la grille ou chevauche une autre pièce, on rammène la pièce à sa position initiale
            if (boundCheck() || checkStates("right"))
            {
                transform.position += new Vector3(- 1, 0, 0);
            }
        }
        //fait tomber la pièce et déplacement vers le bas
        if (Input.GetKeyDown(down) || Time.time - counter >= speed)
        {
            //tant que la pièce n'a pas touché le fond, on décrémente le y
            if (!checkStates("bottom"))
            {
                transform.position += new Vector3(0, -1, 0);
                counter = Time.time;
            }
        }
        //rotation de la pièce
        if (Input.GetKeyDown(up))
        {
            transform.eulerAngles += new Vector3(0, 0, 270);
            //si la rotation fait sortir la pièce de la grille, on annule le déplacement
            if (boundCheck() || checkStates("top"))
            {
                transform.eulerAngles += new Vector3(0, 0, -270);
            }
        }
        spamNew();
    }

    //retourne vrai si la position de la pièce en mouvement est en dehors de la grille
    private bool boundCheck()
    {
        bool retour = false;
        for(int i = 0; i < 4; i++)
        {
            if((int)transform.GetChild(i).position.x > widthPos || (int)transform.GetChild(i).position.x < widthNeg)
            {
                retour = true;
            }
        }
        return retour;
    }

    //retourne vrai si la pièce chevauche une autre après un certain déplacement donné 
    public bool checkStates(string type)
    {
        bool retour = false;
        if (type == "bottom")
        {
            for (int i = 0; i < 4; i++)
            {
                if ((int)transform.GetChild(i).position.y < height || !grid.checkNextLine((int)Mathf.Round(transform.GetChild(i).position.x), (int)Mathf.Round(transform.GetChild(i).position.y)))
                {
                    retour = true;
                }
            }
        }
        if(type == "top" ||type ==  "left" ||type == "right")
        {
            for (int i = 0; i < 4; i++)
            {
                if (!grid.checkCurrentLine((int)Mathf.Round(transform.GetChild(i).position.x), (int)Mathf.Round(transform.GetChild(i).position.y)))
                {
                    retour = true;
                }
            }
        }
        return retour;
    }

    //désactive la pièce précédente et appelle le scrip "creationPiece" pour en créer une nouvelle 
    public void spamNew()
    {
        //on laisse un certain temps avant d'éxécuter le scripte pour laisser le temps au joueur de placer sa pièce
        if(checkStates("bottom") && Time.time - counter >= speed)
        {
            if(transform.position.y == posIni)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            else
            {
                fillGrid();
                enabled = false;
                cameraObject.GetComponent<creationPiece>().createPiece();
            }   
        }
    }

    //rempli la grille du domaine dans le scripte "Tiles" et détruit les images des lignes pleines au besoin
    public void fillGrid()
    {
        //ramène les pôsitions a des int où le coin gauche de la grille est égal à 0
        for (int i = 0; i < 4; i++)
        {
            grid.fillTile((int)Mathf.Round(transform.GetChild(i).position.x), (int)Mathf.Round(transform.GetChild(i).position.y));
            placedCubes[(int)Mathf.Round(transform.GetChild(i).position.y) + 8].Add(transform.GetChild(i).gameObject);
        }
        List<int> fullLines = grid.popLine();
        //si certaines lignes sont pleines...
        if(!(fullLines.Count == 0))
        {
            for(int i = 0; i < fullLines.Count; i++)
            {
                //on détruit les images des blocs qui composent cette ligne
                for(int y = 0; y < 10; y++)
                {
                    Destroy(placedCubes[fullLines[i]][y]);               
                }
                //on enlève la ligne de bloc de la liste
                placedCubes.Remove(placedCubes[fullLines[i]]);
                //pour chaque liste au dessus de la liste qu'on vient de détruire, on fait descendre les pièces de 1
                for(int y = fullLines[i]; y < placedCubes.Count; y++)
                {
                    for (int z = 0; z < placedCubes[y].Count; z++)
                    {
                            placedCubes[y][z].transform.position += new Vector3(0, -1, 0);
                    }
                }
                //on crée une nouvelle ligne pour remplacer l'ancienne
                placedCubes.Add(new List<GameObject>());
            }
            //ajoute le nombre de points en fonction du nombre de lignes 
            pointsSystem.updatePoints(fullLines.Count);
        }
    }
    
   
}
