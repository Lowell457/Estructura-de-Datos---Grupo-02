using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SpaceMapManager : MonoBehaviour
{
    public static SpaceMapManager Instance { get; private set; }

    private MyALGraph<string> graph = new MyALGraph<string>();
    private List<string> selectedPlanets = new List<string>();

    [Header("UI References")]
    public TextMeshProUGUI resultText;

    void Awake()
    {
        Instance = this;
        SetupGraph();
    }

    // Build the initial graph connections.
    void SetupGraph()
    {
        graph.AddEdge("Earth", ("Mars", 5));
        graph.AddEdge("Earth", ("Venus", 8));
        graph.AddEdge("Mars", ("Earth", 5));
        graph.AddEdge("Mars", ("Jupiter", 3));
        graph.AddEdge("Mars", ("Venus", 2));
        graph.AddEdge("Venus", ("Mercury", 4));
        graph.AddEdge("Venus", ("Earth", 8));
        graph.AddEdge("Mercury", ("Venus", 4));
        graph.AddEdge("Jupiter", ("Saturn", 10));
        graph.AddEdge("Jupiter", ("Mars", 3));
        graph.AddEdge("Saturn", ("Jupiter", 10));

        Debug.Log("Graph initialized with vertices: " + string.Join(", ", graph.Vertices));
    }

    // Called when a planet is clicked in the scene.
    public void OnPlanetClicked(PlanetNode node)
    {
        if (selectedPlanets.Contains(node.planetName))
        {
            // Deselect it
            selectedPlanets.Remove(node.planetName);
        }
        else
        {
            // Select it
            selectedPlanets.Add(node.planetName);
        }

        UpdateUI();
    }

    // Called by the UI Button: checks if the path is valid.
    public void CheckPath()
    {
        if (selectedPlanets.Count < 2)
        {
            resultText.text = "Select at least two planets!";
            return;
        }

        int totalCost = 0;
        bool valid = true;

        for (int i = 0; i < selectedPlanets.Count - 1; i++)
        {
            string from = selectedPlanets[i];
            string to = selectedPlanets[i + 1];

            if (!graph.ContainsEdge(from, to))
            {
                valid = false;
                break;
            }

            totalCost += graph.GetWeight(from, to);
        }

        if (valid)
            resultText.text = $"Valid path! Total cost: {totalCost}";
        else
            resultText.text = "Invalid path! No connection between some planets.";
    }

    public void ClearSelection()
    {
        selectedPlanets.Clear();
        UpdateUI();
    }

    private void UpdateUI()
    {
        resultText.text = "Selected: " + string.Join(" -> ", selectedPlanets);
    }
}
