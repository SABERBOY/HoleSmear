using UnityEngine;

public class Diamond : Base
{
    private Vector3 dir;

    // Start is called before the first frame update
    private void Start()
    {
        dir = transform.up;
    }

    // Update is called once per frame
    private void Update()
    {
        //transform.up = dir;
        eul += Vector3.up * Time.deltaTime * 100;
    }

    public static void CreateDia(int num)
    {
        for (var i = 0; i < num; i++)
        {
            var x1 = Random.Range(0, 1f);
            var x2 = Random.Range(-1f, 0);
            var z1 = Random.Range(0, 1f);
            var z2 = Random.Range(-1f, 0);
            Vector3[] dirs =
                {new Vector3(x1, 1, z1), new Vector3(x1, 1, z2), new Vector3(x2, 1, z2), new Vector3(x2, 1, z1)};
            var go = Instantiate(Resources.Load<GameObject>("Prefabs/Diamond"));
            go.transform.position = new Vector3(0, 0, 0) + dirs[i % 4];
            go.GetComponentInChildren<Rigidbody>().AddExplosionForce(600, new Vector3(0, 0, 0), 10, 3);
        }
    }
}