using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text missionText; // asignar en Inspector
    [SerializeField] private Button completeMissionButton; // asignar en Inspector

    [Header("Misiones Iniciales")]
    [SerializeField] private List<string> initialMissions = new List<string>(); // aparecerá en el Inspector

    private MyQueue<string> missionQueue = new MyQueue<string>();

    void Start()
    {
        // Cargar misiones desde la lista al MyQueue
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
