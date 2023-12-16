using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADSN
{
    public class TileSpawner : MonoBehaviour
    {

        [SerializeField]
        private int tileStartCount = 10;
        [SerializeField]
        private int minimumStraightTiles = 3;
        [SerializeField]
        private int maximumStraightTiles = 15;
        [SerializeField]
        private GameObject startingTile;
        [SerializeField]
        private List<GameObject> turnTiles;
        [SerializeField]
        private List<GameObject> obstacles;

        private Vector3 currentTileLocation = Vector3.zero;
        private Vector3 currentTileDirection = Vector3.forward;
        private GameObject prevTile;

        private List<GameObject> currentTiles;
        private List<GameObject> currentObstacles;

        private void Start()
        {
            currentTiles = new List<GameObject>();
            currentObstacles = new List<GameObject>();

            /// Making sure to get a very random number each time through millisecond
            Random.InitState(System.DateTime.Now.Millisecond);

            for (int i = 0; i < tileStartCount; ++i)
            {
                SpawnTile(startingTile.GetComponent<Tile>());
                //Debug.Log(i);
            }

            /// Spawn a turn tile
            SpawnTile(SelectRandomGameObjectFromList(turnTiles).GetComponent<Tile>());
            
        }

        private void SpawnTile(Tile tile, bool spawnObstacle = false)
        {
            /// Rotate turn tiles in the expected angle
            Quaternion newTileRotation = tile.gameObject.transform.rotation * Quaternion.LookRotation(currentTileDirection, Vector3.up);

            /// Saving tile detail for reference
            prevTile = GameObject.Instantiate(tile.gameObject, currentTileLocation, newTileRotation);

            /// Adding tile detail to current list
            currentTiles.Add(prevTile);

            /// Spawn obstacle
            if (spawnObstacle)
            {
                SpawnObstacle();
            }

            /// Setting next tile location only if current tile is STRAIGHT
            if (tile.type == TileType.STRAIGHT)
            {
                currentTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentTileDirection);
            }
            //Debug.Log(currentTileLocation);
        }

        private void SpawnObstacle()
        {
            if (Random.value > 0.2f) return;

            GameObject obstaclePrefab = SelectRandomGameObjectFromList(obstacles);
            
            /// Rotate obstacles in the expected angle
            Quaternion newObstacleRotation = obstaclePrefab.gameObject.transform.rotation * Quaternion.LookRotation(currentTileDirection, Vector3.up);
            
            GameObject obstacle = Instantiate(obstaclePrefab, currentTileLocation, newObstacleRotation);
            currentObstacles.Add(obstacle); 

        }

        public void AddNewDirection(Vector3 direction) 
        {
            /// changing direction of spawning tiles
            currentTileDirection = direction;
            /// deleting previously spawned tiles
            DeletePreviousTiles();

            Vector3 tilePlacementScale;
            if (prevTile.GetComponent<Tile>().type == TileType.SIDEWAYS) 
            { 
                /// when sideways dividing the lenght by half since the path is equal in both directions, but player will travel only one
                /// adding 5 tiles towards the turned direction to place next tile 
                tilePlacementScale = Vector3.Scale((prevTile.GetComponent<Renderer>().bounds.size / 2) + (Vector3.one * startingTile.GetComponent<BoxCollider>().size.z / 2), currentTileDirection);
            }
            else
            {
                /// when Left or right removing the 2 extra tiles in the opposite direction from the pivot point
                /// adding 5 tiles towards the turned direction to place next tile 
                tilePlacementScale = Vector3.Scale((prevTile.GetComponent<Renderer>().bounds.size - (Vector3.one * 2)) + (Vector3.one * startingTile.GetComponent<BoxCollider>().size.z / 2), currentTileDirection);
            }

            currentTileLocation += tilePlacementScale;

            /// Adding random no. of straight paths
            int currentPathLenght = Random.Range(minimumStraightTiles, maximumStraightTiles);
            for (int i = 0; i < currentPathLenght; ++i)
            {
                /// Spawning obstacles except on the first tile
                //SpawnTile(startingTile.GetComponent<Tile>(), i == 0 ? false : true);
                SpawnTile(startingTile.GetComponent<Tile>());
            }

            /// Spawn a turn tile
            SpawnTile(SelectRandomGameObjectFromList(turnTiles).GetComponent<Tile>());
        }

        private void DeletePreviousTiles()
        {
            while (currentTiles.Count != 1)
            {
                GameObject tile = currentTiles[0];
                currentTiles.RemoveAt(0);
                Destroy(tile);
            }

            while (currentObstacles.Count != 0)
            {
                GameObject obstacle = currentObstacles[0];
                currentObstacles.RemoveAt(0);
                Destroy(obstacle);
            }
        }

        private GameObject SelectRandomGameObjectFromList(List<GameObject> list)
        {
            if (list.Count == 0) 
                return null;

            return list[Random.Range(0, list.Count)];
        }
    }
}
