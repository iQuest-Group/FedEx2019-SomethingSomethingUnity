using UnityEngine;

public class FogOfWar : MonoBehaviour
{
	[SerializeField] private GameObject FogOfWarPlane;
	[SerializeField] private GameObject Player;
	[SerializeField] private LayerMask FogLayer;
	[SerializeField] private float Radius = 10f;
	private float RadiusSquare => Radius * Radius;

	private Mesh FogPlaneMesh;
	private Vector3[] FogPlaneVertices;
	private Color[] Colors;

	// Use this for initialization
	void Start()
	{
		Initialize();
	}

	// Update is called once per frame
	void Update()
	{
		if (Player == null)
		{
			var players = GameObject.FindGameObjectsWithTag("Player");
			foreach (var player in players)
			{
				if (player.GetComponent<PlayerManager>().currentPlayer)
				{
					this.Player = player;
				}
			}

			return;
		}

		var ray = new Ray(transform.position, Player.transform.position - transform.position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000, FogLayer, QueryTriggerInteraction.Collide))
		{
			for (int i = 0; i < FogPlaneVertices.Length; i++)
			{
				Vector3 v = FogOfWarPlane.transform.TransformPoint(FogPlaneVertices[i]);
				float dist = Vector3.SqrMagnitude(v - hit.point);
				if (dist < RadiusSquare)
				{
					float alpha = Mathf.Min(Colors[i].a, dist / RadiusSquare);
					Colors[i].a = alpha;
				}
			}
			UpdateColor();
		}
	}

	void Initialize()
	{
		FogPlaneMesh = FogOfWarPlane.GetComponent<MeshFilter>().mesh;
		FogPlaneVertices = FogPlaneMesh.vertices;
		Colors = new Color[FogPlaneVertices.Length];
		for (int i = 0; i < Colors.Length; i++)
		{
			Colors[i] = Color.black;
		}
		UpdateColor();
	}

	void UpdateColor()
	{
		FogPlaneMesh.colors = Colors;
	}
}
