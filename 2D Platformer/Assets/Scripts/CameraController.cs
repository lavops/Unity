using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;
    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;
    public float minWidth, maxWidth;

    public bool stopFollow;

    private Vector2 lastPosistion;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPosistion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollow)
        {
            // Camera follows targeted player with restrictions
            transform.position = new Vector3(Mathf.Clamp(target.position.x, minWidth, maxWidth), Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

            // Background parallax depth ( moving background )
            Vector2 amountToMove = new Vector2(transform.position.x - lastPosistion.x, transform.position.y - lastPosistion.y);

            farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;

            lastPosistion = transform.position;
        }
    }
}
