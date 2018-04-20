using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;





public class Game_Controller : MonoBehaviour {

    public SpriteRenderer sprite;
    int[] list_of_execution = { 0, 0, 0 };
    public Button button_0;
    public Button button_1;
    public Button button_2;
    public Button button_capture;
    private List<Button> buttons;


    public void Start()
    {
        Debug.Log(button_0);
        buttons.Add(button_0);
        buttons.Add(button_1);
        buttons.Add(button_2);

    }
    public void ImBeingPressed(int button_number)
    {
        list_of_execution[button_number] = 1;
        if (SumArray(list_of_execution) == 3) button_capture.interactable = true;
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

    public void Animation(int button_number)
    {

        list_of_execution[button_number] = 1;
        if (SumArray(list_of_execution) == 3) button_capture.interactable = true;

        bool[] list_state = new bool[4];
        int i = 0;
        foreach (Button but in buttons)
        {
            list_state[i] = but.IsInteractable();
            but.interactable = false;
        }

        switch (button_number)
        {
            case 0:
                //animation 0
                for (int y = 0; i > 56; i++)
                {
                    sprite.flipX = !sprite.flipX;

                }
                break;
            case 1:
                //animation 1
                break;
            case 2:
                //animation 2
                break;
            case 3:
                //animation 3
                break;
        }
        i = 0;
        foreach (Button button in buttons)
        {
            button.interactable = list_state[i];
        }
    }
}
