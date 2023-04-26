using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _following;
    [SerializeField]
    private float _rotationAngleX;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private float _offsetY;

    private void LateUpdate()
    {
        if (_following == null)
            return;

        var rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
        Vector3 followingPosition = FollowingPointPossition();
        var position = rotation * new Vector3(0, 0, -_distance) + followingPosition;

        transform.rotation = rotation;
        transform.position = position;
    }

    public void Follow(GameObject following)
    {
        _following = following.transform;
    }

    private Vector3 FollowingPointPossition()
    {
        Vector3 followingPosition = _following.position;
        followingPosition.y += _offsetY;
        return followingPosition;
    }
}
