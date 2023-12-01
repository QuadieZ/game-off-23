using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    Scene currentScene;
    int currentSceneIndex;

    string[] allScenes = {
    "Arc1_firstMeet",
    "Arc1_firstEncounter",
    "Arc1_firstCheckpoint",
    "Arc1_secondAreaEntrance",
    "Arc1_secondEncounters",
    "Arc1_secondCheckpoint",
    "Arc1_finale",
    "Arc1_finalCheckpoint",
    "Arc2_entrance",
    "Arc2_encounter",
    "Arc2_firstCheckpoint",
    "Arc2_secondEntrance",
    "Arc2_secondHall",
    "Arc2_secondCheckpoint",
    "Arc2_finalEntrance",
    "Arc2_finalArkView",
    "Arc2_finalHall",
    "Final_Cutscene"
};

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = System.Array.IndexOf(allScenes, currentScene);
        Debug.Log("Active Scene is '" + currentScene.name + "'.");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D()
    {
        SceneManager.LoadScene(allScenes[currentSceneIndex + 1], LoadSceneMode.Additive);
    }
}
