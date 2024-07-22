using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    Movement movement;
    SpriteRenderer sprite;

    bool isFlipped = false;

    void Start()
    {
        movement = GetComponentInChildren<Movement>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement)
        {
            UpdateSprite(movement.Direction);
        }
    }

    public void UpdateSprite(Vector2 movementDirection)
    {
        if (!sprite) { return; }
        if((movementDirection.x < 0 && !isFlipped) || (movementDirection.x > 0 && isFlipped))
        {
            sprite.flipX = !sprite.flipX;
            isFlipped = !isFlipped;
        }
    }
}
