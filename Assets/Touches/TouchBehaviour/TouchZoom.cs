using UnityEngine;

[CreateAssetMenu(menuName = "TamagoTree_Unity/Touch behaviour/Zoom")]
public class TouchZoom : TouchBehaviour
{
    #region Exposed

    [SerializeField]
    private float _zoomSensitivity = 1;
         
    #endregion

    
    #region Properties

    public float ZoomLevel => _touchDistance;

    #endregion


    #region Main

    public override void Activate()
    {
        base.Activate();

        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        if (BeginMultiTouch(new []{touch1, touch2}))
        {
            _initDistance = Vector2.Distance(touch1.position, touch2.position);
        }
    }

    public override void Tick(float dt)
    {
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        if (MoveMultiTouch(new []{touch1, touch2}))
        {
            Zoom(touch1, touch2);
        }
    }

    public override void Deactivate()
    {
        _touchDistance = 0;

        base.Deactivate();
    }

    private void Zoom(Touch touch1, Touch touch2)
    {
        float newDist = Vector2.Distance(touch1.position, touch2.position);
        _touchDistance = Mathf.Max(0, _initDistance - newDist);
        _touchDistance *= _zoomSensitivity;
    }
         
    #endregion


    #region Utils

    public override bool HasEnoughTouch(int touchCount)
    {
        return touchCount >= 2;
    }

    private bool MoveMultiTouch(Touch[] touches)
    {
        for(int i = touches.Length - 1; i >= 0; i--)
        {
            if(touches[i].phase == TouchPhase.Moved) return true;
        }

        return false;
    }

    private bool BeginMultiTouch(Touch[] touches)
    {
        for(int i = touches.Length - 1; i >= 0; i--)
        {
            if(touches[i].phase == TouchPhase.Began) return true;
        }

        return false;
    }

    #endregion


    #region Private Fields
         
    private float _initDistance;
    private float _touchDistance;

    #endregion
}