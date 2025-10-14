using UnityEngine;
using TMPro;

public class AVLInputHandler : MonoBehaviour
{
    public TMP_InputField inputField;              // InputField en la UI
    public MyVisualAVLTreeWithLines tree;          // Referencia al árbol

    public void OnAddNodeButton()
    {
        if (int.TryParse(inputField.text, out int value))
        {
            tree.Insert(value);                     // Inserta nodo en el AVL
            inputField.text = "";                   // Limpia el campo de texto
        }
    }
}
