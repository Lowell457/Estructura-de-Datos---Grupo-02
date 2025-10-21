using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MySetUIController : MonoBehaviour
{
    [Header("Set type Configuration")]
    [Tooltip("If true, use MySetArray; if not, use MySetList")]
    public bool useArrayImplementation = false;

    [Header("UI References - Set A")]
    public TMP_InputField inputA;
    public TMP_Text outputA;
    public Button addAButton;
    public Button removeAButton;
    public Button clearAButton;
    public Button showAButton;

    [Header("UI References - Set B")]
    public TMP_InputField inputB;
    public TMP_Text outputB;
    public Button addBButton;
    public Button removeBButton;
    public Button clearBButton;
    public Button showBButton;

    [Header("Operations between Sets")]
    public Button unionButton;
    public Button intersectButton;
    public Button differenceButton;
    public TMP_Text outputResult;

    private MySet<int> setA;
    private MySet<int> setB;

    void Start()
    {
        // Create Sets according to the selected implementation 
        if (useArrayImplementation)
        {
            setA = new MySetArray<int>();
            setB = new MySetArray<int>();
        }
        else
        {
            setA = new MySetList<int>();
            setB = new MySetList<int>();
        }

        // Asign listeners for SetA Buttons
        addAButton.onClick.AddListener(() => OnAddClicked(setA, inputA, outputA));
        removeAButton.onClick.AddListener(() => OnRemoveClicked(setA, inputA, outputA));
        clearAButton.onClick.AddListener(() => OnClearClicked(setA, outputA));
        showAButton.onClick.AddListener(() => OnShowClicked(setA, outputA, "A"));

        // Asign listeners for SetB Buttons
        addBButton.onClick.AddListener(() => OnAddClicked(setB, inputB, outputB));
        removeBButton.onClick.AddListener(() => OnRemoveClicked(setB, inputB, outputB));
        clearBButton.onClick.AddListener(() => OnClearClicked(setB, outputB));
        showBButton.onClick.AddListener(() => OnShowClicked(setB, outputB, "B"));

        // Asign listeners for operations
        unionButton.onClick.AddListener(OnUnionClicked);
        intersectButton.onClick.AddListener(OnIntersectClicked);
        differenceButton.onClick.AddListener(OnDifferenceClicked);

        // Initialize texts
        UpdateOutput(outputA, setA);
        UpdateOutput(outputB, setB);
        outputResult.text = "Resultado: { }";
    }

    void OnAddClicked(MySet<int> set, TMP_InputField input, TMP_Text output)
    {
        if (int.TryParse(input.text, out int value))
        {
            set.Add(value);
            input.text = "";
            UpdateOutput(output, set);
        }
    }

    void OnRemoveClicked(MySet<int> set, TMP_InputField input, TMP_Text output)
    {
        if (int.TryParse(input.text, out int value))
        {
            set.Remove(value);
            input.text = "";
            UpdateOutput(output, set);
        }
    }

    void OnClearClicked(MySet<int> set, TMP_Text output)
    {
        set.Clear();
        UpdateOutput(output, set);
    }

    void OnShowClicked(MySet<int> set, TMP_Text output, string setName)
    {
        Debug.Log($"Conjunto {setName}: {set}");
        UpdateOutput(output, set);
    }

    void OnUnionClicked()
    {
        MySet<int> result = setA.Union(setB);
        Debug.Log("Unión (A U B): " + result);
        outputResult.text = "Unión (A U B): " + result;
    }

    void OnIntersectClicked()
    {
        MySet<int> result = setA.Intersect(setB);
        Debug.Log("Intersección (A n B): " + result);
        outputResult.text = "Intersección (A n B): " + result;
    }

    void OnDifferenceClicked()
    {
        MySet<int> result = setA.Difference(setB);
        Debug.Log("Diferencia (A - B): " + result);
        outputResult.text = "Diferencia (A - B): " + result;
    }

    void UpdateOutput(TMP_Text text, MySet<int> set)
    {
        text.text = set.ToString() +
                    $"\nCardinalidad: {set.Cardinality()}" +
                    $"\n¿Vacío?: {(set.IsEmpty() ? "Sí" : "No")}";
    }
}
