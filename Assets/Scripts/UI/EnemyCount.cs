using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public TMP_Text text;

    public DungeonPlayerTracker dungeonPlayerTracker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dungeonPlayerTracker = FindFirstObjectByType<DungeonPlayerTracker>();
        if (!dungeonPlayerTracker) this.enabled = false;
        text.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        int count = dungeonPlayerTracker.currentRoom.enemySpawner.activeEnemies.Count;
        if (count > 0)
        {
            text.text = $"{count} Enemies Left";
        }
        else
        {
            text.text = "";
        }
    }
}
