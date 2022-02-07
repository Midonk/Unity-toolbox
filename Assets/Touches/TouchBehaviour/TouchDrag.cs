using UnityEngine;

[CreateAssetMenu(menuName = "TamagoTree_Unity/Touch behaviour/Drag")]
public class TouchDrag : TouchBehaviour
{
    #region Properties

    public Vector3 DragVector => _dragVector;
         
    #endregion


    #region Main

    public override void Activate()
    {
        base.Activate();

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;
        
        _dragStartPoint = touch.position;
    }

    public override void Tick(float dt)
    {
        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Moved) return;
        
        Drag(touch);
    }

    public override void Deactivate()
    {
        _dragVector = Vector2.zero;
        
        base.Deactivate();
    }

    private void Drag(Touch touch)
    {
        _dragEndPoint = touch.position;
        float deltaX = _dragStartPoint.x - _dragEndPoint.x;
        float deltaY = _dragStartPoint.y - _dragEndPoint.y;
        _dragVector = new Vector2(deltaX, deltaY);
    }

    #endregion

    
    #region Utils

    public override bool HasEnoughTouch(int touchCount)
    {
        return touchCount == 1;
    }

    #endregion

    
    #region Private Fields

    private Vector3 _dragStartPoint;
    private Vector3 _dragEndPoint;
    private Vector2 _dragVector;

    #endregion
}
