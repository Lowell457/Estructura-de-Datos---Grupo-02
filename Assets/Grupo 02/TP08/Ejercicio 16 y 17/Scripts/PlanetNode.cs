using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetNode : MonoBehaviour, IPointerClickHandler
{
    public string planetName;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked planet: " + planetName);
        SpaceMapManager.Instance.OnPlanetClicked(this);
    }
}
