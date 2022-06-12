using DG.Tweening;
using UnityEngine;

public class Tree : Base
{
    private bool IsDoScaleIng;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsDoScaleIng && rig.isKinematic == false)
        {
            IsDoScaleIng = true;
            transform.DOScale(Vector3.one * 0.5f, 1f);
        }
    }
}