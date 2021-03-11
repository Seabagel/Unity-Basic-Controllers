using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

public class Testing : MonoBehaviour
{
    [SerializeField] private bool useJobs;

    // Start is called before the first frame update
    private void Update()
    {
        float numbershit = Time.realtimeSinceStartup;

        if (useJobs)
        {
            NativeList<JobHandle> jobHandlesList = new NativeList<JobHandle>(Allocator.Temp);
            for (int i = 0; i < 10; i++) // Create 10 jobs 
            {
                jobHandlesList.Add(TaskJob());
            }
            JobHandle.CompleteAll(jobHandlesList);
            jobHandlesList.Dispose(); // Always dispose lists
        } else
        {
            for (int i = 0; i < 10; i++) // Create 10 jobs 
            {
                Task();
            }
        }
        

        Debug.Log($"{(Time.realtimeSinceStartup - numbershit) * 1000f } ms");
    }

    private void Task()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }

    private JobHandle TaskJob() // This a method don't forget
    {
        Job job = new Job();
        return job.Schedule();
    }
}

[BurstCompile]
public struct Job : IJob
{
    public void Execute()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
}
