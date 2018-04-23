using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Game_Controller : MonoBehaviour {

    public SpriteRenderer player;
    public SpriteRenderer ennemy;
	int[] list_of_execution = { 0, 0, 0, 1};
    public Button button_0;
    public Button button_1;
    public Button button_2;
    public GameObject button_capture;
    private List<Button> buttons;
    public List<SpriteRenderer> coeurs;
    private int nb_coeurs;
    public Text text;
    public string hit_efficient;
    public string hit_inefficient;
    public String victory_text;
    public System.Random rnd;

    public void Start()
    {
        rnd = new System.Random();
        nb_coeurs = coeurs.Count;
        buttons = new List<Button>();
        buttons.Add(button_0);
        buttons.Add(button_1);
        buttons.Add(button_2);

    }

    /*
    public void ImBeingPressed(int button_number)
    {
        list_of_execution[button_number] = 1;
        if (SumArray(list_of_execution) == 3) button_capture.interactable = true;
    }
    */

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
        SceneManager.LoadScene("transitionVersHub");
    }

    public void StartAnimation(int button_number)
    {
        StartCoroutine(Animation(button_number));
    }

    private IEnumerator Animation(int button_number)
    {
        if (button_number == 3) { text.text = victory_text; } else if (list_of_execution[button_number] != 1) { text.text = hit_efficient; } else { text.text = hit_inefficient; }// C'est très éfficace / Cela ne marche plus, il a évolué!

        bool[] list_state = new bool[4];
        int i = 0;
        foreach (Button but in buttons)
        {
            list_state[i] = but.IsInteractable();
            but.interactable = false;
        }

        int rd = rnd.Next(2, 6);

        switch (button_number)
        {
            case 0:
                //animation 0
                
                for (int y = 0; y < 2*rd; y++)
                {
                    player.flipY = !player.flipY;
                    yield return new WaitForSeconds(0.2f);
                }
                break;
            case 1:
                for (int y = 0; y < 2 * rd; y++)
                {
                    ennemy.flipX = !ennemy.flipX;
                    yield return new WaitForSeconds(0.2f);
                }
                //animation 1
                break;
            case 2:
                for (int y = 0; y < 2 * rd; y++)
                {
                    ennemy.flipY = !ennemy.flipY;
                    yield return new WaitForSeconds(0.2f);
                }
                //animation 2
                break;
            case 3:
                for (int y = 0; y < 2 * rd; y++)
                {
                    player.flipY = !player.flipY;
                    yield return new WaitForSeconds(0.2f);
                }
                ChangeLevel();
                //animation 3
                break;
        }
        i = 0;
        foreach (Button button in buttons)
        {
            button.interactable = list_state[i];
        }

        if (list_of_execution[button_number] != 1 ) { Damaged(); }

        list_of_execution[button_number] = 1;

        if (SumArray(list_of_execution) == 4)
        {
            button_capture.SetActive(true);
        }

        text.text = null;
    }

    private void Damaged()
    {
        coeurs[nb_coeurs - 1].color = Color.white;
        coeurs.RemoveAt(nb_coeurs - 1);
        nb_coeurs -= 1;
    }
}
