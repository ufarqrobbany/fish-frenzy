using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveArrows : PlayerController
{
    protected override void HandleInput()
    {
        // Periksa tombol yang ditekan untuk mengatur arah movement
        if (Input.GetKey(KeyCode.UpArrow)) MovementY = 1;
        if (Input.GetKey(KeyCode.DownArrow)) MovementY = -1;
        if (Input.GetKey(KeyCode.LeftArrow)) MovementX = -1;
        if (Input.GetKey(KeyCode.RightArrow)) MovementX = 1;
    }
}
