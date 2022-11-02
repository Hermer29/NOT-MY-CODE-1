
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LookAtTarget : MonoBehaviour
{
    [field: SerializeField] public Transform Target { get; set; }
    [field: SerializeField] public Point TargetPoint { get; set; }
    [field: SerializeField] public Point ParentPoint { get; set; }

    public float CurrentTToLerp = 0.5f;
    bool _isMovmentTarget;
    int _currentUpdate = 0;
    public void UpdateTransform(Point Target, Point ParentPoint)
    {
        TargetPoint = Target;
        this.ParentPoint = gameObject.GetComponentInParent<Point>();
        this.Target = Target.gameObject.transform;
        _isMovmentTarget = true;
    }

    private void FixedUpdate()
    {
        if (_isMovmentTarget && _currentUpdate < 3)
        {
            transform.right = Target.position - transform.position;
            _currentUpdate++;
            if (_currentUpdate ==2)
            {
                gameObject.transform.position = Vector3.Lerp(transform.position, Target.position, 0.5f);
                StartCoroutine(ActionToImage());
            }
        }
    }
    private IEnumerator ActionToImage()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Image>().enabled = true;
    }
}
