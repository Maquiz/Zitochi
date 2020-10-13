using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    public Plane[][] boardTiles;
    public int rows, cols;
    private Vector3 startingPosition, currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        //initialize the board
        for (int i = 0; i < rows; i++) {
            for(int j = 0; j< cols; j++){
                boardTiles[i][j] =  new Plane();

            }
            //Move twoards other part of board
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
