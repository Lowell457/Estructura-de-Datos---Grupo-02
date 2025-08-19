using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLinkedList; // tu namespace con MyList y MyNode

public class VisualListDemo : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab del círculo (arrastrar en Inspector)
    private MyList<GameObject> objetos = new MyList<GameObject>();

    private int currentIndex = 0; // índice actual

    void Start()
    {
        // Crear algunos objetos de arranque
        for (int i = 0; i < 5; i++)
        {
            AddObject();
        }

        HighlightCurrent();
    }

    void Update()
    {
        // Mover derecha
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentIndex < objetos.Count - 1)
            {
                currentIndex++;
                HighlightCurrent();
            }
        }

        // Mover izquierda
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                HighlightCurrent();
            }
        }

        // Agregar un nuevo nodo al final
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddObject();
            currentIndex = objetos.Count - 1; // mover al nuevo
            HighlightCurrent();
        }

        // Eliminar nodo actual
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!objetos.IsEmpty())
            {
                GameObject toRemove = objetos[currentIndex];
                objetos.RemoveAt(currentIndex);

                if (toRemove != null) Destroy(toRemove);

                // Ajustar índice
                if (currentIndex >= objetos.Count) currentIndex = objetos.Count - 1;

                HighlightCurrent();
            }
        }
    }

    void AddObject()
    {
        int i = objetos.Count;
        Vector2 pos = new Vector2(i * 2 - 4, 0); // separarlos en línea
        GameObject obj = Instantiate(circlePrefab, pos, Quaternion.identity);
        objetos.Add(obj);
    }

    void HighlightCurrent()
    {
        // Resetear todos a blanco
        for (int i = 0; i < objetos.Count; i++)
        {
            GameObject obj = objetos[i];
            if (obj != null)
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                sr.color = Color.white;
            }
        }

        // Resaltar el actual
        if (!objetos.IsEmpty() && currentIndex >= 0)
        {
            GameObject actual = objetos[currentIndex];
            if (actual != null)
            {
                SpriteRenderer sr = actual.GetComponent<SpriteRenderer>();
                sr.color = Color.red;
            }
        }
    }

    // Mostrar info en pantalla
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.black;

        string info = $"Índice actual: {currentIndex} / {objetos.Count - 1}\n" +
                      $"Total de elementos: {objetos.Count}\n" +
                      $"Controles: ← → moverse | A agregar | R eliminar";

        GUI.Label(new Rect(10, 10, 500, 100), info, style);
    }
}
