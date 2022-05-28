using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject[] PlayerPositions;
    public GameObject PlayerGameObject;
    public SpriteRenderer PlayerSpriteRenderer;
    public SpriteRenderer DrinkSpriteRenderer;
    public Sprite playerMovingSprite;
    public Sprite playerTakingIngredientSprite;
    public Sprite Tumbler;
    public Sprite WineGlass;
    public Sprite PintGlass;
    private int playerPositionIndex;
    private bool animationRunning = false;
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
        if (animationRunning)
        {
            return;
        }
        if (Helpers.checkKeyWithPause(KeyCode.D))
        {
            SoundManager.GetInstance().PlayWalkSound();
            movePlayerRight();
        }
        else if (Helpers.checkKeyWithPause(KeyCode.A))
        {
            SoundManager.GetInstance().PlayWalkSound();
            movePlayerLeft();
        }
    }

    void movePlayerRight()
    {
        updatePlayerPosition(playerPositionIndex + 1);
        PlayerSpriteRenderer.flipX = true;
    }

    void movePlayerLeft()
    {
        updatePlayerPosition(playerPositionIndex - 1);
        PlayerSpriteRenderer.flipX = false;
    }

    public void PlayPlayerTakingIngredientAnimation(Action onIngredientAnimationEnd)
    {
        PlayerSpriteRenderer.sprite = playerTakingIngredientSprite;
        animationRunning = true;
        PlayerGameObject.transform.DOJump(PlayerGameObject.transform.position, 0.25f, 1, 0.25f).OnComplete(() => {
            animationRunning = false;
            PlayerSpriteRenderer.sprite = playerMovingSprite;
            onIngredientAnimationEnd();
        });
    }

    public void PlayPlayerServingADrinkAnimation(IngredientType container, Action onServeAnimationEnd)
    {

        DrinkSpriteRenderer.gameObject.transform.position = PlayerGameObject.transform.position;
        DrinkSpriteRenderer.gameObject.SetActive(true);
        switch (container)
        {
            case IngredientType.Tumbler:
                DrinkSpriteRenderer.sprite = Tumbler;
                break;
            case IngredientType.WineGlass:
                DrinkSpriteRenderer.sprite = WineGlass;
                break;
            case IngredientType.PintGlass:
                DrinkSpriteRenderer.sprite = PintGlass;
                break;
            default:
                throw new Exception("Unknown Container Type");
        }
        //Measurements taken from editor :P
        //Vector3(-5.98600006,1.58800006,0.0696392134)
        //Vector3(-5.98600006,1.02400005,0.0696392134)
        //Vector3(0,0.56400001f,0);
        animationRunning = true;
        Vector3 dest = DrinkSpriteRenderer.gameObject.transform.position - new Vector3(0, 0.56400001f, 0);
        DrinkSpriteRenderer.gameObject.transform.DOMoveY(dest.y, 0.5f).OnComplete(() =>
        {
            DrinkSpriteRenderer.gameObject.SetActive(false);
            animationRunning = false;
            onServeAnimationEnd();
        });
    }

    void updatePlayerPosition(int newPosition)
    {
         newPosition = Mathf.Clamp(newPosition, 0, PlayerPositions.Length - 1);
        // Update index
        playerPositionIndex = newPosition;
        // Update position and trigger animation, animation should block movement until it's finished.
        animationRunning = true;
        Sequence sequence = PlayerGameObject.transform.DOJump(PlayerPositions[playerPositionIndex].transform.position, 0.5f, 1, 0.5f);
        sequence.OnComplete(()=> {
            animationRunning = false;
        });
    }
}
