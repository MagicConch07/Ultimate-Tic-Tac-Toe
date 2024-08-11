using UnityEngine;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    public void CreateMark(){
        switch(TicTacTeo.Instance._currentTurn){
            case Turn.PlayerX:
            break;
            case Turn.PlayerO:
            break;
        }
    }
}
