using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static bool Loadfinish = false;

    public GameObject FinishUI;

    /* private void OnTriggerEnter(Collider other)
     {
         GameObject.Find("Character").SendMessage("FinishLine");
     }*/

    private void OnTriggerEnter(Collider other)
    {
        if (Loadfinish)
        {
            finish();
        }
        else
        {
            op();
        }

        void finish()
        {
            FinishUI.SetActive(false);
            Loadfinish = false;
        }

        void op()
        {
            FinishUI.SetActive(true);
            Time.timeScale = 0f;
            Loadfinish = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.Find("Character").SendMessage("FinishLine");
        }
    }
}
