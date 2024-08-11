using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCaster : MonoBehaviour
{
    [SerializeField] private LayerMask _tileLayer;

    private void Update() {
        if(Mouse.current.leftButton.wasPressedThisFrame){
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, int.MaxValue, _tileLayer);
            if(hit){
                Debug.Log(hit.collider.name);
                if(hit.collider.TryGetComponent(out Tile tile)){
                    tile.CreateMark();
                }
            }
        }
    }
}
