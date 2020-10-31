/*
	Copyright (C) 2020 Anarres

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/> 
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStepper : MonoBehaviour
{

    public GameObject[] stepTexts;
    public Button continueButton;
    public Button playButton;
    public Button omitButton;

    public GameObject menuPanel;

    public LevelLoader levelLoader;

    private Animator animator;
    private int currentStep;

    void Start()
    {
        currentStep = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayGame();
        }
    }

    public void NextStep()
    {
        StartCoroutine(FadePanel(true));
        animator.SetTrigger("Next");
    }

    public void PlayGame()
    {
        levelLoader.FadeAndLoadScene("Gameplay");
    }

    void onStepAnimationEnd()
    {
        stepTexts[currentStep].SetActive(false);
        currentStep++;

        stepTexts[currentStep].SetActive(true);
        StartCoroutine(FadePanel(false));

        if (currentStep == stepTexts.Length - 1)
        {
            playButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
            omitButton.gameObject.SetActive(false);
        }
    }


    IEnumerator FadePanel(bool fadeAway)
    {
        CanvasGroup canvas = menuPanel.GetComponent<CanvasGroup>();

        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * 2)
            {
                // set color with i as alpha
                canvas.alpha = i;
                yield return null;
            }
            menuPanel.SetActive(false);
            yield return null;
        }
        // fade from transparent to opaque
        else
        {
            canvas.alpha = 0;
            menuPanel.SetActive(true);
            yield return null;
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime * 2)
            {
                // set color with i as alpha
                canvas.alpha = i;
                yield return null;
            }
        }

    }
}
