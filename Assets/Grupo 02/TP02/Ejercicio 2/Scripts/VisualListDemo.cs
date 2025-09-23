using System;
using UnityEngine;
using TMPro;
using MyLinkedList; // Use own MyList implementation

public class VisualListDemo : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputField;
    public TextMeshProUGUI displayText;
    public TMP_Dropdown typeDropdown; // "String", "Int", "Float"

    [Header("Visual Nodes")]
    public GameObject nodePrefab; // Prefab with a square/circle + TextMeshPro
    public Transform nodesParent; // Container where prefabs are created
    public float spacing = 2.5f;  // Spacing between nodes

    private MyList<string> stringList;
    private MyList<int> intList;
    private MyList<float> floatList;

    private enum ListType { String = 0, Int = 1, Float = 2 }
    private ListType currentType = ListType.String;

    void Start()
    {
        stringList = new MyList<string>();
        intList = new MyList<int>();
        floatList = new MyList<float>();

        typeDropdown.onValueChanged.RemoveAllListeners();
        typeDropdown.onValueChanged.AddListener(OnTypeChanged);
        typeDropdown.value = 0;

        UpdateInputFieldContentType();
        UpdateDisplay();
    }

    private void OnTypeChanged(int index)
    {
        currentType = (ListType)index;
        inputField.text = "";
        UpdateInputFieldContentType();
        UpdateDisplay();
    }

    private void UpdateInputFieldContentType()
    {
        switch (currentType)
        {
            case ListType.String:
                inputField.contentType = TMP_InputField.ContentType.Standard; break;
            case ListType.Int:
                inputField.contentType = TMP_InputField.ContentType.IntegerNumber; break;
            case ListType.Float:
                inputField.contentType = TMP_InputField.ContentType.DecimalNumber; break;
        }
        inputField.ForceLabelUpdate();
    }

    // =========================
    // BUTTONS
    // =========================
    public void AddItem()
    {
        string text = inputField.text;
        if (string.IsNullOrWhiteSpace(text)) return;

        switch (currentType)
        {
            case ListType.String: stringList.Add(text); break;
            case ListType.Int: if (int.TryParse(text, out int i)) intList.Add(i); break;
            case ListType.Float: if (float.TryParse(text, out float f)) floatList.Add(f); break;
        }

        inputField.text = "";
        UpdateDisplay();
    }

    public void RemoveItem()
    {
        string text = inputField.text;
        if (string.IsNullOrWhiteSpace(text)) return;

        switch (currentType)
        {
            case ListType.String: stringList.Remove(text); break;
            case ListType.Int: if (int.TryParse(text, out int i)) intList.Remove(i); break;
            case ListType.Float: if (float.TryParse(text, out float f)) floatList.Remove(f); break;
        }

        inputField.text = "";
        UpdateDisplay();
    }

    public void ClearList()
    {
        switch (currentType)
        {
            case ListType.String: stringList.Clear(); break;
            case ListType.Int: intList.Clear(); break;
            case ListType.Float: floatList.Clear(); break;
        }
        UpdateDisplay();
    }

    // =========================
    // VISUAL RENDERING
    // =========================
    private void UpdateDisplay()
    {
        // Wipes old visual nodes
        foreach (Transform child in nodesParent)
            Destroy(child.gameObject);

        // Draws nodes according to current type
        switch (currentType)
        {
            case ListType.String: DrawList(stringList); displayText.text = stringList.ToString(); break;
            case ListType.Int: DrawList(intList); displayText.text = intList.ToString(); break;
            case ListType.Float: DrawList(floatList); displayText.text = floatList.ToString(); break;
        }
    }

    private void DrawList<T>(MyList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject node = Instantiate(nodePrefab, nodesParent);
            node.transform.localPosition = new Vector3(i * spacing, 0, 0);

            TextMeshProUGUI tmp = node.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null) tmp.text = list[i].ToString();
        }
    }
}
