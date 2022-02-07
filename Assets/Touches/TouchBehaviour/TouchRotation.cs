using UnityEngine;

[CreateAssetMenu(menuName = "TamagoTree_Unity/Touch behaviour/Rotation")]
public class TouchRotation : TouchBehaviour
{
    #region Exposed

    [SerializeField]
    private TouchDrag _drag;
    
    [SerializeField]
    private float _rotSpeed;

    [SerializeField][Range(-1, 1)]
    private int _rotationDirection = -1;
         
    #endregion

    
    #region Main

    public override void Tick(float dt)
    {
        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Moved) return;
        
        Rotate();
    }

    public override void Deactivate()
    {
        _rotation = Vector2.zero;

        base.Deactivate();
    }
    
    private void Rotate()
    {
        var rotationFactor = Time.deltaTime * _rotSpeed * _rotationDirection;
        _rotation.x -= _drag.DragVector.x * rotationFactor;
        _rotation.y += _drag.DragVector.y * rotationFactor;

        //don't understand those digits...should they become variables ?
        _rotation.x = Mathf.Clamp(_rotation.x, 0,40);
        _rotation.y = Mathf.Clamp(_rotation.y, -115, -75);
    }

    #endregion


    #region Utils

    public override bool HasEnoughTouch(int touchCount)
    {
        return touchCount == 1;
    }

    #endregion

    
    #region Private Fields

    private Vector2 _rotation;
         
    #endregion
}