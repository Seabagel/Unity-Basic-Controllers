using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Mesh unitMesh;
    [SerializeField] private Material unitMaterial;
    [SerializeField] private GameObject gameObjectPrefab;
    
    private Entity entityPrefab;
    private World defaultWorld;
    private EntityManager manager;

    [SerializeField] private int gridX;
    [SerializeField] private int gridY;

    [Range(0.1f, 3f)]
    [SerializeField] private float spacing;

    // Start is called before the first frame update
    void Start()
    {
        //MakeEntity();
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        manager = defaultWorld.EntityManager;

        var settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);

        MakeGrid(gridX, gridY, spacing);
    }

    private void MakeGrid(int x, int y, float spacing = 1f)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                MakeSphere(new float3(i * spacing, j * spacing, 0f));
            }
        }
    }

    private void MakeSphere(float3 position)
    {
        Entity entity = manager.Instantiate(entityPrefab);

        manager.SetComponentData(entity, new Translation
        {
            Value = position
        });
    }

    EntityArchetype archetype;
    private void MakeEntity()
    {
        EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype archetype = manager.CreateArchetype(
            typeof(Translation), // x y
            typeof(Rotation), // euler rotation
            typeof(RenderMesh), // mesh and material
            typeof(RenderBounds), // bounding box
            typeof(LocalToWorld) // local space to world space
        );

        Entity entity = manager.CreateEntity(archetype);
        
        manager.AddComponentData(entity, new Translation 
        {
            Value = new float3(2f, 0f, 4f) 
        });

        manager.AddSharedComponentData(entity, new RenderMesh
        {
            mesh = unitMesh,
            material = unitMaterial
        });



    }
}
