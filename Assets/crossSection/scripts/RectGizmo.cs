using UnityEngine;
using System.Collections;

public class RectGizmo : MonoBehaviour
{
    private float _b = 0.1f;

    public float Border
    {
        get => _b;
        set
        {
            _b = value;
            CreateSlicedMesh();
        }
    }

    private float _w = 1.0f;

    public float Width
    {
        get => _w;
        set
        {
            _w = value;
            CreateSlicedMesh();
        }
    }

    private float _h = 1.0f;

    public float Height
    {
        get => _h;
        set
        {
            _h = value;
            CreateSlicedMesh();
        }
    }

    private Planar_xyzClippingSection.ConstrainedAxis _axis = Planar_xyzClippingSection.ConstrainedAxis.X;

    public Planar_xyzClippingSection.ConstrainedAxis Axis
    {
        get => _axis;
        set
        {
            _axis = value;
            CreateSlicedMesh();
        }
    }


    private float _m = 0.4f;

    public float Margin
    {
        get => _m;
        set
        {
            _m = value;
            CreateSlicedMesh();
        }
    }

    public void SetSizedGizmo(Vector3 size, Planar_xyzClippingSection.ConstrainedAxis axis)
    {
        var a0 = 1.0f; //gizmo proportions 
        var a1 = 0.02f; //border proportions 
        _axis = axis;
        switch (_axis)
        {
            case Planar_xyzClippingSection.ConstrainedAxis.X:
                _w = a0 * size.y;
                _h = a0 * size.z;
                break;
            case Planar_xyzClippingSection.ConstrainedAxis.Y:
                _w = a0 * size.x;
                _h = a0 * size.z;
                break;
            case Planar_xyzClippingSection.ConstrainedAxis.Z:
                _w = a0 * size.x;
                _h = a0 * size.y;
                break;
        }

        _b = a1 * (_w + _h);
        CreateSlicedMesh();
    }

    private void CreateSlicedMesh()
    {
        var mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        switch (_axis)
        {
            case Planar_xyzClippingSection.ConstrainedAxis.X:
                mesh.vertices = new Vector3[]
                {
                    new Vector3(0, -_w / 2 - _b, -_h / 2 - _b), new Vector3(0, -_w / 2, -_h / 2 - _b),
                    new Vector3(0, _w / 2, -_h / 2 - _b), new Vector3(0, _w / 2 + _b, -_h / 2 - _b),
                    new Vector3(0, -_w / 2 - _b, -_h / 2), new Vector3(0, -_w / 2, -_h / 2),
                    new Vector3(0, _w / 2, -_h / 2), new Vector3(0, _w / 2 + _b, -_h / 2),
                    new Vector3(0, -_w / 2 - _b, _h / 2), new Vector3(0, -_w / 2, _h / 2),
                    new Vector3(0, _w / 2, _h / 2), new Vector3(0, _w / 2 + _b, _h / 2),
                    new Vector3(0, -_w / 2 - _b, _h / 2 + _b), new Vector3(0, -_w / 2, _h / 2 + _b),
                    new Vector3(0, _w / 2, _h / 2 + _b), new Vector3(0, _w / 2 + _b, _h / 2 + _b)
                };
                break;
            case Planar_xyzClippingSection.ConstrainedAxis.Y:
                mesh.vertices = new Vector3[]
                {
                    new Vector3(-_w / 2 - _b, 0, -_h / 2 - _b), new Vector3(-_w / 2, 0, -_h / 2 - _b),
                    new Vector3(_w / 2, 0, -_h / 2 - _b), new Vector3(_w / 2 + _b, 0, -_h / 2 - _b),
                    new Vector3(-_w / 2 - _b, 0, -_h / 2), new Vector3(-_w / 2, 0, -_h / 2),
                    new Vector3(_w / 2, 0, -_h / 2), new Vector3(_w / 2 + _b, 0, -_h / 2),
                    new Vector3(-_w / 2 - _b, 0, _h / 2), new Vector3(-_w / 2, 0, _h / 2),
                    new Vector3(_w / 2, 0, _h / 2), new Vector3(_w / 2 + _b, 0, _h / 2),
                    new Vector3(-_w / 2 - _b, 0, _h / 2 + _b), new Vector3(-_w / 2, 0, _h / 2 + _b),
                    new Vector3(_w / 2, 0, _h / 2 + _b), new Vector3(_w / 2 + _b, 0, _h / 2 + _b)
                };
                break;
            case Planar_xyzClippingSection.ConstrainedAxis.Z:
                mesh.vertices = new Vector3[]
                {
                    new Vector3(-_w / 2 - _b, -_h / 2 - _b, 0), new Vector3(-_w / 2, -_h / 2 - _b, 0),
                    new Vector3(_w / 2, -_h / 2 - _b, 0), new Vector3(_w / 2 + _b, -_h / 2 - _b, 0),
                    new Vector3(-_w / 2 - _b, -_h / 2, 0), new Vector3(-_w / 2, -_h / 2, 0),
                    new Vector3(_w / 2, -_h / 2, 0), new Vector3(_w / 2 + _b, -_h / 2, 0),
                    new Vector3(-_w / 2 - _b, _h / 2, 0), new Vector3(-_w / 2, _h / 2, 0),
                    new Vector3(_w / 2, _h / 2, 0), new Vector3(_w / 2 + _b, _h / 2, 0),
                    new Vector3(-_w / 2 - _b, _h / 2 + _b, 0), new Vector3(-_w / 2, _h / 2 + _b, 0),
                    new Vector3(_w / 2, _h / 2 + _b, 0), new Vector3(_w / 2 + _b, _h / 2 + _b, 0)
                };
                break;
        }

        mesh.uv = new Vector2[]
        {
            new Vector2(0, 0), new Vector2(_m, 0), new Vector2(1 - _m, 0), new Vector2(1, 0),
            new Vector2(0, _m), new Vector2(_m, _m), new Vector2(1 - _m, _m), new Vector2(1, _m),
            new Vector2(0, 1 - _m), new Vector2(_m, 1 - _m), new Vector2(1 - _m, 1 - _m), new Vector2(1, 1 - _m),
            new Vector2(0, 1), new Vector2(_m, 1), new Vector2(1 - _m, 1), new Vector2(1, 1)
        };

        mesh.triangles = new int[]
        {
            0, 4, 5,
            0, 5, 1,
            1, 5, 6,
            1, 6, 2,
            2, 6, 7,
            2, 7, 3,
            4, 8, 9,
            4, 9, 5,
            //5, 9, 10,
            //5, 10, 6,
            6, 10, 11,
            6, 11, 7,
            8, 12, 13,
            8, 13, 9,
            9, 13, 14,
            9, 14, 10,
            10, 14, 15,
            10, 15, 11
        };
    }
}