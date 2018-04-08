using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour {

    int[] list_of_execution = { 0, 0, 0 };
    public Button button;

    public void ImBeingPressed(int button_number)
    {
        list_of_execution[button_number] = 1;
        if (SumArray(list_of_execution) == 3) button.interactable = true;
    }

    public int SumArray(int[] toBeSummed)
    {
        int sum = 0;
        foreach (int item in toBeSummed)
        {
            sum += item;
        }
        return sum;
    }
    public void ChangeLevel()
    {
        SceneManager.LoadScene("Hub");
    }
}
