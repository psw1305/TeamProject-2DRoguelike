using UnityEngine;

public class NavigationBaker : MonoBehaviour
{

    NavMeshPlus.Components.NavMeshSurface surfaces;


    private void Awake()
    {
        surfaces = GetComponent<NavMeshPlus.Components.NavMeshSurface>();
    }

    private void OnEnable()
    {
        surfaces.BuildNavMesh();
    }

  

}