// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Collections;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    // External parameters/variables
    [SerializeField] private GameObject enemyTemplate;
    [SerializeField] private int enemyRows;
    [SerializeField] private int enemyCols;
    [SerializeField] private float enemySpacing;
    [SerializeField] private float stepSize;
    [SerializeField] private float stepTime;
    [SerializeField] private float leftBoundaryX;
    [SerializeField] private float rightBoundaryX;
    private float xDirection = 1.0f;

    private void Start()
    {
        // Initialise the swarm by instantiating enemy prefabs.
        GenerateSwarm();
        
        // Start swarm at the far left.
        transform.localPosition = new Vector3(this.leftBoundaryX, 0f, 0f);

        // Use a coroutine to periodically step the swarm. Coroutines are worth
        // learning about if you are unfamiliar with them. In Unity they allow
        // us to define sequences that span multiple frames in a very clean way.
        // Although it might look like it, using coroutines is *not* the same as
        // using multithreading! Read more here:
        // https://docs.unity3d.com/Manual/Coroutines.html
        StartCoroutine(StepSwarmPeriodically());
    }
    
    private IEnumerator StepSwarmPeriodically() 
    {
        //stepTime = 0.5f;
        // Yep, this is an infinite loop, but the gameplay isn't ever "halted"
        // since the function is invoked as a coroutine. It's also automatically 
        // stopped when the game object is destroyed.
        while (true)
        {
            yield return new WaitForSeconds(this.stepTime); // Not blocking!
            StepSwarm();
        }
    }

    // Automatically generate swarm of enemies based on the given serialized
    // attributes/parameters.
    private void GenerateSwarm()
    {
        int row = 10;
        int column = 5;
        Vector3 spawnPosition = new Vector3(-2, 0, 0);
        Vector3 rowSpace = new Vector3(1, 0, 0);
        Vector3 columnSpace = new Vector3(0, 0, 2);

        // Write code here...
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < column; j++)
            {
                var enemy = Instantiate(enemyTemplate, transform);
                enemy.transform.localPosition = spawnPosition + columnSpace*j + rowSpace*i;
            }
            spawnPosition += rowSpace;
        }

    }

    // Step the swarm across the screen, based on the current direction, or down
    // and reverse when it reaches the edge.
    private void StepSwarm()
    {
        // Write code here...
        var dropping = xDirection < 0f && transform.position.x <= leftBoundaryX ||
                        xDirection > 0f && transform.position.x + (enemyCols - 1) * 1 >= rightBoundaryX;
        if (dropping)
        {
            transform.Translate(Vector3.back * stepSize);
            xDirection *= -1;
        }
        else
        {
            transform.Translate(Vector3.right*stepSize*xDirection);
        }
        
        // Tip: You probably want a private variable to keep track of the
        // direction the swarm is moving. You could alternate this between 1 and
        // -1 to serve as a vector multiplier when stepping the swarm.
    }
}
