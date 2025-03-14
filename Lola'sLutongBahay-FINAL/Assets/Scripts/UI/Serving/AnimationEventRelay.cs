using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public GameObject eggpanel, tapapanel, garlicpanel, steamedricepanel, servepanel;

    public void TriggerChildSound(string panelName)
    {
        GameObject targetPanel = panelName switch
        {
            "egg" => eggpanel,
            "tapa" => tapapanel,
            "garlic" => garlicpanel,
            "rice" => steamedricepanel,
            "serve" => servepanel,
            _ => null
        };

        if (targetPanel != null)
        {
            var soundScript = targetPanel.GetComponent<ObjectPanelSound>();
            soundScript?.PlayObjectSound();
        }
        else
        {
            Debug.LogWarning($"No panel found for: {panelName}");
        }
    }
}