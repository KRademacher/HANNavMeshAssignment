using UnityEngine;
using UnityEngine.AI;

public class LevelCreator : MonoBehaviour
{
    public GameObject player;
    public GameObject wall;

    public NavMeshSurface surface;

    public int width = 20;
    public int height = 20;

    private bool playerSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();

        surface.BuildNavMesh();
    }

    private void GenerateLevel()
    {
        Vector3 parentPosition = gameObject.transform.parent.transform.position;
        int xPos = (int)parentPosition.x;
        int zPos = (int)parentPosition.z;

        // Loop over the grid
        for (int x = 0; x <= width; x += 2)
        {
            for (int y = 0; y <= height; y += 2)
            {
                // Should we place a wall?
                if (Random.value > .7f)
                {
                    // Spawn a wall
                    Vector3 pos = new Vector3((x + xPos) - width / 2f, 1f, (y + zPos) - height / 2f);
                    Instantiate(wall, pos, Quaternion.identity, transform);
                }
                else if (player && !playerSpawned) // Should we spawn a player?
                {
                    // Spawn the player
                    Vector3 pos = new Vector3((x + xPos) - width / 2f, 1.25f, (y + zPos) - height / 2f);
                    Instantiate(player, pos, Quaternion.identity);
                    playerSpawned = true;
                }
            }
        }
    }
}