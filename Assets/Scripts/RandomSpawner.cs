using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Sprite[] spritesToUse; // Array of sprites to randomly choose from.
    public float spawnInterval = 1f; // Time between each spawn.
    public Vector3 objectScale = new Vector3(1f, 1f, 1f); // Scale to apply to each object.
    private Vector3 localSpawnForce = new Vector3(0f, 0f, 5f); // Force to apply along the object's local Z-axis.

    private void Start()
    {
        StartCoroutine(SpawnObjectWithSprite());
    }

    private IEnumerator SpawnObjectWithSprite()
    {
        while (true)
        {
            GameObject newObj = new GameObject("SpawnedObject");
            newObj.transform.position = transform.position; // Position the new object at the spawner's location.
            newObj.transform.rotation = transform.rotation; // Align the new object's rotation with the spawner's rotation.

            // Adjust rotation here: Rotate the sprite 90 degrees around the Z-axis.
            // For a 2D sprite in a 3D world, you might need to adjust the X or Y axis instead, depending on your setup.
            newObj.transform.Rotate(0, 90, 0);

            Sprite selectedSprite = spritesToUse[Random.Range(0, spritesToUse.Length)]; // Randomly select a sprite.
            SpriteRenderer renderer = newObj.AddComponent<SpriteRenderer>();
            renderer.sprite = selectedSprite;

            Rigidbody rb = newObj.AddComponent<Rigidbody>();
            Vector3 globalDirection = transform.TransformDirection(localSpawnForce);
            rb.AddForce(globalDirection, ForceMode.Impulse);

            newObj.transform.localScale = objectScale; // Apply the desired scale to the object.

            Destroy(newObj, 1f); // Destroy the object after 1 second.

            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before spawning the next object.
        }
    }
}