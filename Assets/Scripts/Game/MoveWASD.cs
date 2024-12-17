using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveWASD : PlayerController
{
    protected override void HandleInput()
    {
        // Periksa tombol yang ditekan untuk mengatur arah movement
        if (Input.GetKey(KeyCode.W)) MovementY = 1;
        if (Input.GetKey(KeyCode.S)) MovementY = -1;
        if (Input.GetKey(KeyCode.A)) MovementX = -1;
        if (Input.GetKey(KeyCode.D)) MovementX = 1;
    }
}
