using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionsManager : MonoBehaviour {

    /*Transitions
     *This effect has different steps with different time intervals
     * 1. Freeze frame
     * 2. Change screen, change glitch color setting
     * 3. Change post processing profile to Transition PP
     * 4. Display transition text
     * 5. Display loadint text
     * 6. Display the scene it is transitioning to
     * 7. End transition
     */

    public Camera mainCamera;
    public Canvas mainCanvas;
    public Canvas transitionCanvas;
    public Text transitionTitle;
    public Text loadingText;
    public Text sceneTitle;

    public float defaultGlitchAmount;
    public float initialGlitchAmount;
    public float glitchAmount;
    GlitchEffect glitch;

    // Use this for initialization
    void Start () {
        glitch = mainCamera.GetComponent<GlitchEffect>();
    }

    IEnumerator StartSequence()
    {
        glitch.flipIntensity = initialGlitchAmount;
        yield return new WaitForSeconds(1f);
        StartCoroutine(Glitch(initialGlitchAmount, 0.4f));
        StartCoroutine(DisplayText(transitionTitle));
    }

    IEnumerator Glitch (float amount, float length)
    {
        glitch.flipIntensity = amount;
        yield return new WaitForSeconds(length);
        glitch.flipIntensity = defaultGlitchAmount;
    }

    IEnumerator DisplayText(Text text) {
        text.enabled = true;
        yield return new WaitForSeconds(3f);
        text.enabled = false;
        StartCoroutine(AnimateLoadingText());
    }

    IEnumerator AnimateLoadingText() {
        //Loading text
        loadingText.enabled = true;
        loadingText.text = "READING";

        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(0.8f);
            for (int j = 0; j < 3; j++) {
                loadingText.text += ".";
                yield return new WaitForSeconds(0.8f);
            }
            loadingText.text = "READING";
        }

        yield return new WaitForSeconds(2f);
        loadingText.enabled = false;

        //Glitch
        StartCoroutine(Glitch(initialGlitchAmount, 0.5f));

        //Scene text
        yield return new WaitForSeconds(0.7f);
        sceneTitle.enabled = true;
        yield return new WaitForSeconds(4f);
    }

    public void Transition() {

        glitch.colorIntensity = 0;

        //Display transition canvas
        mainCanvas.enabled = false;
        transitionCanvas.enabled = true;

        //Display transition text sequence
        StartCoroutine(StartSequence());
    }
}
