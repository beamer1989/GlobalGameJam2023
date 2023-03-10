using UnityEngine;

public class CharacterSoftBody : MonoBehaviour
{
	public int width = 5;
	public int height = 5;
    public float bodyMass = 0.05f;
    public float bodyLinearDrag = 100f;
    public float bodyAngularDrag = 100f;
    public int referencePointsCount = 12;
	public float referencePointRadius = 0.25f;
	public float mappingDetail = 10;
	public float springDampingRatio = 0;
	public float springFrequency = 2;
	
	public PhysicsMaterial2D surfaceMaterial;
	public Rigidbody2D[] allReferencePoints;
	
	[HideInInspector] public GameObject[] referencePoints;
    [HideInInspector] private Vector3[] referencePointOriginalLocations;

    int vertexCount;
	Vector3[] vertices;
	int[] triangles;
	Vector2[] uv;
	Vector3[,] offsets;
	float[,] weights;
	
	void Awake()
    {
        CreateReferencePoints();
        CreateMesh();
        MapVerticesToReferencePoints();
    }

    public void ResetReferencePoints()
    {
        Debug.Log("ResettingReferencePoints");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        for (int i = 0; i < referencePointsCount; i++)
        {
            Debug.Log("ResettingReferencePoint: " + referencePointOriginalLocations[i]);
            referencePoints[i].transform.localPosition = referencePointOriginalLocations[i];
        }
    }

    void CreateReferencePoints()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        referencePoints = new GameObject[referencePointsCount];
        referencePointOriginalLocations = new Vector3[referencePointsCount];
        Vector3 offsetFromCenter = ((0.5f - referencePointRadius) * Vector3.up);
        float angle = 360.0f / referencePointsCount;

        for (int i = 0; i < referencePointsCount; i++)
        {
            referencePoints[i] = new GameObject();
            referencePoints[i].name = $"Rigidbody {i}";
            referencePoints[i].tag = gameObject.tag;
            referencePoints[i].layer = 2; //This is the Ignore Raycasts layer!
            //referencePoints[i].AddComponent<PropagateCollisions>();
            referencePoints[i].transform.parent = transform;

            Quaternion rotation = Quaternion.AngleAxis(angle * (i - 1), Vector3.back);
            Vector3 tmp = rotation * offsetFromCenter;
            referencePoints[i].transform.localPosition = tmp;
            referencePointOriginalLocations[i] = tmp;

                Rigidbody2D body = referencePoints[i].AddComponent<Rigidbody2D>();
            body.constraints = RigidbodyConstraints2D.None;
            body.mass = bodyMass;
            body.interpolation = rigidbody.interpolation;
            body.collisionDetectionMode = rigidbody.collisionDetectionMode;
            body.gravityScale = 0f;
            body.drag = bodyLinearDrag;
            body.angularDrag = bodyAngularDrag;
            allReferencePoints[i] = body;


            CircleCollider2D newCircleCollider = referencePoints[i].AddComponent<CircleCollider2D>();
            newCircleCollider.radius = referencePointRadius * (transform.localScale.x / 2);
            if (surfaceMaterial != null)
            {
                newCircleCollider.sharedMaterial = surfaceMaterial;
            }

            AttachWithSpringJoint(referencePoints[i], gameObject);
            if (i > 0)
            {
                AttachWithSpringJoint(referencePoints[i], referencePoints[i - 1]);
            }
        }
        AttachWithSpringJoint(referencePoints[0], referencePoints[referencePointsCount - 1]);

        IgnoreCollisionsBetweenReferencePoints();
    }

    void AttachWithSpringJoint(GameObject referencePoint, GameObject connected)
    {
        SpringJoint2D springJoint = referencePoint.AddComponent<SpringJoint2D>();
        springJoint.connectedBody = connected.GetComponent<Rigidbody2D>();
        springJoint.connectedAnchor = LocalPosition(referencePoint) - LocalPosition(connected);
        springJoint.distance = 0;
        springJoint.dampingRatio = springDampingRatio;
        springJoint.frequency = springFrequency;
    }

    void IgnoreCollisionsBetweenReferencePoints()
    {
        int i;
        int j;

        for (i = 0; i < referencePointsCount; i++)
        {
            for (j = i; j < referencePointsCount; j++)
            {
                CircleCollider2D a = referencePoints[i].GetComponent<CircleCollider2D>();
                CircleCollider2D b = referencePoints[j].GetComponent<CircleCollider2D>();
                Physics2D.IgnoreCollision(a, b, true);
            }
        }
    }

    void CreateMesh()
    {
        vertexCount = (width + 1) * (height + 1);

        int trianglesCount = width * height * 6;
        vertices = new Vector3[vertexCount];
        triangles = new int[trianglesCount];
        uv = new Vector2[vertexCount];
        int t;

        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                int v = (width + 1) * y + x;
                vertices[v] = new Vector3(x / (float)width - 0.5f,
                        y / (float)height - 0.5f, 0);
                uv[v] = new Vector2(x / (float)width, y / (float)height);

                if (x < width && y < height)
                {
                    t = 3 * (2 * width * y + 2 * x);

                    triangles[t] = v;
                    triangles[++t] = v + width + 1;
                    triangles[++t] = v + width + 2;
                    triangles[++t] = v;
                    triangles[++t] = v + width + 2;
                    triangles[++t] = v + 1;
                }
            }
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    void MapVerticesToReferencePoints()
    {
        offsets = new Vector3[vertexCount, referencePointsCount];
        weights = new float[vertexCount, referencePointsCount];

        for (int i = 0; i < vertexCount; i++)
        {
            float totalWeight = 0;

            for (int j = 0; j < referencePointsCount; j++)
            {
                offsets[i, j] = vertices[i] - LocalPosition(referencePoints[j]);
                weights[i, j] =
                    1 / Mathf.Pow(offsets[i, j].magnitude, mappingDetail);
                totalWeight += weights[i, j];
            }

            for (int j = 0; j < referencePointsCount; j++)
            {
                weights[i, j] /= totalWeight;
            }
        }
    }

    void Update()
    {
        UpdateVertexPositions();
    }

    void UpdateVertexPositions()
    {
        Vector3[] vertices = new Vector3[vertexCount];

        for (int i = 0; i < vertexCount; i++)
        {
            vertices[i] = Vector3.zero;

            for (int j = 0; j < referencePointsCount; j++)
            {
                vertices[i] += weights[i, j] *
                    (LocalPosition(referencePoints[j]) + offsets[i, j]);
            }
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }

    Vector3 LocalPosition(GameObject obj)
    {
        return transform.InverseTransformPoint(obj.transform.position);
    }
}