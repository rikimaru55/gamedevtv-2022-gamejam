using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] PlayerPositions;
    public GameObject PlayerSprite;
    private int playerPositionIndex;
    public int PlayerPositionIndex
    {
        get
        {
            return playerPositionIndex;
        }
    }

    private bool inMoveAnimation;
    // Start is called before the first frame update
    void Start()
    {
        updatePlayerPosition(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            movePlayerRight();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            movePlayerLeft();
        }
    }

    void movePlayerRight()
    {
        updatePlayerPosition(playerPositionIndex + 1);
    }

    void movePlayerLeft()
    {
        updatePlayerPosition(playerPositionIndex - 1);
    }

    void updatePlayerPosition(int newPosition)
    {
         newPosition = Mathf.Clamp(newPosition, 0, PlayerPositions.Length - 1);
        // Update index
        playerPositionIndex = newPosition;
        // Update position
        PlayerSprite.transform.position = PlayerPositions[playerPositionIndex].transform.position;
        // Trigger animation, animation should block movement until it's finished.
    }
}
