using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    public delegate void XCameraDelegate(float deltaX);
    public XCameraDelegate onCameraTranslateByX;

    public delegate void XYCameraDelegate(float deltaX, float deltaY);
    public XYCameraDelegate onCameraTranslateByAny;
    private float oldPositionX;
    private float oldPositionY;
    [SerializeField] private bool verticalParallaxEnabled;

    void Start()
    {
        oldPositionX = transform.position.x;
        oldPositionY = transform.position.y;
    }
    void Update()
    {
        if (!verticalParallaxEnabled)
        {
            if (transform.position.x != oldPositionX)
            {
                if (onCameraTranslateByX != null)
                {
                    float deltaX = oldPositionX - transform.position.x;
                    onCameraTranslateByX(deltaX);
                }
                oldPositionX = transform.position.x;
            }
        }
        else
        {
            if (transform.position.x != oldPositionX || transform.position.y != oldPositionY)
            {
                if (onCameraTranslateByAny != null)
                {
                    float deltaX = oldPositionX - transform.position.x;
                    float deltaY = oldPositionY - transform.position.y;
                    onCameraTranslateByAny(deltaX, deltaY);
                }
                oldPositionX = transform.position.x;
                oldPositionY = transform.position.y;
            }
        }

    }
}