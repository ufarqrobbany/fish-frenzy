using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TombolKredit : MonoBehaviour
{
    public GameObject popupPanel;  // Panel yang akan dijadikan popup
    public Button[] otherButtons;  // Tombol-tombol lain yang ingin disembunyikan

    public void ShowPopup()
    {
        // Tampilkan panel popup
        popupPanel.SetActive(true);

        // Sembunyikan tombol-tombol lain
        foreach (Button button in otherButtons) {
            button.gameObject.SetActive(false);
        }
    }

    public void HidePopup()
    {
        // Sembunyikan panel popup
        popupPanel.SetActive(false);

        // Tampilkan kembali tombol-tombol lain
        foreach (Button button in otherButtons) {
            button.gameObject.SetActive(true);
        }
    }
}
