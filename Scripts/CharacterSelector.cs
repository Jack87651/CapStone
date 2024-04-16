using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Array of all character prefabs
    public string gameplaySceneName = "Gameplay"; // Scene to load after selection
    private float lastClickTime;
    private float doubleClickTime = 0.25f; // Time interval for double-click detection
    private GameObject lastClicked; // Track the last clicked GameObject

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit GameObject is a child of this folder
                if (hit.transform.IsChildOf(transform))
                {
                    if (hit.collider.gameObject == lastClicked && Time.time - lastClickTime <= doubleClickTime)
                    {
                        // Double click detected on the same object
                        SelectCharacter(hit.collider.gameObject);
                    }
                    lastClicked = hit.collider.gameObject;
                    lastClickTime = Time.time;
                }
            }
        }
    }

    private void SelectCharacter(GameObject characterOption)
    {
        int index = System.Array.IndexOf(characterPrefabs, characterOption.GetComponent<CharacterOption>().characterPrefab);
        if (index != -1)
        {
            GameObject selectedCharacter = Instantiate(characterPrefabs[index], Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(selectedCharacter); // Keep the character in the next scene
            SceneManager.LoadScene(gameplaySceneName); // Transition to the gameplay scene
        }
    }
}

public class CharacterOption : MonoBehaviour
{
    public GameObject characterPrefab; // Assigned individually per character option in the Inspector
}
