using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    public int flapForce = 200; // By how much does the bird jump in the air
    public int numberOfLives = 3; // How many collisions can the bird withstand?
    public int scrollingSpeed = -2; // by how much should the ground move continuously on the x-axis to the left
    private float groundSizeX = 23.64f; // Value of the actual length of a single ground element within the game
    private bool isDead = false; // Has the bird collided and is now dead?
    private int score = 0; // Score value of the player, initially 0

    public Text scoreTextElement; // Score text object, referenced to ScoreText from the Unity GUI
    public Text remainingPipesTextElement; // Text object showing remaining pipe obstacles, referenced to ScoreText from the Unity GUI
    public Text healthTextElement; // Health text object, referenced to HealthText from the Unity GUI

    public GameObject stoneGround; // Stone Ground Texture object, referenced using the Unity GUI
    public GameObject stoneNextGround; // Stone Ground Texture of the continuing ground object, referenced using the Unity GUI
    public GameObject gameOverTextElement; // Game-over text object, referenced to GameOverText from the Unity GUI
    public GameObject lifeUpTextElement; // Life-Up text object, referenced to GameOverText from the Unity GUI
    public GameObject youWinTextElement; // You-win text object, referenced to GameOverText from the Unity GUI

    private GameObject[] pipeObjectsArray; // The array of pipe objects, referenced using the Unity GUI
    public GameObject pipeObjects; // The parent of the three pipe objects, referenced using the Unity GUI
    private int numberOfPipes; // Total number of pipes, used for storing total number of obstacles
    private int remainingPipesNumber; // Updatable number of remaining obstacles until game finish

    private Rigidbody2D birdPhysicsBody; // Rigid body component of the bird
    private Rigidbody2D scrollingGroundPhysicsBody; // Rigid body component of the ground
    private Rigidbody2D scrollingNextGroundPhysicsBody; // Rigid body component of the continuing ground

    private Animator animator; // Needed for the update of the animation transitions

    // Start is called before the first frame update
    void Start() {
        birdPhysicsBody = GetComponent<Rigidbody2D>(); // Get reference to the physics body
        animator = GetComponent<Animator>(); // Get reference to Animator transitioning object

        scrollingGroundPhysicsBody = stoneGround.GetComponent<Rigidbody2D>(); // Get reference to the ground physics body
        scrollingGroundPhysicsBody.velocity = new Vector2(scrollingSpeed, 0); // Setting the initial ground velocity, moving horizontally

        scrollingNextGroundPhysicsBody = stoneNextGround.GetComponent<Rigidbody2D>(); // Get reference to the continuing ground physics body
        scrollingNextGroundPhysicsBody.velocity = new Vector2(scrollingSpeed, 0); // Setting the initial continuing ground velocity, moving horizontally

        numberOfPipes = Random.Range(10, 50); // Randomly select the required number of obstacles for the player

        // Initialize all the game pipe objects on the screen
        pipeObjectsArray = new GameObject[numberOfPipes]; // Setup the pipe object array
        for (int i = 0; i < numberOfPipes; i++) {
            if (i >= 1) { // if this is not the first pipe,
                // the new pipe may be initialized at between 5 to 10 units from the previous one
                pipeObjectsArray[i] = (GameObject) Instantiate(pipeObjects, new Vector2(pipeObjectsArray[i-1].transform.position.x + Random.Range(5f, 10f), Random.Range(-1.31f, 2.23f)), Quaternion.identity);
            } else { // if this is the first instantiated pipe
                // we want it to be a bit further than the initial start coordinates of the bird, so the new pipe may be initialized at between 8 to 10 units from the bird
                pipeObjectsArray[i] = (GameObject) Instantiate(pipeObjects, new Vector2(Random.Range(8f, 10f), Random.Range(-1.31f, 2.23f)), Quaternion.identity);
            }
            pipeObjectsArray[i].GetComponent<Rigidbody2D>().velocity = new Vector2(scrollingSpeed, 0); // Setting the initial pipe object parent velocity, moving horizontally
        }

        remainingPipesNumber = numberOfPipes; // initially load the total number of obstacles into the variable, 
        remainingPipesTextElement.text = "Pipes : " + remainingPipesNumber.ToString(); // and show it on the screen 
    }

    // Update is called once per frame
    void Update() {
        MoveGround(stoneGround, stoneNextGround); // Try to check whether the ground needs repositioning (and eventually performing it)
        MoveGround(stoneNextGround, stoneGround); // Try to check whether the ground needs repositioning (and eventually performing it)

        if (isDead != true) { // if the bird is not yet dead,
            if (Input.GetMouseButtonDown(0) == true) { // if the user clicks the left mouse-button,
                birdPhysicsBody.velocity = new Vector2(0, 0); // remove any gravity force from the bird
                birdPhysicsBody.AddForce(new Vector2(0, flapForce)); // add the upward flap force for the bird
                animator.SetTrigger("JumpTrigger"); // set the animation to the one transitioning using JumpTrigger
            }
        } else { // bird is dead now
            scrollingGroundPhysicsBody.velocity = new Vector2(0, 0); // stop the ground from moving
            scrollingNextGroundPhysicsBody.velocity = new Vector2(0, 0); // stop the continuing ground from moving

            for (int i = 0; i < numberOfPipes; i++) {
                pipeObjectsArray[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); // stop the pipe object parent from moving
            }

            if (Input.GetMouseButtonDown(0) == true) { // if the user clicks the left mouse button,
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene, which is the game scene, aka. restart the game
            }
        }
    }

    // Calling this function whenever collisions are detected
    void OnCollisionEnter2D(Collision2D other) {
        numberOfLives -= 1; // remove 1 life from bird
        birdPhysicsBody.velocity = new Vector2(0, 0); // remove any gravity force from the bird

        // if the number of lives is not negative (due to extra edge collisions, it may happen), we want to update this information on screen to the player
        if (numberOfLives >= 0) {
            healthTextElement.text = "Lives : " + numberOfLives.ToString(); // update the life number on screen
        }

        if (numberOfLives == 0) { // if the bird has no more lives,
            isDead = true; // the bird must not move anymore
            animator.SetTrigger("LostTrigger"); // set the animation to the one transitioning using LostTrigger
            gameOverTextElement.SetActive(true); // set visibility of the Game Over text to be visible
        } else {
            animator.SetTrigger("HitTrigger"); // set the animation to the one transitioning using HitTrigger
        }
    }

    // Check if any of the two ground elements need repositioning for background reusability
    void MoveGround(GameObject ground1, GameObject ground2) { // ground1 is always the ground left behind, and ground2 is the current ground seen by the player
        if (ground1.transform.position.x < ground2.transform.position.x - groundSizeX) {  // if ground1 is now completely off-screen
            ground1.transform.position = (Vector2) ground1.transform.position + new Vector2(groundSizeX * 2, 0); // ground1 will be moved exactly in front of ground2
        }
    }

    // Calling this function whenever an object has entered a trigger area of another object
    void OnTriggerEnter2D() {
        // if the pipe trigger is entered, the player has passed the pipe obstacle and the score should be updated by +1
        score += 1;
        scoreTextElement.text = "Score : " + score.ToString(); // show the score update on screen

        remainingPipesNumber -= 1; // decrease the obstacle number by 1
        remainingPipesTextElement.text = "Pipes : " + remainingPipesNumber.ToString(); // show the change on screen

        if (score > 0 && score % 5 == 0) { // if the player has passed a multiple of 5 obstacles,
            lifeUpTextElement.SetActive(true); // make the life-up text be visible
            numberOfLives += 1; // the player receives an additional life

            if (numberOfLives >= 0) { // might not be needed, just for consistency safety purposes
                healthTextElement.text = "Lives : " + numberOfLives.ToString(); // show this update on screen
            }
        } else {
            lifeUpTextElement.SetActive(false); // make the life-up text invisible
        }

        if (score >= numberOfPipes) { // if the player has passed all the obstacles
            youWinTextElement.SetActive(true); // make the You-Win text invisible

            // Stop everything from moving on the screen
            scrollingGroundPhysicsBody.velocity = new Vector2(0, 0); // stop the ground from moving
            scrollingNextGroundPhysicsBody.velocity = new Vector2(0, 0); // stop the continuing ground from moving

            for (int i = 0; i < numberOfPipes; i++) {
                pipeObjectsArray[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); // stop the pipe object parent from moving
            }
            Destroy(this); // finally, destroy this GameObject so that the bird falldown will not lead to any more life, score or isDead updates (basically, don't let anything happen)
        }
    }
}