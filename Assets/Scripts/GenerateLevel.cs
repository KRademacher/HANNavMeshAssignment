using UnityEngine;
using UnityEngine.AI;

public class GenerateLevel : MonoBehaviour
{
    public GameObject player;
    public GameObject wall;

    public NavMeshSurface[] navMeshSurfaces;

    public int width = 18;
    public int height = 18;

    private bool playerSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMaze();

        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();
        foreach (var surface in navMeshSurfaces)
        {
            surface.BuildNavMesh();
        }
    }

    private void GenerateMaze()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (var platform in platforms)
        {
            Vector3 platformPosition = platform.transform.position;
            int xPos = (int)platformPosition.x;
            int zPos = (int)platformPosition.z;

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
}