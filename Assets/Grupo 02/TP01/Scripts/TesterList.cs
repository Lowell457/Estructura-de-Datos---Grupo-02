using System.Globalization;
using TMPro;
using UnityEngine;

public class TesterList : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI displayText;
    public TMP_Dropdown typeDropdown; // Options must be in order: "String", "Int", "Float"

    private SimpleList<string> stringList;
    private SimpleList<int> intList;
    private SimpleList<float> floatList;

    private enum ListType { String = 0, Int = 1, Float = 2 }
    private ListType currentType = ListType.String;

    private void Start()
    {
        stringList = new SimpleList<string>();
        intList = new SimpleList<int>();
        floatList = new SimpleList<float>();

        if (typeDropdown != null)
        {
            typeDropdown.onValueChanged.RemoveAllListeners();
            typeDropdown.onValueChanged.AddListener(OnTypeChanged);
            typeDropdown.value = 0;
        }

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
        if (inputField == null) return;

        switch (currentType)
        {
            case ListType.String:
                inputField.contentType = TMP_InputField.ContentType.Standard;
                break;
            case ListType.Int:
                inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
                break;
            case ListType.Float:
                inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
                break;
        }
        inputField.ForceLabelUpdate();
    }

    public void AddItem()
    {
        string text = inputField.text;
        if (string.IsNullOrWhiteSpace(text)) return;

        bool added = false;

        switch (currentType)
        {
            case ListType.String:
                stringList.Add(text);
                added = true;
                break;

            case ListType.Int:
                if (TryParseInt(text, out int intVal))
                {
                    intList.Add(intVal);
                    added = true;
                }
                else
                {
                    Debug.LogWarning($"Invalid integer input: '{text}'");
                }
                break;

            case ListType.Float:
                if (TryParseFloat(text, out float floatVal))
                {
                    floatList.Add(floatVal);
                    added = true;
                }
                else
                {
                    Debug.LogWarning($"Invalid float input: '{text}'");
                }
                break;
        }

        if (added)
        {
            inputField.text = "";
            UpdateDisplay();
        }
    }

    public void RemoveItem()
    {
        string text = inputField.text;
        if (string.IsNullOrWhiteSpace(text)) return;

        bool removed = false;

        switch (currentType)
        {
            case ListType.String:
                removed = stringList.Remove(text);
                break;

            case ListType.Int:
                if (TryParseInt(text, out int intVal))
                    removed = intList.Remove(intVal);
                else
                    Debug.LogWarning($"Invalid integer input: '{text}'");
                break;

            case ListType.Float:
                if (TryParseFloat(text, out float floatVal))
                {
                
                    removed = floatList.Remove(floatVal);

                    if (!removed)
                    {
                        float tol = 1e-5f;
                        for (int i = 0; i < floatList.Count; i++)
                        {
                            float candidate = floatList[i];
                            if (Mathf.Abs(candidate - floatVal) <= tol)
                            {
                                // remove this candidate
                                floatList.Remove(candidate);
                                removed = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid float input: '{text}'");
                }
                break;
        }

        if (!removed) Debug.Log("Element not found in the list.");

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

    private void UpdateDisplay()
    {
        string content = "(empty)";
        switch (currentType)
        {
            case ListType.String: content = stringList.ToString(); break;
            case ListType.Int: content = intList.ToString(); break;
            case ListType.Float: content = floatList.ToString(); break;
        }
        displayText.text = $"(List of {currentType}): {content}";
    }

    private bool TryParseInt(string text, out int value)
    {
      
        if (int.TryParse(text, System.Globalization.NumberStyles.Integer, CultureInfo.CurrentCulture, out value))
            return true;
        return int.TryParse(text, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
    }

    private bool TryParseFloat(string text, out float value)
    {
        
        if (float.TryParse(text, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out value))
            return true;
        return float.TryParse(text, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value);
    }
}
