using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    //une liste de liste qui modélise la grille de jeu
    private List<List<int>> lineList = new List<List<int>>()
    {
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>(),
        new List<int>()
    };
    public int width = 10;
    public int height = 16;
    public int positionX = 4;
    public int positionY = 8;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            createLines(i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        popLine();
    }

    //permet de remplir une ligne de la grille de 0
    public void createLines(int p_i)
    {
        for(int y = 0; y < width;  y++)
        {
            lineList[p_i].Add(0);
        }
    }

    //permet de changer la valeur d'une case de la grille de 0 à 1 quand une pièce l'occupe
    public void fillTile(int posX, int posY)
    {
        int tile = (int)Mathf.Round(posX);
        int line = (int)Mathf.Round(posY);
        lineList[line+positionY][tile+positionX] = 1;
    }

    //retourne vrai si une case en dessous d'une case à une position x posX et une position y posY est vide
    public bool checkNextLine(int posX, int posY)
    {
        //on rammène la position donnée par l'image à la position dans la grille (car le coin gauche de la grille n'est pas aligné sur le x=0 et y=0)
        int tile = (int)Mathf.Round(posX) + positionX;
        int line = (int)Mathf.Round(posY) + positionY;
        bool retour = true;
        if (lineList[line-1][tile] == 1)
        {
            retour = false;
        }
        return retour;
    }
    //retourne vrai si une ligne de position y posY est complète (aucune case vide)
    public bool lineIsFull(int posY)
    {
        bool retour = true;
        for(int i = 0; i < width; i++)
        {
            //si une case est vide, la ligne n'est pas pleine: on retourne false
            if(lineList[posY][i] == 0)
            {
                retour = false;
            }
        }
        return retour;
    }
    //retourne vrai si une case à position x posX et positiony PosY est vide
    public bool checkCurrentLine(int posX, int posY)
    {
        int tile = posX + positionX;
        int line = posY + positionY;
        bool retour = true;
        if (lineList[line][tile] == 1)
        {
            retour = false;
        }
        return retour;
    }

    //retourne une liste des positions y des lignes pleines, les enlève de la liste lineList puis crée le même nombre de nouvelles lignes pour les remplacer
    public List<int> popLine()
    {
        List<int> poppedLines = new List<int>();
        for(int i = lineList.Count-1; i >= 0 ; i--)
        {
            //si la ligne est pleine, on l'ajoute à la liste qui sera retournée par la fonction puis on la supprime puis
            // on en crée une nouvelle à la fin (qui est le sommet de la grille)
            if (lineIsFull(i))
            {
                poppedLines.Add(i);
                lineList.Remove(lineList[i]);
                lineList.Add(new List<int>());
                createLines(15);
            }
        }
        return poppedLines;
    }

    //fonction de test permettant de vérifier l'état de la grille en tout temps dans la console
    public void printGrid()
    {
        for(int i = 0; i < lineList.Count; i++)
        {
            for(int y = 0; y < width; y++)
            {
                Debug.Log(lineList[i][y]);
            }
        }
    }


}
