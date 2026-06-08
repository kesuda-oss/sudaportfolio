using TMPro;
using UnityEngine;

public class GoalMessage : MonoBehaviour
{
    public GameObject messageUI;
  
   

    public void Show()
    {   
        messageUI.SetActive(true);
        
    }
}