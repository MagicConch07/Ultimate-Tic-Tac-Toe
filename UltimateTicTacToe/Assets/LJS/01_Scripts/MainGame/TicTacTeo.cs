using UnityEngine;

public enum Turn{
    PlayerX, PlayerO
}

public class TicTacTeo : MonoSingleton<TicTacTeo>
{
    [HideInInspector] public Turn _currentTurn;

    private void Start() {
        _currentTurn = (Turn)Random.Range(0, 2);
        Debug.Log(_currentTurn);
    }
}
