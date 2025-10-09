using UnityEngine;
using UnityEngine.EventSystems;

public class ClickArmy : MonoBehaviour
{
    GameData gameData;
    public Transform selectedArmy;

    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask hitLayers;

    void Start()
    {
        gameData = GetComponent<GameData>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitLayers))
            {
                Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
                if (hit.collider.tag != "army")
                {
                    Debug.Log("Hit object isn't an \"army\" tag");
                    return;
                }

                selectedArmy = hit.collider.GetComponent<Transform>();
                Army selectedArmyComponent = hit.collider.GetComponent<Army>();

                if (selectedArmyComponent.owner != gameData.playingAsTag)
                {
                    Debug.Log($"Selected: {selectedArmyComponent.owner} // Playing as: {gameData.playingAsTag}");
                    return;
                }

                selectedArmy.Find("BackgroundImg").GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                if (selectedArmy) selectedArmy.Find("BackgroundImg").GetComponent<SpriteRenderer>().color = Color.blue;
                selectedArmy = null;
            }
        }
    }
}