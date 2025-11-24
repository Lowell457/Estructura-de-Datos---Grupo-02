using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject scoreEntryPrefab;
    public TMP_InputField nameInput;
    public TMP_InputField scoreInput;
    public Button addButton;

    public Button preOrderButton;
    public Button inOrderButton;
    public Button postOrderButton;
    public Button levelOrderButton;

    public int cantidadScoresAleatorios = 100;

    private AVLTree avlTree = new AVLTree();

    private enum SortMode { InOrder, PreOrder, PostOrder, LevelOrder }
    private SortMode currentSort = SortMode.InOrder;

    private void Start()
    {
        // SUBSCRIBES HERE
        avlTree.OnNodeCreated += HandleNodeCreated;

        // random score generation
        for (int i = 0; i < cantidadScoresAleatorios; i++)
        {
            int score = Random.Range(0, 10000);
            string name = "Player" + Random.Range(1, 999);
            avlTree.Insert(score, name);
        }

        addButton.onClick.AddListener(AddNewScore);

        preOrderButton.onClick.AddListener(() => {
            currentSort = SortMode.PreOrder;
            var list = avlTree.PreOrder();
            Debug.Log("[PreOrder] " + string.Join(", ", list.ConvertAll(n => $"{n.PlayerName}:{n.Score}")));
            UpdateUI();
        });
        inOrderButton.onClick.AddListener(() => {
            currentSort = SortMode.InOrder;
            var list = avlTree.InOrder();
            Debug.Log("[InOrder] " + string.Join(", ", list.ConvertAll(n => $"{n.PlayerName}:{n.Score}")));
            UpdateUI();
        });

        postOrderButton.onClick.AddListener(() => { 
            currentSort = SortMode.PostOrder; 
            var list = avlTree.PostOrder();
            Debug.Log("[PostOrder] " + string.Join(", ", list.ConvertAll(n => $"{n.PlayerName}:{n.Score}")));
            UpdateUI(); });
        
        levelOrderButton.onClick.AddListener(() => { 
            currentSort = SortMode.LevelOrder; 
            var list = avlTree.LevelOrder();
            Debug.Log("[LevelOrder] " + string.Join(", ", list.ConvertAll(n => $"{n.PlayerName}:{n.Score}")));
            UpdateUI(); });

        UpdateUI();
    }

    private void HandleNodeCreated(NodeTp7 node)
    {
        Debug.Log($"[EVENT] Node created: {node.PlayerName} - {node.Score}");
    }

    private void AddNewScore()
    {
        string playerName = nameInput.text;
        int score = int.Parse(scoreInput.text);

        // Checks if name already exists
        if (avlTree.ContainsName(playerName))
        {
            Debug.LogWarning($"Cannot add score. The name '{playerName}' already exists.");
            return;
        }

        avlTree.Insert(score, playerName);

        Debug.Log($"[LOG] Added: {playerName} ({score})");

        nameInput.text = "";
        scoreInput.text = "";

        UpdateUI();
    }


    void UpdateUI()
    {
        // Cleans panels
        for (int i = contentPanel.childCount - 1; i >= 0; i--)
            Destroy(contentPanel.GetChild(i).gameObject);

        List<NodeTp7> list;

        switch (currentSort)
        {
            case SortMode.PreOrder: list = avlTree.PreOrder(); break;
            case SortMode.PostOrder: list = avlTree.PostOrder(); break;
            case SortMode.LevelOrder: list = avlTree.LevelOrder(); break;
            default: list = avlTree.InOrder(); break; // Descending order (from biggest to smallest)
        }

        // Show ALL scores
        foreach (var node in list)
        {
            GameObject entry = Instantiate(scoreEntryPrefab, contentPanel);
            entry.transform.localScale = Vector3.one;

            TMP_Text txt = entry.GetComponent<TMP_Text>()
                ?? entry.GetComponentInChildren<TMP_Text>(true);

            if (txt != null)
                txt.text = $"{node.PlayerName} - {node.Score}";
        }
    }
}
