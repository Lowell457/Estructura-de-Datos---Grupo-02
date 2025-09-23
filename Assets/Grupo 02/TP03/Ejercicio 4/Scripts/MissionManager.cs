using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MissionManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text missionText; // assign in the Inspector
    [SerializeField] private Button completeMissionButton; // assign in the Inspector

    [Header("Misiones Iniciales")]
    [SerializeField] private List<string> initialMissions = new List<string>(); // will appear in the Inspector

    private MyQueue<string> missionQueue = new MyQueue<string>();

    void Start()
    {
        // Load missions from List MyQueue
        foreach (var mission in initialMissions)
        {
            missionQueue.Enqueue(mission);
        }

        ShowNextMission();
        completeMissionButton.onClick.AddListener(CompleteMission);
    }

    private void ShowNextMission()
    {
        if (missionQueue.TryPeek(out string mission))
        {
            missionText.text = "Objetivo: " + mission;
        }
        else
        {
            missionText.text = "¡Juego completado!";
            completeMissionButton.gameObject.SetActive(false);
        }
    }

    public void CompleteMission()
    {
        if (missionQueue.TryDequeue(out string _))
        {
            ShowNextMission();
        }
    }
}
